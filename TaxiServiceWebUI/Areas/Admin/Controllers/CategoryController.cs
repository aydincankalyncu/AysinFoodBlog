using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiServiceWebUI.Areas.Admin.Models;

namespace TaxiServiceWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IFoodCategoryService _categoryService;
        private readonly IFoodService _foodService;
        public CategoryController(IFoodCategoryService categoryService, IFoodService foodService)
        {
            _categoryService = categoryService;
            _foodService = foodService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetFoodCategoryList();
            CategoryListViewModel model = new CategoryListViewModel
            {
                Categories = categories
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> Add(IFormFile file, CategoryAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                FoodCategory foodCategory = new FoodCategory();
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var imagePath = string.Format($"{Guid.NewGuid()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\categories", imagePath);
                    foodCategory.ImagePath = imagePath;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    foodCategory.CategoryName = model.Category.CategoryName;
                    _categoryService.Add(foodCategory);
                }
                return RedirectToAction("index");
            }
            
        }

        public IActionResult Delete(int categoryId)
        {
            var foods = _foodService.GetFoodsByCategory(categoryId);
            if(foods.Count == 0)
            {
                var categoryImage = _categoryService.GetFoodCategoryById(categoryId).Data.ImagePath;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\categories", categoryImage);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                _categoryService.Delete(categoryId);
                return RedirectToAction("index");
            }
            else
            {
                var categories = _categoryService.GetFoodCategoryList();
                CategoryListViewModel model = new CategoryListViewModel
                {
                    Categories = categories
                };
                ViewBag.ErrorMessage = "Silmek istediğiniz kategoriye ait yemekler mevcut, bu işlemi yapabilmek için önce ilgili yemekleri silmeniz gerekmektedir.";
                return View("index", model);
            }

        }

        public IActionResult Update(int categoryId)
        {
            var category = _categoryService.GetFoodCategoryById(categoryId).Data;
            CategoryDetailViewModel model = new CategoryDetailViewModel
            {
                Category = category
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IFormFile file, CategoryDetailViewModel model)
        {
            if(file != null)
            {
                DeleteCategoryImage(model.Category.Id);
                var extension = Path.GetExtension(file.FileName);
                var imagePath = string.Format($"{Guid.NewGuid()}{extension}");
                model.Category.ImagePath = imagePath;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\categories", imagePath);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            else
            {
                model.Category.ImagePath = _categoryService.GetFoodCategoryById(model.Category.Id).Data.ImagePath;
            }

            FoodCategory category = new FoodCategory();
            category.Id = model.Category.Id;
            category.CategoryName = model.Category.CategoryName;
            category.ImagePath = model.Category.ImagePath;
            _categoryService.Update(category);
            return RedirectToAction("index");
        }

        #region Private Methods
        private void DeleteCategoryImage(int categoryId)
        {
            var category = _categoryService.GetFoodCategoryById(categoryId).Data;
            var categoryImage = category.ImagePath;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\categories", categoryImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        #endregion


    }
}
