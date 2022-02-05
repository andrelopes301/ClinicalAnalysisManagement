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
using X.PagedList;

namespace LabTestesOnline.Controllers
{
    public class TestesPossiveisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestesPossiveisController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        // GET: TestesPossiveis
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.TestesPossiveis.Include(t => t.CentroAnalise);
            return View(await applicationDbContext.ToListAsync());

        }
        */


        // GET: TestesPossiveis
        public async Task<IActionResult> Index(string localidade, string pesquisa, int ? page)
        {

            var tvm = new TestesPossiveisViewModel();
            int pagina = (page == null || page < 1) ? 1 : page.Value;
            int nreg = 6;



            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var centros = _context.CentrosAnalises.Where(c => c.GestorId == int.Parse(userId)).Select(C => C.Id).FirstOrDefault();

            IQueryable<TestesPossiveis> testes = _context.TestesPossiveis.Include(p => p.CentroAnalise).AsQueryable();

            if (User.IsInRole("Gestor"))
            {
                testes = _context.TestesPossiveis.Include(p => p.CentroAnalise).Where(p => p.CentroAnaliseId == centros).AsQueryable();
            }

            IQueryable<TestesPossiveis> testes_filtered = null;
            bool search_empty = false;



            if (!string.IsNullOrEmpty(pesquisa))
            {

                testes_filtered = testes.Where(p => p.TipoTeste.Contains(pesquisa)
                                                        || p.CentroAnalise.Localidade.Contains(pesquisa)
                                                        || p.CentroAnalise.Nome.Contains(pesquisa));

                search_empty = !(testes_filtered.Any());

                tvm.Procura = pesquisa;
            }


            tvm.TestesComContagemDeLocalidades(testes);

            if (search_empty)
            {
                testes = testes_filtered;
            }

            //Localidade
            if (!string.IsNullOrEmpty(localidade))
            {
                testes = testes.Where(p => p.CentroAnalise.Localidade == localidade);
                tvm.Localidade = localidade;
            }


            tvm.testesPorPagina( testes, pagina, nreg);

            return View(tvm);


        }






        // GET: TestesPossiveis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testesPossiveis = await _context.TestesPossiveis
                .Include(t => t.CentroAnalise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testesPossiveis == null)
            {
                return NotFound();
            }

            return View(testesPossiveis);
        }

        [Authorize(Roles = "Gestor,Admin")]
        // GET: TestesPossiveis/Create
        public IActionResult Create(){

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["TiposTestes"] = ViewModels.Utils.GetTiposTestes();

            if (User.IsInRole("Gestor")) {      
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises.Include(c => c.Gestor).Where(m => m.Gestor.Id.ToString() == userId), "Id", "Nome");
            }
            else{
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises, "Id", "Nome");
            }



            return View();
        }

        // POST: TestesPossiveis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoTeste,CentroAnaliseId")] TestesPossiveis testesPossiveis)
        {
            if (ModelState.IsValid)
            {

                
                _context.Add(testesPossiveis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises, "Id", "Nome", testesPossiveis.CentroAnaliseId);



            return View(testesPossiveis);
        }

        // GET: TestesPossiveis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testesPossiveis = await _context.TestesPossiveis.FindAsync(id);
            if (testesPossiveis == null)
            {
                return NotFound();
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["TiposTestes"] = ViewModels.Utils.GetTiposTestes();
            if (User.IsInRole("Gestor")) {      
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises.Include(c => c.Gestor).Where(m => m.Gestor.Id.ToString() == userId), "Id", "Nome");
            }
            else{
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises, "Id", "Nome");
            }

            return View(testesPossiveis);
        }

        // POST: TestesPossiveis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoTeste,CentroAnaliseId")] TestesPossiveis testesPossiveis)
        {
            if (id != testesPossiveis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testesPossiveis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestesPossiveisExists(testesPossiveis.Id))
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
            ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises, "Id", "Nome", testesPossiveis.CentroAnaliseId);
            return View(testesPossiveis);
        }

        // GET: TestesPossiveis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testesPossiveis = await _context.TestesPossiveis
                .Include(t => t.CentroAnalise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testesPossiveis == null)
            {
                return NotFound();
            }

            return View(testesPossiveis);
        }

        // POST: TestesPossiveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testesPossiveis = await _context.TestesPossiveis.FindAsync(id);
            _context.TestesPossiveis.Remove(testesPossiveis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestesPossiveisExists(int id)
        {
            return _context.TestesPossiveis.Any(e => e.Id == id);
        }
    }
}
