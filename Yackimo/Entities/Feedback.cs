using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    public class Feedback
    {
        public int FeedBackId { get; set; }

        public int Sender { get; set; }
        public int ProfileId { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}