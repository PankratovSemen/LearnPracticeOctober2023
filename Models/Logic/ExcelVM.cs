using LearnPractice.Models.Database;

namespace LearnPractice.Models.Logic
{
    public class ExcelVM
    {
        public ExcelVM()
        {
            CarsL = new List<Cars>();
        }

        public List<Cars> CarsL { get; set; }

        public int ErrorTotal { get; set; }
    }
}
