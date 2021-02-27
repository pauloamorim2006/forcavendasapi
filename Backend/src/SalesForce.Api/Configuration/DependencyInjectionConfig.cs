using ERP.Api.Extensions;
using ERP.Business.Intefaces;
using ERP.Business.Notificacoes;
using ERP.Business.Services;
using ERP.Data.Context;
using ERP.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ERP.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<SalesForceDbContext>();            
            services.AddScoped<IUnidadeRepository, UnidadeRepository>();
            services.AddScoped<IProdutoServicoRepository, ProdutoServicoRepository>();            
            services.AddScoped<ICidadeRepository, CidadeRepository>();                        
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddScoped<ICondicaoPagamentoRepository, CondicaoPagamentoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();            
            services.AddScoped<IPedidoRepository, PedidoRepository>();            

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUnidadeService, UnidadeService>();
            services.AddScoped<IProdutoServicoService, ProdutoServicoService>();            
            services.AddScoped<ICidadeService, CidadeService>();                        
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IFormaPagamentoService, FormaPagamentoService>();
            services.AddScoped<ICondicaoPagamentoService, CondicaoPagamentoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IPedidoService, PedidoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}