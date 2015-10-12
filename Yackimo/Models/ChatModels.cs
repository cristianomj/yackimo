using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yackimo.Entities;

namespace Yackimo.Models
{
    public class ChatModel
    {
        public int ChatId { get; set; }
        public bool ChatExists { get; set; }
        public string With { get; set; }
        public string ConnectionId { get; set; }
        public string Body { get; set; }
        public virtual ICollection<ChatMessage> ChatHistory { get; set; }
    }
}