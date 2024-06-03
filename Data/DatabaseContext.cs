using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NeoImobSystem_API.Model;

namespace NeoImobSystem_API.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Casa> Casas { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Inquilino> Inquilinos { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // One to Many - Usuario -> Entidades

            modelBuilder.Entity<Casa>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Casas)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Contratos)
                .HasForeignKey(c => c.UsuarioId);

            //modelBuilder.Entity<Contrato>()
            //    .HasOne(c => c.Casa)
            //    .WithOne(c => c.Contrato)
            //    .HasForeignKey<Casa>(c => c.ContratoId);

            modelBuilder.Entity<Inquilino>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Inquilinos)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Proprietario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Proprietarios)
                .HasForeignKey(c => c.UsuarioId);

        }
    }
}
