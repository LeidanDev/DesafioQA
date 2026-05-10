import { test as base } from '@playwright/test'
import { PessoaHelper } from './helpers/pessoaHelper'

type Fixtures = {
    pessoaHelper: PessoaHelper
}

export const test = base.extend<Fixtures>({
    pessoaHelper: async ({ request }, use) => {
        await use(new PessoaHelper(request))
    }
})

export { expect } from '@playwright/test'
