import { Page } from '@playwright/test'

export class PessoaPages {
  constructor(private page: Page) {}

  async goto() {
    await this.page.goto('http://localhost:5173/pessoas')
  }

  async createPerson(data: { nome: string; dataNascimento: string }) {
    // 🔥 ABRIR FORMULÁRIO (isso estava faltando)
    await this.page.click('text=Adicionar Pessoa')

    // agora sim os inputs aparecem
    await this.page.fill('input[name="nome"]', data.nome)
    await this.page.fill('input[name="dataNascimento"]', data.dataNascimento)

    await this.page.click('text=Salvar')
  }
}