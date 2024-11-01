using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCFirstDatabase.Models;

namespace MVCFirstDatabase.Controllers
{
    public class ParqueosController : Controller
    {
        private readonly ControParqueoContext _context;

        public ParqueosController(ControParqueoContext context)
        {
            _context = context;
        }

        // GET: Parqueos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parqueos.ToListAsync());
        }

        // GET: Parqueos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parqueo = await _context.Parqueos
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (parqueo == null)
            {
                return NotFound();
            }

            return View(parqueo);
        }

        // GET: Parqueos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parqueos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,Estado,Tipo")] Parqueo parqueo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parqueo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parqueo);
        }

        // GET: Parqueos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parqueo = await _context.Parqueos.FindAsync(id);
            if (parqueo == null)
            {
                return NotFound();
            }
            return View(parqueo);
        }

        // POST: Parqueos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Numero,Estado,Tipo")] Parqueo parqueo)
        {
            if (id != parqueo.Numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parqueo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParqueoExists(parqueo.Numero))
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
            return View(parqueo);
        }

        // GET: Parqueos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parqueo = await _context.Parqueos
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (parqueo == null)
            {
                return NotFound();
            }

            return View(parqueo);
        }

        // POST: Parqueos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parqueo = await _context.Parqueos.FindAsync(id);
            if (parqueo != null)
            {
                _context.Parqueos.Remove(parqueo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParqueoExists(int id)
        {
            return _context.Parqueos.Any(e => e.Numero == id);
        }
    }
}
