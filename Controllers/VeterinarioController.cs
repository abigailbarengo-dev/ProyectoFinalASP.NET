using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalLab.Data;
using ProyectoFinalLab.Models;
using X.PagedList.Extensions;

namespace ProyectoFinalLab.Controllers
{
    public class VeterinarioController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VeterinarioController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Veterinario
        public async Task<IActionResult> Index(string buscar, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var vet = from vete in _context.Veterinario select vete;

            if (!String.IsNullOrEmpty(buscar))
            {
                vet = vet.Where(s => s.Nombre!.Contains(buscar));
            }

            var vetList = await vet.OrderByDescending(s => s.Id).ToListAsync();
            var vetesPaginados = vetList.ToPagedList(pageNumber, pageSize);

            return View(vetesPaginados);
        }


        // GET: Veterinario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // GET: Veterinario/Create
        [Authorize(Roles = "Admin,Manager")]    // NO CAMBIAR NOMBRES DE ADMIN Y MANAGER
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterinario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nombre, Especialidad, Imagen")] Veterinario veterinario, IFormFile imagen)
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivoFoto = archivos[0];
                if (archivoFoto.Length > 0)
                {
                    var pathDestino = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos");

                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "");
                    var extension = Path.GetExtension(archivoFoto.FileName);
                    archivoDestino += extension;

                    using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                    {
                        archivoFoto.CopyTo(filestream);
                        if (veterinario.Imagen != null)
                        {
                            var archivoViejo = Path.Combine(pathDestino, veterinario.Imagen!);
                            if (System.IO.File.Exists(archivoViejo))
                            {
                                System.IO.File.Delete(archivoViejo);
                            }
                        }
                        veterinario.Imagen = archivoDestino;
                    }
                }
            }

            _context.Add(veterinario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(veterinario);
        }


        // GET: Veterinario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Especialidad, Imagen")] Veterinario veterinario)
        {
            if (id != veterinario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarioExists(veterinario.Id))
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
            return View(veterinario);
        }

        // GET: Veterinario/Delete/5
        [Authorize(Roles = "Admin,Manager")]    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // POST: Veterinario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinario = await _context.Veterinario.FindAsync(id);
            if (veterinario != null)
            {
                _context.Veterinario.Remove(veterinario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarioExists(int id)
        {
            return _context.Veterinario.Any(e => e.Id == id);
        }
    }
}
