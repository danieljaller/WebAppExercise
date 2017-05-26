using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShop.ViewModels
{
    public class AdminViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 25 letters")]
        public string PieName { get; set; }

        [StringLength(250)]
        public string ExtraDescription { get; set; }
        public string CategoryName { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public int PieId { get; set; }
        public List<SelectListItem> Pies { get; set; }

        public decimal Price { get; set; }
    }
}
