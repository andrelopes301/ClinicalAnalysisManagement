using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabTestesOnline.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LabTestesOnline.ViewModels;

namespace LabTestesOnline.Controllers
{
    [Authorize(Roles = "Admin,Gestor")]
    public class CentroAnalisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CentroAnalisesController(ApplicationDbContext context)
        {
            _context = context;
        }
/*
        // GET: CentroAnalises
        public async Task<IActionResult> Index()
        {


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Gestor")) {
                return View(await _context.CentrosAnalises.Include(c => c.Gestor)
                .Where(m => m.Gestor.Id.ToString() == userId).ToListAsync());
            }
            else {
                return View(await _context.CentrosAnalises.ToListAsync());
            }

        }
*/

        public async Task<IActionResult> Index(int? page)
        {

            var cvm = new CentrosAnalisesViewModel();
            int pagina = (page == null || page < 1) ? 1 : page.Value;
            int nreg = 5;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IQueryable<CentroAnalise> centroAnalises;

       // IQueryable<CentroAnalise> centroAnalises = _context.TestesPossiveis.Include(p => p.CentroAnalise).AsQueryable();

            if (User.IsInRole("Gestor"))
            {

                centroAnalises = _context.CentrosAnalises.Include(c => c.Gestor)
               .Where(m => m.Gestor.Id.ToString() == userId).AsQueryable();

            }
            else
            {
               centroAnalises = _context.CentrosAnalises.AsQueryable();
               // return View(await _context.CentrosAnalises.ToListAsync());
            }


            cvm.paginacao( centroAnalises, pagina, nreg);


            return View(cvm);

        }












        // GET: CentroAnalises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroAnalise = await _context.CentrosAnalises
                .Include(c => c.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (centroAnalise == null)
            {
                return NotFound();
            }

            return View(centroAnalise);
        }

        // GET: CentroAnalises/Create
        public IActionResult Adicionar()
        {

            ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Nome");
            return View();
        }

        // POST: CentroAnalises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar([Bind("Id,Nome,Localidade,NumLimiteTestes,HorarioAbertura,HorarioEncerramento,GestorId")] CentroAnalise centroAnalise)
        {
            ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Nome");
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Gestor")) {               
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    Console.WriteLine("User Id >> " + userId);

                    centroAnalise.GestorId = int.Parse(userId);
                    Console.WriteLine("Gestor Id >> " + centroAnalise.GestorId);


                    var gestorr = await _context.Gestores.FindAsync(centroAnalise.GestorId);
                    centroAnalise.Gestor = gestorr;
                }else{

                    var gestor = await _context.Gestores.FindAsync(centroAnalise.GestorId);
                    Console.WriteLine("Gestor nome >> " + gestor.Nome);
                    Console.WriteLine("Gestor id >> " + gestor.Id);
                    centroAnalise.Gestor = gestor;
                Console.WriteLine("Gestor nome1 >> " + centroAnalise.Gestor.Nome);
                }
                _context.Add(centroAnalise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }         

            return View(centroAnalise);
        }

        // GET: CentroAnalises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroAnalise = await _context.CentrosAnalises.FindAsync(id);
            if (centroAnalise == null)
            {
                return NotFound();
            }
            ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Nome");

            return View(centroAnalise);
        }


        // POST: CentroAnalises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Localidade,NumLimiteTestes,HorarioAbertura,HorarioEncerramento,GestorId")] CentroAnalise centroAnalise)
        {
            if (id != centroAnalise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
               
          Console.WriteLine("centroAnalise >> " + centroAnalise);

                    if (User.IsInRole("Gestor"))
                    {
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        Console.WriteLine("User Id >> " + userId);

                        centroAnalise.GestorId = int.Parse(userId);
                        Console.WriteLine("Gestor Id >> " + centroAnalise.GestorId);


                        var gestorr = await _context.Gestores.FindAsync(centroAnalise.GestorId);
                        centroAnalise.Gestor = gestorr;
                        
                    }

                    var gestor = await _context.Gestores.FindAsync(centroAnalise.GestorId);
                    centroAnalise.Gestor = gestor;




                    _context.Update(centroAnalise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentroAnaliseExists(centroAnalise.Id))
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
               ViewData["GestorId"] = new SelectList(_context.Gestores, "Id", "Nome");
            return View(centroAnalise);
        }

        // GET: CentroAnalises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroAnalise = await _context.CentrosAnalises
                .Include(c => c.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (centroAnalise == null)
            {
                return NotFound();
            }

            return View(centroAnalise);
        }

        // POST: CentroAnalises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centroAnalise = await _context.CentrosAnalises.FindAsync(id);
            _context.CentrosAnalises.Remove(centroAnalise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentroAnaliseExists(int id)
        {
            return _context.CentrosAnalises.Any(e => e.Id == id);
        }
    }
}
