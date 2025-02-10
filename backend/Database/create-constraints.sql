ALTER TABLE individual_person ADD CONSTRAINT unique_cpf UNIQUE (cpf);
ALTER TABLE legal_person ADD CONSTRAINT unique_cnpj UNIQUE (cnpj);
