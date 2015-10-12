using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yackimo.Models
{
    [Table("Product")]
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        // Product information
        [StringLength(50)]
        [Required(ErrorMessage="Please enter a name for the product")]
        public string Name { get; set; }
        
        [DataType(DataType.MultilineText)]
        [StringLength(100)]
        [Required(ErrorMessage="Please enter a description for the product")]
        public string Description { get; set; }
        
        [StringLength(50)]
        [Required(ErrorMessage="Please enter a category for the product")]
        public string Category { get; set; }

        // Product's views
        public int Views { get; set; }

        // Date Created
        public DateTime DataCreated { get; set; }

        // Product's picture
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue=false)]
        public string ImageMimeType { get; set; }

        // UserProfile associated with product
        public int UserId { get; set; }
    }
}