using ShelfLife.Models;
using System.Collections.Generic;
namespace ShelfLife.ViewModels
{
    public class FoodItemViewModel
    {
        public List<FoodItem> foodItems { get; set; } = new List<FoodItem>();
        public int FoodItemIdToDelete;

    }
}
