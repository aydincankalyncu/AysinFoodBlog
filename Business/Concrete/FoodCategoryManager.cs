using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class FoodCategoryManager : IFoodCategoryService
    {
        private IFoodCategoryDal _foodCategoryDal;
        public FoodCategoryManager(IFoodCategoryDal foodCategoryDal)
        {
            _foodCategoryDal = foodCategoryDal;
        }
        public void Add(FoodCategory foodCategory)
        {
            _foodCategoryDal.Add(foodCategory);
        }

        public void Delete(int foodCategoryId)
        {
            _foodCategoryDal.Delete(new FoodCategory { Id = foodCategoryId });
        }

        public IDataResult<FoodCategory> GetFoodCategoryById(int foodCategoryId)
        {
            return new SuccessDataResult<FoodCategory>(_foodCategoryDal.Get(p => p.Id == foodCategoryId));
        }

        public List<FoodCategory> GetFoodCategoryList()
        {
            return _foodCategoryDal.GetList().ToList();
        }

        public void Update(FoodCategory foodCategory)
        {
            _foodCategoryDal.Update(foodCategory);
        }
    }
}
