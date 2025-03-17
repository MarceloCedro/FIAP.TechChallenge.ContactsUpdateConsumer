﻿using FIAP.TechChallenge.ContactsUpdateConsumer.Application;
using FIAP.TechChallenge.ContactsUpdateConsumer.Domain;
using FIAP.TechChallenge.ContactsUpdateConsumer.Infrastructure;

namespace ContactsUpdateConsumer.Api.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoriesDependency();
            services.AddDbContextDependency(connectionString);
            services.AddServicesDependency();
            services.AddApplicationDependency();
        }
    }
}
