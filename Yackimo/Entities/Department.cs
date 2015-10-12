using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        // Relations
        public virtual ICollection<Category> Categories { get; set; }
    }
}