using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        // Self Reference
        public virtual int? ParentId { get; set; } 
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }

        // References
        public virtual int DepartmentId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}