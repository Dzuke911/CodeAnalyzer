using Microsoft.EntityFrameworkCore;
using System;

namespace CodeAnalyzer.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<FileEntity> Files { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskEntity>().HasKey(t => new { t.FileID, t.StringNumber });
        }
    }
}
