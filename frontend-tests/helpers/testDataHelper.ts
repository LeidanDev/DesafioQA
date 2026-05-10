import { APIRequestContext } from '@playwright/test'

export class TestDataHelper {
    constructor(private request: APIRequestContext) {}

    async criarPessoaMaiorDeIdade() {
        const nome = `Pessoa ${Date.now()}`

        const response = await this.request.post('http://localhost:5000/api/v1/Pessoas', {
            data: {
                nome,
                dataNascimento: '1990-01-01'
            }
        })

        if (!response.ok()) {
            throw new Error(`Erro ao criar pessoa: ${response.status()}`)
        }

        const pessoa = await response.json()

        return {
            id: pessoa.id,
            nome
        }
    }
}