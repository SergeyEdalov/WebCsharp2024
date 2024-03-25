using Homework_3.Abstractions;
using Homework_3.Mapper;
using Homework_3.Services;
using Homework_3.Query;
using Microsoft.EntityFrameworkCore;
using Homework_3.Mutation;
using Autofac.Extensions.DependencyInjection;
using Autofac;

namespace Homework_3
{
    //Добавьте отдельный сервис позволяющий хранить информацию о товарах на складе/магазине.
    //Реализуйте к нему доступ посредством API и GraphQL.
    //Реализуйте API-Gateway для API сервиса склада и API-сервиса из второй лекции.
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
            builder.Services.AddSingleton<IStorageInfo, StorageInfo>();


            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new AppDbContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });
            //builder.Services.AddDbContext<AppDbContext>(conf => conf.UseNpgsql(builder.Configuration.GetConnectionString("db")));

            //builder.Services.UseNpgsql(connectionString);
            builder.Services.AddGraphQLServer()
                .AddQueryType<MyQuery>()
                .AddMutationType<MyMutation>();

            var app = builder.Build();

            app.MapGraphQL();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();
        }
    }
}
