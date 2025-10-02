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
    public class OrdenItemsController : Controller
    {
        private readonly BurgiplotContext _context;

        public OrdenItemsController(BurgiplotContext context)
        {
            _context = context;
        }

        // GET: OrdenItems
        public async Task<IActionResult> Index()
        {
            var burgiplotContext = _context.OrdenItems.Include(o => o.Material).Include(o => o.Orden);
            return View(await burgiplotContext.ToListAsync());
        }

        // GET: OrdenItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenItem = await _context.OrdenItems
                .Include(o => o.Material)
                .Include(o => o.Orden)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenItem == null)
            {
                return NotFound();
            }

            return View(ordenItem);
        }

        // GET: OrdenItems/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id");
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id");
            return View();
        }

        // POST: OrdenItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrdenId,MaterialId,Cantidad,PrecioUnit,Total")] OrdenItem ordenItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id", ordenItem.MaterialId);
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id", ordenItem.OrdenId);
            return View(ordenItem);
        }

        // GET: OrdenItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenItem = await _context.OrdenItems.FindAsync(id);
            if (ordenItem == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id", ordenItem.MaterialId);
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id", ordenItem.OrdenId);
            return View(ordenItem);
        }

        // POST: OrdenItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrdenId,MaterialId,Cantidad,PrecioUnit,Total")] OrdenItem ordenItem)
        {
            if (id != ordenItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenItemExists(ordenItem.Id))
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
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Id", ordenItem.MaterialId);
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id", ordenItem.OrdenId);
            return View(ordenItem);
        }

        // GET: OrdenItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenItem = await _context.OrdenItems
                .Include(o => o.Material)
                .Include(o => o.Orden)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenItem == null)
            {
                return NotFound();
            }

            return View(ordenItem);
        }

        // POST: OrdenItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenItem = await _context.OrdenItems.FindAsync(id);
            if (ordenItem != null)
            {
                _context.OrdenItems.Remove(ordenItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenItemExists(int id)
        {
            return _context.OrdenItems.Any(e => e.Id == id);
        }
    }
}
