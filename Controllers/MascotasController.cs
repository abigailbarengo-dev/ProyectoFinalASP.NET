using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalLab.Data;
using ProyectoFinalLab.Models;
using X.PagedList.Extensions;

namespace ProyectoFinalLab.Controllers
{
    public class MascotasController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MascotasController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Mascotas      
        public IActionResult Index(string buscar, int? page, string filtro)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var mascotas = from mascota in _context.Mascotas select mascota;

            // Filtro por nombre si se proporciona
            if (!String.IsNullOrEmpty(buscar))
            {
                mascotas = mascotas.Where(s => s.Nombre!.Contains(buscar));
            }

            // Lógica de ordenación basada en el filtro (fijarse en URL dice ascendente/descendente al cambiar orden)
            switch (filtro)
            {
                case "NombreDescendente":
                    mascotas = mascotas.OrderByDescending(m => m.Nombre);
                    ViewData["FiltroNombre"] = "NombreAscendente";
                    break;
                case "RazaDescendente":
                    mascotas = mascotas.OrderByDescending(m => m.Raza);
                    ViewData["FiltroRaza"] = "RazaAscendente";
                    break;
                case "RazaAscendente":
                    mascotas = mascotas.OrderBy(m => m.Raza);
                    ViewData["FiltroRaza"] = "RazaDescendente";
                    break;
                case "NombreAscendente":
                    mascotas = mascotas.OrderBy(m => m.Nombre);
                    ViewData["FiltroNombre"] = "NombreDescendente";
                    break;
                default:
                    mascotas = mascotas.OrderBy(m => m.Nombre);
                    ViewData["FiltroNombre"] = "NombreDescendente";
                    mascotas = mascotas.OrderBy(m => m.Raza);
                    ViewData["FiltroRaza"] = "RazaAscendente";
                    break;
            }


            var mascotasPaginadas = mascotas.ToPagedList(pageNumber, pageSize);


            return View(mascotasPaginadas);
        }



        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // GET: Mascotas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Especie,Raza,FechaNacimiento, Hora, Sexo, Imagen")] Mascota mascota, IFormFile imagen)
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
                        if (mascota.Imagen != null)
                        {
                            var archivoViejo = Path.Combine(pathDestino, mascota.Imagen!);
                            if (System.IO.File.Exists(archivoViejo))
                            {
                                System.IO.File.Delete(archivoViejo);
                            }
                        }
                        mascota.Imagen = archivoDestino;
                    }
                }
            }

            _context.Add(mascota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(mascota);
        }


        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Especie,Raza,FechaNacimiento, Hora, Sexo, Imagen")] Mascota mascota, IFormFile imagen)
        {
            if (id != mascota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                            if (mascota.Imagen != null)
                            {
                                var archivoViejo = Path.Combine(pathDestino, mascota.Imagen!);
                                if (System.IO.File.Exists(archivoViejo))
                                {
                                    System.IO.File.Delete(archivoViejo);
                                }
                            }
                            mascota.Imagen = archivoDestino;
                        }
                    }
                }
                try
                {
                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.Id))
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
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                // Elimina el archivo de imagen si existe
                if (!string.IsNullOrEmpty(mascota.Imagen))
                {
                    string ruta = Path.Combine(_webHostEnvironment.WebRootPath, mascota.Imagen.TrimStart('/'));
                    if (System.IO.File.Exists(ruta))
                    {
                        System.IO.File.Delete(ruta);
                    }
                }

                _context.Mascotas.Remove(mascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
            return _context.Mascotas.Any(e => e.Id == id);
        }

        
    }
}


