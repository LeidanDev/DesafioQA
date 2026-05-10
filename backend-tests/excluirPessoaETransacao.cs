using System.Net;
using System.Net.Http.Json;
using Xunit;

public class PessoaCascadeTests : BaseApiTest
{
    private const string Version = "1";

    [Fact]
    public async Task Deve_Excluir_Pessoa_E_Remover_Transacoes_Associadas()
    {
        // -------------------------
        // 1. CRIAR CATEGORIA
        // -------------------------
        var categoriaPayload = new
        {
            descricao = "Alimentação",
            finalidade = 0
        };

        var categoriaResponse = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Categorias",
            categoriaPayload);

        categoriaResponse.EnsureSuccessStatusCode();

        var categoria = await categoriaResponse.Content.ReadFromJsonAsync<CategoriaResponse>();

        Assert.NotNull(categoria);
        Assert.NotEqual(Guid.Empty, categoria!.Id);

        // -------------------------
        // 2. CRIAR PESSOA
        // -------------------------
        var pessoaPayload = new
        {
            nome = "Pessoa Teste Cascade",
            dataNascimento = "2000-01-01"
        };

        var pessoaResponse = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Pessoas",
            pessoaPayload);

        pessoaResponse.EnsureSuccessStatusCode();

        var pessoa = await pessoaResponse.Content.ReadFromJsonAsync<PessoaResponse>();

        Assert.NotNull(pessoa);
        Assert.NotEqual(Guid.Empty, pessoa!.Id);

        // -------------------------
        // 3. CRIAR TRANSAÇÃO
        // -------------------------
        var transacaoPayload = new
        {
            descricao = "Transação teste",
            valor = 100.0,
            tipo = 0, // 0 ou 1 conforme ETipo
            categoriaId = categoria.Id,
            pessoaId = pessoa.Id,
            data = DateTime.UtcNow
        };

        var transacaoResponse = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Transacoes",
            transacaoPayload);

        var transacaoBody = await transacaoResponse.Content.ReadAsStringAsync();

        Assert.True(
            transacaoResponse.StatusCode == HttpStatusCode.Created,
            $"Erro ao criar transação: {transacaoBody}"
        );

        var transacao = await transacaoResponse.Content.ReadFromJsonAsync<TransacaoResponse>();

        Assert.NotNull(transacao);
        Assert.Equal(pessoa.Id, transacao!.PessoaId);

        // -------------------------
        // 4. EXCLUIR PESSOA
        // -------------------------
        var deleteResponse = await _client.DeleteAsync(
            $"/api/v{Version}/Pessoas/{pessoa.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // -------------------------
        // 5. VALIDAR CASCADE DELETE
        // -------------------------
        var getTransacoesResponse = await _client.GetAsync(
            $"/api/v{Version}/Transacoes");

        getTransacoesResponse.EnsureSuccessStatusCode();

        var lista = await getTransacoesResponse.Content
            .ReadFromJsonAsync<TransacaoPagedResult>();

        var existeTransacaoDaPessoa = lista?.Items?
            .Any(t => t.PessoaId == pessoa.Id) ?? false;

        Assert.False(
            existeTransacaoDaPessoa,
            "Transações da pessoa deveriam ser removidas após delete (cascade)."
        );
    }

    // -------------------------
    // DTOs locais do teste
    // -------------------------
    private class CategoriaResponse
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public int Finalidade { get; set; }
    }

    private class PessoaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    private class TransacaoResponse
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Tipo { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid PessoaId { get; set; }
    }

    private class TransacaoPagedResult
    {
        public List<TransacaoResponse> Items { get; set; }
    }
}