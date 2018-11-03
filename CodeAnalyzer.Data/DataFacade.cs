using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAnalyzer.Data
{
    public class DataFacade
    {
        public static IConfiguration Configuration { get; set; }

        public static void InitDatabase(string connectionString = null)
        {
            if (connectionString == null)
            {
                connectionString = Configuration.GetConnectionString("Database");
            }

            DbContextOptionsBuilder<DatabaseContext> contextOptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            contextOptionsBuilder.UseSqlServer(connectionString);

            using (DatabaseContext context = new DatabaseContext(contextOptionsBuilder.Options))
            {
                context.Database.Migrate();
            }
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));
        }
    }
}
