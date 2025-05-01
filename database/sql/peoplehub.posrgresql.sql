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


