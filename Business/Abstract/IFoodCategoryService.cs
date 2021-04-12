using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFoodCategoryService
    {
        void Add(FoodCategory foodCategory);
        void Update(FoodCategory foodCategory);
        void Delete(int foodCategoryId);
        IDataResult<FoodCategory> GetFoodCategoryById(int foodCategoryId);
        List<FoodCategory> GetFoodCategoryList();
    }
}
