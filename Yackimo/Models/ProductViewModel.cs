using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string OwnerName { get; set; }
        public int OwnerId { get; set; }
    }
}