using LabTestesOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LabTestesOnline.ViewModels;
using System.Security.Claims;
using X.PagedList;
using Microsoft.EntityFrameworkCore;




namespace LabTestesOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Utilizador> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<Utilizador> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index(string localidade, int? page)
        {

            var tvm = new TestesPossiveisViewModel();
            int pagina = (page == null || page < 1) ? 1 : page.Value;
            int nreg = 5;

            IQueryable<TestesPossiveis> testes = _context.TestesPossiveis.Include(p => p.CentroAnalise).AsQueryable();

            tvm.TestesComContagemDeLocalidades(testes);
    
            //Localidade
            if (!string.IsNullOrEmpty(localidade))
            {
                testes = testes.Where(p => p.CentroAnalise.Localidade == localidade);
                tvm.Localidade = localidade;
            }

            tvm.testesPorPagina( testes, pagina, nreg);


            return View(tvm);










        }
 




        [Authorize]
        public IActionResult Privacy()
        {

            
            ViewBag.Id = _userManager.GetUserId(User);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
