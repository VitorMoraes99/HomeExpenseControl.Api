# ⚙️ Home Expense Control - API (Back-end)

Este é o módulo Back-end do sistema de Controle de Gastos Residenciais. Uma API RESTful robusta construída em .NET, responsável por gerenciar toda a lógica de negócios, persistência de dados e segurança da aplicação.

## 🚀 Tecnologias Utilizadas

- **C# .NET** - Framework principal para construção da Web API.
- **Entity Framework Core (EF Core)** - ORM (Object-Relational Mapper) para manipulação do banco de dados.
- **SQLite** - Banco de dados leve e relacional, ideal para o escopo do projeto.
- **Swagger (OpenAPI)** - Documentação interativa e interface de testes para os endpoints.

## 🏗️ Arquitetura e Padrões Aplicados

A arquitetura foi desenhada visando a separação de responsabilidades (Separation of Concerns) e fácil manutenção:

- **Controllers:** Camada de entrada das requisições HTTP, delegando as regras de negócio para os serviços.
- **Services:** Contém o "coração" do sistema, isolando a lógica de negócios e as regras de validação. O uso de Interfaces (`Interfaces/`) permite fácil injeção de dependência e futuros testes unitários.
- **DTOs (Data Transfer Objects):** Previne o _Over-Posting_ e protege os modelos de domínio, definindo exatamente o que entra e o que sai da API.
- **Models:** Entidades que representam as tabelas no banco de dados.

## ✨ Regras de Negócio Implementadas

A API garante a integridade dos dados antes mesmo deles chegarem ao banco:

1. **Validação de Idade:** Lançamentos de despesas/receitas verificam a idade da Pessoa. Menores de 18 anos só podem registrar "Despesas".
2. **Deleção em Cascata (Cascade Delete):** Ao deletar uma Pessoa, todas as suas Transações são automaticamente removidas do banco para evitar dados órfãos.
3. **Validação de Categorias:** Garantia de que uma transação de "Receita" não seja atrelada a uma categoria exclusiva de "Despesa".

## ⚙️ Como executar o projeto localmente

### Pré-requisitos

- [SDK do .NET](https://dotnet.microsoft.com/download) instalado na sua máquina.
- Ferramentas do Entity Framework Core (`dotnet tool install --global dotnet-ef`).

### Passo a passo

1. Clone o repositório
   git clone [COLOQUE_A_URL_AQUI]

2. Acesse a pasta principal do projeto Back-end
   cd [NOME_DA_PASTA_DO_BACKEND]

3. Restaure as dependências
   dotnet restore

4. Crie/Atualize o banco de dados (Migrations)
   O SQLite será criado automaticamente na pasta raiz aplicando as tabelas mais recentes:
   dotnet ef database update

5. Inicie o servidor
   dotnet run

6. Teste com o Swagger
   Com a aplicação rodando, acesse no seu navegador:
   http://localhost:[PORTA]/swagger
   (Substitua [PORTA] pela porta informada no terminal, geralmente 5000, 5001 ou superior).

## 📁 Estrutura de Diretórios

HomeExpenseControl.WebAPI/
├── Controllers/ # Controladores da API (Endpoints)
├── DTOs/ # Objetos de Transferência de Dados
├── Migrations/ # Histórico de versões do banco de dados (EF Core)
├── Models/ # Entidades de Domínio
├── Services/ # Regras de Negócio e Interfaces
├── AppDbContext.cs # Configuração do Entity Framework
└── Program.cs # Configuração principal e Injeção de Dependências
