# Clean Architecture – Implementation in .NET

## 📌 About the Project
**PeopleHub** is a Proof of Concept (PoC) for an API developed in **.NET 8**, following the principles of **Clean Architecture** and **Domain-Driven Design (DDD)** to ensure a modular, testable, and easily maintainable codebase. 

The project's architecture has been carefullu designed to promote the **separation of concernss**, isolating business rules from infrastructure and external frameworks.

---

## 📄 Documentation

For more details on the project's architecture and implementation, refer to the PoC documentation:

📄 [Project Architecture Document](backend/docs/PoC-PeopleHub-v0.pdf)


## 🚀 Tecnologies and Frameworks Used
| Tecnology | Description |
|------------|-----------|
| **.NET 8** | main framewokr for API development. |
| **ASP.NET Core** | Used to create the RESTful API. |
| **Entity Framework Core** | ORM for database mapping and persistence. |
| **PostgreSQL** | Database used in the application.. |
| **Asp.Versioning.Mvc** |API versioning management. |
| **Npgsql.EntityFrameworkCore.PostgreSQL** | EF Core driver for PostgreSQL integration. |
| **BCrypt.Net-Next** | Library for secure password hashing. |
| **Swasgbuckle.AspNetCore** | Library for API documentation and Swagger UI generation. |

---

## 📡 Main Endpoints

### 🔑 **Authentication**
- `POST /api/v1.0/auth/login` → Authenticates the user and generates a JWT token.  
- `POST /api/v1.0/auth/register` → Creates a new user account.  

### 👤 **Individual (Person)**
- `POST /api/v1.0/individual` → Registers a new individual person.  
- `GET /api/v1.0/individual/{cpf}` → Retrieves an individual person by CPF.  
- `PUT /api/v1.0/individual` → Updates and individual persons's data.  
- `DELETE /api/v1.0/individual` → Remove um cadastro de pessoa física.  

### 🏢 **Pessoa Jurídica**
- `POST /api/v1.0/legal` → Cadastra uma nova empresa.  
- `PUT /api/v1.0/legal` → Atualiza os dados da empresa.  
- `DELETE /api/v1.0/legal` → Deletes an individual person's record..  

### 📷 **Image Upload**
- `POST /api/v1.0/upload-photo` → Uploads the user's photo or the company's logo.  

---

## 🛠️ How to Run the Project

### 📌 **Prerequisites**
- **.NET 8** installed  
- **PostgreSQL** configured  

### 📥 **Stepes to Run Locally**
1️⃣ Clone the repository:  
```bash
git clone https://github.com/seu-repositorio/PeopleHub.git
cd PeopleHub
```
2️⃣ Configure the database in the **appsettings.json** file.  
3️⃣ Run the application:  
```bash
dotnet run --project 01-Presentation/PeopleHub.Api
```

---

## 📜 License  
This project is licensed under the **MIT License**.  

---
