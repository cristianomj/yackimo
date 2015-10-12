using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yackimo.Models
{
    public class AddOrUpdateProduct
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        // Product Information
        [Required(ErrorMessage = "Please enter product's name.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter product's description.")]
        [StringLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<string> Departments { get; set; }
        public List<string> Categories { get; set; }

        // Product's Category
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [Display(Name="Quantity")]
        [Range(1, 100, ErrorMessage="Quantity must be between 0 and 100")]
        public int Qty { get; set; }

        // Product's Owner 
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
    }
}