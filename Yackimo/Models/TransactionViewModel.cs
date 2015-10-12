using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    public class TransactionViewModel
    {
        // Requestor Info
        public UserProfile Requestor { get; set; }
        
        //// Requestee Info
        public UserProfile Requestee { get; set; }
        public int RequestedProduct { get; set; }
    }
}