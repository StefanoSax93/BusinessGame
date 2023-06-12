using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessGame.Models;

namespace BusinessGame.Controllers
{
    public class ShopsController : Controller
    {
        private readonly BusinessGameContext _context;

        public ShopsController(BusinessGameContext context)
        {
            _context = context;
        }

        // GET: Shops
        public async Task<IActionResult> Index()
        {
			var query = from n in _context.Shops
						join p in _context.Products on n.IdProduct equals p.Id
						select new
						{
                            Id = n.Id,
							Prodotto = p.Nome
						};
            return View(query);
			
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shops == null)
            {
                return NotFound();
            }

            var shops = await _context.Shops.FindAsync(id);
            if (shops == null)
            {
                return NotFound();
            }
            return View(shops);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,IdProduct")] Shops shops)
        {
            if (id != shops.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopsExists(shops.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shops);
        }

        private bool ShopsExists(int id)
        {
          return (_context.Shops?.Any(e => e.Id == id)).GetValueOrDefault();
        }

		public IActionResult AggiungiProdotto()
		{
			ViewData["Prodotto"] = new SelectList(_context.Products, "Id", "Nome");
			return View();
		}

		[HttpPost]

		public IActionResult AggiungiProdotto(int idprodotto)
		{
			var prodotto = _context.Products.FirstOrDefault(p => p.Id == idprodotto);

            if (prodotto.Quantita > 0)
            {
				prodotto.Quantita--;
				_context.SaveChanges();

				var newRecord = new Shops();
				newRecord.Nome = "Negozio";
				newRecord.IdProduct = idprodotto;

				_context.Shops.Add(newRecord);
				_context.SaveChanges();

				return RedirectToAction("Index");
			} else
            {
                return RedirectToAction("AggiungiProdotto");
            }
			
		}
	}
}
