using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TaxiServiceWebUI.Areas.Admin.Models;

namespace TaxiServiceWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IFoodCategoryService _categoryService;
        private readonly IInfoService _infoService;
        public HomeController(IFoodService foodService, IFoodCategoryService categoryService,
                              IInfoService infoService)
        {
            _foodService = foodService;
            _categoryService = categoryService;
            _infoService = infoService;
        }
       
        public IActionResult Index()
        {
            var foodsInfo = _foodService.GetFoodList();
            var categoriesInfo = _categoryService.GetFoodCategoryList();
            var userInfo = _infoService.GetInfo();
            HomeIndexViewModel model = new HomeIndexViewModel
            {
                Foods = foodsInfo,
                FoodCategories = categoriesInfo,
                Infos = userInfo
            };
            return View(model);
        }

        
       
    }
}
