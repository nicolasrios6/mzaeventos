using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MzaEventos.Data;
using MzaEventos.Models;
using MzaEventos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MzaEventos.Controllers
{
    [Authorize]
    public class EventoController : Controller
    {
        private readonly EventosDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly ImageStorage _imageStorage;

        public EventoController(EventosDbContext context, UserManager<Usuario> userManager, ImageStorage imageStorage)
        {
            _context = context;
            _userManager = userManager;
            _imageStorage = imageStorage;
        }

        // GET: Eventoes
        public async Task<IActionResult> Index()
        {
            var eventosDbContext = _context.Eventos.Include(e => e.Categoria);
            return View(await eventosDbContext.ToListAsync());
        }

        // GET: Eventoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventoes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        // POST: Eventoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventoCreateViewModel evento)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                string? imagenUrl = null;

                if(evento.Imagen != null)
                {
                    try
                    {
                        imagenUrl = await _imageStorage.SaveAsync(userId, evento.Imagen);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Imagen", $"Error al procesar la imagen: {ex.Message}");
                        return View(evento);
                    }
                }

                var nuevoEvento = new Evento
                {
                    Titulo = evento.Titulo,
                    Descripcion = evento.Descripcion,
                    FechaHora = evento.FechaHora,
                    Ubicacion = evento.Ubicacion,
                    CategoriaId = evento.CategoriaId,
                    LinkEntradas = evento.LinkEntradas,
                    UrlImagen = evento.UrlImagen
                };

                _context.Add(nuevoEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", evento.CategoriaId);
            return View(evento);
        }

        // GET: Eventoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", evento.CategoriaId);
            return View(evento);
        }

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaHora,Ubicacion,CategoriaId,LinkEntradas,Destacado,Activo,FechaCreacion,UrlImagen")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", evento.CategoriaId);
            return View(evento);
        }

        // GET: Eventoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
