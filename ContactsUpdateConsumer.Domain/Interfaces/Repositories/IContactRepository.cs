using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task UpdateAsync(Contact contact);
    }
}
