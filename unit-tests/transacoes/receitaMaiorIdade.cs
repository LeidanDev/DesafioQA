using Xunit;
using System;
using System.Reflection;
using MinhasFinancas.Domain.Entities;

public class PermitirTransacaoMaior
{
    [Fact]
    public void Deve_Permitir_Receita_Para_Maior_De_Idade()
    {
        // Arrange
        var pessoa = new Pessoa
        {
            Nome = "Maria",
            DataNascimento = DateTime.Today.AddYears(-25)
        };

        var categoria = new Categoria
        {
            Descricao = "Salário",
            Finalidade = Categoria.EFinalidade.Receita
        };

        var transacao = new Transacao
        {
            Descricao = "Salário",
            Valor = 2000,
            Tipo = Transacao.ETipo.Receita,
            Data = DateTime.Today
        };

        // Act & Assert (não deve lançar exceção)
        var ex = Record.Exception(() =>
        {
            typeof(Transacao)
                .GetProperty("Pessoa")!
                .SetValue(transacao, pessoa);
        });

        Assert.Null(ex);
    }
}