﻿using FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ContactsInsertProducer.IntegrationTests.Config;

public class TestDbMemoryFixture : IDisposable
{
    public ContactsDbContext ContactsDbContext { get; set; }

    public TestDbMemoryFixture()
    {
        var options = new DbContextOptionsBuilder<ContactsDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        ContactsDbContext = new ContactsDbContext(options);

        ContactsDbContext.Database.EnsureDeleted();
        ContactsDbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        ContactsDbContext.Database.EnsureDeleted();
        ContactsDbContext.Dispose();
    }
}
