using TeamProjectServer.Models;
using Microsoft.EntityFrameworkCore;

namespace TeamProjectServer.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //서버 데이터 테이블

        public DbSet<UserData> Users { get; set; }
    }
}
