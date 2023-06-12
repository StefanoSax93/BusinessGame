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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NuovoUtente([Bind("Id,Nome")] Users utente)
        {
            if (ModelState.IsValid)
            {
				_context.Users.Add(utente);
				await _context.SaveChangesAsync();
				return RedirectToAction("ElencoUtenti");
			}
			return View(utente);

		}

		public async Task<IActionResult> ModificaUtente(int? id)
        {
			if (id == null || _context.Users == null)
			{
				return NotFound();
			}

			var utente = await _context.Users.FindAsync(id);
			if (utente == null)
			{
				return NotFound();
			}
			return View(utente);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ModificaUtente(Users utente)
        {
            _context.Users.Update(utente);
            _context.SaveChanges();
            return RedirectToAction("ElencoUtenti");
        }
    }
}
