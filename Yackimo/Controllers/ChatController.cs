using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yackimo.Entities;
using Yackimo.Infrastructure;
using Yackimo.Models;

namespace Yackimo.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private YackimoDb db = new YackimoDb();

        //
        // GET: /Chat/Inbox

        public ActionResult Inbox()
        {
            var id = WebSecurity.GetUserId(User.Identity.Name);
            var model = db.ChatInboxes.Single(i => i.UserId == id);

            return View(model);
        }

        //
        // GET: /Chat/GetLastMessage

        public ActionResult GetLastMessage(IQueryable<ChatMessage> messages)
        {
            var message = messages.OrderByDescending(m => m.Date).Take(1).Select(m => m.Body).ToString();

            return Content(message);
        }

        //
        // GET: /Chat/Room

        [HttpGet]
        public ActionResult Room(string id)
        {
            // Get current user's id
            var myId = WebSecurity.GetUserId(User.Identity.Name);

            ChatModel model = new ChatModel();

            // Check for existing room in user's inbox
            if (db.ChatInboxes.Single(i => i.UserId == myId).ChatRooms.Any(cr => cr.ConnectionId == string.Format("{0};{1}", User.Identity.Name, id)
                || cr.ConnectionId == string.Format("{0};{1}", id, User.Identity.Name)))
            {
                var room = db.ChatInboxes.Single(i => i.UserId == myId).ChatRooms.Single(cr => cr.ConnectionId == string.Format("{0};{1}", User.Identity.Name, id)
                || cr.ConnectionId == string.Format("{0};{1}", id, User.Identity.Name));

                model.ChatExists = true;
                model.ChatId = room.ChatRoomId;
                model.ConnectionId = room.ConnectionId;
                model.ChatHistory = room.Messages.ToList();
            }

            else
            {
                model.ChatExists = false;
            }

            return View(model);
        }

        // POST: /Chat/Room

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Room(ChatModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ChatMessage newMessage = new ChatMessage
                {
                    From = User.Identity.Name,
                    Body = model.Body,
                    Date = DateTime.Now,
                    isRead = false
                };

                if (model.ChatExists)
                {
                    var chat = db.ChatRooms.Find(model.ChatId);

                    if (model.Body != null)
                    {
                        chat.Messages.Add(newMessage);
                        db.SaveChanges();
                    }

                    return RedirectToLocal(returnUrl);
                }
            }

            return View(model);
        }
        //
        // GET: /Chat/Start

        public ActionResult Start(string id)
        {
            var toId = WebSecurity.GetUserId(id);
            var myId = WebSecurity.GetUserId(User.Identity.Name);

            var inboxes = db.ChatInboxes.Where(i => i.UserId == toId || i.UserId == myId).ToList();

            ChatRoom room = new ChatRoom()
            {
                ConnectionId = string.Format("{0};{1}", User.Identity.Name, id),
                User1 = User.Identity.Name,
                User2 = id
            };

            db.ChatRooms.Add(room);
            //db.SaveChanges();

            foreach (var inbox in inboxes)
                inbox.ChatRooms.Add(room);

            db.SaveChanges();

            return RedirectToAction("~/Home/Index");
        }

        //
        // GET: /Chat/Find

        public ActionResult Find(string id)
        {
            var myId = WebSecurity.GetUserId(User.Identity.Name);

            var room = db.ChatInboxes.FirstOrDefault(i => i.UserId == myId).ChatRooms.Single(cr => cr.ConnectionId == string.Format("{0};{1}", User.Identity.Name, id)
                || cr.ConnectionId == string.Format("{0};{1}", id, User.Identity.Name));

            ChatMessage newMessage = new ChatMessage
            {
                From = User.Identity.Name,
                Body = "This is a test",
                Date = DateTime.Now,
                isRead = false
            };

            room.Messages.Add(newMessage);
            db.SaveChanges();

            room.Messages.Remove(newMessage);

            return View();
        }

        public ActionResult FindMessage(string id)
        {
            var myId = WebSecurity.GetUserId(User.Identity.Name);

            var room = db.ChatInboxes.FirstOrDefault(i => i.UserId == myId).ChatRooms.Single(cr => cr.ConnectionId == string.Format("{0};{1}", User.Identity.Name, id)
                || cr.ConnectionId == string.Format("{0};{1}", id, User.Identity.Name));

            var message = room.Messages.Single(m => m.Body == "This is a test");

            return View();
        }

        ////
        //// GET: /Chat/Chat

        //[HttpGet]
        //public ActionResult ChatRoom(string id)
        //{
        //    var myId = WebSecurity.GetUserId(User.Identity.Name);
        //    var toId = WebSecurity.GetUserId(id);

        //    var myInbox = db.ChatInboxes.Single(i => i.UserId == myId);
        //    ChatModel model = new ChatModel();

        //    // Check if chat exists
        //    bool chatExists = myInbox.ChatRooms.Any(cr => cr.ConnectionId.Contains(string.Format("{0}{1}{2}{3}", myId.ToString(), User.Identity.Name, toId.ToString(), id))
        //        || cr.ConnectionId.Contains(string.Format("{0}{1}{2}{3}", toId.ToString(), id, myId.ToString(), User.Identity.Name)));

        //    // If it does exists
        //    if (chatExists)
        //    {
        //        var chat = myInbox.ChatRooms.Single(cr => cr.ConnectionId.Contains(string.Format("{0}{1}{2}{3}", myId.ToString(), User.Identity.Name, toId.ToString(), id))
        //            || cr.ConnectionId.Contains(string.Format("{0}{1}{2}{3}", toId.ToString(), id, myId.ToString(), User.Identity.Name)));

        //        // Pull messages from it
        //        model.ChatId = chat.ChatRoomId;
        //        model.ChatExists = true;
        //        model.MessageHistory = (List<Message>)chat.Messages;

        //        return View(model);
        //    }

        //    // If it does not exist
        //    model.With = id;
        //    model.ConnectionId = string.Format("{0}{1}", toId, id);

        //    return View(model);
        //}

        ////
        //// POST: /Chat/ChatRoom

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ChatRoom(ChatModel model, string returnUrl)
        //{
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ChatRoom(ChatModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var myId = WebSecurity.GetUserId(User.Identity.Name);
        //        var toId = WebSecurity.GetUserId(model.With);

        //        var inboxes = db.ChatInboxes.Where(i => i.UserId == myId || i.UserId == myId).ToList();

        //        ChatMessage newMessage = new ChatMessage()
        //        {
        //            From = User.Identity.Name,
        //            Body = model.Body,
        //            isRead = false,
        //            Date = DateTime.Now,
        //        };

        //        // If does not exist yet
        //        if (!model.ChatExists)
        //        {
        //            ChatRoom myChat = new ChatRoom()
        //            {
        //                With = model.With
        //            };

        //            ChatRoom toChat = new ChatRoom()
        //            {
        //                With = User.Identity.Name
        //            };

        //            foreach (var inbox in inboxes)
        //            {
        //                if (inbox.UserId == myId)
        //                {
        //                    inbox.ChatRooms.Add(myChat);
        //                }
        //                else
        //            }
        //            newMessage.ChatRoomId = myChat.ChatInboxId;
        //            myChat.Messages.Add(newMessage);

        //            newMessage.ChatRoomId = toChat.ChatInboxId;
        //            toChat.Messages.Add(newMessage);

        //            db.SaveChanges();

        //            foreach (var inbox in inboxes)
        //            {
        //                if (inbox.UserId == myId)
        //                {
        //                    inbox.ChatRooms.Add(myChat);
        //                }
        //                else
        //                {
        //                    inbox.ChatRooms.Add(toChat);
        //                }
        //            }

        //            db.SaveChanges();

        //            return RedirectToLocal(returnUrl);
        //        }

        //        // if chat exists
        //        newMessage.ChatRoomId = model.ChatId;

        //        foreach (var inbox in inboxes)
        //        {
        //            if (inbox.ChatInboxId == myId)
        //            {
        //                inbox.ChatRooms.Single(cr => cr.With == model.With).Messages.Add(newMessage);
        //            }
        //            else
        //            {
        //                inbox.ChatRooms.Single(cr => cr.With == User.Identity.Name).Messages.Add(newMessage);
        //            }
        //        }

        //        return RedirectToLocal(returnUrl);
        //    }
        //    return View(model);
        //}

        [AllowAnonymous]
        public FileContentResult GetImage(string id)
        {
            UserProfile user = db.UserProfiles.Single(u => u.UserName == id);

            if (user != null)
            {
                return File(user.Picture, user.MimeType);
            }
            else
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
