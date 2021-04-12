using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiServiceWebUI.Dtos;

namespace TaxiServiceWebUI.Models
{
    public class InfoViewModel
    {
        public Info Info { get; set; }
        public ContactDTO Contact { get; set; }
    }
}
