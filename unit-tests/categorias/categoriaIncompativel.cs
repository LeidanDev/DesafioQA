using Xunit;
using System;
using System.Reflection;
using MinhasFinancas.Domain.Entities;

public class CategoriaIncompativel
{
    [Fact]
    public void Deve_Rejeitar_Transacao_Quando_Categoria_For_Incompativel_Com_Tipo()
    {
        // Arrange
        var pessoa = new Pessoa
        {
            Nome = "Maria",
            DataNascimento = DateTime.Today.AddYears(-30)
        };

        // Categoria de DESPESA
        var categoria = new Categoria
        {
            Descricao = "Alimentação",
            Finalidade = Categoria.EFinalidade.Despesa
        };

        var transacao = new Transacao
        {
            Descricao = "Salário",
            Valor = 2000,
            Tipo = Transacao.ETipo.Receita, // ❌ incompatível com despesa
            Data = DateTime.Today
        };

        // Act
        var ex = Assert.Throws<TargetInvocationException>(() =>
        {
            typeof(Transacao)
                .GetProperty("Categoria")!
                .SetValue(transacao, categoria);
        });

        // Assert
        Assert.IsType<InvalidOperationException>(ex.InnerException);

        Assert.Equal(
            "Não é possível registrar receita em categoria de despesa.",
            ex.InnerException!.Message
        );
    }
}