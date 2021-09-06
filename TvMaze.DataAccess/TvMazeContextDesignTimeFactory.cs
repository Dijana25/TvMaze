using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TvMaze.DataAccess.Models;

namespace TvMaze.DataAccess
{
    public class TvMazeContextDesignTimeFactory : IDesignTimeDbContextFactory<TvMazeContext>
    {
        public TvMazeContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();            

            var optionsBuilder = new DbContextOptionsBuilder<TvMazeContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("TvMaze"));

            return new TvMazeContext(optionsBuilder.Options);
        }
    }
}
