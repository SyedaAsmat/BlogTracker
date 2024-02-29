using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppServiceLayer.Models;

namespace AppServiceLayer.Data
{
    public class AppServiceLayerDbContext : DbContext
    {
        public AppServiceLayerDbContext (DbContextOptions<AppServiceLayerDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmpInfo> EmpInfo { get; set; } = default!;

        public DbSet<BlogInfo>? BlogInfo { get; set; }
    }
}
