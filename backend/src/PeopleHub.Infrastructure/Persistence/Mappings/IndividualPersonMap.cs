using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Infrastructure.Persistence.Mappings;

public class IndividualPersonMap : IEntityTypeConfiguration<IndividualPersonEntity>
{
    public void Configure(EntityTypeBuilder<IndividualPersonEntity> builder)
    {
        builder.ToTable("individual_person");

        builder.HasKey(e => e.PersonId);
        builder.Property(e => e.PersonId)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("full_name");

        var cpfConverter = new ValueConverter<Cpf, string>(
            cpf => cpf.Value,  // Apenas acessa o valor
            value => new Cpf(value) // Apenas cria o Cpf sem null
        );

        builder.Property(e => e.Cpf)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnName("cpf")
            .HasConversion(cpfConverter);

        builder.HasIndex(e => e.Cpf)
            .IsUnique();

        builder.Property(e => e.BirthDate)
            .IsRequired()
            .HasColumnName("birth_date");

        builder.Property(e => e.Gender)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("gender");

        builder.Property(e => e.Photo)
            .HasColumnType("bytea")
            .HasColumnName("photo");

        builder.OwnsOne(e => e.Address, address =>
        {
            address.Property(a => a.Street).IsRequired().HasMaxLength(255).HasColumnName("street");
            address.Property(a => a.Number).IsRequired().HasMaxLength(10).HasColumnName("number");
            address.Property(a => a.Complement).HasMaxLength(255).HasColumnName("complement");
            address.Property(a => a.City).IsRequired().HasMaxLength(100).HasColumnName("city");
            address.Property(a => a.State).IsRequired().HasMaxLength(50).HasColumnName("state");
            address.Property(a => a.ZipCode).IsRequired().HasMaxLength(20).HasColumnName("zip_code");
        });

        builder.OwnsOne(e => e.Email, email =>
        {
            email.Property(e => e.Value).IsRequired().HasMaxLength(255).HasColumnName("email");
        });
    }
}