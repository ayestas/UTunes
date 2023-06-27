using Microsoft.EntityFrameworkCore;
using UTunes.Core.Entities;
using UTunes.Infrastructure.Database.DatabaseConfiguration;

namespace UTunes.Infrastructure.Database;
public class UTunesContext : DbContext
{
    public UTunesContext(DbContextOptions<UTunesContext> options)
        : base(options)
    {

    }

    public DbSet<Album> Album { get; set; }
    public DbSet<Song> Song { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlbumEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SongEntityConfiguration());
    }
}

