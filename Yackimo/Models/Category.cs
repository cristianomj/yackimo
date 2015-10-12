using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    [Table("Category")]
    public class Category
    {
        public int CategoryId { get; set; }

        public int Name { get; set; }
    }
}