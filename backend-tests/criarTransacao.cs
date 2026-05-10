using System.Net;
using System.Net.Http.Json;
using Xunit;

public class TransacaoApiTests : BaseApiTest
{
    private const string Version = "1";

    [Fact]
    public async Task Deve_Criar_Transacao_Valida_Com_Sucesso()
    {
        // -------------------------
        // 1. CRIAR CATEGORIA
        // -------------------------
        var categoriaPayload = new
        {
            descricao = "Categoria Teste",
            finalidade = 0
        };

        var categoriaResponse = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Categorias",
            categoriaPayload);

        categoriaResponse.EnsureSuccessStatusCode();

        var categoria = await categoriaResponse.Content.ReadFromJsonAsync<CategoriaResponse>();

        Assert.NotNull(categoria);

        // -------------------------
        // 2. CRIAR PESSOA
        // -------------------------
        var pessoaPayload = new
        {
            nome = "Pessoa Transação",
            dataNascimento = "2000-01-01"
        };

        var pessoaResponse = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Pessoas",
            pessoaPayload);

        pessoaResponse.EnsureSuccessStatusCode();

        var pessoa = await pessoaResponse.Content.ReadFromJsonAsync<PessoaResponse>();

        Assert.NotNull(pessoa);

        // -------------------------
        // 3. CRIAR TRANSAÇÃO (TESTE PRINCIPAL)
        // -------------------------
        var transacaoPayload = new
        {
            descricao = "Transação válida",
            valor = 150.50,
            tipo = 0, // Receita ou conforme ETipo
            categoriaId = categoria!.Id,
            pessoaId = pessoa!.Id,
            data = DateTime.UtcNow
        };

        var response = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Transacoes",
            transacaoPayload);

        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode
        );

        var transacao = await response.Content.ReadFromJsonAsync<TransacaoResponse>();

        Assert.NotNull(transacao);
        Assert.Equal("Transação válida", transacao!.Descricao);
        Assert.Equal(pessoa.Id, transacao.PessoaId);
        Assert.Equal(categoria.Id, transacao.CategoriaId);
        Assert.True(transacao.Valor > 0);
    }

    // -------------------------
    // DTOs locais
    // -------------------------
    private class CategoriaResponse
    {
        public Guid Id { get; set; }
    }

    private class PessoaResponse
    {
        public Guid Id { get; set; }
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
}