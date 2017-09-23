using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BitcoinShow.Web.Models
{
    
    public class BitcoinShowDBContext : DbContext
    {
        public BitcoinShowDBContext(DbContextOptions<BitcoinShowDBContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}