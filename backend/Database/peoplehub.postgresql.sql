CREATE TABLE users (
    email VARCHAR(255) PRIMARY KEY, -- Email will be the primary key
    password_hash TEXT NOT NULL     -- Password stored securely (hashed)
);
