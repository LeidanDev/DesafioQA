// // helpers/categoriaHelper.ts

// import { Page, expect } from '@playwright/test'

// export class CategoriaHelper {

//     async criarCategoria(
//         page: Page,
//         descricao: string,
//         finalidade: 'Receita' | 'Despesa'
//     ) {

//         await page.getByRole('navigation', { name: 'Main navigation' })
//             .getByRole('link', { name: 'Categorias' })
//             .click()

//         await page.getByRole('button', {
//             name: 'Adicionar Categoria'
//         }).click()

//         await page.fill('#descricao', descricao)

//         await page.selectOption('#finalidade', {
//             label: finalidade
//         })

//         await page.getByRole('button', {
//             name: 'Salvar'
//         }).click()

//         await expect(
//             page.getByText('Categoria salva com sucesso!')
//         ).toBeVisible()
//     }
// }

import { APIRequestContext, expect } from '@playwright/test'

export class CategoriaHelper {
  constructor(private request: APIRequestContext) {}

  async criarCategoria(
    descricao: string,
    finalidade: number = 0
  ) {
    const response = await this.request.post('/categoria', {
      data: {
        descricao,
        finalidade
      }
    })

    expect(response.ok()).toBeTruthy()

    const text = await response.text()

    if (!text) {
      return null
    }

    try {
      return JSON.parse(text)
    } catch {
      return text
    }
  }
}