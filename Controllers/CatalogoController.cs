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

        public CatalogoController(ILogger<CatalogoController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchString){
            var productos= from o in _context.DataProductos select o; 
            if(searchString=="Invierno" || searchString=="Verano"){
                productos = productos.Where(s => s.Temporada==searchString);
            }else if(!String.IsNullOrEmpty(searchString)){
                productos = productos.Where(s => s.Nombre.Contains(searchString));
            }
      
            return View(await productos.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}