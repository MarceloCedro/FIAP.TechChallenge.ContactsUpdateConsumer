using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Entities;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsUpdateProducer.Domain.DTOs.EntityDTOs;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Application.Applications
{
    public class ContactApplication(IContactService contactService,
                                    ILogger<ContactApplication> logger) : IContactApplication
    {
        private readonly IContactService _contactService = contactService;
        private readonly ILogger<ContactApplication> _logger = logger;

        public async Task UpdateContactAsync(ContactDto contactDto)
        {
            try
            {
                var contact = new Contact
                {
                    Id = contactDto.Id,
                    Name = contactDto.Name,
                    Email = contactDto.Email,
                    AreaCode = contactDto.AreaCode,
                    Phone = contactDto.Phone
                };

                await _contactService.UpdateAsync(contact);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}