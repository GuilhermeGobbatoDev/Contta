# Contta 💜

**Seu dinheiro. Seu controle.**

O **Contta** é um projeto de aplicação web para gerenciamento financeiro pessoal, desenvolvido com **C# e .NET 10**, utilizando ASP.NET Core Web API.

O objetivo é ajudar pessoas a organizar suas finanças, controlar despesas, acompanhar gastos e tomar decisões financeiras mais conscientes.

O projeto também tem como finalidade o aprendizado prático de desenvolvimento Back-end, arquitetura de software, APIs REST e persistência de dados.

## 🚀 Funcionalidades

### ✅ Implementadas

- Cadastro de despesas com descrição, valor, categoria e data.
- Listagem de despesas cadastradas.
- Busca de despesas por ID.
- Edição de despesas existentes.
- Exclusão de despesas.
- Pesquisa de despesas por descrição.
- Filtros por categoria, mês e ano.
- Cálculo do total de despesas na camada de serviços.
- Validações de dados e tratamento de exceções.
- Respostas HTTP adequadas (200, 201, 204, 400 e 404).
- DTOs para cadastro, atualização e resposta.

### 🔧 Em desenvolvimento

- Integração completa do Service com o PostgreSQL.
- Implementação do Repository Pattern para operações de persistência.
- Substituição do armazenamento em memória pelo banco de dados.
- Refatoração e organização das camadas da aplicação.

### 📌 Funcionalidades planejadas

- Cadastro e autenticação de usuários.
- Registro e gerenciamento de receitas.
- Cálculo do saldo disponível.
- Atualização parcial de despesas com PATCH.
- Planejamento de orçamento mensal.
- Sugestão de limite diário de gastos.
- Metas de economia.
- Relatórios e gráficos financeiros.
- Interface web para interação com o usuário.
- Testes automatizados.

## 🛠️ Tecnologias utilizadas

**Back-end**
- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- LINQ
- API REST

**Banco de dados**
- PostgreSQL
- Npgsql (provedor PostgreSQL para EF Core)
- EF Core Migrations

**Arquitetura e boas práticas**
- Orientação a Objetos (POO)
- Separação de responsabilidades
- Repository Pattern
- Interfaces e Injeção de Dependência
- DTOs (Data Transfer Objects)
- Princípio DRY
- Tratamento de exceções

**Ferramentas**
- Visual Studio
- Git e GitHub
- pgAdmin 4
- Arquivos `.http` para testes de requisições

## 🏗️ Arquitetura do projeto

O Contta utiliza uma arquitetura organizada em três projetos, com responsabilidades separadas:

```text
Contta/
│
├── Contta.Api/
│   ├── Controllers/
│   ├── DTOs/
│   └── Program.cs
│
├── Contta.Core/
│   ├── Models/
│   ├── Services/
│   └── Interfaces/
│
└── Contta.Infrastructure/
    ├── Data/
    │   ├── ConttaDbContext.cs
    │   └── Migrations/
    └── Repositories/
```

### Contta.Api

Responsável por receber requisições HTTP, chamar os serviços e retornar respostas adequadas aos clientes.

### Contta.Core

Contém os modelos, as regras de negócio, as validações e as interfaces que definem os contratos da aplicação.

### Contta.Infrastructure

Responsável pelo acesso aos dados e pela integração com PostgreSQL, utilizando Entity Framework Core e Repositories.

### Fluxo da aplicação

```text
Cliente
   ↓
Controller
   ↓
Service
   ↓
IDespesaRepository
   ↓
DespesaRepository
   ↓
ConttaDbContext
   ↓
PostgreSQL
```

Esse é o fluxo arquitetural que está sendo implementado para a persistência completa dos dados.

## 🌐 Endpoints da API

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/despesas` | Listar despesas |
| GET | `/api/despesas/{id}` | Buscar despesa pelo ID |
| POST | `/api/despesas` | Cadastrar despesa |
| PUT | `/api/despesas/{id}` | Atualizar despesa |
| DELETE | `/api/despesas/{id}` | Excluir despesa |
| GET | `/api/despesas/pesquisar?descricao=Mercado` | Pesquisar por descrição |
| GET | `/api/despesas/categoria?categoria=Alimentação` | Filtrar por categoria |
| GET | `/api/despesas/data?mes=10&ano=2026` | Filtrar por período |

Os endpoints foram implementados e testados durante o desenvolvimento. A migração completa de suas operações para o PostgreSQL ainda está em andamento.

## 🗄️ Banco de dados

O projeto utiliza **PostgreSQL**, integrado ao Entity Framework Core.

A estrutura inicial do banco `contta_db` já foi criada utilizando Migrations.

A tabela `Despesas` possui os campos:

| Campo | Tipo |
|---|---|
| Id | integer (PK) |
| Descricao | text |
| Valor | numeric |
| Categoria | text |
| Data | date |

A persistência por meio do Repository está sendo integrada à camada de serviços.

## ⚙️ Executando o projeto

**Pré-requisitos:**
- .NET SDK 10
- PostgreSQL
- Git

Clone o repositório:

```bash
git clone https://github.com/GuilhermeGobbatoDev/Contta.git
cd Contta
```

Restaure as dependências:

```bash
dotnet restore
```

Configure a Connection String `ConnectionStrings:ConttaDb` nos User Secrets do projeto `Contta.Api`, apontando para um banco PostgreSQL local.

Para preparar um novo banco, aplique as Migrations:

```bash
dotnet ef database update --project Contta.Infrastructure --startup-project Contta.Api
```

Execute a API:

```bash
dotnet run --project Contta.Api
```

**Observação:** a integração completa dos endpoints com o banco ainda está em desenvolvimento.

## 🎯 Objetivo do projeto

O Contta é desenvolvido como projeto pessoal de aprendizado e portfólio, com foco em:

- Desenvolvimento Back-end com C# e .NET.
- Construção e manutenção de APIs REST.
- Organização de código e arquitetura em camadas.
- Aplicação de princípios de orientação a objetos.
- Integração com bancos de dados relacionais.
- Evolução incremental utilizando Git e GitHub.

## 📊 Status

🚧 **Em desenvolvimento — Web API implementada e integração com PostgreSQL em andamento.**

## 👨‍💻 Desenvolvedor

Desenvolvido por [GuilhermeGobbatoDev](https://github.com/GuilhermeGobbatoDev).

O projeto será atualizado continuamente conforme novas funcionalidades e melhorias forem implementadas.