using System.ComponentModel.DataAnnotations;

namespace QuickHelpers.DbTest
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        
        public string Model { get; set; }
        
        public int Price { get; set; }
    }
}