using FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.Data;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository

    {
        private readonly ContactsDbContext _context;

        public ContactRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}