﻿using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Entities;
using System.Reflection;

namespace PeopleHub.Infrastructure.Persistence;

public class PeopleHubDbContext : DbContext
{
    public PeopleHubDbContext(DbContextOptions<PeopleHubDbContext> options) : base(options) { }

    public DbSet<IndividualPersonEntity> IndividualPersons { get; set; }
    public DbSet<LegalPersonEntity> LegalPersons { get; set; }
    public DbSet<UserAccountEntity> UserAccounts { get; set; }
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<PersonTypeEntity> PersonTypes { get; set; }
    public DbSet<AddressTypeEntity> AddressTypes { get; set; }
    public DbSet<PersonAddressEntity> PersonAddresses { get; set; }
    public DbSet<UserAccountStatusHistoryEntity> UserAccountStatusHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}