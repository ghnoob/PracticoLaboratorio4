using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticoLaboratorio4.Data;
using PracticoLaboratorio4.Models;
using PracticoLaboratorio4.Utils;

namespace PracticoLaboratorio4.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly ApplicationDbContext _context;

        private const string ImagePathWeb = "/img/peliculas/";
        private readonly string imagePathServer;

        public PeliculasController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;

            imagePathServer = Path.Combine(env.WebRootPath, "img", "peliculas");
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 5;
            return View(await PaginatedList<Pelicula>.CreateAsync(
                _context.Peliculas
                    .AsNoTracking()
                    .Include(p => p.Genero)
                    .Include(p => p.Director)
                    .OrderBy(p => p.Titulo),
                pageNumber ?? 1,
                pageSize
            ));
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Director)
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(
                _context.Directores.AsNoTracking().OrderBy(d => d.Nombre),
                "Id",
                "Nombre"
            );

            ViewData["GeneroId"] = new SelectList(
                _context.Generos.AsNoTracking().OrderBy(g => g.Descripcion),
                "Id",
                "Descripcion"
            );

            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,GeneroId,DirectorId,Resumen,FechaEstreno,ImagenPortada,Trailer")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                if (pelicula.ImagenPortada != null)
                {
                    pelicula.UrlImagenPortada = ImagePathWeb + FileManager.Create(pelicula.ImagenPortada, imagePathServer);
                }

                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DirectorId"] = new SelectList(
                _context.Directores.AsNoTracking().OrderBy(d => d.Nombre),
                "Id",
                "Nombre"
            );

            ViewData["GeneroId"] = new SelectList(
                _context.Generos.AsNoTracking().OrderBy(g => g.Descripcion),
                "Id",
                "Descripcion"
            );

            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            ViewData["DirectorId"] = new SelectList(
                _context.Directores.AsNoTracking().OrderBy(d => d.Nombre),
                "Id",
                "Nombre"
            );

            ViewData["GeneroId"] = new SelectList(
                _context.Generos.AsNoTracking().OrderBy(g => g.Descripcion),
                "Id",
                "Descripcion"
            );

            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,GeneroId,DirectorId,Resumen,FechaEstreno,ImagenPortada,UrlImagenPortada,Trailer")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (pelicula.ImagenPortada != null)
                {
                    if (!string.IsNullOrEmpty(pelicula.UrlImagenPortada))
                    {
                        FileManager.Delete(Path.Combine(imagePathServer, pelicula.UrlImagenPortada.Split('/').Last()));
                    }

                    pelicula.UrlImagenPortada = ImagePathWeb + FileManager.Create(pelicula.ImagenPortada, imagePathServer);
                }

                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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

            ViewData["DirectorId"] = new SelectList(
                _context.Directores.AsNoTracking().OrderBy(d => d.Nombre),
                "Id",
                "Nombre"
            );

            ViewData["GeneroId"] = new SelectList(
                _context.Generos.AsNoTracking().OrderBy(g => g.Descripcion),
                "Id",
                "Descripcion"
            );

            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Director)
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);

            if (!string.IsNullOrEmpty(pelicula.UrlImagenPortada))
            {
                FileManager.Delete(Path.Combine(imagePathServer, pelicula.UrlImagenPortada.Split('/').Last()));
            }

            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
