using LearnPractice.Areas.Identity.Data;
using LearnPractice.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LearnPractice.Models.Database
{
    public class CarsContext:DbContext
    {
        public DbSet<Cars> Cars { get; set; } = null!;
        public CarsContext(DbContextOptions<CarsContext> options)
            : base(options)
        {
           
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        
    }
}
