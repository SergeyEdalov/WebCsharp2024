using Homework_4._1.Abstractions;
using Homework_4._1.Mapper;
using Homework_4._1.Services;
using Homework_4._1.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Homework_4._1.Mutation;
using Autofac.Extensions.DependencyInjection;
using Autofac;

namespace Homework_4._1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
       
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            //builder.Services.AddPooledDbContextFactory<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("db")));

            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IStorageService, StorageService>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();

            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new AppDbContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddGraphQLServer().AddQueryType<MyQuery>()
                .AddMutationType<MyMutation>();

            var app = builder.Build();

            app.MapGraphQL();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();
        }
    }
}
