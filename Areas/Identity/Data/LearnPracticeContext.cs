using LearnPractice.Areas.Identity.Data;
using LearnPractice.Models.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LearnPractice.Data;

public class LearnPracticeContext : IdentityDbContext<LearnPracticeUser>
{
    public LearnPracticeContext(DbContextOptions<LearnPracticeContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

       
    }
}
