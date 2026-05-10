using System.Net;
using System.Net.Http.Json;
using Xunit;

public class TransacaoRegrasCategoriaTests : BaseApiTest
{
    private const string Version = "1";

    [Fact]
    public async Task Nao_Deve_Permitir_Categoria_Inexistente_Na_Transacao()
    {
        // -------------------------
        // 1. CRIAR PESSOA VÁLIDA
        // -------------------------
        var pessoaPayload = new
        {
            nome = "Pessoa Teste",
            dataNascimento = "2000-01-01"
        };

        var pessoaResponse = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Pessoas",
            pessoaPayload);

        pessoaResponse.EnsureSuccessStatusCode();

        var pessoa = await pessoaResponse.Content.ReadFromJsonAsync<PessoaResponse>();

        Assert.NotNull(pessoa);

        // -------------------------
        // 2. USAR CATEGORIA INVÁLIDA
        // -------------------------
        var categoriaInvalida = Guid.NewGuid(); // nunca existe

        // -------------------------
        // 3. TENTAR CRIAR TRANSAÇÃO
        // -------------------------
        var transacaoPayload = new
        {
            descricao = "Transação inválida",
            valor = 100,
            tipo = 0,
            categoriaId = categoriaInvalida,
            pessoaId = pessoa!.Id,
            data = DateTime.UtcNow
        };

        var response = await _client.PostAsJsonAsync(
            $"/api/v{Version}/Transacoes",
            transacaoPayload);

        var body = await response.Content.ReadAsStringAsync();

        // -------------------------
        // 4. VALIDAR BLOQUEIO
        // -------------------------
        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode
        );

        Assert.Contains("Categoria", body);
    }

    // -------------------------
    // DTO mínimo
    // -------------------------
    private class PessoaResponse
    {
        public Guid Id { get; set; }
    }
}