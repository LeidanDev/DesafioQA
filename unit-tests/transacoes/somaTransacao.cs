using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using MinhasFinancas.Domain.Entities;

public class SomaTransacaoTests
{
    [Fact]
    public void Deve_Calcular_Saldo_Correto_Entre_Receitas_e_Despesas()
    {
        // Arrange
        var transacoes = new List<Transacao>
        {
            new Transacao
            {
                Descricao = "Salário",
                Valor = 3000,
                Tipo = Transacao.ETipo.Receita,
                Data = DateTime.Today
            },
            new Transacao
            {
                Descricao = "Freelance",
                Valor = 1000,
                Tipo = Transacao.ETipo.Receita,
                Data = DateTime.Today
            },
            new Transacao
            {
                Descricao = "Aluguel",
                Valor = 1500,
                Tipo = Transacao.ETipo.Despesa,
                Data = DateTime.Today
            }
        };

        // Act
        var totalReceitas = transacoes
            .Where(t => t.Tipo == Transacao.ETipo.Receita)
            .Sum(t => t.Valor);

        var totalDespesas = transacoes
            .Where(t => t.Tipo == Transacao.ETipo.Despesa)
            .Sum(t => t.Valor);

        var saldo = totalReceitas - totalDespesas;

        // Assert
        Assert.Equal(4000, totalReceitas);
        Assert.Equal(1500, totalDespesas);
        Assert.Equal(2500, saldo);
    }
}