using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using TaxiServiceWebUI.Areas.Admin.Models;
using System.Globalization;

namespace TaxiServiceWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FoodController : Controller
    {
        private IFoodService _foodService;
        private IFoodCategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        private readonly IFoodCommentService _commentService;

        public FoodController(IFoodService foodService, IFoodCategoryService categoryService,
            IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IFoodCommentService commentService)
        {
            _foodService = foodService;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _commentService = commentService;
    }
        public IActionResult Index()
        {
            var foods = _foodService.GetFoodList();
            FoodListViewModel model = new FoodListViewModel
            {
                Foods = foods
            };
            return View(model);
        }
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetFoodCategoryList(), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Food food, IFormFile file)
        {
            var categoryName = _categoryService.GetFoodCategoryById(food.CategoryId);
            var day = DateTime.Now.Day;
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            var year = DateTime.Now.Year;
            
            
            food.CategoryName = categoryName.Data.CategoryName;
            food.OriginalFoodName = file.FileName;
            food.FoodPublishDate = day + " " + month + " " + year;
            
            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var imagePath = string.Format($"{Guid.NewGuid()}{extension}");
                food.FoodImage = imagePath;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imagePath);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _foodService.Add(food);
            return RedirectToAction("index");
        }

        public IActionResult Update(int foodId)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetFoodCategoryList(), "Id", "CategoryName");
            var food = _foodService.GetFoodById(foodId);
            FoodDetailViewModel model = new FoodDetailViewModel
            {
                Food = food.Data
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FoodDetailViewModel model, IFormFile file)
        {
            var categoryName = _categoryService.GetFoodCategoryById(model.Food.CategoryId);
            var day = DateTime.Now.Day;
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            var year = DateTime.Now.Year;


            model.Food.CategoryName = categoryName.Data.CategoryName;
           
            model.Food.FoodPublishDate = day + " " + month + " " + year;

            if (file != null)
            {
                model.Food.OriginalFoodName = file.FileName;
                DeleteFoodImage(model);
                var extension = Path.GetExtension(file.FileName);
                var imagePath = string.Format($"{Guid.NewGuid()}{extension}");
                model.Food.FoodImage = imagePath;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imagePath);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            else
            {
                model.Food.FoodImage = _foodService.GetFoodById(model.Food.Id).Data.FoodImage;
            }

            Food food = new Food();
            food.Id = model.Food.Id;
            food.CategoryId = model.Food.CategoryId;
            food.CategoryName = model.Food.CategoryName;
            food.FoodDescription = model.Food.FoodDescription;
            food.FoodImage = model.Food.FoodImage;
            food.FoodPublishDate = model.Food.FoodPublishDate;
            food.OriginalFoodName = model.Food.OriginalFoodName;
            food.FoodTitle = model.Food.FoodTitle;
            _foodService.Update(food);
            return RedirectToAction("index");
        }


        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();


            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\ckeditorimages", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/images/ckeditorimages/"}{fileName}";

            return Json(new { uploaded = true, url });

        }

        public IActionResult Delete(int foodId)
        {
            var foodImage = _foodService.GetFoodById(foodId).Data.FoodImage;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", foodImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _foodService.Delete(foodId);
            DeleteFoodComments(foodId);
            return RedirectToAction("index");
        }

        [HttpPost]
        public JsonResult GetFoodInfo(string queryString)
        {
            var foodList = _foodService.GetFoodList();
            var foodInfo = foodList.Where(p => p.FoodTitle.Contains(queryString) ||
                                                                p.FoodPublishDate.Contains(queryString) ||
                                                                p.CategoryName.Contains(queryString));
            return Json(foodInfo);
        }

        #region Private Methods
        private void DeleteFoodImage(FoodDetailViewModel model) // Process which deletes the foods image.
        {
            var food = _foodService.GetFoodById(model.Food.Id);
            var foodImage = food.Data.FoodImage;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", foodImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        private void DeleteFoodComments(int foodId) // After deleting food, deletes the food comments belong that food.
        {
            var foodComments = _commentService.GetFoodComments(foodId);
            foreach (var foodComment in foodComments)
            {
                _commentService.Delete(foodComment.Id);
            }
        }

        #endregion


    }
}
