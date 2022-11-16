using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using princeshop.Data;
using princeshop.Models;
using Microsoft.EntityFrameworkCore;

using OfficeOpenXml;
using OfficeOpenXml.Table;
using Rotativa.AspNetCore;

namespace princeshop.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly ApplicationDbContext _context;

        public PedidoController(ILogger<PedidoController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var pedidos=from o in _context.DataPedido select o;
            pedidos=pedidos.Include(p =>p.pago);
            return  View(pedidos.ToList());
        }
        public IActionResult ExportarExcel()
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var pedidos = _context.DataPedido.AsNoTracking().ToList();
            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add("Pedidos");
                worksheet.Cells["A1"].LoadFromCollection(pedidos, PrintHeaders: true);
                for (var col = 1; col < pedidos.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }
                // Agregar formato de tabla
                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: pedidos.Count + 1, toColumn: 2), "Pedidos");
                tabla.ShowHeader = true;
                tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = true;

                return File(libro.GetAsByteArray(), excelContentType, "Pedidos.xlsx");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}