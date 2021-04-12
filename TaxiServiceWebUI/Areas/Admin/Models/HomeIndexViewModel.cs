using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiServiceWebUI.Areas.Admin.Models
{
    public class HomeIndexViewModel
    {
        public List<Food> Foods { get; set; }
        public List<FoodCategory> FoodCategories { get; set; }
        public List<Info> Infos { get; set; }
    }
}
