using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BitcoinShow.Web.Models
{
    
    public class BitcoinShowDBContext : DbContext
    {
        public BitcoinShowDBContext(DbContextOptions<BitcoinShowDBContext> options) : base(options)
        {
            QuestionLevelEnum.GetValues(typeof(QuestionLevelEnum))
                               .Cast<object>()
                               .Select(value => (QuestionLevelEnum)value)
                               .ToList()
                               .ForEach(instance => QuestionLevels.Add(new QuestionLevel() {
                                   Id = (int)instance,
                                   Name = instance.ToString(), 
                                   Description=instance.GetEnumDescription() }));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionOption>()
                .HasOne<Question>(o=>o.Question)
                .WithMany(q=>q.QuestionOptions);

            modelBuilder.Entity<Question>()
            .Property(p=>p.AnswerId).IsRequired();
            modelBuilder.Entity<Question>()
            .Property(p=>p.Title).IsRequired();
        }

        

        /// <summary>
        /// Set of Questions
        /// </summary>
        public DbSet<Question> Questions { get; set; }

        /// <summary>
        /// Set of QuestionOptions
        /// </summary>
        public DbSet<QuestionOption> Options { get; set; }

        /// <summary>
        /// Set of QuestionLevel
        /// </summary>
        public DbSet<QuestionLevel> QuestionLevels { get; set; }

        /// <summary>
        /// Set of Users
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// Set of Awards
        /// </summary>
        public DbSet<Award> Awards { get; set; }
    }
}