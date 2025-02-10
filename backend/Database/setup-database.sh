#!/bin/bash

# Configurações do banco de dados
CONTAINER_NAME="postgresql"  # Nome do container PostgreSQL no Docker
DATABASE_NAME="people_hub"
USER="postgres"
PASSWORD="your_password"

echo "🔍 Verificando se o banco de dados '$DATABASE_NAME' existe..."
EXISTING_DB=$(docker exec -i $CONTAINER_NAME psql -U $USER -tAc "SELECT 1 FROM pg_database WHERE datname='$DATABASE_NAME'")

if [ "$EXISTING_DB" != "1" ]; then
    echo "📌 Banco de dados '$DATABASE_NAME' não encontrado. Criando..."
    docker exec -i $CONTAINER_NAME psql -U $USER < create-db-people_hub.sql
    if [ $? -eq 0 ]; then
        echo "✅ Banco de dados '$DATABASE_NAME' criado com sucesso!"
    else
        echo "❌ Erro ao criar o banco de dados!"
        exit 1
    fi
else
    echo "✅ O banco de dados '$DATABASE_NAME' já existe."
fi

# Caminho dos scripts SQL
SQL_SCRIPTS=(
  "create-table-individual_person.sql"
  "create-table-legal_person.sql"
  "create-constraints.sql"
  "create-table-users.sql"
  "create-table-audit_logs.sql"
)

echo "🚀 Iniciando a configuração do banco de dados..."

# Loop para executar cada script SQL dentro do container do PostgreSQL
for SCRIPT in "${SQL_SCRIPTS[@]}"; do
    echo "📂 Executando $SCRIPT..."
    docker exec -i $CONTAINER_NAME psql -U $USER -d $DATABASE_NAME < $SCRIPT
    if [ $? -eq 0 ]; then
        echo "✅ $SCRIPT executado com sucesso!"
    else
        echo "❌ Erro ao executar $SCRIPT"
        exit 1
    fi
done

echo "🎉 Configuração do banco de dados concluída com sucesso!"
