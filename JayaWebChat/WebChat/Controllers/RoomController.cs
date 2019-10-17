using System.Linq;
using System.Web.Mvc;
using WebChat.Models;

namespace WebChat.Controllers
{
    public class RoomController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new JayaModelContainer())
            {
                var rooms = context.Rooms;
                return View(rooms.ToList());
            }
        }

        public ActionResult Chat(int idRoom)
        {
            using (var context = new JayaModelContainer())
            {
                var room = context.Rooms.FirstOrDefault(r => r.IdRoom == idRoom);
                ViewBag.Title = $"Chat de la sala {room.Name}";
                var chats = context.Chats.Include("User").Where(c => c.IdRoom == idRoom);
                return View(chats.ToList());
            }
        }
    }
}