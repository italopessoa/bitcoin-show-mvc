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
        
        #region Sets
        
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        
        #endregion

    }
}