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