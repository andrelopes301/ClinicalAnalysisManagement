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
    [Authorize(Roles = "Tecnico,Admin,Cliente")]
    public class TestesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Testes
        [Authorize(Roles = "Cliente,Tecnico,Admin")]
        public async Task<IActionResult> Index(string search)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var testes = _context.Testes.Include(t => t.CentroAnalise)
                    .Include(t => t.Cliente)
                    .Include(t => t.TecnicoResponsavel)
                    .Where(t => t.ClienteId != null).AsQueryable();

            if (User.IsInRole("Tecnico"))
            {
                testes = _context.Testes.Include(t => t.CentroAnalise).Include(t => t.Cliente)
                    .Include(t => t.TecnicoResponsavel)
                    .Where(t => t.TecnicoResponsavelId == int.Parse(userId) && t.ClienteId != null).AsQueryable();


                if (!string.IsNullOrEmpty(search))
                    testes = testes.Where(t => t.Cliente.Nome.Contains(search)
                    || t.DataInicio.ToString().Contains(search));
            }



            return View(await testes.ToListAsync());


        }




        [Authorize(Roles = "Cliente,Tecnico")]
        public async Task<IActionResult> Agendados()
        {


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var testes = _context.Testes.Include(t => t.CentroAnalise)
                .Include(t => t.Cliente).Include(t => t.TecnicoResponsavel)
                .Where(t => t.ClienteId==null && t.TecnicoResponsavelId == int.Parse(userId) && (t.DataFinal.CompareTo(DateTime.Now) == 1 || t.Resultado == "Em Espera...")).AsQueryable();

         
            return View(await testes.ToListAsync());

        }




        // GET: Lista de Agendamentos Possiveis
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> PossiveisAgendamentos()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(await _context.Testes.Include(t => t.CentroAnalise)
                .Include(t => t.Cliente).Include(t => t.TecnicoResponsavel)
                .Where(t=> t.Estado == false && t.DataInicio.CompareTo(DateTime.Now)==1).ToListAsync());



        }
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Resultados()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
  

            return View(await _context.Testes.Include(t => t.CentroAnalise)
                .Include(t => t.Cliente).Include(t => t.TecnicoResponsavel)
                .Where(t => t.ClienteId == int.Parse(userId) && t.Estado == true && (t.DataFinal.CompareTo(DateTime.Now) == -1 || t.Resultado!="Em Espera...")).ToListAsync());

        }



        // GET: Testes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = await _context.Testes
                .Include(t => t.CentroAnalise)
                .Include(t => t.Cliente)
                .Include(t => t.TecnicoResponsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teste == null)
            {
                return NotFound();
            }

            return View(teste);
        }

        // GET: Testes/Create
        [Authorize(Roles = "Tecnico,Admin")]
        public IActionResult Create()
        {



            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tecnico_CentroAnaliseID = _context.Tecnicos.Where(t => t.Id.ToString() == userId)
            .Select(t => t.CentroAnalise.Id).FirstOrDefault();

            if (User.IsInRole("Tecnico"))
                ViewData["Testes"] = new SelectList(_context.TestesPossiveis.Include(t => t.CentroAnalise)
            .Where(t => t.CentroAnalise.Id == tecnico_CentroAnaliseID), "Id", "TipoTeste");
            else { 
                ViewData["Testes"] = ViewModels.Utils.GetTiposTestes();
                ViewData["TecnicoResponsavelId"] = new SelectList(_context.Tecnicos, "Id","Nome");
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id","Nome");
            }

            return View();
        }

        // POST: Testes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoTeste,DataInicio,DataFinal,Resultado,Estado,ClienteId,CentroAnaliseId,TecnicoResponsavelId")] Teste teste)
        {


            //,CentroAnaliseId,TecnicoResponsavelId,ClienteId

            if (ModelState.IsValid)
            {
           

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
               
                var tipoTeste = _context.TestesPossiveis.Where(t => t.Id.ToString() == teste.TipoTeste)
                .Select(t => t.TipoTeste).FirstOrDefault();


                if (User.IsInRole("Tecnico")) {


                    teste.TipoTeste = tipoTeste;
                    var tecnico_CentroAnaliseID = _context.Tecnicos.Where(t => t.Id.ToString() == userId)
                .Select(t => t.CentroAnalise.Id).FirstOrDefault();

                    teste.CentroAnaliseId = tecnico_CentroAnaliseID;
                    teste.TecnicoResponsavelId = int.Parse(userId);
                }
                else
                {
                    ViewData["Testes"] = ViewModels.Utils.GetTiposTestes();
                    ViewData["TiposTestes"] = ViewModels.Utils.GetTiposTestes();
                    ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", teste.ClienteId);
                    ViewData["TecnicoResponsavelId"] = new SelectList(_context.Tecnicos, "Id", "Nome", teste.TecnicoResponsavelId);


                    var tecnico_CentroAnaliseID = _context.Tecnicos.Where(t => t.Id == teste.TecnicoResponsavelId).Select(t => t.CentroAnaliseId).FirstOrDefault();

                    Console.WriteLine("CENTRO DE ANALISE ID" + tecnico_CentroAnaliseID);

                    if (tecnico_CentroAnaliseID != null)
                        teste.CentroAnaliseId = tecnico_CentroAnaliseID;


                }



                _context.Add(teste);
                await _context.SaveChangesAsync();
                return RedirectToAction("Agendados", "Testes");
            }
              return View(teste);
        }

        // GET: Testes/Edit/5
        [Authorize(Roles = "Tecnico,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = await _context.Testes.FindAsync(id);
            if (teste == null)
            {
                return NotFound();
            }


            ViewData["Resultados"] = ViewModels.Utils.GetResultado();
            if (User.IsInRole("Admin"))
            {
                ViewData["Testes"] = ViewModels.Utils.GetTiposTestes();
                ViewData["TiposTestes"] = ViewModels.Utils.GetTiposTestes();
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", teste.ClienteId);
                ViewData["TecnicoResponsavelId"] = new SelectList(_context.Tecnicos, "Id", "Nome", teste.TecnicoResponsavelId);

                var tecnico_CentroAnaliseID = _context.Tecnicos.Where(t => t.Id == teste.TecnicoResponsavelId).Select(t => t.CentroAnaliseId).FirstOrDefault();
                if(tecnico_CentroAnaliseID != null)
                    teste.CentroAnaliseId = tecnico_CentroAnaliseID;

            }
            else if (User.IsInRole("Tecnico"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var tecnico_CentroAnaliseID = _context.Tecnicos.Where(t => t.Id.ToString() == userId)
                .Select(t => t.CentroAnalise.Id).FirstOrDefault();


                ViewData["Testes"] = new SelectList(_context.TestesPossiveis.Include(t => t.CentroAnalise)
                .Where(t => t.CentroAnalise.Id == tecnico_CentroAnaliseID), "Id", "TipoTeste");

            }

            return View(teste);
        }

        // POST: Testes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoTeste,DataInicio,DataFinal,Resultado,Estado,ClienteId,CentroAnaliseId,TecnicoResponsavelId")] Teste teste)
        {
            if (id != teste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TesteExists(teste.Id))
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

        

            return View(teste);
        }

        [Authorize(Roles = "Tecnico,Admin")]
        public async Task<IActionResult> CheckList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = await _context.Testes.FindAsync(id);
            if (teste == null)
            {
                return NotFound();
            }


            ViewData["Resultados"] = ViewModels.Utils.GetResultado();
            if (User.IsInRole("Admin"))
            {
                ViewData["Testes"] = ViewModels.Utils.GetTiposTestes();
                ViewData["TiposTestes"] = ViewModels.Utils.GetTiposTestes();
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", teste.ClienteId);
                ViewData["TecnicoResponsavelId"] = new SelectList(_context.Tecnicos, "Id", "Nome", teste.TecnicoResponsavelId);

                var tecnico_CentroAnaliseID = _context.Tecnicos.Where(t => t.Id == teste.TecnicoResponsavelId).Select(t => t.CentroAnaliseId).FirstOrDefault();
                if (tecnico_CentroAnaliseID != null)
                    teste.CentroAnaliseId = tecnico_CentroAnaliseID;

            }
            else if (User.IsInRole("Tecnico"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var tecnico_CentroAnaliseID = _context.Tecnicos.Where(t => t.Id.ToString() == userId)
                .Select(t => t.CentroAnalise.Id).FirstOrDefault();
                var teste_procedimento = _context.Procedimentos.Where(p => p.TestesPossiveis.TipoTeste == teste.TipoTeste)
                .Select(t => t.TestesPossiveis.Id).FirstOrDefault();


                
                ViewData["TiposTestes"] = new String(teste.TipoTeste);
                ViewData["ClienteId"] = (teste.ClienteId);
                ViewData["ClienteINome"] = _context.Clientes.Where(t => t.Id == teste.ClienteId).Select(t => t.Nome).FirstOrDefault();

                ViewData["TecnicoResponsavelId"] = teste.TecnicoResponsavelId;
                ViewData["TecnicoResponsavelNome"] = _context.Tecnicos.Where(t => t.Id == teste.TecnicoResponsavelId).Select(t => t.Nome).FirstOrDefault();
                
                ViewData["Procedimentos"] = new List<Procedimento>(_context.Procedimentos
                    .Where(p => p.TestesPossiveis.CentroAnaliseId == tecnico_CentroAnaliseID && p.TestesPossiveis.Id == teste_procedimento));
                ViewData["Testes"] = new SelectList(_context.TestesPossiveis.Include(t => t.CentroAnalise)
                .Where(t => t.CentroAnalise.Id == tecnico_CentroAnaliseID), "Id", "Nome");

            }

            return View(teste);
        }

        // POST: Testes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckList(int id, [Bind("Id,TipoTeste,DataInicio,DataFinal,Resultado,Estado,ClienteId,CentroAnaliseId,TecnicoResponsavelId")] Teste teste)
        {
            if (id != teste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TesteExists(teste.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                 return RedirectToAction("Index", "Home");
            }



            return View(teste);
        }

        // GET: Testes/Delete/5
        [Authorize(Roles = "Tecnico,Cliente,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = await _context.Testes
                .Include(t => t.CentroAnalise)
                .Include(t => t.Cliente)
                .Include(t => t.TecnicoResponsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teste == null)
            {
                return NotFound();
            }

            return View(teste);
        }

        // POST: Testes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teste = await _context.Testes.FindAsync(id);
            _context.Testes.Remove(teste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TesteExists(int id)
        {
            return _context.Testes.Any(e => e.Id == id);
        }




        public async Task<IActionResult> Agendar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var teste = await _context.Testes.FindAsync(id);
            if (teste == null)
            { 
                return NotFound();
            }
             ViewData["TipoTeste"] = teste.TipoTeste;
             ViewData["DataInicio"] = teste.DataInicio;
             ViewData["DataFinal"] = teste.DataFinal;
             ViewData["Resultado"] = teste.Resultado;
             ViewData["ClienteId"] = teste.ClienteId;
             ViewData["CentroAnaliseId"] = teste.CentroAnaliseId;
             ViewData["TecnicoResponsavelId"] = teste.TecnicoResponsavelId;

            teste.Resultado ="Em espera...";

            return View(teste);
        }

        // POST: Testes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agendar(int id, [Bind("Id,TipoTeste,DataInicio,DataFinal,Resultado,Estado,ClienteId,CentroAnaliseId,TecnicoResponsavelId")] Teste teste)
        {
            if (id != teste.Id)
            {      
                return View(teste);
            }
            if (ModelState.IsValid) {
                try  {

                    var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    teste.ClienteId = userId;    
                    teste.Resultado ="Em espera...";
                    _context.Testes.Update(teste);            
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)    {
                    if (!TesteExists(teste.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(teste);
        }



        public async Task<IActionResult> Cancelar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var teste = await _context.Testes.FindAsync(id);
            if (teste == null)
            {
                return NotFound();
            }
            ViewData["TipoTeste"] = teste.TipoTeste;
            ViewData["DataInicio"] = teste.DataInicio;
            ViewData["DataFinal"] = teste.DataFinal;
            ViewData["Resultado"] = teste.Resultado;
            ViewData["ClienteId"] = teste.ClienteId;
            ViewData["CentroAnaliseId"] = teste.CentroAnaliseId;
            ViewData["TecnicoResponsavelId"] = teste.TecnicoResponsavelId;



            bool cancelar = (teste.DataInicio - DateTime.Now) <= TimeSpan.FromHours(24);

            ViewData["Cancelar"] = cancelar;



            return View(teste);
        }

        // POST: Testes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id, [Bind("Id,TipoTeste,DataInicio,DataFinal,Resultado,Estado,ClienteId,CentroAnaliseId,TecnicoResponsavelId")] Teste teste)
        {
            if (id != teste.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    teste.ClienteId = null;
                    teste.Resultado = "";
                    _context.Testes.Update(teste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TesteExists(teste.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(teste);
        }
    }
}
