using Contta.Infrastructure.Identity;
using Contta.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Contta.Infrastructure.Data

{
    public class ConttaDbContext : IdentityDbContext<ApplicationUser>
    {
        public ConttaDbContext (DbContextOptions<ConttaDbContext> options) : base(options)
        {
            
        }

        public DbSet<Despesas> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Despesas>().Property(despesa => despesa.Data).HasColumnType("date");

            modelBuilder.Entity<Despesas>().HasOne<ApplicationUser>().WithMany().HasForeignKey(despesa => despesa.UsuarioId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
