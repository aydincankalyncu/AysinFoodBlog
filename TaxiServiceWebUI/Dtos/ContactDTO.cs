using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiServiceWebUI.Dtos
{
    public class ContactDTO
    {
        [Required(ErrorMessage ="Lütfen bu alanı doldurunuz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        public string Message { get; set; }
    }
}
