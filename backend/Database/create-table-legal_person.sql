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
