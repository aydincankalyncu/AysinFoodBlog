using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiServiceWebUI.Models
{
    public class FoodViewModel
    {
        public Food Food { get; set; }
        public List<Food> Foods { get; set; }
        public List<FoodCategory> FoodCategories { get; set; }
        public List<FoodComment> FoodComments { get; set; }
        public Info Info { get; set; }
        public FoodComment FoodComment { get; set; }
    }
}
