using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    public class Test
    {
        public int id { get; set; }
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DataCreated { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }

    }
}