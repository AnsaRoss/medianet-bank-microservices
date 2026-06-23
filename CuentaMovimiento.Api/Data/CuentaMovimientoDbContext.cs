using CuentaMovimiento.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuentaMovimiento.Api.Data
{
    public class CuentaMovimientoDbContext : DbContext
    {
        public CuentaMovimientoDbContext(DbContextOptions<CuentaMovimientoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cuenta>()
                .HasIndex(c => c.NumeroCuenta)
                .IsUnique();

            modelBuilder.Entity<Movimiento>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Movimiento>()
                .HasOne<Cuenta>()
                .WithMany()
                .HasForeignKey(m => m.CuentaId);
            
            modelBuilder.Entity<Cuenta>()
            .Property(c => c.SaldoInicial)
            .HasPrecision(18, 2);

            modelBuilder.Entity<Cuenta>()
                .Property(c => c.SaldoActual)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Saldo)
                .HasPrecision(18, 2);
        }
    }
}