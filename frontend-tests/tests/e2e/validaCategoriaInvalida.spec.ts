import { test, expect } from '@playwright/test'

test('Verifica categoria incorreta', async ({ page }) => {
  await page.goto('http://localhost:5173/transacoes');

  await page.getByText('Adicionar Transação').click()

  await page.locator('#descricao').fill('Teste de categoria invalida');

  await page.locator('#valor').fill('2000');

  await page.locator('#data').fill('2026-05-09');

  await page.locator('#tipo').selectOption('Receita');

  await page.locator('#pessoa-select').click();

  await page.locator('#pessoa-select').fill('Fernando');

  await page.getByText('Fernando').click();

  await page.locator('#categoria-select').click();

  await page.getByRole('option', { name: 'Aluguel', exact: true }).click();

  await page.getByText('Salvar').click()

  await expect(
    page.getByText('Erro ao salvar transação. Tente novamente.')
  ).toBeVisible();

});
