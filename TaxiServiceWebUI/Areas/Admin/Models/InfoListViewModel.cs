﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiServiceWebUI.Areas.Admin.Models
{
    public class InfoListViewModel
    {
        public Info Info { get; set; }
        public int FoodCount { get; set; }
        public int CategoryCount { get; set; }
    }
}
