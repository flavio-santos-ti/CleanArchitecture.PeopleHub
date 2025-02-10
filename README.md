# Clean Architecture – Implementação no .NET

## 📌 Sobre o Projeto  
O **PeopleHub** é uma Prova de Conceito (PoC) de uma API desenvolvida em **.NET 8**, seguindo os princípios da **Clean Architecture** e **Domain-Driven Design (DDD)** para garantir um código modular, testável e de fácil manutenção.  

A arquitetura do projeto foi cuidadosamente projetada para promover a **separação de responsabilidades**, isolando regras de negócio da infraestrutura e frameworks externos.

---

## 📄 Documentação

Para mais detalhes sobre a arquitetura e implementação do projeto, consulte a documentação da PoC:

📄 [Documento da Arquitetura do Projeto](backend/docs/PoC-PeopleHub-v0.pdf)


## 🚀 Tecnologias e Frameworks Utilizados  
| Tecnologia | Descrição |
|------------|-----------|
| **.NET 8** | Framework principal para desenvolvimento da API. |
| **ASP.NET Core** | Utilizado para criar a API RESTful. |
| **Entity Framework Core** | ORM para mapeamento e persistência no banco de dados. |
| **PostgreSQL** | Banco de dados utilizado na aplicação. |
| **Asp.Versioning.Mvc** | Gerenciamento de versionamento da API. |
| **Npgsql.EntityFrameworkCore.PostgreSQL** | Driver EF Core para integração com PostgreSQL. |
| **BCrypt.Net-Next** | Biblioteca para hash seguro de senhas. |

---

## 📖 Diretrizes de Implementação  

### ✅ **Padrões e Boas Práticas**  
✔ Aplicação dos princípios **SOLID** e **DDD**.  
✔ Separação de responsabilidades seguindo a Clean Architecture.  
✔ Uso de **injeção de dependência** para desacoplamento.  
✔ Versionamento de API com **Asp.Versioning.Mvc**.  
✔ Logs de auditoria implementados para rastreamento de ações.  

### ✅ **Definição de Repositórios e Camadas**  
✔ **Repositórios** seguem o padrão **Repository Pattern** para abstração da camada de persistência.  
✔ **Casos de uso (Use Cases)** encapsulam regras de negócio e orquestram operações.  
✔ **Autenticação JWT** implementada na camada de configuração (**AppConfig**).  

---

## 📡 Endpoints Principais  

### 🔑 **Autenticação**
- `POST /api/v1.0/auth/login` → Autentica o usuário e gera um token JWT.  
- `POST /api/v1.0/auth/register` → Cria uma nova conta de usuário.  

### 👤 **Pessoa Física**
- `POST /api/v1.0/individual` → Cadastra uma nova pessoa física.  
- `GET /api/v1.0/individual/{cpf}` → Consulta pessoa física por CPF.  
- `PUT /api/v1.0/individual` → Atualiza dados de uma pessoa física.  
- `DELETE /api/v1.0/individual` → Remove um cadastro de pessoa física.  

### 🏢 **Pessoa Jurídica**
- `POST /api/v1.0/legal` → Cadastra uma nova empresa.  
- `PUT /api/v1.0/legal` → Atualiza os dados da empresa.  
- `DELETE /api/v1.0/legal` → Remove uma empresa cadastrada.  

### 📷 **Upload de Imagem**
- `POST /api/v1.0/upload-photo` → Faz o upload da foto do usuário ou logotipo da empresa.  

---

## 🛠️ Como Executar o Projeto  

### 📌 **Pré-requisitos**
- **.NET 8** instalado  
- **PostgreSQL** configurado  

### 📥 **Passos para rodar localmente**
1️⃣ Clone o repositório:  
```bash
git clone https://github.com/seu-repositorio/PeopleHub.git
cd PeopleHub
```
2️⃣ Configure o banco de dados no arquivo **appsettings.json**  
3️⃣ Execute a aplicação:  
```bash
dotnet run --project 01-Presentation/PeopleHub.Api
```

---

## 👨‍💻 Contribuição  
Se deseja contribuir com melhorias, siga os seguintes passos:
1. Faça um **fork** do projeto  
2. Crie um **branch** com sua feature  
3. Abra um **Pull Request**  

---

## 📜 Licença  
Este projeto é licenciado sob a **MIT License**.  

---
