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

namespace LabTestesOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GestoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GestoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gestors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gestores.ToListAsync());
        }

        // GET: Gestors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gestor == null)
            {
                return NotFound();
            }

            return View(gestor);
        }

        // GET: Gestors/Create
        /*
        public IActionResult Create()
        {

            return View();
        }

        // POST: Gestors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sexo,DataNascimento,Localidade,Morada,Contacto,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Gestor gestor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gestor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gestor);
        }
        */

        // GET: Gestors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["SexoLista"] = ViewModels.Utils.GetSexoLista();
            if (id == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestores.FindAsync(id);
            if (gestor == null)
            {
                return NotFound();
            }
            return View(gestor);
        }

        // POST: Gestors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Sexo,DataNascimento,Localidade,Morada,Contacto,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Gestor gestor)
        {
            if (id != gestor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gestor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GestorExists(gestor.Id))
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
            return View(gestor);
        }

        // GET: Gestors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gestor == null)
            {
                return NotFound();
            }

            return View(gestor);
        }

        // POST: Gestors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gestor = await _context.Gestores.FindAsync(id);
            _context.Gestores.Remove(gestor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GestorExists(int id)
        {
            return _context.Gestores.Any(e => e.Id == id);
        }
    }
}
