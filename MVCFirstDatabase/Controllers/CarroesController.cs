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
    public class CarroesController : Controller
    {
        private readonly ControParqueoContext _context;

        public CarroesController(ControParqueoContext context)
        {
            _context = context;
        }

        // GET: Carroes
        public async Task<IActionResult> Index()
        {
            var controParqueoContext = _context.Carros.Include(c => c.FkClienteNavigation);
            return View(await controParqueoContext.ToListAsync());
        }

        // GET: Carroes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Include(c => c.FkClienteNavigation)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // GET: Carroes/Create
        public IActionResult Create()
        {
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion");
            return View();
        }

        // POST: Carroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Color,Modelo,Tipo,FkCliente")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", carro.FkCliente);
            return View(carro);
        }

        // GET: Carroes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", carro.FkCliente);
            return View(carro);
        }

        // POST: Carroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Placa,Color,Modelo,Tipo,FkCliente")] Carro carro)
        {
            if (id != carro.Placa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.Placa))
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
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", carro.FkCliente);
            return View(carro);
        }

        // GET: Carroes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carros
                .Include(c => c.FkClienteNavigation)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Carroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if (carro != null)
            {
                _context.Carros.Remove(carro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(string id)
        {
            return _context.Carros.Any(e => e.Placa == id);
        }
    }
}
