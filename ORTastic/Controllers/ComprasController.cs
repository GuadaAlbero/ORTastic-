using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ORTastic.Context;
using ORTastic.Models;


namespace ORTastic.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ORTasticDatabaseContext _context;

        public ComprasController(ORTasticDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Compras.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Eventos/comprar
        public async Task<IActionResult> Comprar()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,EventoId,Precio")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                compra.UsuarioId = HttpContext.Session.GetInt32("IdUsuario").Value;
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compra);
        }

    }
}


