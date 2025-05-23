﻿using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public class PersonOldRepository : IPersonOldRepository
{
    private readonly PeopleHubDbContext _context;

    public PersonOldRepository(PeopleHubDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(IndividualPersonEntity person)
    {
        await _context.IndividualPersons.AddAsync(person);
    }

    public async Task AddAsync(LegalPersonEntity person)
    {
        await _context.LegalPersons.AddAsync(person);
    }

    public async Task<IndividualPersonEntity?> GetByCpfAsync(string cpf)
    {
        return await _context.IndividualPersons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Cpf == cpf);
    }

    public async Task<LegalPersonEntity?> GetByCnpjAsync(string cnpj)
    {
        return await _context.LegalPersons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Cnpj == cnpj);
    }

    public async Task UploadProfilePictureAsync(IndividualPersonEntity person)
    {
        _context.IndividualPersons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task UploadCompanyLogoPhotoAsync(LegalPersonEntity person)
    {
        _context.LegalPersons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(IndividualPersonEntity person)
    {
        _context.IndividualPersons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LegalPersonEntity person)
    {
        _context.LegalPersons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(IndividualPersonEntity person)
    {
        _context.IndividualPersons.Remove(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LegalPersonEntity person)
    {
        _context.LegalPersons.Remove(person);
        await _context.SaveChangesAsync();
    }
}