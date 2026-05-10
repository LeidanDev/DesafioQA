import { test, expect } from '@playwright/test'

test('Deve criar categoria', async ({ page }) => {
  await page.goto('http://localhost:5173/categorias');

  await page.getByText('Adicionar Categoria').click()

  await page.locator('#descricao').fill('Teste de criar categoria');

  await page.getByText('Salvar').click()

  await expect(
    page.getByText('Categoria salva com sucesso!')
  ).toBeVisible();

});
