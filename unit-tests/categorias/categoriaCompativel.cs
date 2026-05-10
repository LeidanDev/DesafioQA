using Xunit;
using System;
using System.Reflection;
using MinhasFinancas.Domain.Entities;

public class PermitirTransacaoCompativel
{
    [Fact]
    public void Deve_Permitir_Transacao_Quando_Categoria_For_Compativel_Com_Tipo()
    {
        // Arrange
        var pessoa = new Pessoa
        {
            Nome = "João",
            DataNascimento = DateTime.Today.AddYears(-30)
        };

        var categoria = new Categoria
        {
            Descricao = "Alimentação",
            Finalidade = Categoria.EFinalidade.Despesa
        };

        var transacao = new Transacao
        {
            Descricao = "Supermercado",
            Valor = 150,
            Tipo = Transacao.ETipo.Despesa,
            Data = DateTime.Today
        };

        // Act
        var ex = Record.Exception(() =>
        {
            typeof(Transacao)
                .GetProperty("Categoria")!
                .SetValue(transacao, categoria);

            typeof(Transacao)
                .GetProperty("Pessoa")!
                .SetValue(transacao, pessoa);
        });

        // Assert
        Assert.Null(ex);
    }
}