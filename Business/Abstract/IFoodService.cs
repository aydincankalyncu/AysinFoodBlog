using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFoodService
    {
        void Add(Food food);
        void Update(Food food);
        void Delete(int foodId);
        IDataResult<Food> GetFoodById(int foodId);
        List<Food> GetFoodList();
        List<Food> GetFoodsByCategory(int categoryId);
    }
}
