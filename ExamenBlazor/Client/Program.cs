using ExamenBlazor.Client.Helpers;
using ExamenBlazor.Client.Repositorios;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExamenBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //llamar al metodo ConfigureServices
            ConfigureServices(builder.Services);
            await builder.Build().RunAsync();
        }

        //configurar el sistema de inyeccion de dependencias del lado del cliente blazor
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();//configurar el sistema de autorizacion
            services.AddSingleton<ServiciosSingleton>();
            services.AddTransient<ServicioTransient>();

            //poner la interface y el servicio de repositorios
            services.AddScoped<IRepositorio, Repositorio>();

            //ponemos la instancia a servir cuando se nos pida un IMostrarMensajes 
            services.AddScoped<IMostrarMensaje, MostrarMensajes>();          
        }
    }
}
