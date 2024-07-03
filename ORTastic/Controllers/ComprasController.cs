using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        // GET: Compras/Index
        public async Task<IActionResult> Index()
        {
            int? usuarioId = HttpContext.Session.GetInt32("IdUsuario");

            if (usuarioId == null)
            {
                // Manejar el caso en que el usuario no está identificado
                return RedirectToAction("Login", "Account"); // O cualquier otra acción apropiada
            }

            var compraDetalles = await (from compra in _context.Compras
                                        join usuario in _context.Usuarios on compra.UsuarioId equals usuario.Id
                                        join evento in _context.Eventos on compra.EventoId equals evento.Id
                                        where compra.UsuarioId == usuarioId
                                        select new CompraDetalle
                                        {
                                            IdCompra = compra.Id,
                                            NombreUsuario = usuario.Username,
                                            NombreEvento = evento.Nombreevento,
                                            CantidadEntradas = compra.CantidadEntradas,
                                            PrecioTotal = compra.Precio
                                        }).ToListAsync();

            ViewBag.CompraDetalles = compraDetalles;

            return View(compraDetalles);
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int? usuarioId = HttpContext.Session.GetInt32("IdUsuario");

            if (usuarioId == null)
            {
                // Manejar el caso en que el usuario no está identificado
                return RedirectToAction("Login", "Account"); // O cualquier otra acción apropiada
            }

            var compra = await _context.Compras
                .Where(c => c.UsuarioId == usuarioId && c.Id == id)
                .FirstOrDefaultAsync();

            if (compra == null)
            {
                return NotFound();
            }

            // Crear un modelo de vista para los detalles de la compra
            var compraDetalle = new CompraDetalle
            {
                IdCompra = compra.Id,
                NombreUsuario = _context.Usuarios.FirstOrDefault(u => u.Id == compra.UsuarioId)?.Username,
                NombreEvento = _context.Eventos.FirstOrDefault(e => e.Id == compra.EventoId)?.Nombreevento,
                CantidadEntradas = compra.CantidadEntradas,
                PrecioTotal = compra.Precio
            };

            return View(compraDetalle);
        }


        // GET: Compras/Comprar
        public IActionResult Comprar()
        {
            try
            {
                var eventos = _context.Eventos.ToList();
                var viewModel = new CompraViewModel
                {
                    Eventos = eventos,
                    Compra = new Compra()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cargar los eventos: " + ex.Message);
                return View(new CompraViewModel());
            }
        }


        // POST: Compras/Comprar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comprar(CompraViewModel viewModel)
        {
            // No es necesario validar el campo Eventos para esta acción
            ModelState.Remove("Eventos");

            if (ModelState.IsValid)
            {
                try
                {
                    var evento = await _context.Eventos.FindAsync(viewModel.Compra.EventoId);

                    if (evento != null)
                    {
                        if (evento.Fecha < DateTime.Now)
                        {
                            ModelState.AddModelError(string.Empty, "No se pueden comprar entradas para un evento pasado.");
                        }
                        else if (evento.Stock >= viewModel.CantidadEntradas)
                        {
                            evento.Stock -= viewModel.CantidadEntradas;
                            _context.Update(evento);

                            int? usuarioId = HttpContext.Session.GetInt32("IdUsuario");
                            if (usuarioId == null)
                            {
                                ModelState.AddModelError(string.Empty, "No se puede identificar al usuario.");
                            }
                            else
                            {
                                viewModel.Compra.UsuarioId = usuarioId.Value;
                                viewModel.Compra.CantidadEntradas = viewModel.CantidadEntradas;
                                viewModel.Compra.Precio = evento.Precio * viewModel.CantidadEntradas;

                                _context.Add(viewModel.Compra);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "No hay suficiente stock disponible.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El evento seleccionado no existe.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al procesar la compra: " + ex.Message);
                }
            }

            viewModel.Eventos = _context.Eventos.ToList();
            return View(viewModel);
        }

    }
}




