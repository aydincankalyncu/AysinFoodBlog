using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using TaxiServiceWebUI.Areas.Admin.Models;
using TaxiServiceWebUI.Models;

namespace TaxiServiceWebUI.Controllers
{
    public class HomeController : Controller
    {
        private IFoodService _foodService;
        public HomeController(IFoodService foodService)
        {
            _foodService = foodService;
        }
        public IActionResult Index()
        {
            var lastThreeFoods = _foodService.GetFoodList().TakeLast(3).ToList();
            var foods = _foodService.GetFoodList();
            FoodListtViewModel model = new FoodListtViewModel
            {
                Foods = foods,
                LastThreeFoods = lastThreeFoods
            };
            return View(model);
        }
    }
}
