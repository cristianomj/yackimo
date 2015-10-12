using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Yackimo.Entities
{
    [Table("ChatInbox")]
    public class ChatInbox
    {
        public virtual int ChatInboxId { get; set; }

        public virtual ICollection<ChatRoom> ChatRooms { get; set; }
        public virtual int UserId { get; set; }
    }

    [Table("ChatRoom")]
    public class ChatRoom
    {
        public virtual int ChatRoomId { get; set; }

        public virtual string ConnectionId { get; set; }
        public virtual string User1 { get; set; }
        public virtual string User2 { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }
        public virtual ICollection<ChatInbox> ChatInboxIds { get; set; }
    }

    [Table("ChatMessage")]
    public class ChatMessage
    {
        public virtual int ChatMessageId { get; set; }

        public virtual string Body { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string From { get; set; }
        public virtual bool isRead { get; set; }

        public virtual int ChatRoomId { get; set; }
    }
}