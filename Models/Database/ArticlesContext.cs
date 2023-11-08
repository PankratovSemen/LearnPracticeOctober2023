using Microsoft.EntityFrameworkCore;

namespace LearnPractice.Models.Database
{
    public class ArticlesContext:DbContext
    {
        public DbSet<Articles> Articles { get; set; } = null!;
        public ArticlesContext(DbContextOptions<ArticlesContext> options)
            : base(options)
        {
            
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
