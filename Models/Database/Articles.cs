using System.ComponentModel.DataAnnotations;

namespace LearnPractice.Models.Database
{
    public class Articles
    {
        public int Id { get; set; }
        [MaxLength(70)]
        public string Title { get; set; }
        [MaxLength(500)]
       public string Description { get; set; }
        [MinLength(40)]
       public string Content { get; set; }

        public string? Author { get; set; }

        public DateTime Date { get; set; }

        public string ImageLink { get; set; }
    }
}
