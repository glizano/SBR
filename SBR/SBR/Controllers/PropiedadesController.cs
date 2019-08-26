using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SBR.Data;
using SBR.Models;

namespace SBR.Controllers
{
    [Authorize]
    public class PropiedadesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PropiedadesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this._hostingEnvironment = hostingEnvironment;
        }

        // GET: Propiedades
        public async Task<IActionResult> Index()
        {
            var p = await _context.Propiedades.ToListAsync();
            foreach (Propiedad propiedad in p)
            {
                var filesPath = $"{this._hostingEnvironment.WebRootPath}\\files\\propiedades\\{propiedad.Id}";
                string[] fileEntries = Directory.GetFiles(filesPath).Select(Path.GetFileName).ToArray();
                foreach (string fileName in fileEntries)
                    propiedad.imagenes.Add(fileName);
            }
            return View(p);
        }

        // GET: Propiedades/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propiedad = await _context.Propiedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propiedad == null)
            {
                return NotFound();
            }

            return View(propiedad);
        }

        // GET: Propiedades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Propiedades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Propiedad propiedad, IFormFile imgPrincipal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propiedad);
                await _context.SaveChangesAsync();

                var filesPath = $"{this._hostingEnvironment.WebRootPath}\\files\\propiedades\\{propiedad.Id}";
                var fileName = ContentDispositionHeaderValue.Parse(imgPrincipal.ContentDisposition).FileName;

                fileName = fileName.Contains("\\")
                ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                : fileName.Trim('"');

                fileName = verifyFileName(fileName, filesPath);

                var fullFilePath = Path.Combine(filesPath, fileName);               

                if (imgPrincipal != null && imgPrincipal.Length > 0)
                {
                    if (!Directory.Exists(filesPath))
                        Directory.CreateDirectory(filesPath);
                    using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await imgPrincipal.CopyToAsync(stream);
                    }
                }               

                return RedirectToAction(nameof(Index));
            }
            return View(propiedad);
        }

        // GET: Propiedades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad == null)
            {
                return NotFound();
            }
            return View(propiedad);
        }

        // POST: Propiedades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Precio,Moneda,Descripcion,NombrePropietario,ContactoPropietario")] Propiedad propiedad)
        {
            if (id != propiedad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propiedad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropiedadExists(propiedad.Id))
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
            return View(propiedad);
        }

        // GET: Propiedades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propiedad = await _context.Propiedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propiedad == null)
            {
                return NotFound();
            }

            return View(propiedad);
        }

        // POST: Propiedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propiedad = await _context.Propiedades.FindAsync(id);
            _context.Propiedades.Remove(propiedad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropiedadExists(int id)
        {
            return _context.Propiedades.Any(e => e.Id == id);
        }

        public static string GetMimeTypeByWindowsRegistry(string fileNameOrExtension)
        {
            string mimeType = "application/unknown";
            string ext = (fileNameOrExtension.Contains(".")) ? System.IO.Path.GetExtension(fileNameOrExtension).ToLower() : "." + fileNameOrExtension;
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public bool UploadFiles(IFormFile file)
        {
            var filesPath = $"{this._hostingEnvironment.WebRootPath}\\files";
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

            // Ensure the file name is correct
            fileName = fileName.Contains("\\")
                ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                : fileName.Trim('"');

            fileName = verifyFileName(fileName, filesPath);

            var fullFilePath = Path.Combine(filesPath, fileName);
            if (!Directory.Exists(filesPath))
                Directory.CreateDirectory(filesPath);
            using (FileStream stream = new FileStream(fullFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return true;
        }

        public static string verifyFileName(string fileName, string path)
        {
            fileName = fileName.Replace('+', ' ');
            var fullFilePath = Path.Combine(path, fileName);
            var ext = Path.GetExtension(fileName);
            var noExt = Path.GetFileNameWithoutExtension(fileName);
            int cont = 1;

            while (System.IO.File.Exists(fullFilePath))
            {
                fileName = noExt + "(" + cont + ")" + ext;
                fullFilePath = Path.Combine(path, fileName);
                cont++;
            }
            return fileName;
        }
    }
}
