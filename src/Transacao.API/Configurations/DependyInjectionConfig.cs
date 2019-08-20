using Microsoft.Extensions.DependencyInjection;
using Transacao.Application;
using Transacao.Application.Interfaces;
using Transacao.Domain.Interfaces;
using Transacao.Domain.Interfaces.Repositories;
using Transacao.Domain.Interfaces.Services;
using Transacao.Domain.Notifications;
using Transacao.Domain.Services;
using Transacao.Infra.Data.Context;
using Transacao.Infra.Data.Repositories;

namespace Transacao.API.Configurations
{
    public static class DependyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<TransacaoContext>();

            services.AddScoped<INotificador, Notificador>();

            //Application
            services.AddScoped<IContaAppService, ContaAppService>();
            services.AddScoped<ITransacaoAppService, TransacaoAppService>();

            //Service
            services.AddScoped<IContaService, ContaService>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            //Repository
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();

            return services;
        }
    }
}
