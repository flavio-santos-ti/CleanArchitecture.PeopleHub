#!/bin/bash
# chmod +x init-db.sh
# ./init-db.sh


# Nome do container conforme definido no docker-compose.yaml
CONTAINER_NAME="postgresql"

# Nome do usuário PostgreSQL
DB_USER="postgres"

# Nome do arquivo SQL contendo a configuração do banco
SQL_FILE="create-db-people_hub.sql"

# Aguarda o PostgreSQL iniciar completamente
echo "Aguardando o PostgreSQL iniciar..."
sleep 10

# Executa o script SQL dentro do container
echo "Executando script SQL '$SQL_FILE' no PostgreSQL..."
docker cp $SQL_FILE $CONTAINER_NAME:/tmp/$SQL_FILE

docker exec -i $CONTAINER_NAME psql -U $DB_USER -f /tmp/$SQL_FILE

echo "Banco de dados configurado com sucesso!"
