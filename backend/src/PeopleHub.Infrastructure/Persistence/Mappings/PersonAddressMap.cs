using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Infrastructure.Persistence.Mappings
{
    public class PersonAddressMap : IEntityTypeConfiguration<PersonAddressEntity>
    {
        public void Configure(EntityTypeBuilder<PersonAddressEntity> builder)
        {
            builder.ToTable("person_address");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.PersonId)
                .HasColumnName("person_id")
                .IsRequired();

            builder.Property(e => e.AddressTypeCode)
                .HasColumnName("address_type")
                .HasColumnType("char(1)")
                .IsRequired();

            builder.Property(e => e.Street)
                .HasColumnName("street")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Number)
                .HasColumnName("number")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Complement)
                .HasColumnName("complement")
                .HasMaxLength(255);

            builder.Property(e => e.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.ZipCode)
                .HasColumnName("zip_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}
