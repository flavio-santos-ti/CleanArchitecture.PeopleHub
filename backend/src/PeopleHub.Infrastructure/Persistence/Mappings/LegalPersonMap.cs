using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Infrastructure.Persistence.Mappings;

public class LegalPersonMap : IEntityTypeConfiguration<LegalPersonEntity>
{
    public void Configure(EntityTypeBuilder<LegalPersonEntity> builder)
    {
        builder.ToTable("legal_person");

        builder.HasKey(e => e.PersonId);
        builder.Property(e => e.PersonId)
            .HasColumnName("id");

        builder.Property(e => e.LegalName)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("legal_name");

        builder.Property(e => e.TradeName)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("trade_name");

        var cnpjConverter = new ValueConverter<Cnpj, string>(
            cnpj => cnpj.Value,  // Apenas acessa o valor
            value => new Cnpj(value) // Apenas cria o Cnpj sem null
        );

        builder.Property(e => e.Cnpj)
            .IsRequired()
            .HasMaxLength(14)
            .HasColumnName("cnpj")
            .HasConversion(cnpjConverter);

        builder.HasIndex(e => e.Cnpj)
            .IsUnique();

        builder.Property(e => e.StateRegistration)
            .HasMaxLength(50)
            .HasColumnName("state_registration");

        builder.Property(e => e.MunicipalRegistration)
            .HasMaxLength(50)
            .HasColumnName("municipal_registration");

        builder.Property(e => e.LegalRepresentativeName)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("legal_representative_name");

        var cpfConverter = new ValueConverter<Cpf?, string>(
            cpf => cpf == null ? string.Empty : cpf.Value, // Se for nulo, salva como string vazia
            value => string.IsNullOrEmpty(value) ? null : new Cpf(value) // Se string vazia, define como null
        );

        builder.Property(e => e.LegalRepresentativeCpf)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnName("legal_representative_cpf")
            .HasConversion(cpfConverter);

        builder.Property(e => e.Logo)
            .HasColumnType("bytea")
            .HasColumnName("logo");

        builder.OwnsOne(e => e.Phone, phone =>
        {
            phone.Property(p => p.Number).IsRequired().HasMaxLength(20).HasColumnName("phone");
        });

        builder.OwnsOne(e => e.Email, email =>
        {
            email.Property(e => e.Value).IsRequired().HasMaxLength(255).HasColumnName("email");
        });
    }
}
