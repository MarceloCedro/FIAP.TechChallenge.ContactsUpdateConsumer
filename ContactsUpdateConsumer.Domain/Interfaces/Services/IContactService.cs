using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Services
{
    public interface IContactService
    {
        Task UpdateAsync(Contact contact);
    }
}