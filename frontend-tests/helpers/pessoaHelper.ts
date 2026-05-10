// // import { APIRequestContext } from '@playwright/test'

// // export class PessoaHelper {
// //     private request: APIRequestContext

// //     constructor(request: APIRequestContext) {
// //         this.request = request
// //     }

// //     async criarPessoa(nome?: string, dataNascimento = '1990-01-01') {
// //         const nomePessoa = nome ?? `Pessoa ${Date.now()}`

// //         const response = await this.request.post('http://localhost:5000/api/v1/Pessoas', {
// //             data: {
// //                 nome: nomePessoa,
// //                 dataNascimento
// //             }
// //         })

// //         if (!response.ok()) {
// //             const body = await response.text()
// //             throw new Error(`Erro ao criar pessoa: ${response.status()} - ${body}`)
// //         }

// //         const pessoa = await response.json()

// //         return {
// //             id: pessoa.id,
// //             nome: pessoa.nome
// //         }
// //     }

// //     async criarPessoaMaiorDeIdade() {
// //         return this.criarPessoa(`Pessoa ${Date.now()}`, '1990-01-01')
// //     }

// //     async criarPessoaMenorDeIdade() {
// //         return this.criarPessoa(`Pessoa ${Date.now()}`, '2010-01-01')
// //     }

// //     async deletarPessoa(id: string) {
// //         const response = await this.request.delete(
// //             `http://localhost:5000/api/v1/Pessoas/${id}`
// //         )

// //         // não quebra teste se já foi deletado
// //         if (!response.ok()) {
// //             console.warn(`⚠ Não foi possível deletar pessoa ${id}: ${response.status()}`)
// //         }
// //     }
// // }

// import { APIRequestContext } from '@playwright/test'

// export class PessoaHelper {
//     constructor(private request: APIRequestContext) {}

//     async criarPessoa({ nome, dataNascimento }: { nome: string, dataNascimento: string }) {
//         const response = await this.request.post('http://localhost:5000/api/v1/Pessoas', {
//             data: {
//                 nome,
//                 dataNascimento
//             }
//         })

//         if (!response.ok()) {
//             const body = await response.text()
//             throw new Error(`Erro ao criar pessoa: ${response.status()} - ${body}`)
//         }

//         return await response.json()
//     }

//     // atalhos (opcional, mas melhora legibilidade)
//     criarMaiorDeIdade(nome = `Pessoa ${Date.now()}`) {
//         return this.criarPessoa({
//             nome,
//             dataNascimento: '1990-01-01'
//         })
//     }

//     criarMenorDeIdade(nome = `Pessoa ${Date.now()}`) {
//         return this.criarPessoa({
//             nome,
//             dataNascimento: '2010-01-01'
//         })
//     }
// }

import { APIRequestContext, expect } from '@playwright/test'

export class PessoaHelper {
  constructor(private request: APIRequestContext) {}

  async criarPessoa(nome: string, idade: number) {
    const response = await this.request.post('/pessoa', {
      data: {
        nome,
        idade
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

  async criarPessoaMaiorDeIdade(nome: string) {
    return await this.criarPessoa(nome, 25)
  }

  async criarPessoaMenorDeIdade(nome: string) {
    return await this.criarPessoa(nome, 10)
  }
}