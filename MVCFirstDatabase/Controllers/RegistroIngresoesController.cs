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
    public class RegistroIngresoesController : Controller
    {
        private readonly ControParqueoContext _context;

        public RegistroIngresoesController(ControParqueoContext context)
        {
            _context = context;
        }

        // GET: RegistroIngresoes
        public async Task<IActionResult> Index()
        {
            var controParqueoContext = _context.RegistroIngresos.Include(r => r.FkCarroNavigation).Include(r => r.FkClienteNavigation).Include(r => r.FkParqueoNavigation);
            return View(await controParqueoContext.ToListAsync());
        }

        // GET: RegistroIngresoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroIngreso = await _context.RegistroIngresos
                .Include(r => r.FkCarroNavigation)
                .Include(r => r.FkClienteNavigation)
                .Include(r => r.FkParqueoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroIngreso == null)
            {
                return NotFound();
            }

            return View(registroIngreso);
        }

        // GET: RegistroIngresoes/Create
        public IActionResult Create()
        {
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa");
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion");
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero");
            return View();
        }

        // POST: RegistroIngresoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FkCliente,FkCarro,FechaHoraIngreso,FkParqueo")] RegistroIngreso registroIngreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroIngreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa", registroIngreso.FkCarro);
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", registroIngreso.FkCliente);
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero", registroIngreso.FkParqueo);
            return View(registroIngreso);
        }

        // GET: RegistroIngresoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroIngreso = await _context.RegistroIngresos.FindAsync(id);
            if (registroIngreso == null)
            {
                return NotFound();
            }
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa", registroIngreso.FkCarro);
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", registroIngreso.FkCliente);
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero", registroIngreso.FkParqueo);
            return View(registroIngreso);
        }

        // POST: RegistroIngresoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FkCliente,FkCarro,FechaHoraIngreso,FkParqueo")] RegistroIngreso registroIngreso)
        {
            if (id != registroIngreso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroIngreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroIngresoExists(registroIngreso.Id))
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
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa", registroIngreso.FkCarro);
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", registroIngreso.FkCliente);
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero", registroIngreso.FkParqueo);
            return View(registroIngreso);
        }

        // GET: RegistroIngresoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroIngreso = await _context.RegistroIngresos
                .Include(r => r.FkCarroNavigation)
                .Include(r => r.FkClienteNavigation)
                .Include(r => r.FkParqueoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroIngreso == null)
            {
                return NotFound();
            }

            return View(registroIngreso);
        }

        // POST: RegistroIngresoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroIngreso = await _context.RegistroIngresos.FindAsync(id);
            if (registroIngreso != null)
            {
                _context.RegistroIngresos.Remove(registroIngreso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroIngresoExists(int id)
        {
            return _context.RegistroIngresos.Any(e => e.Id == id);
        }
    }
}
