using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LabTestesOnline.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LabTestesOnline.Controllers
{

    [Authorize(Roles = "Gestor,Admin")]
    public class TecnicosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Utilizador> _userManager;


        public TecnicosController(ApplicationDbContext context,ILogger<HomeController> logger, UserManager<Utilizador> userManager)
        {
            _context = context;
              _logger = logger;
            _userManager = userManager;
        }

        // GET: Tecnicos
        public async Task<IActionResult> Index()
        {

              var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Gestor")) {
                var centros = _context.CentrosAnalises.Where(g => g.GestorId == int.Parse(userId)).Select(C => C.Id).FirstOrDefault();
                return View(await _context.Tecnicos.Where(t => t.CentroAnalise.Id == centros).ToListAsync());
            }
            else{
                return View(await _context.Tecnicos.ToListAsync());
            }


        }

        // GET: Tecnicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return View(tecnico);
        }

        // GET: Tecnicos/Create
        public IActionResult Create()  {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["SexoLista"] = ViewModels.Utils.GetSexoLista();
            //ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises.Where(p => p.GestorId.ToString() == userId), "Id", "Nome");
            

            if (User.IsInRole("Gestor")) {      
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises
                .Include(c => c.Gestor).Where(m => m.Gestor.Id.ToString() == userId), "Id", "Nome");
            }
            else{
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises, "Id", "Nome");
            }

            return View();
        }

        // POST: Tecnicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CentroAnaliseId,Nome,Sexo,DataNascimento,Localidade,Morada,Contacto,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {

                String password = "Tecnico_123";
                tecnico.UserName = tecnico.Email;
                tecnico.EmailConfirmed = true;
                tecnico.PhoneNumber = tecnico.Contacto;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var centros = _context.CentrosAnalises.Where(g => g.GestorId == int.Parse(userId)).Select(C => C.Id).FirstOrDefault();    
                tecnico.CentroAnaliseId = centros;
                var centroAnalise1 = await _context.CentrosAnalises.FindAsync(tecnico.CentroAnaliseId);
                tecnico.CentroAnalise = centroAnalise1;



                await _userManager.CreateAsync(tecnico, password);
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(tecnico, "Tecnico");

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tecnico);
        }

        // GET: Tecnicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos.FindAsync(id);
             if (tecnico == null)
            {
                return NotFound();
            }


            ViewData["SexoLista"] = ViewModels.Utils.GetSexoLista();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Gestor")) {      
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises
                .Include(c => c.Gestor).Where(m => m.Gestor.Id.ToString() == userId), "Id", "Nome");
            }
            else{
                ViewData["CentroAnalise"] = new SelectList(_context.CentrosAnalises, "Id", "Nome");
            }


            return View(tecnico);
        }

        // POST: Tecnicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CentroAnaliseId,Nome,Sexo,DataNascimento,Localidade,Morada,Contacto,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Tecnico tecnico)
        {
            if (id != tecnico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
/*
                        Console.WriteLine("Id >> " + tecnico.Id);
                            Console.WriteLine("Nome >> " + tecnico.Nome);
                                Console.WriteLine("Sexo >> " + tecnico.Sexo);
                                    Console.WriteLine("LocDalidade >> " + tecnico.DataNascimento);
                                        Console.WriteLine("Localidade >> " + tecnico.Localidade);
                                          Console.WriteLine("Morada >> " + tecnico.Morada);
                                             Console.WriteLine("Cotnacto >> " + tecnico.Contacto);
                                                 Console.WriteLine("Centro de anliaseee >> " + tecnico.CentroAnalise);

                    Console.WriteLine("Email >> " + tecnico.Email);
                    Console.WriteLine("Tecnico centro anali Id >> " + tecnico.CentroAnaliseId);
                Console.WriteLine("Tecnico >> " + tecnico);
 
                    var centroAnalise = await _context.CentrosAnalises.FindAsync(tecnico.CentroAnaliseId);          
                    tecnico.CentroAnalise = centroAnalise;
                      Console.WriteLine("Centro analise >> " + tecnico.CentroAnalise);

                    Console.WriteLine("Centro analise Id >> " + centroAnalise.Id);
                    tecnico.CentroAnalise.Id = centroAnalise.Id;
*/
                    _context.Update(tecnico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnicoExists(tecnico.Id))
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


            return View(tecnico);
        }

        // GET: Tecnicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tecnico == null)
            {
                return NotFound();
            }

            return View(tecnico);
        }

        // POST: Tecnicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            _context.Tecnicos.Remove(tecnico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnicoExists(int id)
        {
            return _context.Tecnicos.Any(e => e.Id == id);
        }
    }
}
