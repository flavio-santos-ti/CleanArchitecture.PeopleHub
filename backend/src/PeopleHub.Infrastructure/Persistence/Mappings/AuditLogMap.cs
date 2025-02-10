using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Infrastructure.Persistence.Mappings
{
    public class AuditLogMap : IEntityTypeConfiguration<AuditLogEntity>
    {
        public void Configure(EntityTypeBuilder<AuditLogEntity> builder)
        {
            builder.ToTable("audit_logs");

            builder.HasKey(al => al.Id);
            builder.Property(al => al.Id)
                .HasColumnName("id") 
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(al => al.UserEmail)
                .HasColumnName("user_email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(al => al.UserIp)
                .HasColumnName("user_ip")
                .HasMaxLength(45)
                .HasDefaultValue("Unknown IP");

            builder.Property(al => al.EventAction)
                .HasColumnName("event_action")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(al => al.ContextName)
                .HasColumnName("context_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(al => al.HttpStatusCode)
                .HasColumnName("http_status_code")
                .IsRequired();

            builder.Property(al => al.EventData)
                .HasColumnName("event_data")
                .HasColumnType("jsonb");

            builder.Property(al => al.EventTimestamp)
                .HasColumnName("event_timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
