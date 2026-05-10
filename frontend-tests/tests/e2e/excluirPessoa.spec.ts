import { test, expect } from '@playwright/test'

test('Deletar usuario', async ({ page }) => {
  await page.goto('http://localhost:5173/pessoas');

  await page.getByText('Adicionar Pessoa').click()

  await page.locator('#nome').click();
  await page.locator('#nome').fill('Carlos');

  await page.locator('#dataNascimento').fill('1995-05-09');

  await page.getByText('Salvar').click()

  await page.goto('http://localhost:5173/transacoes');

  await page.getByText('Adicionar Transação').click()

  await page.locator('#descricao').fill('Teste de exclusão');

  await page.locator('#valor').fill('5000');

  await page.locator('#data').fill('2026-05-09');

  await page.locator('#tipo').selectOption('Receita');

  await page.locator('#pessoa-select').click();

  await page.locator('#pessoa-select').fill('Carlos');

  await page.getByText('Carlos').click();

  await page.locator('#categoria-select').click();

  await page.locator('#categoria-select').fill('Freelance');

  await page.getByText('Freelance').click();

  await page.getByText('Salvar').click()

  await page.goto('http://localhost:5173/pessoas');

  // await page
  //   .locator('tr', { hasText: 'Carlos' })
  //   .getByText('Deletar')
  //   .click();

  const linhaCarlos = page.locator('tr', { hasText: 'Carlos' });

  await linhaCarlos.getByText('Deletar').click();

  // se existir confirmação
  await page.getByRole('button', { name: 'Confirmar' }).click();

  await expect(linhaCarlos).toBeHidden();
});
