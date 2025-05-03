using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Infrastructure.Persistence.Mappings;

public class PersonTypeMap : IEntityTypeConfiguration<PersonTypeEntity>
{
    public void Configure(EntityTypeBuilder<PersonTypeEntity> builder)
    {
        builder.ToTable("person_type");

        builder.HasKey(e => e.Code);

        builder.Property(e => e.Code)
            .HasColumnName("code")
            .HasColumnType("char(1)");

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("description");
    }
}
