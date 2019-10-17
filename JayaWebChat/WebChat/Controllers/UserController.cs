using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new JayaModelContainer())
            {
                var users = context.Users;
                var usersWithId = new List<User>();
                foreach (var user in users)
                {
                    if (usersWithId.Any(u => u.Nick == user.Nick))
                        user.Nick = $"{user.Nick}{user.IdUser}";
                    usersWithId.Add(user);
                }
                return View(users.ToList());
            }
        }

        public ActionResult RoomsUser(int idUser)
        {
            using (var context = new JayaModelContainer())
            {
                var user = context.Users.FirstOrDefault(u => u.IdUser == idUser);
                ViewBag.Title = $"Salas de {user.Nick}";
                ViewBag.idUser = idUser;
                var rooms = context.Rooms.Where(r => r.Chats.Any(c => c.IdUser == idUser));
                return View(rooms.ToList());
            }
        }
        public ActionResult RemoveUser(int idUser)
        {
            using (var context = new JayaModelContainer())
            {
                using (var transaction = new TransactionScope())
                {
                    var user = context.Users.FirstOrDefault(u => u.IdUser == idUser);
                    user.WasRemoved = true;
                    user.IsActive = false;
                    context.SaveChanges();
                    ViewBag.nickname = user.Nick;
                    var rooms = context.Rooms.Where(r => r.IsActive && r.Chats.Any(c => c.IdUser == idUser) && r.Chats.Count(c => c.IdUser == idUser && c.IsEvent) % 2 != 0);
                    foreach (var room in rooms)
                    {
                        ViewBag.idRoom = room.IdRoom;
                        room.CurrentUsers--;
                        if (room.CurrentUsers == 0)
                            room.IsActive = false;
                        context.Chats.Add(new Chat { IdRoom = room.IdRoom, IdUser = idUser, IsEvent = true, Message = $"{user.Nick} Ha sido expulsado" });
                        context.SaveChanges();
                    }
                    transaction.Complete();
                }
                return View();
            }
        }
        public ActionResult MessagesUser(int idRoom, int idUser)
        {
            using (var context = new JayaModelContainer())
            {
                var user = context.Users.FirstOrDefault(u => u.IdUser == idUser);
                var room = context.Rooms.FirstOrDefault(r => r.IdRoom == idRoom);
                var chats = context.Chats.Where(c => c.IdRoom == idRoom && c.IdUser == idUser);
                ViewBag.Title = $"Mensajes de {user.Nick} en la sala {room.Name}";
                ViewBag.idUser = idUser;
                return View(chats.ToList());
            }
        }
    }
}