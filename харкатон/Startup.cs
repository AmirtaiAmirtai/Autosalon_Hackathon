using Microsoft.OpenApi.Models;

namespace харкатон;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // ... другие настройки ...

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
            });


        }

    public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
    {
        // ... другие настройки ...

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toy Store API V1");
            });

        }
    }
}