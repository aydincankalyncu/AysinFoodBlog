using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class FoodCategory: IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }

    }
}
