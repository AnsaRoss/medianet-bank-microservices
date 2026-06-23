using ClientePersona.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientePersona.Api.Data
{
    public class ClientePersonaDbContext : DbContext
    {
        public ClientePersonaDbContext(DbContextOptions<ClientePersonaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.ClienteId)
                .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Identificacion)
                .IsUnique();
        }
    }
}