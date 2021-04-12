using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using TaxiServiceWebUI.Models;

namespace TaxiServiceWebUI.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IFoodCategoryService _foodCategoryService;
        private readonly IFoodCommentService _commentService;
        private readonly IInfoService _infoService;
        public FoodController(IFoodService foodService, 
                              IFoodCategoryService foodCategoryService,
                              IFoodCommentService commentService,
                              IInfoService infoService)
        {
            _foodService = foodService;
            _foodCategoryService = foodCategoryService;
            _commentService = commentService;
            _infoService = infoService;
        }

        public IActionResult GetFood(int foodId)
        {
            var food = _foodService.GetFoodById(foodId);
            var foodList = _foodService.GetFoodList().Take(3);
            var foodCategoryList = _foodCategoryService.GetFoodCategoryList();
            var commentList = _commentService.GetFoodComments(foodId);
            var info = _infoService.GetInfo().First() ;
            if(food.Data == null)
            {
                return View();
            }
            FoodViewModel model = new FoodViewModel
            {
                Food = food.Data,
                Foods = foodList.ToList(),
                FoodCategories = foodCategoryList,
                FoodComments = commentList,
                Info = info
            };
            return View(model);
        }

        public IActionResult FoodMain()
        {
            var foodList = _foodService.GetFoodList();
            var info = _infoService.GetInfo().First();
            var categoryList = _foodCategoryService.GetFoodCategoryList().Take(4);
            FoodPageViewModel model = new FoodPageViewModel
            {
                FoodCategories = categoryList.ToList(),
                Info = info,
                Foods = foodList
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult MakeComment(FoodViewModel model)
        {
            var day = DateTime.Now.Day;
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            var year = DateTime.Now.Year;
            model.FoodComment.FoodId = model.Food.Id;
            model.FoodComment.Date = day + " " + month + " " + year;
            _commentService.Add(model.FoodComment);
            return RedirectToAction("GetFood", new { foodId = model.Food.Id });
        }

        public IActionResult GetCategory(int categoryId)
        {
            var foodList = _foodService.GetFoodsByCategory(categoryId);
            var foodCategoryList = _foodCategoryService.GetFoodCategoryList().Take(3);
            var info = _infoService.GetInfo().First();
            CategoryViewModel model = new CategoryViewModel
            {
                Foods = foodList,
                FoodCategories = foodCategoryList.ToList(),
                Info = info
            };
            return View(model);
        }
    }
}
