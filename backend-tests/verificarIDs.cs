using System.Net;
using System.Net.Http.Json;
using Xunit;

public class PessoaDuplicidadeTests : BaseApiTest
{
    [Fact]
    public async Task Deve_Gerar_Ids_Diferentes_Para_Pessoas_Com_Mesmo_Nome_E_Idade()
    {
        // ARRANGE
        var payload = new
        {
            nome = "João Repetido",
            dataNascimento = "1990-01-01"
        };

        // ACT
        var response1 = await _client.PostAsJsonAsync("/api/v1/Pessoas", payload);
        var response2 = await _client.PostAsJsonAsync("/api/v1/Pessoas", payload);

        // ASSERT STATUS
        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);

        // ASSERT BODY
        var pessoa1 = await response1.Content.ReadFromJsonAsync<PessoaResponse>();
        var pessoa2 = await response2.Content.ReadFromJsonAsync<PessoaResponse>();

        Assert.NotNull(pessoa1);
        Assert.NotNull(pessoa2);

        // ASSERT IDS DIFERENTES
        Assert.NotEqual(pessoa1!.Id, pessoa2!.Id);

        // ASSERT DADOS IGUAIS
        Assert.Equal(pessoa1.Nome, pessoa2.Nome);
        Assert.Equal(pessoa1.DataNascimento, pessoa2.DataNascimento);
    }

    public class PessoaResponse
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
    }
}