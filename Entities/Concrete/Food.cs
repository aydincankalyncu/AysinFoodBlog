using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Food: IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string FoodTitle { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public string FoodPublishDate { get; set; }
        public string CategoryName { get; set; }
        public string OriginalFoodName { get; set; }

    }
}
