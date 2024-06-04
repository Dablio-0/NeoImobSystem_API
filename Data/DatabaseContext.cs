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

        public DbSet<CasaProprietario> CasaProprietarios { get; set; }
        public DbSet<ContratoInquilino> ContratoInquilinos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region One to One - Casa <-> Contrato
            modelBuilder.Entity<Casa>()
                .HasOne(c => c.Contrato)
                .WithOne(c => c.Casa)
                .HasForeignKey<Casa>(c => c.ContratoId);
            #endregion

            #region One to Many - Usuario -> Entidades (Casa, Contrato, Inquilino, Proprietario)
            modelBuilder.Entity<Casa>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Casas)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Contratos)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Inquilino>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Inquilinos)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Proprietario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Proprietarios)
                .HasForeignKey(c => c.UsuarioId);
            #endregion

            #region Many to Many - Casa <-> Proprietario
            modelBuilder.Entity<CasaProprietario>()
                .HasKey(cp => cp.Id);

            modelBuilder.Entity<CasaProprietario>()
                .HasOne(cp => cp.Casa)
                .WithMany(c => c.CasaProprietarios)
                .HasForeignKey(cp => cp.CasaId);

            modelBuilder.Entity<CasaProprietario>()
                .HasOne(cp => cp.Proprietario)
                .WithMany(c => c.CasaProprietarios)
                .HasForeignKey(cp => cp.ProprietarioId);
            #endregion

            #region Many to Many - Contrato <-> Inquilino
            modelBuilder.Entity<ContratoInquilino>()
                .HasKey(cp => cp.Id);

            modelBuilder.Entity<ContratoInquilino>()
                .HasOne(cp => cp.Contrato)
                .WithMany(c => c.ContratoInquilinos)
                .HasForeignKey(cp => cp.ContratoId);

            modelBuilder.Entity<ContratoInquilino>()
                .HasOne(cp => cp.Inquilino)
                .WithMany(c => c.ContratoInquilinos)
                .HasForeignKey(cp => cp.InquilinoId);
            #endregion

        }
    }
}
