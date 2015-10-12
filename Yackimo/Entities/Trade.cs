using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    public class Trade
    {
        public virtual int TradeId { get; set; }
        public virtual int FromUser { get; set; }
        public virtual int ToUser { get; set; }
        public virtual int TradedFor { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}