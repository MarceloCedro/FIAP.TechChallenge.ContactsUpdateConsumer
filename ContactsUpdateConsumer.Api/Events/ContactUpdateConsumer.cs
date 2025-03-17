using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsUpdateProducer.Domain.DTOs.EntityDTOs;
using MassTransit;

namespace ContactsUpdateConsumer.Api.Events
{
    public class ContactUpdateConsumer : IConsumer<ContactDto>
    {
        private readonly IContactApplication _contactApplication;
        private Timer _timer;

        public ContactUpdateConsumer(IContactApplication contactApplication)
        {
            _contactApplication = contactApplication;
        }

        public Task Consume(ConsumeContext<ContactDto> context)
        {
            try
            {
                if (context.Message != null)
                {
                    return _contactApplication.UpdateContactAsync(context.Message);
                }
                else
                    throw new Exception("Message cannot be null or empty.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
