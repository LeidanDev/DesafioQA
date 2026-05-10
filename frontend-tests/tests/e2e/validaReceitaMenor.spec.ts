import { test, expect } from '@playwright/test'

test('não deve permitir receita para menor de idade', async ({ page }) => {
  await page.goto('http://localhost:5173/transacoes');

  await page.getByText('Adicionar Transação').click()

  await page.locator('#descricao').fill('Teste receita menor');

  await page.locator('#valor').fill('150');

  await page.locator('#data').fill('2026-05-09');

  await page.locator('#tipo').selectOption('Receita');

  await page.locator('#pessoa-select').click();

  await page.locator('#pessoa-select').fill('Pedro');

  await page.getByText('Pedro').click();

  await expect(page.getByText('Menores só podem registrar despesas.')).toBeVisible();

  await page.locator('#categoria-select').click();

  await page.locator('#categoria-select').fill('Freelance');

  await page.getByText('Freelance').click();

  await page.getByText('Salvar').click()

  await expect(
    page.getByText('Menores só podem registrar despesas.')
  ).toBeVisible();

});
