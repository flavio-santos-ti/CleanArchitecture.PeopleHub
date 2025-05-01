-- Tabela base para qualquer pessoa / Base table for any person
CREATE TABLE person (
    id UUID PRIMARY KEY,                -- Identificador único / Unique identifier
    person_type VARCHAR(10) NOT NULL    -- Tipo: 'individual' ou 'legal' / Type: 'individual' or 'legal'
        CHECK (person_type IN ('individual', 'legal'))
);



-- Tabela de usuários do sistema / System users table
CREATE TABLE users (
    email VARCHAR(255) PRIMARY KEY, -- E-mail do usuário / User email (primary key)
    password_hash TEXT NOT NULL     -- Senha em hash / Hashed user password
);

-- Tabela de endereços de pessoas / Table of person addresses
CREATE TABLE person_address (
    id UUID PRIMARY KEY,            -- Identificador único / Unique identifier
    street VARCHAR(255) NOT NULL,   -- Nome da rua / Street name
    number VARCHAR(10) NOT NULL,    -- Número do endereço / Address number
    complement VARCHAR(255),        -- Complemento (opcional) / Address complement (optional)
    city VARCHAR(100) NOT NULL,     -- Cidade / City
    state VARCHAR(50) NOT NULL,     -- Estado / State
    zip_code VARCHAR(20) NOT NULL,  -- CEP / ZIP code
    phone VARCHAR(20) NOT NULL,     -- Telefone / Phone number
    email VARCHAR(255) NOT NULL,    -- E-mail associado / Associated email
    created_at TIMESTAMP            -- Data de criação / Creation timestamp
);


CREATE TABLE legal_person (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    legal_name VARCHAR(200) NOT NULL,
    trade_name VARCHAR(200) NOT NULL,
    cnpj CHAR(14) UNIQUE NOT NULL,
    state_registration VARCHAR(50),
    municipal_registration VARCHAR(50),
    street VARCHAR(255) NOT NULL,
    number VARCHAR(10) NOT NULL,
    complement VARCHAR(255),
    city VARCHAR(100) NOT NULL,
    state VARCHAR(50) NOT NULL,
    zip_code VARCHAR(20) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(255) NOT NULL,
    legal_representative_name VARCHAR(150) NOT NULL,
    legal_representative_cpf CHAR(11) NOT NULL,
    logo BYTEA, -- Armazena o logotipo como BLOB (binário)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Criando índice para otimizar buscas pelo CNPJ
CREATE INDEX idx_legal_person_cnpj ON legal_person (cnpj);

CREATE TABLE individual_person (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    full_name VARCHAR(150) NOT NULL,
    cpf CHAR(11) UNIQUE NOT NULL,
    birth_date DATE NOT NULL,
    gender SMALLINT NOT NULL,
    street VARCHAR(255) NOT NULL,
    number VARCHAR(10) NOT NULL,
    complement VARCHAR(255),
    city VARCHAR(100) NOT NULL,
    state VARCHAR(50) NOT NULL,
    zip_code VARCHAR(20) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(255) NOT NULL,
    photo BYTEA, -- Armazena a foto como BLOB (binário)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Criando índice para otimizar buscas pelo CPF
CREATE INDEX idx_individual_person_cpf ON individual_person (cpf);


--*********************************************************

-- Tabela base para qualquer pessoa / Base table for any person
CREATE TABLE person (
    id UUID PRIMARY KEY,                                      -- Identificador único / Unique identifier
    person_type VARCHAR(10) NOT NULL                          -- Tipo: 'individual' ou 'legal' / Type: 'individual' or 'legal'
        CHECK (person_type IN ('individual', 'legal'))
);

-- Tabela de tipos de endereço / Address type table
CREATE TABLE address_type (
    id SERIAL PRIMARY KEY,                                    -- ID sequencial / Sequential ID
    code VARCHAR(30) UNIQUE NOT NULL,                         -- Código interno / Internal code (ex: 'billing')
    description VARCHAR(100) NOT NULL                         -- Descrição legível / Human-readable description
);

-- Tabela de endereços associados a pessoas / Table of addresses linked to people
CREATE TABLE person_address (
    id UUID PRIMARY KEY,                                      -- Identificador único / Unique identifier

    person_id UUID NOT NULL REFERENCES person(id),            -- Pessoa associada / Linked person
    address_type_id INT NOT NULL REFERENCES address_type(id), -- Tipo do endereço / Address type

    street VARCHAR(255) NOT NULL,                             -- Rua / Street
    number VARCHAR(10) NOT NULL,                              -- Número / Number
    complement VARCHAR(255),                                  -- Complemento / Complement
    city VARCHAR(100) NOT NULL,                               -- Cidade / City
    state VARCHAR(50) NOT NULL,                               -- Estado / State
    zip_code VARCHAR(20) NOT NULL,                            -- CEP / ZIP code
    phone VARCHAR(20) NOT NULL,                               -- Telefone / Phone
    email VARCHAR(255) NOT NULL,                              -- E-mail associado / Associated email

    created_at TIMESTAMP                                      -- Data de criação / Creation timestamp
);

-- Tabela de pessoa jurídica / Table for legal entity
CREATE TABLE legal_person (
    person_id UUID PRIMARY KEY REFERENCES person(id),         -- Referência à pessoa base / Reference to base person
    legal_name VARCHAR(200) NOT NULL,                         -- Razão social / Legal name
    trade_name VARCHAR(200) NOT NULL,                         -- Nome fantasia / Trade name
    cnpj CHAR(14) UNIQUE NOT NULL,                            -- CNPJ único / Unique CNPJ
    state_registration VARCHAR(50),                           -- Inscrição estadual / State registration
    municipal_registration VARCHAR(50),                       -- Inscrição municipal / Municipal registration
    legal_representative_name VARCHAR(150) NOT NULL,          -- Nome do representante legal / Representative name
    legal_representative_cpf CHAR(11) NOT NULL,               -- CPF do representante / Representative CPF
    logo BYTEA,                                               -- Logotipo da empresa / Company logo
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP            -- Data de criação / Creation timestamp
);

-- Tabela de pessoa física / Table for individual person
CREATE TABLE individual_person (
    person_id UUID PRIMARY KEY REFERENCES person(id),         -- Referência à pessoa base / Reference to base person
    full_name VARCHAR(150) NOT NULL,                          -- Nome completo / Full name
    cpf CHAR(11) UNIQUE NOT NULL,                             -- CPF único / Unique CPF
    birth_date DATE NOT NULL,                                 -- Data de nascimento / Birth date
    gender SMALLINT NOT NULL,                                 -- Gênero (0: indefinido, 1: masc, 2: fem, etc.) / Gender
    photo BYTEA,                                              -- Foto da pessoa / Person photo
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP            -- Data de criação / Creation timestamp
);

-- Índices para buscas otimizadas
CREATE INDEX idx_legal_person_cnpj ON legal_person (cnpj);
CREATE INDEX idx_individual_person_cpf ON individual_person (cpf);
CREATE INDEX idx_person_address_person_id ON person_address (person_id);
CREATE INDEX idx_person_address_type ON person_address (address_type_id);
