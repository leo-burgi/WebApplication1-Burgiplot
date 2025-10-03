using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.Data.SqlClient;

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

            try
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {

                    case 50011: // Error de trigger personalizado
                        ModelState.AddModelError(nameof(Cliente.DNI), "DNI incorrecto; debe tener 7 u 8 digitos.");
                        break;
                    case 50010:
                        ModelState.AddModelError(nameof(Cliente.CUIT_CUIL), "CUIT/CUIL incorrecto; debe tener 11 digitos");
                        break;
                    case 2627: // Violación de restricción de clave única
                    case 2601: // Violación de restricción de clave única (índice único)
                        ModelState.AddModelError(nameof(Cliente.CUIT_CUIL), "El CUIT/CUIL ya existe en la base de datos.");
                        break;
                    
                    default:
                        ModelState.AddModelError(string.Empty, $"Error de base de datos ({sqlEx.Number}).Detalle: {sqlEx.Message}");
                        break;
                }
                return View(cliente);
            }
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
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clientes.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                switch (sqlEx.Number)
                {
                    case 50011: // Error de trigger personalizado
                        ModelState.AddModelError(nameof(Cliente.DNI), "DNI incorrecto; debe tener 7 u 8 digitos.");
                        break;
                    case 50010:
                        ModelState.AddModelError(nameof(Cliente.CUIT_CUIL), "CUIT/CUIL incorrecto; debe tener 11 digitos");
                        break;
                    case 2627: // Violación de restricción de clave única
                    case 2601: // Violación de restricción de clave única (índice único)
                        ModelState.AddModelError(nameof(Cliente.CUIT_CUIL), "El CUIT/CUIL ya existe en la base de datos.");
                        break;
                    
                    default:
                        ModelState.AddModelError(string.Empty, $"Error de base de datos ({sqlEx.Number}).Detalle: {sqlEx.Message}");
                        break;
                }
                return View(cliente);
            }
            
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
