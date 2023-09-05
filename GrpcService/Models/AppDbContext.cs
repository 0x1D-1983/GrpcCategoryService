using System;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Models
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("DbConnection"));
        }

        public DbSet<Category> Categories { get; set; }
    }
}

