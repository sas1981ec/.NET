using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
                return Index();
            ViewData["NickName"] = nickname == "" ? null : nickname;
            using (var context = new JayaModelContainer())
            {
                if (context.Users.Any(u => u.Nick == nickname.ToLower() && u.IsActive))
                    ModelState.AddModelError("Nick", "El nickname ya existe.");
                else
                {
                    var user = new User { IsActive = true, Nick = nickname.ToLower() };
                    context.Users.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Room", new { nickname, id = user.IdUser });
                }
                return View();
            }
        }

        public ActionResult Room(string nickname, int id)
        {
            ViewBag.nickname = nickname;
            ViewBag.id = id;
            using (var context = new JayaModelContainer())
            {
                var rooms = context.Rooms.Where(r => r.IsActive);
                return View(rooms.ToList());
            }
        }

        public ActionResult Create(string nickname, int id)
        {
            ViewBag.nickname = nickname;
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Room room, string nickname, int id)
        {
            if (!ModelState.IsValid)
                return View(room);
            using (var context = new JayaModelContainer())
            {
                var newRoom = new Room { CurrentUsers = 1, IsActive = true, Name = room.Name, Chats = new List<Chat> { new Chat { IsEvent = true, IdUser = id, Message = $"{nickname} Ha ingresado" } } };
                context.Rooms.Add(newRoom);
                context.SaveChanges();
                return RedirectToAction("Chat", new { nickname, idUser = id, idRoom = newRoom.IdRoom });
            }
        }

        public ActionResult Chat(string nickname, int idUser, int idRoom)
        {
            ViewBag.nickname = nickname;
            ViewBag.idUser = idUser;
            ViewBag.idRoom = idRoom;
            using (var context = new JayaModelContainer())
            {
                var user = context.Users.FirstOrDefault(u => u.IdUser == idUser && u.WasRemoved);
                if (user != null)
                    return RedirectToAction("RemoveUser");
                var chat = context.Chats.Include("User").Where(c => c.Room.IdRoom == idRoom);
                ViewBag.Title = $"Chat {chat.FirstOrDefault().Room.Name}";
                return View(chat.ToList());
            }
        }

        public ActionResult RemoveUser()
        {
            return View();
        }

        public ActionResult ChatWithRegister(string nickname, int idUser, int idRoom)
        {
            ViewBag.nickname = nickname;
            ViewBag.idUser = idUser;
            ViewBag.idRoom = idRoom;
            using (var context = new JayaModelContainer())
            {
                using (var transaction = new TransactionScope())
                {
                    var room = context.Rooms.FirstOrDefault(r => r.IdRoom == idRoom);
                    
                    room.CurrentUsers++;
                    context.SaveChanges();
                    context.Chats.Add(new Chat { IdRoom = idRoom, IdUser = idUser, IsEvent = true, Message = $"{nickname} Ha ingresado" });
                    context.SaveChanges();
                    transaction.Complete();
                }
                return View();
                //return RedirectToAction("Chat", new { nickname, idUser, idRoom });
            }
        }

        public JsonResult SaveMessage(string message, int idUser, int idRoom)
        {
            using (var context = new JayaModelContainer())
            {
                context.Chats.Add(new Chat { IdRoom = idRoom, IdUser = idUser, Message = message });
                context.SaveChanges();
            }
            return null;
        }

        public ActionResult Getout(string nickname, int idUser, int idRoom)
        {
            using (var context = new JayaModelContainer())
            {
                using (var transaction = new TransactionScope())
                {
                    var room = context.Rooms.FirstOrDefault(r => r.IdRoom == idRoom);
                    room.CurrentUsers--;
                    if (room.CurrentUsers == 0)
                        room.IsActive = false;
                    context.SaveChanges();
                    context.Chats.Add(new Chat { IdRoom = idRoom, IdUser = idUser, IsEvent = true, Message = $"{nickname} Ha salido" });
                    context.SaveChanges();
                    transaction.Complete();
                }
                return RedirectToAction("Room", new { nickname, id = idUser });
            }
        }
    }
}