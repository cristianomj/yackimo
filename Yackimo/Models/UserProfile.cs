using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yackimo.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        // Account Information
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }

        // Personal Information
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Address
        // Add Address object

        // Profile Picture
        public byte[] Picture { get; set; }
        [StringLength(30)]
        [HiddenInput(DisplayValue=false)]
        public string MimeType { get; set; }

        public virtual ICollection<Product> Products { get; set; }        
    }
}