using Microsoft.EntityFrameworkCore;

namespace HumansInHarmony.Models
{
    public class SongContext : DbContext
    {
        public DbSet<SongInfo> SongInfo { get; set; }
        public DbSet<LikedSongs> LikedSongs { get; set; }
        public DbSet<DislikedSongs> DislikedSongs { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Secret.ConnectionUrl);
        }
    }
}
