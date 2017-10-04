using BitcoinShow.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BitcoinShow.Test
{
    public static class DbContextFactory
    {
        public static BitcoinShowDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            return new BitcoinShowDBContext(options);
        }
    }
}
