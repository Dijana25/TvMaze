using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TvMaze.ApiClient;
using TvMaze.DataAccess.Models;
using TvMaze.DataAccess.Repositories;

namespace TvMaze.Scraper.Runner
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services, configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var scraper = serviceProvider.GetRequiredService<TvMazeScraper>();

            //Get from MazeAPI and write into Db
            await scraper.RunAsync();

            serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<TvMazeScraper>();
            services.AddTransient<ITvMazeRepository, TvMazeRepository>();
            services.AddTransient<ITvMazeApiClient, TvMazeApiClient>();

            services.AddHttpClient<TvMazeApiClient>()
                .AddPolicyHandler(RetryPolicyProvider.Get());

            services.AddDbContext<TvMazeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TvMaze"))
            );

            services.AddLogging(builder => builder.AddConsole());
        }
    }
}
