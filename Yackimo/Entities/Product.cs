using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual DateTime? UploadDate { get; set; }

        public virtual byte[] ProductImage { get; set; }
        [StringLength(25)]
        public virtual string MimeType { get; set; }

        public virtual int Views { get; set; }

        public virtual int Qty { get; set; }

        // Relations
        public virtual UserProfile User { get; set; }
        public virtual int UserId { get; set; }
        public virtual int CategoryId { get; set; }

        public virtual ICollection<Bag> Bags { get; set; }

        public virtual ICollection<Trade> Trades { get; set; }
    }
}