using desafio_catalogo_jogos.Models;
using desafio_catalogo_jogos.Repositories;
using desafio_catalogo_jogos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace desafio_catalogo_jogos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conn = "server=127.0.0.1;port=3306;database=catalogo_jogos;uid=root;pwd=1234";
            services.AddDbContext<ProducerContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));
            services.AddDbContext<GameContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameRepository, GameMySQLRepository>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IProducerRepository, ProducerMySQLRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio - Catálogo de Jogo", Version = "V1" });

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                c.IncludeXmlComments(Path.Combine(basePath, fileName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GameContext gameContext, ProducerContext producerContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "desafio_catalogo_jogos v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            InitializeDBData.Initialize(gameContext, producerContext);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}