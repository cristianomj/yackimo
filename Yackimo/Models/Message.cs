using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        [Required(ErrorMessage="Subject required!")]
        public string Subject { get; set; }
        [Required(ErrorMessage="Content required!")]
        public string Content { get; set; }
        public int UserId { get; set; }
        [Display(Name = "From")]
        public string SenderName { get; set; }
        public bool isRead { get; set; }
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }
    }
}