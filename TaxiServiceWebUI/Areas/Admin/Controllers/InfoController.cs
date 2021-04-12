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
    public class InfoController : Controller
    {
        private readonly IInfoService _infoService;
        private readonly IFoodService _foodService;
        private readonly IFoodCategoryService _categoryService;
        public InfoController(IInfoService infoService, IFoodService foodService, IFoodCategoryService categoryService)
        {
            _infoService = infoService;
            _foodService = foodService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var infoList = _infoService.GetInfo().First();
            var foodCount = _foodService.GetFoodList().Count;
            var categoryCount = _categoryService.GetFoodCategoryList().Count();
            ViewBag.Title = "Kişisel Bilgi Bölümü";
            InfoListViewModel model = new InfoListViewModel
            {
                Info = infoList,
                FoodCount = foodCount,
                CategoryCount = categoryCount
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(InfoListViewModel model, IFormFile file)
        {
            var userImage = _infoService.GetInfo().First().ImagePath;
            if (file == null)
            {
                model.Info.ImagePath = userImage;
                _infoService.Update(model.Info);

            }
            else
            {
                if (file != null)
                {
                    DeleteUserImage(userImage);
                    var extension = Path.GetExtension(file.FileName);
                    var imagePath = string.Format($"{Guid.NewGuid()}{extension}");
                    model.Info.ImagePath = imagePath;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\profile", imagePath);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                _infoService.Update(model.Info);
            }
            return RedirectToAction("index");
        }

        private void DeleteUserImage(string userImage)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\profile", userImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
