using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFoodCommentService
    {
        void Add(FoodComment foodComment);
        void Delete(int commentId);
        void Update(FoodComment foodComment);
        List<FoodComment> GetFoodComments(int foodId);

    }
}
