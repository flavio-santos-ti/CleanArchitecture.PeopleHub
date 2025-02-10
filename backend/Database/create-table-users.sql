CREATE TABLE users (
    email VARCHAR(255) PRIMARY KEY,  -- Email será a chave primária
    password_hash TEXT NOT NULL      -- Senha armazenada de forma segura (hash)
);
