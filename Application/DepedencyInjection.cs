using Application.Repository;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IProdutoService, ProdutoService>();

            services.AddSingleton<IClienteRepository, ClienteRepository>();
            services.AddSingleton<IProdutoRepository, ProdutoRepository>();
                
            return services;

        }
    }
}
