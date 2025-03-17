using FIAP.TechChallenge.ContactsUpdateProducer.Domain.DTOs.EntityDTOs;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Applications
{
    public interface IContactApplication
    {
        Task UpdateContactAsync(ContactDto contactDto);
    }
}