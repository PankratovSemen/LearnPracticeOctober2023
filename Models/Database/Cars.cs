using LearnPractice.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearnPractice.Models.Database
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string IdPts { get; set; }
        public string IdSts { get; set; }
        public int Sum { get; set; }
        public int MilHour { get; set; }
        public string? Preview { get; set; }
       
        
        public  string? UserId { get; set; }

    }
}
