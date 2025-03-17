using ContactsUpdateConsumer.Api.Events;
using FIAP.TechChallenge.ContactsInsertProducer.IntegrationTests.Config;
using FIAP.TechChallenge.ContactsUpdateConsumer.Application.Applications;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Services;
using FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.Repositories;
using FIAP.TechChallenge.ContactsUpdateProducer.Domain.DTOs.EntityDTOs;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace FIAP.TechChallenge.ContactsConsult.IntegrationTest.Validations
{
    public class ContactConsumerTest : BaseServiceTests
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactService _contactService;
        private readonly IContactApplication _contactApplication;
        private readonly ContactUpdateConsumer _contactsConsumer;
        
        private Mock<ILogger<ContactService>> _loggerServiceMock;
        private Mock<ILogger<ContactApplication>> _loggerApplicationMock;
        public readonly Random RandomId;

        public ContactConsumerTest()
        {            
            _loggerServiceMock = new Mock<ILogger<ContactService>>();
            _loggerApplicationMock = new Mock<ILogger<ContactApplication>>();

            _contactRepository = new ContactRepository(_context);

            _contactService = new ContactService(_contactRepository, _loggerServiceMock.Object);
           
            _contactApplication = new ContactApplication(_contactService,
                                                         _loggerApplicationMock.Object);
            
            _contactsConsumer = new ContactUpdateConsumer(_contactApplication);

            RandomId = new Random();
        }

        [Fact]
        public async Task UpdateContactEmptyMessageAsync()
        {
            var context = Mock.Of<ConsumeContext<ContactDto>>(_ =>
            _.Message == null);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _contactsConsumer.Consume(context));

            Assert.Equal("Message cannot be null or empty.", exception.Message);
        }

        [Fact]
        public async Task UpdateContactSuccessAsync()
        {
            var contact = ContactFixtures.CreateFakeContact(RandomId.Next(99));

            await _context.AddRangeAsync(contact);

            await SaveChanges();

            var contactDtoTestObject = new ContactDto()
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                AreaCode = contact.AreaCode,
                Phone = contact.Phone
            };

            var context = Mock.Of<ConsumeContext<ContactDto>>(_ =>
            _.Message == contactDtoTestObject);

            var exception = Record.ExceptionAsync(() => _contactsConsumer.Consume(context));
            Assert.Null(exception.Result);
        }
    }
}
