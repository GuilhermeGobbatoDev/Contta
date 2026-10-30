using Contta.Models;
using Microsoft.EntityFrameworkCore;


namespace Contta.Infrastructure.Data

{
    public class ConttaDbContext : DbContext
    {
        public ConttaDbContext (DbContextOptions<ConttaDbContext> options) : base(options)
        {
            
        }

        public DbSet<Despesas> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
           modelBuilder.Entity<Despesas>().Property(despesa => despesa.Data).HasColumnType("date");

        }
    }
}
