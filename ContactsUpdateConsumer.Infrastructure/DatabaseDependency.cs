using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.Data;
using FIAP.TechChallenge.ContactsInsertConsumer.Infrastructure.ElasticSearch;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain.Interfaces.ElasticSearch;
using FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure.ElasticSearch;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddRepositoriesDependency(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IContactRepository, ContactRepository>();

            service.Configure<ElasticSettings>(configuration.GetSection("ElasticSettings"));
            service.AddSingleton<IElasticSettings>(sp => sp.GetRequiredService<IOptions<ElasticSettings>>().Value);
            service.AddSingleton(typeof(IElasticClient<>), typeof(ElasticClient<>));

            return service;
        }

        public static IServiceCollection AddDbContextDependency(this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<ContactsDbContext>(options => options.UseMySql(connectionString,
                                                               new MySqlServerVersion(new Version(8, 0, 21)),
                                                               mySqlOptions => mySqlOptions.MigrationsAssembly("FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure")));

            return service;
        }
    }
}
