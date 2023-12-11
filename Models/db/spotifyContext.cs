using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Spotify.Models.db
{
    public partial class spotifyContext : DbContext
    {
        public spotifyContext()
        {
        }

        public spotifyContext(DbContextOptions<spotifyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cancion> Cancions { get; set; } = null!;
        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //              optionsBuilder.UseMySql("server=localhost;port=3306;database=spotify;uid=root;password=zatt01", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Cancion>(entity =>
            {
                entity.ToTable("cancion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Album).HasMaxLength(255);

                entity.Property(e => e.Genero).HasMaxLength(255);

                entity.Property(e => e.Interprete).HasMaxLength(255);

                entity.Property(e => e.Portada).HasMaxLength(255);

                entity.Property(e => e.Titulo).HasMaxLength(255);

                entity.HasMany(d => d.Idplaylists)
                    .WithMany(p => p.Idcancions)
                    .UsingEntity<Dictionary<string, object>>(
                        "Cancionplaylist",
                        l => l.HasOne<Playlist>().WithMany().HasForeignKey("Idplaylist").HasConstraintName("cancionplaylist_ibfk_2"),
                        r => r.HasOne<Cancion>().WithMany().HasForeignKey("Idcancion").HasConstraintName("cancionplaylist_ibfk_1"),
                        j =>
                        {
                            j.HasKey("Idcancion", "Idplaylist").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("cancionplaylist");

                            j.HasIndex(new[] { "Idplaylist" }, "idplaylist");

                            j.IndexerProperty<int>("Idcancion").HasColumnName("idcancion");

                            j.IndexerProperty<int>("Idplaylist").HasColumnName("idplaylist");
                        });
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("playlist");

                entity.HasIndex(e => e.Idusuario, "idusuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasColumnName("descripcion");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("playlist_ibfk_1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
