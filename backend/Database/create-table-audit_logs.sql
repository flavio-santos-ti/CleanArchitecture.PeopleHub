
CREATE TABLE audit_logs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    event_timestamp  TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
    event_action VARCHAR(255) NOT NULL, 
    context_name VARCHAR(255) NOT NULL, 
    http_status_code INTEGER NOT NULL,
    event_data JSONB, 
    user_email VARCHAR(255) NOT NULL, 
    user_ip VARCHAR(45) NOT NULL DEFAULT 'Unknown IP' 
);
