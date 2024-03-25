using Ocelot.DependencyInjection;

namespace Homework_3_Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("Homework_3_ocelot.json")
                .Build();

            builder.Services.AddOcelot(configuration);

            var app = builder.Build();

            app.Run();
        }
    }
}
