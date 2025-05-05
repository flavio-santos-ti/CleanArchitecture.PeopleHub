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

        builder.Property(e => e.PersonId)
            .HasColumnName("person_id");

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
    }
}