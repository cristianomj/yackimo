using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    [Table("TradeRoute")]
    public class TradeRoutes
    {
        public int TradeRoutesId { get; set; }
        public string RouteName { get; set; }

        public int RequestorId { get; set; }
        public int RequesteeId { get; set; }

        public virtual ICollection<RouteProducts> RouteProducts { get; set; }
    }
}