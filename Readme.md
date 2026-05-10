## 📘 Quickstart — Desenvolvimento Local

Este guia explica como rodar o projeto localmente utilizando Docker (recomendado) ou executando cada parte separadamente.

## 🧰 Pré-requisitos

Antes de começar, você precisa ter instalado:

Docker e Docker Compose
.NET SDK (versão 9 ou superior)
Bun (para rodar o frontend localmente sem Docker)

## 🐳 Rodando com Docker (Recomendado)

Essa é a forma mais simples de executar o projeto completo.

## ▶️ Subir todos os serviços:

docker-compose up --build

## 🌐 Após subir, os serviços estarão disponíveis em:

    Frontend: http://localhost:5173

    API: http://localhost:5000

    Swagger: http://localhost:5000/swagger

## 💻 Rodando sem Docker (modo manual)

Se preferir rodar cada parte separadamente:

## 🔧 Backend (API)

cd api/MinhasFinancas.API
dotnet build
dotnet run --project MinhasFinancas.API.csproj

## 🌐 Frontend

cd web
bun install
bun run dev

## 🧪 Rodando os testes

# 🔙 Testes da API (.NET)

cd backend-tests
dotnet test

## 🧩 Testes de unidade

cd unit-tests
dotnet test

## 🎭 Testes E2E (Playwright)

cd frontend-tests
npm install
npx playwright install
npx playwright test

## 🔍 Desafios encontrados

- Não foi possível concluir a configuração completa do CI com acesso ao repositório privado em tempo hábil, então deixei o pipeline estruturado para execução futura.

- A estrutura do projeto não está totalmente organizada no padrão ideal (como Page Object Model). Devido ao tempo disponível, priorizei a implementação e validação dos testes E2E funcionais utilizando Playwright.

- Em um cenário de evolução do projeto, a próxima melhoria seria refatorar os testes para o padrão Page Object Model, visando melhor manutenção e reutilização.


