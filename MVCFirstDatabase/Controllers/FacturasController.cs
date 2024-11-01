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
    public class FacturasController : Controller
    {
        private readonly ControParqueoContext _context;

        public FacturasController(ControParqueoContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var controParqueoContext = _context.Facturas.Include(f => f.FkClienteNavigation).Include(f => f.NoFacturaNavigation);
            return View(await controParqueoContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.FkClienteNavigation)
                .Include(f => f.NoFacturaNavigation)
                .FirstOrDefaultAsync(m => m.NoFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion");
            ViewData["NoFactura"] = new SelectList(_context.RegistroSalidas, "Id", "Id");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoFactura,FkCliente,Nit,Vehiculo,Total,Descuento")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", factura.FkCliente);
            ViewData["NoFactura"] = new SelectList(_context.RegistroSalidas, "Id", "Id", factura.NoFactura);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", factura.FkCliente);
            ViewData["NoFactura"] = new SelectList(_context.RegistroSalidas, "Id", "Id", factura.NoFactura);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoFactura,FkCliente,Nit,Vehiculo,Total,Descuento")] Factura factura)
        {
            if (id != factura.NoFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.NoFactura))
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
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "NoIdentificacion", "NoIdentificacion", factura.FkCliente);
            ViewData["NoFactura"] = new SelectList(_context.RegistroSalidas, "Id", "Id", factura.NoFactura);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.FkClienteNavigation)
                .Include(f => f.NoFacturaNavigation)
                .FirstOrDefaultAsync(m => m.NoFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.NoFactura == id);
        }
    }
}
