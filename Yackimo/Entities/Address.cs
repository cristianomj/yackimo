using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int AddressId { get; set; }

        [StringLength(50)]
        public virtual string Street { get; set; }
        [StringLength(50)]
        public virtual string City { get; set; }
        [StringLength(25)]
        public virtual string State { get; set; }
        [StringLength(25)]
        public virtual string ZipCode { get; set; }
        [StringLength(25)]
        public virtual string Type { get; set; }

        // Relations
        public virtual int UserId { get; set; }
    }
}