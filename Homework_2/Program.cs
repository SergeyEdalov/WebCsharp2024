using Autofac;
using Autofac.Extensions.DependencyInjection;
using Homework_2.Abstractions;
using Homework_2.Models.Repositories;
using Homework_2.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Homework_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProFile));
            builder.Services.AddMemoryCache(mc => mc.TrackStatistics = true);

            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new ProductContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddSingleton<IProductRepository, ProductRepository>();

            var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticWorkCache");
            Directory.CreateDirectory(staticFilesPath);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath), 
                RequestPath ="/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
