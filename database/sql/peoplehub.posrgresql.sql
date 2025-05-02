----------------------------------------------------------------------------------------------------------
-- Tabela de tipos de pessoa (Física ou Jurídica)
-- Table of person types (Individual or Legal entity)

CREATE TABLE person_type (
    code CHAR(1) PRIMARY KEY,         -- Código: 'F' ou 'J' / Code: 'F' or 'J'
    description VARCHAR(50) NOT NULL  -- Descrição do tipo de pessoa / Person type description
);

-- Inserção dos tipos de pessoa padrão
-- Insert default person types

INSERT INTO person_type (code, description) VALUES
('F', 'Pessoa Física'),    -- Individual person
('J', 'Pessoa Jurídica');  -- Legal entity

-----------------------------------------------------------------------------------------------------------
-- Tabela base para qualquer pessoa (física ou jurídica)
-- Base table for any person (individual or legal entity)

CREATE TABLE person (
    id UUID PRIMARY KEY,         -- Identificador único / Unique identifier
    person_type CHAR(1) NOT NULL -- Tipo de pessoa: 'F' ou 'J' / Person type: 'F' or 'J'
);

-- Restrição de chave estrangeira para o tipo de pessoa
-- Foreign key constraint to person type

ALTER TABLE person
ADD CONSTRAINT fk_person_type
FOREIGN KEY (person_type) REFERENCES person_type(code);

----------------------------------------------------------------------------------------------------------
-- Tabela de tipos de endereço vinculados à pessoa
-- Table of address types linked to a person

CREATE TABLE address_type (
    code CHAR(1) PRIMARY KEY,         -- Código do tipo / Type code
    description VARCHAR(100) NOT NULL -- Descrição legível / Human-readable description
);

-- Inserção dos tipos de endereço padrão
-- Insert default address types

INSERT INTO address_type (code, description) VALUES
('B', 'Endereço de cobrança'),        -- Billing address
('S', 'Endereço de entrega'),         -- Shipping address
('F', 'Endereço fiscal'),             -- Fiscal address
('C', 'Endereço de correspondência'), -- Correspondence address
('R', 'Endereço residencial'),        -- Residential address
('M', 'Endereço comercial');          -- Commercial address

----------------------------------------------------------------------------------------------------------
-- Tabela de endereços associados a uma pessoa
-- Table of addresses linked to a person

CREATE TABLE person_address (
    id UUID PRIMARY KEY,                                -- Identificador único / Unique identifier

    person_id UUID NOT NULL,                            -- Referência para pessoa / Reference to person
    address_type CHAR(1) NOT NULL,                      -- Tipo de endereço / Address type ('B', 'S', 'F', 'C', 'R', 'M')

    street VARCHAR(255) NOT NULL,                       -- Rua / Street
    number VARCHAR(10) NOT NULL,                        -- Número / Number
    complement VARCHAR(255),                            -- Complemento (opcional) / Complement (optional)
    city VARCHAR(100) NOT NULL,                         -- Cidade / City
    state VARCHAR(50) NOT NULL,                         -- Estado / State
    zip_code VARCHAR(20) NOT NULL,                      -- CEP / ZIP code
    phone VARCHAR(20) NOT NULL,                         -- Telefone / Phone
    email VARCHAR(255) NOT NULL,                        -- E-mail associado / Email

    is_active BOOLEAN NOT NULL DEFAULT TRUE,            -- Endereço ativo? / Is the address active?

    created_at TIMESTAMP                                -- Data de criação / Creation timestamp
);

-- Restrição de chave estrangeira para pessoa
-- Foreign key constraint to person
ALTER TABLE person_address
ADD CONSTRAINT fk_person_address_person
FOREIGN KEY (person_id) REFERENCES person(id);

-- Restrição de chave estrangeira para tipo de endereço
-- Foreign key constraint to address type
ALTER TABLE person_address
ADD CONSTRAINT fk_person_address_type
FOREIGN KEY (address_type) REFERENCES address_type(code);

-- Índice para otimizar buscas por pessoa
-- Index to optimize searches by person
CREATE INDEX idx_person_address_person_id ON person_address (person_id);

-- Índice para otimizar buscas por tipo de endereço
-- Index to optimize searches by address type
CREATE INDEX idx_person_address_type ON person_address (address_type);

----------------------------------------------------------------------------------------------------------
-- Tabela de pessoa jurídica vinculada à tabela person
-- Table of legal entity linked to person table
CREATE TABLE legal_person (
    person_id UUID PRIMARY KEY,                             -- Referência para a pessoa base / Reference to base person
    legal_name VARCHAR(200) NOT NULL,                       -- Razão social / Legal name
    trade_name VARCHAR(200) NOT NULL,                       -- Nome fantasia / Trade name
    cnpj CHAR(14) UNIQUE NOT NULL,                          -- CNPJ único / Unique CNPJ
    state_registration VARCHAR(50),                         -- Inscrição estadual / State registration
    municipal_registration VARCHAR(50),                     -- Inscrição municipal / Municipal registration
    legal_representative_name VARCHAR(150) NOT NULL,        -- Nome do representante legal / Legal representative name
    legal_representative_cpf CHAR(11) NOT NULL,             -- CPF do representante / Representative CPF
    logo BYTEA,                                             -- Logotipo da empresa / Company logo
    created_at TIMESTAMP                                    -- Data de criação definida pela aplicação / Creation timestamp set by application
);

-- Restrição de chave estrangeira para person
-- Foreign key constraint to person
ALTER TABLE legal_person
ADD CONSTRAINT fk_legal_person_person
FOREIGN KEY (person_id) REFERENCES person(id);

-- Índice para otimizar buscas por CNPJ
-- Index to optimize CNPJ searches
CREATE INDEX idx_legal_person_cnpj ON legal_person (cnpj);

----------------------------------------------------------------------------------------------------------
-- Tabela de pessoa física vinculada à tabela person
-- Table of individual person linked to person table

CREATE TABLE individual_person (
    person_id UUID PRIMARY KEY,      -- Referência à pessoa base / Reference to base person
    full_name VARCHAR(150) NOT NULL, -- Nome completo / Full name
    cpf CHAR(11) UNIQUE NOT NULL,    -- CPF único / Unique CPF
    birth_date DATE NOT NULL,        -- Data de nascimento / Birth date
    gender SMALLINT NOT NULL,        -- Gênero / Gender
    photo BYTEA,                     -- Foto da pessoa / Person photo
    created_at TIMESTAMP             -- Data de criação definida pela aplicação / Creation timestamp set by application
);

-- Restrição de chave estrangeira para person
-- Foreign key constraint to person

ALTER TABLE individual_person
ADD CONSTRAINT fk_individual_person_person
FOREIGN KEY (person_id) REFERENCES person(id);

----------------------------------------------------------------------------------------------------------
-- Tabela de contas de usuário do sistema
-- Table of system user accounts
CREATE TABLE user_account (
    id UUID PRIMARY KEY,                     -- Identificador único / Unique identifier
    email VARCHAR(255) UNIQUE NOT NULL,      -- E-mail de login / Login email
    password_hash TEXT NOT NULL,             -- Senha em hash / Hashed user password
    person_id UUID NOT NULL,                 -- Pessoa vinculada / Linked person
    is_active BOOLEAN NOT NULL DEFAULT TRUE, -- Conta ativa? / Is the account active?
    created_at TIMESTAMP,                    -- Data de criação / Creation timestamp
    updated_at TIMESTAMP                     -- Data de atualização / Update timestamp
);

-- FK para pessoa
ALTER TABLE user_account
ADD CONSTRAINT fk_user_account_person
FOREIGN KEY (person_id) REFERENCES person(id);

----------------------------------------------------------------------------------------------------------
-- Tabela de histórico de status de ativação de usuários
-- Table of user activation status history
CREATE TABLE user_status_history (
    id UUID PRIMARY KEY,                                  -- Identificador único do histórico / Unique history record ID

    user_id UUID NOT NULL,                                -- ID do usuário afetado / Affected user ID
    changed_by UUID NOT NULL,                             -- Pessoa que realizou a alteração / Person who made the change

    old_status BOOLEAN NOT NULL,                          -- Status anterior / Previous status
    new_status BOOLEAN NOT NULL,                          -- Novo status / New status

    changed_at TIMESTAMP NOT NULL                         -- Data da alteração / Change timestamp
);

-- Restrição de chave estrangeira para usuário
-- Foreign key constraint to user
ALTER TABLE user_status_history
ADD CONSTRAINT fk_user_status_user
FOREIGN KEY (user_id) REFERENCES users(id);

-- Restrição de chave estrangeira para quem alterou (pessoa)
-- Foreign key constraint to person (who changed)
ALTER TABLE user_status_history
ADD CONSTRAINT fk_user_status_changed_by
FOREIGN KEY (changed_by) REFERENCES person(id);
