using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Info: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Job { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string MapUrl { get; set; }

    }
}
