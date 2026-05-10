using Xunit;
using System;
using System.Reflection;
using MinhasFinancas.Domain.Entities;

public class TransacaoTests
{
    [Fact]
    public void Deve_Bloquear_Receita_Para_Menor_De_Idade()
    {
        // Arrange
        var pessoa = new Pessoa
        {
            Nome = "João",
            DataNascimento = DateTime.Today.AddYears(-10)
        };

        var categoria = new Categoria
        {
            Descricao = "Salário",
            Finalidade = Categoria.EFinalidade.Receita
        };

        var transacao = new Transacao
        {
            Descricao = "Salário",
            Valor = 1000,
            Tipo = Transacao.ETipo.Receita,
            Data = DateTime.Today
        };

        // Act
        var ex = Assert.Throws<TargetInvocationException>(() =>
        {
            typeof(Transacao)
                .GetProperty("Pessoa")!
                .SetValue(transacao, pessoa);
        });

        // Assert (IMPORTANTE: olhar InnerException)
        Assert.IsType<InvalidOperationException>(ex.InnerException);

        Assert.Equal(
            "Menores de 18 anos não podem registrar receitas.",
            ex.InnerException!.Message
        );
    }
}