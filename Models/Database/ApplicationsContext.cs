using Microsoft.EntityFrameworkCore;

namespace LearnPractice.Models.Database
{
    public class ApplicationsContext:DbContext
    {
        public DbSet<Applications> Applications { get; set; } = null!;
        public ApplicationsContext(DbContextOptions<ApplicationsContext> options)
            : base(options)
        {

            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
