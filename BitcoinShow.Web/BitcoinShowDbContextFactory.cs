using System.IO;
using BitcoinShow.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BitcoinShow.Web
{
    public class BitcoinShowDbContextFactory : IDesignTimeDbContextFactory<BitcoinShowDBContext>
    {
        public BitcoinShowDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BitcoinShowDBContext>();

            var connectionString = configuration.GetConnectionString("SqlServer");
            builder.UseSqlServer(connectionString);

            return new BitcoinShowDBContext(builder.Options);
        }
    }
}