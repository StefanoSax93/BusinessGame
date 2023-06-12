using BusinessGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace BusinessGame.Controllers
{
    public class UsersController : Controller
    {
        private readonly BusinessGameContext _context;

        public UsersController(BusinessGameContext context)
        {
            _context = context;
        }

        public IActionResult ElencoUtenti()
        {
            var query = from u in _context.Users
                        select u;

            return View(query);
        }

        public IActionResult NuovoUtente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuovoUtente(Users utente)
        {
            _context.Users.Add(utente);
            _context.SaveChanges();
            return RedirectToAction("ElencoUtenti");
        }

        public IActionResult ModificaUtente(int? id)
        {
            var utente = _context.Users.Find(id);
            return View(utente);
        }

        [HttpPost]
        public IActionResult ModificaUtente(Users utente)
        {
            _context.Users.Update(utente);
            _context.SaveChanges();
            return RedirectToAction("ElencoUtenti");
        }
    }
}
