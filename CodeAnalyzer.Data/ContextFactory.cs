using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAnalyzer.Data
{
    class ContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DatabaseContext> contextOptionBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            contextOptionBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CodeAnalyzerDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new DatabaseContext(contextOptionBuilder.Options);
        }
    }
}
