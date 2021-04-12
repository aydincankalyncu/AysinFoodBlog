using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class FoodCommentManager : IFoodCommentService
    {
        private readonly IFoodCommentDal _commentDal;
        public FoodCommentManager(IFoodCommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void Add(FoodComment foodComment)
        {
            _commentDal.Add(foodComment);
        }

        public void Delete(int commentId)
        {
            _commentDal.Delete(new FoodComment { Id = commentId });
        }

        public List<FoodComment> GetFoodComments(int foodId)
        {
            return _commentDal.GetList(p => p.FoodId == foodId).ToList();
        }

        public void Update(FoodComment foodComment)
        {
            _commentDal.Update(foodComment);
        }
    }
}
