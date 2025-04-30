
-- Tabela de usuários do sistema / System users table
CREATE TABLE users (
    email VARCHAR(255) PRIMARY KEY, -- E-mail do usuário / User email (primary key)
    password_hash TEXT NOT NULL     -- Senha em hash / Hashed user password
);


-- Tabela de endereços de pessoas / Table of person addresses
CREATE TABLE person_address (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),   -- Identificador único / Unique identifier
    street VARCHAR(255) NOT NULL,                    -- Nome da rua / Street name
    number VARCHAR(10) NOT NULL,                     -- Número do endereço / Address number
    complement VARCHAR(255),                         -- Complemento (opcional) / Address complement (optional)
    city VARCHAR(100) NOT NULL,                      -- Cidade / City
    state VARCHAR(50) NOT NULL,                      -- Estado / State
    zip_code VARCHAR(20) NOT NULL,                   -- CEP / ZIP code
    phone VARCHAR(20) NOT NULL,                      -- Telefone / Phone number
    email VARCHAR(255) NOT NULL,                     -- E-mail associado / Associated email
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP   -- Data de criação / Creation timestamp
);
