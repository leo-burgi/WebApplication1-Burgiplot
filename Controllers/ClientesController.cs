using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClientesController : Controller
    {
        private readonly BurgiplotContext _context;
        public ClientesController(BurgiplotContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Cliente.ToListAsync();
            return View(clientes);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Dirección,Telefono,Correo,Dni")] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                foreach (var kv in ModelState.Where(x => x.Value!.Errors.Count > 0))
                {
                    Console.WriteLine($"Campo {kv.Key}: {string.Join(" | ", kv.Value!.Errors.Select(e => e.ErrorMessage))}");
                }
                return View(cliente);

            }
            cliente.CreatedAtUtc = DateTime.UtcNow;
            cliente.UpdateAtUtc = DateTime.UtcNow;

            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Dirección,Telefono,Correo,Dni,RowVer")] Cliente cliente)
        {
            if (id != cliente.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                foreach (var kv in ModelState.Where(x => x.Value!.Errors.Count > 0))
                    Console.WriteLine($"Campo {kv.Key}: {string.Join(" | ", kv.Value!.Errors.Select(e => e.ErrorMessage))}");
                return View(cliente);
            }

            try
            {
                // sólo actualizá campos editables
                cliente.UpdateAtUtc = DateTime.UtcNow;

                _context.Attach(cliente);
                _context.Entry(cliente).Property(c => c.Nombre).IsModified = true;
                _context.Entry(cliente).Property(c => c.Dirección).IsModified = true;
                _context.Entry(cliente).Property(c => c.Telefono).IsModified = true;
                _context.Entry(cliente).Property(c => c.Correo).IsModified = true;
                _context.Entry(cliente).Property(c => c.Dni).IsModified = true;
                _context.Entry(cliente).Property(c => c.UpdateAtUtc).IsModified = true;

                // RowVer se usa solo para concurrencia (no se marca como modificado)
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cliente.Any(e => e.Id == id))
                    return NotFound();

                // choque de concurrencia: recargá y avisá
                ModelState.AddModelError(string.Empty, "Otro usuario modificó este registro. Actualizá la página y reintentá.");
                var dbCliente = await _context.Cliente.AsNoTracking().FirstAsync(c => c.Id == id);
                return View(dbCliente);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, byte[]? rowVer)
        {
            var stub = new Cliente { Id = id };
            _context.Attach(stub);
            if (rowVer != null && rowVer.Length > 0)
            {
                _context.Entry(stub).Property(c => c.RowVer).OriginalValue = rowVer;
            }
            _context.Cliente.Remove(stub);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
