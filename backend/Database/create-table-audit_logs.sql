
CREATE TABLE audit_logs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    event_timestamp  TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
    event_action VARCHAR(255) NOT NULL, -- Nome da ação (CREATE, UPDATE, DELETE)
    context_name VARCHAR(255) NOT NULL, -- Nome da entidade afetada
    http_status_code INTEGER NOT NULL,
    event_data JSONB, 
    user_email VARCHAR(255) NOT NULL, -- Usuário que realizou a ação
    user_ip VARCHAR(45) NOT NULL DEFAULT 'Unknown IP' -- IP do usuário autenticado
);
