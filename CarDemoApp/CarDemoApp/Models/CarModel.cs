using SQLite;

namespace CarDemoApp.Models
{

    public class CarModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Line { get; set; }
        public int Year { get; set; }
    }
}
