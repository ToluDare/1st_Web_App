using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class Application_db_context : DbContext
    {
        public Application_db_context(DbContextOptions<Application_db_context> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Action",
                    DisplayOrder = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "SciFi",
                    DisplayOrder = 2
                },
                new Category
                {
                    Id = 3,
                    Name = "Horro",
                    DisplayOrder = 3
                }

                );
        }
    }

}
