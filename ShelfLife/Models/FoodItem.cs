using System;
using MySql.Data.MySqlClient;
namespace ShelfLife.Models
{
    public class FoodItem
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpired { get; set; }
        public int DaysRemaining { get; set; }
        public int ShelfLife { get; set; }
    }
}
