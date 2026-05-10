import { Page, expect } from '@playwright/test'

export async function selecionarOpcaoCustomizada(
  page: Page,
  seletor: string,
  texto: string
) {
  await page.locator(seletor).click()

  const option = page
    .locator('[role="option"]')
    .filter({ hasText: texto })
    .first()

  await expect(option).toBeVisible({
    timeout: 15000
  })

  await option.click()
}