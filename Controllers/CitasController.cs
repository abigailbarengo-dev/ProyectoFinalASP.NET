using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalLab.Models;
using X.PagedList.Extensions;
using System.Data;
using System.IO;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using ProyectoFinalLab.Data;

namespace ProyectoFinalLab.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationContext _context;

        public CitasController(ApplicationContext context)
        {
            _context = context;
        }



        // GET: Citas
        public async Task<IActionResult> Index(string buscar, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            // Incluyendo las entidades relacionadas
            var citas = _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Veterinario)
                .Include(c => c.Mascota)
                .AsQueryable();

            if (!String.IsNullOrEmpty(buscar))
            {
                citas = citas.Where(s => s.Estado.Contains(buscar));
            }

            var citaList = await citas.OrderByDescending(s => s.Id).ToListAsync();
            var citasPaginadas = citaList.ToPagedList(pageNumber, pageSize);

            return View(citasPaginadas);
        }


        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public async Task<IActionResult> Create()
        {
            // Obtener las listas de Veterinarios, Clientes y Mascotas de manera asincrónica
            ViewData["VeterinarioId"] = new SelectList(await _context.Veterinario.ToListAsync(), "Id", "Nombre");
            ViewData["ClienteId"] = new SelectList(await _context.Clientes.ToListAsync(), "Id", "Nombre");
            ViewData["MascotaId"] = new SelectList(await _context.Mascotas.ToListAsync(), "Id", "Nombre");

            return View();
        }


        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]                          // fijarse si nombrevet genera problmea
        public async Task<IActionResult> Create([Bind("Id,Fecha,Hora,Motivo,Estado, NombreVet, ClienteId, MascotaId, VeterinarioId")] Citas cita)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["VeterinarioId"] = new SelectList(await _context.Veterinario.ToListAsync(), "Id", "Nombre");
            ViewData["ClienteId"] = new SelectList(await _context.Clientes.ToListAsync(), "Id", "Nombre");
            ViewData["MascotaId"] = new SelectList(await _context.Mascotas.ToListAsync(), "Id", "Nombre");

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Hora,Motivo,Estado, NombreVet, ClienteId, MascotaId, VeterinarioId")] Citas cita)
        {
            if (id != cita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.Id))
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
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}
