using Microsoft.EntityFrameworkCore;
using BlogTracker.Models;
using APPUILayer.Models;

namespace APPUILayer.Data
{
    public class APPUIDbContext : DbContext
    {
        public APPUIDbContext (DbContextOptions<APPUIDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminInfo> AdminInfo { get; set; } = default!;
        public DbSet<EmpInfo> EmpInfo { get; set; } = default!;
        public DbSet<BlogInfo>? BlogInfo { get; set; }
    }
}
