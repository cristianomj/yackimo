using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("Bags")]
    public class Bag
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int BagId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual int UserId { get; set; }
    }
}