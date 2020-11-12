using System;
using Microsoft.Extensions.DependencyInjection;

namespace MMTShop.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddTransient<IDependency, DependencyClass>();
        }
    }
}
