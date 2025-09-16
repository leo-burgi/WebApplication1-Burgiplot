using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FacturaItemsController : Controller
    {
        private readonly BurgiplotContext _context;

        public FacturaItemsController(BurgiplotContext context)
        {
            _context = context;
        }

        // GET: FacturaItems
        public async Task<IActionResult> Index()
        {
            var burgiplotContext = _context.FacturaItems.Include(f => f.Factura).Include(f => f.Material);
            return View(await burgiplotContext.ToListAsync());
        }

        // GET: FacturaItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaItem = await _context.FacturaItems
                .Include(f => f.Factura)
                .Include(f => f.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturaItem == null)
            {
                return NotFound();
            }

            return View(facturaItem);
        }

        // GET: FacturaItems/Create
        public IActionResult Create()
        {
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id");
            return View();
        }

        // POST: FacturaItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FacturaId,MaterialId,Descripcion,Cantidad,PrecioUnit")] FacturaItem facturaItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturaItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id", facturaItem.FacturaId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id", facturaItem.MaterialId);
            return View(facturaItem);
        }

        // GET: FacturaItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaItem = await _context.FacturaItems.FindAsync(id);
            if (facturaItem == null)
            {
                return NotFound();
            }
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id", facturaItem.FacturaId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id", facturaItem.MaterialId);
            return View(facturaItem);
        }

        // POST: FacturaItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacturaId,MaterialId,Descripcion,Cantidad,PrecioUnit")] FacturaItem facturaItem)
        {
            if (id != facturaItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaItemExists(facturaItem.Id))
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
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "Id", "Id", facturaItem.FacturaId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id", facturaItem.MaterialId);
            return View(facturaItem);
        }

        // GET: FacturaItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaItem = await _context.FacturaItems
                .Include(f => f.Factura)
                .Include(f => f.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturaItem == null)
            {
                return NotFound();
            }

            return View(facturaItem);
        }

        // POST: FacturaItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturaItem = await _context.FacturaItems.FindAsync(id);
            if (facturaItem != null)
            {
                _context.FacturaItems.Remove(facturaItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaItemExists(int id)
        {
            return _context.FacturaItems.Any(e => e.Id == id);
        }
    }
}
