using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PizzaPlace.Shared;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using PizzaPlace.Client.Services;

namespace PizzaPlace.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddTransient<IMenuService, MenuService>();
            builder.Services.AddTransient<IOrderService, ConsoleOrderService>();
            await builder.Build().RunAsync();
        }
    }
}