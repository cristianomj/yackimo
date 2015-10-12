using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int TagId { get; set; }

        [StringLength(25)]
        public virtual string TagName { get; set; }

        // Relations
        public virtual ICollection<Product> Products { get; set; }
    }
}