using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Transacao.API.Configurations
{
    public static class SwaggerDocConfig
    {
        public static IServiceCollection ConfigDocumentacaoComSwagger(this IServiceCollection services)
        {
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Transacao Financeira API",
                        Version = "v1",
                        Description = "API responsável pelas transações financeiras [ LAB ] - criada com o ASP.NET Core 2.2",
                        Contact = new Contact
                        {
                            Name = "Vagner Alcantara",
                            Url = "https://github.com/VagnerAlcantara"
                        }
                    });
            });

            return services;
        }
    }
}
