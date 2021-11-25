using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticoLaboratorio4.Data;
using PracticoLaboratorio4.Models;
using PracticoLaboratorio4.Utils;
using Microsoft.AspNetCore.Hosting;

namespace PracticoLaboratorio4.Controllers
{
    public class DirectoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        private const string ImagePathWeb = "/img/directores/";
        private readonly string imagePathServer;

        public DirectoresController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;

            imagePathServer = Path.Combine(env.WebRootPath, "img", "directores");
        }

        // GET: Directores
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 5;
            return View(await PaginatedList<Director>.CreateAsync(
                _context.Directores.AsNoTracking().OrderBy(d => d.Nombre),
                pageNumber ?? 1,
                pageSize
            ));
        }

        // GET: Directores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directores
                .Include(m => m.Peliculas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Directores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Biografia,Foto")] Director director)
        {
            if (ModelState.IsValid)
            {
                if (director.Foto != null)
                {
                    director.UrlFoto = ImagePathWeb + FileManager.Create(director.Foto, imagePathServer);
                }
                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(director);
        }

        // GET: Directores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directores.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // POST: Directores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Biografia,Foto,UrlFoto")] Director director)
        {
            if (id != director.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (director.Foto != null)
                {
                    if (!string.IsNullOrEmpty(director.UrlFoto))
                    {
                        FileManager.Delete(Path.Combine(imagePathServer, director.UrlFoto.Split('/').Last()));
                    }

                    director.UrlFoto = ImagePathWeb + FileManager.Create(director.Foto, imagePathServer);
                }

                try
                {
                    _context.Update(director);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.Id))
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
            return View(director);
        }

        // GET: Directores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Directores.FindAsync(id);

            if (!string.IsNullOrEmpty(director.UrlFoto))
            {
                FileManager.Delete(Path.Combine(imagePathServer, director.UrlFoto.Split('/').Last()));
            }

            _context.Directores.Remove(director);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorExists(int id)
        {
            return _context.Directores.Any(e => e.Id == id);
        }
    }
}
