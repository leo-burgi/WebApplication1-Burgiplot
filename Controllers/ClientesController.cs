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
    public class ClientesController : Controller
    {
        //---------Comunicación con la base de datos
        private readonly BurgiplotContext _context;
        public ClientesController(BurgiplotContext context)
        {
            _context = context;
        }

        //------------------------------------INDEX--------------------------------------
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return View(clientes);
        }

        //----------------------------------DETALLES-------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        //------------------------------------CREAR--------------------------------------
        // GET(MOSTRAR FORMULARIO)
        public IActionResult Create()
        {
            return View();
        }

        // POST(VALIDAR Y RECIBIR DATOS)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid) return View(cliente);
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //--------------------------------------EDITAR--------------------------------------
        // GET(MOSTRAR FORMULARIO CON DATOS DEL CLIENTE)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // POST(VALIDAR Y ACTUALIZAR DATOS)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id) return NotFound();
            if (!ModelState.IsValid) return View(cliente);
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clientes.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        //-------------------------------------ELIMINAR--------------------------------------
        // GET(MOSTRAR FORMULARIO CON DATOS)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST(VALIDAR Y ELIMINAR DATOS)(coincidir nombres sino renombrar #ojo scaffold)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
