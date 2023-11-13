using System.ComponentModel.DataAnnotations;

namespace LearnPractice.Models.Database
{
    public class Services
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int IdCar { get; set; }
        public string LoginUser { get; set; }
        [MaxLength(500)]
        public string DescriptionFail { get; set; }
        public string Status { get; set; }
        public string? Message { get; set; } 
    }
}
