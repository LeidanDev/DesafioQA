// import { Page } from '@playwright/test'

// export class TransacaoPages {
//   constructor(private page: Page) {}

//   async goto() {
//     await this.page.goto('http://localhost:5173/transacoes')
//   }

//   async createTransaction(data: {
//     descricao: string
//     valor: string
//     tipo: string
//     categoria: string
//     pessoa: string
//   }) {
//     await this.page.click('text=Adicionar Transação')

//     await this.page.fill('input[name="descricao"]', data.descricao)
//     await this.page.fill('input[name="valor"]', data.valor)

//     const hoje = new Date().toISOString().split('T')[0]
//     await this.page.fill('input[name="data"]', hoje)

//     await this.page.selectOption('select[name="tipo"]', data.tipo)

//     // pessoa (CORRIGIDO)
//     await this.page.fill('input[placeholder="Pesquisar pessoas..."]', data.pessoa)
//     await this.page.click(`text=${data.pessoa}`)

//     // categoria (CORRIGIDO)
//     await this.page.fill('input[placeholder="Pesquisar categorias..."]', data.categoria)
//     await this.page.click(`text=${data.categoria}`)

//     await this.page.click('text=Salvar')
//   }
// }

import { Page } from '@playwright/test'

export class TransacaoPages {
  constructor(private page: Page) {}

  async goto() {
    await this.page.goto('http://localhost:5173/transacoes')
  }

  async createTransaction(data: {
    descricao: string
    valor: string
    tipo: string
    pessoa: string
    categoria: string
  }) {

    await this.page.click('text=Adicionar Transação')

    // descrição
    await this.page.fill('input[name="descricao"]', data.descricao)

    // valor
    await this.page.fill('input[name="valor"]', data.valor)

    // tipo
    await this.page.selectOption('select[name="tipo"]', data.tipo)

    // data (fixa e segura pra testes)
    await this.page.fill('input[name="data"]', '2026-05-09')

    // =========================
    // PESSOA (autocomplete estável)
    // =========================
    await this.page.click('text=Pesquisar pessoas...')
    await this.page.fill('input[placeholder="Pesquisar pessoas..."]', data.pessoa)

    // evita duplicidade / strict mode
    await this.page.getByText(data.pessoa).first().click()

    // =========================
    // CATEGORIA (autocomplete estável)
    // =========================
    await this.page.click('text=Pesquisar categorias...')
    await this.page.fill('input[placeholder="Pesquisar categorias..."]', data.categoria)

    // evita duplicidade / strict mode
    await this.page.getByText(data.categoria).first().click()

    // salvar
    await this.page.click('text=Salvar')
  }
}