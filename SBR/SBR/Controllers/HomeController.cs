using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SBR.Data;
using SBR.Models;

namespace SBR.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string categoria, string operacion, string provincia)
        {
            var p = _context.Propiedades.ToList();
            if (!string.IsNullOrEmpty(categoria))
            {
                p=p.Where(m => m.Categoria == categoria).ToList();
            }
            if (!string.IsNullOrEmpty(operacion))
            {
                p = p.Where(m => m.Tipo == operacion).ToList();
            }
            if (!string.IsNullOrEmpty(provincia))
            {
                p = p.Where(m => m.Provincia == provincia).ToList();
            }
            foreach (Propiedad propiedad in p)
            {
                var filesPath = $"{this._hostingEnvironment.WebRootPath}\\files\\propiedades\\{propiedad.Id}";
                string[] fileEntries = Directory.GetFiles(filesPath).Select(Path.GetFileName).ToArray();
                foreach (string fileName in fileEntries)
                    propiedad.imagenes.Add(fileName);
            }
            ViewBag.categoria = categoria;
            ViewBag.operacion = operacion;
            ViewBag.provincia = provincia;
            return View(p);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
