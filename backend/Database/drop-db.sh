#!/bin/bash

# Nome do container conforme definido no docker-compose.yaml
CONTAINER_NAME="postgresql"

# Nome do usuário PostgreSQL
DB_USER="postgres"

# Nome do banco de dados a ser removido
DB_NAME="people_hub"

# Aguarda o PostgreSQL iniciar completamente
echo "Aguardando o PostgreSQL iniciar..."
sleep 5

# Executa o comando SQL dentro do container para remover o banco
echo "Removendo o banco de dados '$DB_NAME'..."
docker exec -i $CONTAINER_NAME psql -U $DB_USER -c "DROP DATABASE IF EXISTS $DB_NAME;"

echo "Banco de dados '$DB_NAME' removido com sucesso!"
