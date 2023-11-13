using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LearnPractice.Models.Database
{
    public class ServicesContext:DbContext
    {
        public DbSet<Services> Services { get; set; } = null!;
        public ServicesContext(DbContextOptions<ServicesContext> options)
            : base(options)
        {

            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
