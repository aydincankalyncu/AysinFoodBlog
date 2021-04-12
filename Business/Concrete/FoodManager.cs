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
    public class FoodManager : IFoodService
    {
        private IFoodDal _foodDal;
        public FoodManager(IFoodDal foodDal)
        {
            _foodDal = foodDal;
        }
        public void Add(Food food)
        {
            _foodDal.Add(food);
        }

        public void Delete(int foodId)
        {
            _foodDal.Delete(new Food { Id = foodId});
        }

        public IDataResult<Food> GetFoodById(int foodId)
        {
            return new SuccessDataResult<Food>(_foodDal.Get(p => p.Id == foodId));
        }

        public List<Food> GetFoodList()
        {
            return _foodDal.GetList().ToList();
        }

        public List<Food> GetFoodsByCategory(int categoryId)
        {
            return _foodDal.GetList(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Food food)
        {
            _foodDal.Update(food);
        }
    }
}
