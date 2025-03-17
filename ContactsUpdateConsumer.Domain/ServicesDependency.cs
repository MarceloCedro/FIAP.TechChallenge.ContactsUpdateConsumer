using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Domain
{
    public static class ServicesDependency
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactService, ContactService>();

            return service;
        }
    }
}
