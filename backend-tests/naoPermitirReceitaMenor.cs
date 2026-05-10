using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Xunit;

public class TransacaoRegrasTests : BaseApiTest
{
    [Fact]
    public async Task Nao_Deve_Permitir_Receita_Para_Menor_De_Idade()
    {
        // ARRANGE - criar pessoa menor
        var pessoaPayload = new
        {
            nome = $"Menor {DateTime.Now.Ticks}",
            dataNascimento = "2010-01-01"
        };

        var pessoaResponse =
            await _client.PostAsJsonAsync("/api/v1/Pessoas", pessoaPayload);

        Assert.Equal(HttpStatusCode.Created, pessoaResponse.StatusCode);

        var pessoa = await pessoaResponse.Content
            .ReadFromJsonAsync<PessoaResponse>();

        Assert.NotNull(pessoa);

        // ARRANGE - transação inválida
        var transacaoPayload = new
        {
            descricao = "Salário menor",
            valor = 1000,
            data = DateTime.Now.ToString("yyyy-MM-dd"),
            tipo = "receita",
            pessoaId = pessoa!.Id
        };

        // ACT
        var response =
            await _client.PostAsJsonAsync("/api/v1/Transacoes", transacaoPayload);

        // ASSERT - status
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        // ASSERT - retorno estruturado da API (correto)
        var error = await response.Content
        .ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(error);

        Assert.Equal((int)HttpStatusCode.BadRequest, error!.Status);
        Assert.NotNull(error.Errors); // validação real existe aqui
    }

    public class PessoaResponse
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
    }
}