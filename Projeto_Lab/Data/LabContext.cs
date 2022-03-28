using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Projeto_Lab.Models;

#nullable disable

namespace Projeto_Lab.Data
{
    public partial class LabContext : DbContext
    {
        public LabContext()
        {
        }

        public LabContext(DbContextOptions<LabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ListaFavPrato> ListaFavPratos { get; set; }
        public virtual DbSet<ListaFavRestaurante> ListaFavRestaurantes { get; set; }
        public virtual DbSet<Prato> Pratos { get; set; }
        public virtual DbSet<PratoDium> PratoDia { get; set; }
        public virtual DbSet<RestTipo> RestTipos { get; set; }
        public virtual DbSet<Restaurante> Restaurantes { get; set; }
        public virtual DbSet<Suspenso> Suspensos { get; set; }
        public virtual DbSet<TipoServico> TipoServicos { get; set; }
        public virtual DbSet<Utilizador> Utilizadors { get; set; }
        public virtual DbSet<Notificaçao> Notificaçao { get; set; } 
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        if (!optionsBuilder.IsConfigured)
    //        {
    //            optionsBuilder.UseSqlServer("name=LabContext");
    //        }
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

    //        modelBuilder.Entity<Administrador>(entity =>
    //        {
    //            entity.HasKey(e => e.IdA)
    //                .HasName("PK__Administ__B770B52D54880F20");

    //            entity.Property(e => e.IdA).ValueGeneratedNever();

    //            entity.HasOne(d => d.CriadoPorNavigation)
    //                .WithMany(p => p.InverseCriadoPorNavigation)
    //                .HasForeignKey(d => d.CriadoPor)
    //                .HasConstraintName("FK__Administr__criad__3B75D760");

    //            entity.HasOne(d => d.IdANavigation)
    //                .WithOne(p => p.Administrador)
    //                .HasForeignKey<Administrador>(d => d.IdA)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Administra__Id_A__3A81B327");
    //        });

    //        modelBuilder.Entity<AspNetRole>(entity =>
    //        {
    //            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
    //                .IsUnique()
    //                .HasFilter("([NormalizedName] IS NOT NULL)");
    //        });

    //        modelBuilder.Entity<AspNetUser>(entity =>
    //        {
    //            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
    //                .IsUnique()
    //                .HasFilter("([NormalizedUserName] IS NOT NULL)");
    //        });

    //        modelBuilder.Entity<AspNetUserLogin>(entity =>
    //        {
    //            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
    //        });

    //        modelBuilder.Entity<AspNetUserRole>(entity =>
    //        {
    //            entity.HasKey(e => new { e.UserId, e.RoleId });
    //        });

    //        modelBuilder.Entity<AspNetUserToken>(entity =>
    //        {
    //            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
    //        });

    //        modelBuilder.Entity<Cliente>(entity =>
    //        {
    //            entity.HasKey(e => e.IdC)
    //                .HasName("PK__Cliente__B770B52B424C66C2");

    //            entity.Property(e => e.IdC).ValueGeneratedNever();

    //            entity.Property(e => e.Nome).IsUnicode(false);

    //            entity.HasOne(d => d.IdCNavigation)
    //                .WithOne(p => p.Cliente)
    //                .HasForeignKey<Cliente>(d => d.IdC)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Cliente__Id_C__3F466844");

    //            entity.HasOne(d => d.SuspensoPorNavigation)
    //                .WithMany(p => p.Clientes)
    //                .HasForeignKey(d => d.SuspensoPor)
    //                .HasConstraintName("FK__Cliente__Suspens__3E52440B");
    //        });

    //        modelBuilder.Entity<ListaFavPrato>(entity =>
    //        {
    //            entity.HasKey(e => new { e.IdCl, e.PratoId })
    //                .HasName("PK__Lista_Fa__785D855E018F9B8D");

    //            entity.Property(e => e.Notifica).HasDefaultValueSql("((0))");

    //            entity.HasOne(d => d.IdClNavigation)
    //                .WithMany(p => p.ListaFavPratos)
    //                .HasForeignKey(d => d.IdCl)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Lista_Fav__Id_Cl__68487DD7");

    //            entity.HasOne(d => d.Prato)
    //                .WithMany(p => p.ListaFavPratos)
    //                .HasForeignKey(d => d.PratoId)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Lista_Fav__Prato__693CA210");
    //        });

    //        modelBuilder.Entity<ListaFavRestaurante>(entity =>
    //        {
    //            entity.HasKey(e => new { e.IdR, e.IdC })
    //                .HasName("PK__Lista_Fa__1C07BE4C7A15822B");

    //            entity.Property(e => e.Notifica).HasDefaultValueSql("((0))");

    //            entity.HasOne(d => d.IdCNavigation)
    //                .WithMany(p => p.ListaFavRestaurantes)
    //                .HasForeignKey(d => d.IdC)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Lista_Fav___Id_c__6D0D32F4");

    //            entity.HasOne(d => d.IdRNavigation)
    //                .WithMany(p => p.ListaFavRestaurantes)
    //                .HasForeignKey(d => d.IdR)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Lista_Fav___Id_r__6E01572D");
    //        });

    //        modelBuilder.Entity<Prato>(entity =>
    //        {
    //            entity.HasKey(e => e.IdP)
    //                .HasName("PK__Prato__B770B53E7B70725D");

    //            entity.Property(e => e.Nome).IsUnicode(false);

    //            entity.Property(e => e.Tipo).IsUnicode(false);
    //        });

    //        modelBuilder.Entity<PratoDium>(entity =>
    //        {
    //            entity.HasKey(e => new { e.IdPd, e.IdR })
    //                .HasName("PK__Prato_Di__CD9B73FEB93D5530");

    //            entity.Property(e => e.Descricao).IsUnicode(false);

    //            entity.Property(e => e.Foto).IsUnicode(false);

    //            entity.Property(e => e.Tipo).IsUnicode(false);

    //            entity.HasOne(d => d.IdPdNavigation)
    //                .WithMany(p => p.PratoDia)
    //                .HasForeignKey(d => d.IdPd)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Prato_Dia__Id_Pd__48CFD27E");

    //            entity.HasOne(d => d.IdPd1)
    //                .WithMany(p => p.PratoDia)
    //                .HasForeignKey(d => d.IdPd)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Prato_Dia__Id_Pd__47DBAE45");
    //        });

    //        modelBuilder.Entity<RestTipo>(entity =>
    //        {
    //            entity.HasKey(e => new { e.IdR, e.IdTp })
    //                .HasName("PK__Rest_Tip__E61E74BE6532DB1B");

    //            entity.HasOne(d => d.IdRNavigation)
    //                .WithMany(p => p.RestTipos)
    //                .HasForeignKey(d => d.IdR)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Rest_Tipo__Id_R__59FA5E80");

    //            entity.HasOne(d => d.IdTpNavigation)
    //                .WithMany(p => p.RestTipos)
    //                .HasForeignKey(d => d.IdTp)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Rest_Tipo__Id_Tp__5AEE82B9");
    //        });

    //        modelBuilder.Entity<Restaurante>(entity =>
    //        {
    //            entity.HasKey(e => e.IdR)
    //                .HasName("PK__Restaura__B770B53C885111A6");

    //            entity.Property(e => e.IdR).ValueGeneratedNever();

    //            entity.Property(e => e.Foto).IsUnicode(false);

    //            entity.Property(e => e.Localizacao).IsUnicode(false);

    //            entity.Property(e => e.Nome).IsUnicode(false);

    //            entity.Property(e => e.TipoServico).IsUnicode(false);

    //            entity.HasOne(d => d.IdRNavigation)
    //                .WithOne(p => p.Restaurante)
    //                .HasForeignKey<Restaurante>(d => d.IdR)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Restaurant__Id_R__4316F928");

    //            entity.HasOne(d => d.RegistadoPorNavigation)
    //                .WithMany(p => p.Restaurantes)
    //                .HasForeignKey(d => d.RegistadoPor)
    //                .HasConstraintName("FK__Restauran__Regis__4222D4EF");
    //        });

    //        modelBuilder.Entity<Suspenso>(entity =>
    //        {
    //            entity.HasKey(e => new { e.IdU, e.IdAdm })
    //                .HasName("PK__Suspenso__5269C2FD13E0F2F6");

    //            entity.Property(e => e.DataBloqueio).HasDefaultValueSql("(getdate())");

    //            entity.Property(e => e.Motivo).IsUnicode(false);

    //            entity.HasOne(d => d.IdAdmNavigation)
    //                .WithMany(p => p.Suspensos)
    //                .HasForeignKey(d => d.IdAdm)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Suspenso__Id_Adm__5535A963");

    //            entity.HasOne(d => d.IdUNavigation)
    //                .WithMany(p => p.Suspensos)
    //                .HasForeignKey(d => d.IdU)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Suspenso__Id_U__5441852A");
    //        });

    //        modelBuilder.Entity<TipoServico>(entity =>
    //        {
    //            entity.HasKey(e => e.IdTp)
    //                .HasName("PK__Tipo_Ser__16EC182499238ACA");

    //            entity.Property(e => e.Nome).IsUnicode(false);
    //        });

    //        modelBuilder.Entity<Utilizador>(entity =>
    //        {
    //            entity.Property(e => e.Email).IsUnicode(false);

    //            entity.Property(e => e.Pass).IsUnicode(false);

    //            entity.Property(e => e.Username).IsUnicode(false);
    //        });

    //        OnModelCreatingPartial(modelBuilder);
    //    }

    //    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
