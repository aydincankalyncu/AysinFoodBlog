using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiServiceWebUI.Models
{
    public class CategoryViewModel
    {
        public List<Food> Foods { get; set; }
        public Info Info { get; set; }
        public List<FoodCategory>  FoodCategories {get; set;}
    }
}
