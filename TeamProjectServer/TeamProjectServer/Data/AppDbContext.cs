using Microsoft.EntityFrameworkCore;
using TeamProjectServer.Models;

namespace TeamProjectServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WeaponData> weapons { get; set; }
        public DbSet<AccessoryData> accessorys { get; set; }
        public DbSet<ArtifactData> artifacts { get; set; }
        public DbSet<PlayerInitData> playerInits { get; set; }
        public DbSet<SkillData> skills { get; set; }
        public DbSet<StageData> stages { get; set; }
    }
}
