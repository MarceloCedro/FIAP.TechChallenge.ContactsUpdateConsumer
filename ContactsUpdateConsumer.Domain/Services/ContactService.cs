using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.ElasticSearch;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Services
{
    public class ContactService(
        IContactRepository contactRepository, 
        IElasticClient<Contact> elasticClient, 
        ILogger<ContactService> logger) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ILogger<ContactService> _logger = logger;
        private readonly IElasticClient<Contact> _elasticClient = elasticClient;

        public async Task UpdateAsync(Contact contactUpdate)
        {
            try
            {
                await _contactRepository.UpdateAsync(contactUpdate);

                await _elasticClient.Replace(contactUpdate.Id.ToString(), contactUpdate, "contacts-indexed-v2");
            }
            catch (Exception e)
            {
                var message = $"Some error occour when trying to update a Contact. Error: {e.Message}";
                _logger.LogError(message);
                throw new Exception(message);
            }
        }
    }
}