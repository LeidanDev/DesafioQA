import { defineConfig } from '@playwright/test'

export default defineConfig({
  testDir: './tests/e2e',

  /* não paralelizar (importante pra evitar conflito de dados) */
  fullyParallel: false,
  workers: 1,

  /* tempo máximo por teste */
  timeout: 30 * 1000,

  /* configurações globais do browser */
  use: {
    baseURL: 'http://localhost:5173',

    headless: false, // útil pra ver execução na prova
    viewport: { width: 1280, height: 720 },

    actionTimeout: 10 * 1000,
    navigationTimeout: 15 * 1000,

    ignoreHTTPSErrors: true,

    // NÃO gerar artefatos (como você pediu)
    screenshot: 'off',
    video: 'off',
    trace: 'off'
  },

  /* relatório simples (padrão bom pra prova técnica) */
  reporter: [
    ['list']
  ]
})