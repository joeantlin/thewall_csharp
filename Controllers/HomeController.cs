using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWall.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TheWall.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Register", "Login");
            }
            List<Message> allMessages = dbContext.Messages
                .Include(i => i.MessageComments)
                .ThenInclude(j => j.User)
                .Include(k => k.User)
                .ToList();
            ViewBag.AllMessages = allMessages;

            int? IntVariable = HttpContext.Session.GetInt32("UserID");
            int sessionID = IntVariable ?? default(int);
            User thisUser = dbContext.Users
                .Include(i => i.UserComments)
                .ThenInclude(j => j.Message)
                .FirstOrDefault(l => l.Id == sessionID);
            ViewBag.ThisUser = thisUser;
            return View();
        }
        [Route("addmessage")]
        [HttpPost]
        public IActionResult AddMessage(FormContent formcontent)
        {
            if(ModelState.IsValid)
            {
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                int sessionID = IntVariable ?? default(int);

                Message newMessage = new Message();
                newMessage.Content = formcontent.Content;
                newMessage.UserId = sessionID;
                dbContext.Messages.Add(newMessage);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [Route("addcomment/{mId}")]
        [HttpPost]
        public IActionResult AddComment(FormContent formcontent, int mId)
        {
            if(ModelState.IsValid)
            {
                int? IntVariable = HttpContext.Session.GetInt32("UserID");
                int sessionID = IntVariable ?? default(int);

                Comment newComment = new Comment();
                newComment.Content = formcontent.Content;
                newComment.UserId = sessionID;
                newComment.MessageId = mId;
                dbContext.Comments.Add(newComment);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
