using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiServiceWebUI.Models
{
    public class FoodListtViewModel
    {
        public List<Food> Foods{ get; set; }
        public List<Food> LastThreeFoods { get; set; }
    }
}
