using FIAP.TechChallenge.ContactsUpdateConsumer.Application.Applications;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Applications;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Application
{
    public static class ApplicationDependency
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactApplication, ContactApplication>();

            return service;
        }
    }
}
