using System.Net;
using System.Net.Http.Json;
using Xunit;

public class PessoaApiTests : BaseApiTest
{
    [Fact]
    public async Task Deve_Criar_Pessoa_E_Retornar_Dados_Corretos()
    {
        // ARRANGE
        var nomePessoa = $"Pessoa API {DateTime.Now.Ticks}";

        var payload = new
        {
            nome = nomePessoa,
            dataNascimento = "1990-01-01"
        };

        // ACT
        var response = await _client.PostAsJsonAsync("/api/v1/Pessoas", payload);

        // ASSERT STATUS
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // ASSERT BODY
        var pessoaCriada =
            await response.Content.ReadFromJsonAsync<PessoaResponse>();

        Assert.NotNull(pessoaCriada);
        Assert.NotNull(pessoaCriada!.Id);
        Assert.Equal(nomePessoa, pessoaCriada.Nome);
        Assert.StartsWith("1990-01-01", pessoaCriada.DataNascimento);
    }

    public class PessoaResponse
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
    }
}

