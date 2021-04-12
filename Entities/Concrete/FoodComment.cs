using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class FoodComment: IEntity
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }

    }
}
