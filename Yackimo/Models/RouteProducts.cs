using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    [Table("RouteProducts")]
    public class RouteProducts
    {
        public int RouteProductsId { get; set; }
        public int Product { get; set; }

        public int TradeRoutesId { get; set; }
    }
}