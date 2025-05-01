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

