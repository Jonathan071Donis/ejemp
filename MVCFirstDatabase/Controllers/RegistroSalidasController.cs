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
    public class RegistroSalidasController : Controller
    {
        private readonly ControParqueoContext _context;

        public RegistroSalidasController(ControParqueoContext context)
        {
            _context = context;
        }

        // GET: RegistroSalidas
        public async Task<IActionResult> Index()
        {
            var controParqueoContext = _context.RegistroSalidas.Include(r => r.FkCarroNavigation).Include(r => r.FkClienteNavigation).Include(r => r.FkParqueoNavigation).Include(r => r.FkTarifaNavigation).Include(r => r.IdNavigation);
            return View(await controParqueoContext.ToListAsync());
        }

        // GET: RegistroSalidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroSalida = await _context.RegistroSalidas
                .Include(r => r.FkCarroNavigation)
                .Include(r => r.FkClienteNavigation)
                .Include(r => r.FkParqueoNavigation)
                .Include(r => r.FkTarifaNavigation)
                .Include(r => r.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroSalida == null)
            {
                return NotFound();
            }

            return View(registroSalida);
        }

        // GET: RegistroSalidas/Create
        public IActionResult Create()
        {
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa");
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion");
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero");
            ViewData["FkTarifa"] = new SelectList(_context.Tarifas, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.RegistroIngresos, "Id", "Id");
            return View();
        }

        // POST: RegistroSalidas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FkCliente,FkCarro,FkParqueo,HoraIngreso,HoraSalida,TotalHoras,FkTarifa,Descuento,TotalCalculado")] RegistroSalida registroSalida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroSalida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa", registroSalida.FkCarro);
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", registroSalida.FkCliente);
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero", registroSalida.FkParqueo);
            ViewData["FkTarifa"] = new SelectList(_context.Tarifas, "Id", "Id", registroSalida.FkTarifa);
            ViewData["Id"] = new SelectList(_context.RegistroIngresos, "Id", "Id", registroSalida.Id);
            return View(registroSalida);
        }

        // GET: RegistroSalidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroSalida = await _context.RegistroSalidas.FindAsync(id);
            if (registroSalida == null)
            {
                return NotFound();
            }
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa", registroSalida.FkCarro);
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", registroSalida.FkCliente);
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero", registroSalida.FkParqueo);
            ViewData["FkTarifa"] = new SelectList(_context.Tarifas, "Id", "Id", registroSalida.FkTarifa);
            ViewData["Id"] = new SelectList(_context.RegistroIngresos, "Id", "Id", registroSalida.Id);
            return View(registroSalida);
        }

        // POST: RegistroSalidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FkCliente,FkCarro,FkParqueo,HoraIngreso,HoraSalida,TotalHoras,FkTarifa,Descuento,TotalCalculado")] RegistroSalida registroSalida)
        {
            if (id != registroSalida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroSalida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroSalidaExists(registroSalida.Id))
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
            ViewData["FkCarro"] = new SelectList(_context.Carros, "Placa", "Placa", registroSalida.FkCarro);
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", registroSalida.FkCliente);
            ViewData["FkParqueo"] = new SelectList(_context.Parqueos, "Numero", "Numero", registroSalida.FkParqueo);
            ViewData["FkTarifa"] = new SelectList(_context.Tarifas, "Id", "Id", registroSalida.FkTarifa);
            ViewData["Id"] = new SelectList(_context.RegistroIngresos, "Id", "Id", registroSalida.Id);
            return View(registroSalida);
        }

        // GET: RegistroSalidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroSalida = await _context.RegistroSalidas
                .Include(r => r.FkCarroNavigation)
                .Include(r => r.FkClienteNavigation)
                .Include(r => r.FkParqueoNavigation)
                .Include(r => r.FkTarifaNavigation)
                .Include(r => r.IdNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroSalida == null)
            {
                return NotFound();
            }

            return View(registroSalida);
        }

        // POST: RegistroSalidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroSalida = await _context.RegistroSalidas.FindAsync(id);
            if (registroSalida != null)
            {
                _context.RegistroSalidas.Remove(registroSalida);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroSalidaExists(int id)
        {
            return _context.RegistroSalidas.Any(e => e.Id == id);
        }
    }
}
