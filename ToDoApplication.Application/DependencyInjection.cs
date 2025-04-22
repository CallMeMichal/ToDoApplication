using Microsoft.Extensions.DependencyInjection;
using ToDoApplication.Application.Service;
using ToDoApplication.Common.Interfaces;
using ToDoApplication.Infrastructure.Repositories;

namespace ToDoApplication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IToDoRepostiory,ToDoRepository>();
            services.AddScoped<IToDoService,ToDoService>();
            return services;
        }
    }
}
