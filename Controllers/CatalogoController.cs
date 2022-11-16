using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using princeshop.Data;
using princeshop.Models;

namespace princeshop.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager; 

        public CatalogoController(ILogger<CatalogoController> logger,ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? searchString){
            var productos= from o in _context.DataProductos select o;
            productos=productos.Where(s => s.Status=="Activo"); 
            if(searchString=="Invierno" || searchString=="Verano"){
                productos = productos.Where(s => s.Temporada==searchString);
            }else if(!String.IsNullOrEmpty(searchString)){
                productos = productos.Where(s => s.Nombre.Contains(searchString));
            }
      
            return View(await productos.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id){
            Producto objProduct = await _context.DataProductos.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }
         public async Task<IActionResult> Add(int? id,string? talla){
            
            var userID = _userManager.GetUserName(User);
            if(userID==null){
                ViewData["Message"]="Por favor debe loguearse antes de agregar un producto";
                List<Producto> productos= new List<Producto>();
                return View("Index",productos);
            }else{
                var producto = await _context.DataProductos.FindAsync(id);    
                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                proforma.Precio = producto.Precio;
                proforma.Cantidad = 1;
                proforma.UserID = userID;
                proforma.Talla=talla;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}