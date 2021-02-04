using System;
using AutoMapper;
using BeetleX.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrintService.Api;
using PrintService.Infrastructure.Redis;

namespace PrintService
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
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddTransient<RedisDB>(provider =>
            {
                var db = new RedisDB();
                var redisHost = Configuration.GetValue<string>("RedisHost");
                db.Host.AddWriteHost(redisHost);
                return db;
            });

            services.AddTransient<ILockRegistry, RedisLockRegistry>();
            services.AddTransient(typeof(IRedisQueue<>), typeof(RedisQueue<>));
            services.AddTransient(typeof(IRedisSequence<>), typeof(RedisSequence<>));

            services.AddAutoMapper(cfg=>cfg.AddProfile(typeof(MapperProfile)),
                AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
