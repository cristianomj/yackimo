using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("UserProfile")]
    public class UserProfile
    {
        // Account Info
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public virtual int UserId { get; set; }
        public virtual string UserName { get; set; }

        // Personal Info
        [StringLength(25)]
        public virtual string FirstName { get; set; }
        [StringLength(25)]
        public virtual string LastName { get; set; }
        [StringLength(50)]
        public virtual string EmailAddress { get; set; }

        public virtual byte[] Picture { get; set; }
        [StringLength(25)]
        public virtual string MimeType { get; set; }

        // FEEDBACK

        // Relations
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        
        // Bag
        public virtual Bag Bag { get; set; }
        public virtual int? BagId { get; set; }

        // Chat
        public virtual ChatInbox ChatInbox { get; set; }
        public virtual int? ChatInboxId { get; set; }
    }
}