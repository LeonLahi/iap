using Microsoft.EntityFrameworkCore;
using iap.API.Models;

namespace iap.API.Data
{
    public class IapDbContext : DbContext
    {
        public IapDbContext(DbContextOptions<IapDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<TrackGenre> TrackGenres { get; set; }
        public DbSet<ListeningSession> ListeningSessions { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Filter out soft-deleted records
            modelBuilder.Entity<Playlist>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Track>()
                .HasQueryFilter(t => !t.IsDeleted);

            // Composite primary keys for join tables
            modelBuilder.Entity<PlaylistTrack>()
                .HasKey(pt => new { pt.PlaylistId, pt.TrackId });

            modelBuilder.Entity<TrackGenre>()
                .HasKey(tg => new { tg.TrackId, tg.GenreId });

            // Self referencing relationship for Playlist folders
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Fix cascade cycles
            modelBuilder.Entity<ListeningSession>()
                .HasOne(ls => ls.User)
                .WithMany(u => u.ListeningSessions)
                .HasForeignKey(ls => ls.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ListeningSession>()
                .HasOne(ls => ls.Track)
                .WithMany(t => t.ListeningSessions)
                .HasForeignKey(ls => ls.TrackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlaylistTrack>()
                .HasOne(pt => pt.Track)
                .WithMany(t => t.PlaylistTracks)
                .HasForeignKey(pt => pt.TrackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrackGenre>()
                .HasOne(tg => tg.Track)
                .WithMany(t => t.TrackGenres)
                .HasForeignKey(tg => tg.TrackId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.Track)
                .WithMany(t => t.Chapters)
                .HasForeignKey(c => c.TrackId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}