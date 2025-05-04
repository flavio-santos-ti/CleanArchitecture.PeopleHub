using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Infrastructure.Persistence.Mappings
{
    public class UserAccountStatusHistoryMap : IEntityTypeConfiguration<UserAccountStatusHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<UserAccountStatusHistoryEntity> builder)
        {
            builder.ToTable("user_account_status_history");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.UserAccountId)
                .HasColumnName("user_account_id")
                .IsRequired();

            builder.Property(e => e.ChangedBy)
                .HasColumnName("changed_by")
                .IsRequired();

            builder.Property(e => e.OldStatus)
                .HasColumnName("old_status")
                .IsRequired();

            builder.Property(e => e.NewStatus)
                .HasColumnName("new_status")
                .IsRequired();

            builder.Property(e => e.ChangedAt)
                .HasColumnName("changed_at")
                .IsRequired();
        }
    }
}
