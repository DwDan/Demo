# Demo.Todos API

API RESTful para gerenciamento de tarefas (ToDo), desenvolvida com .NET 8, seguindo os padrões de DDD, Repository e Unit of Work.

## ⚙️ Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (PostgreSQL)
- Docker / Docker Compose
- Swagger / OpenAPI

## 🚀 Como executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/products/docker-desktop)
- [PostgreSQL (opcional, se não usar Docker)]

### 🔧 Clonando o repositório

```bash
git clone https://github.com/DwDan/Demo.git
cd Demo
```

### 🐳 Executando com Docker

```bash
docker compose up --build
```

A API estará disponível em: [http://localhost:8080/swagger](http://localhost:8080/swagger)

### 📦 Executando localmente (sem Docker)

1. Configure o PostgreSQL local com a string de conexão em `appsettings.json`.
2. Execute as migrations:

```bash
dotnet ef database update --project src/Services/Todos/Demo.Todos.Infrastructure --startup-project src/Services/Todos/Demo.Todos.WebApi
```

3. Inicie a API:

```bash
dotnet run --project src/Services/Todos/Demo.Todos.WebApi
```

## ✅ Funcionalidades

- Criar, listar, atualizar e remover tarefas
- Filtro por status e data de vencimento
- Documentação com Swagger
- Injeção de dependência
- Repository + Unit of Work

## 🧪 Testes (bônus)

```bash
dotnet test
```

## 📂 Estrutura do projeto

```
src/
├── Crosscutting/
├── Services/
│   └── Todos/
│       ├── Application/
│       ├── Domain/
│       ├── Infrastructure/
│       ├── IoC/
│       └── WebApi/
docker-compose.yml
```

