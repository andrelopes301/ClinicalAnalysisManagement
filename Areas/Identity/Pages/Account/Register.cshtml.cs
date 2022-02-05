using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LabTestesOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace LabTestesOnline.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Utilizador> _signInManager;
        private readonly UserManager<Utilizador> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        public RegisterModel(
            UserManager<Utilizador> userManager,
            SignInManager<Utilizador> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }



        public class InputModel
        {


            [Required]
            [StringLength(100, ErrorMessage = "Deve introduzir um nome válido!")]
            [RegularExpression(@"^[-'a-zA-Z]{1,}(?: [-'a-zA-Z]+)?(?: [-'a-zA-Z]+)?$", ErrorMessage = "Deve introduzir um nome válido! (Não aceita acentos, cedilhas...)")]
            [Display(Name = "Nome Completo")]
            public string Nome { get; set; }


            [Required]
            [EmailAddress]
            [StringLength(100, ErrorMessage = "Deve introduzir um nome válido!")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "A password deve apresentar no mínimo 6 caracteres!", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Password")]
            [Compare("Password", ErrorMessage = "A password e a confirmação da password colocadas são diferentes.")]
            public string ConfirmPassword { get; set; }


      
            [Required]
            [Display(Name = "Tipo de Utilizador")]
            public int RoleID { get; set; }



            [Required]
            [Display(Name = "Sexo")]
            public Char Sexo { get; set; }

            [Required]
            [Display(Name = "Data de Nascimento")]
            public DateTime DataNascimento { get; set; }

            [Required]
            [Display(Name = "Localidade")]
            public String Localidade { get; set; }

            [Required]
            [Display(Name = "Morada")]
            public String Morada { get; set; }

            [Required]
            [Phone]
            [DataType(DataType.PhoneNumber)]
            [StringLength(9, ErrorMessage = "O telefone deve possuir 9 números!", MinimumLength = 9)]
            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Número de Telefone inválido!")]
            [Display(Name = "Telefone")]
            public String Contacto { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            
        ViewData["SexoLista"] = ViewModels.Utils.GetSexoLista();
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {


            ViewData["SexoLista"] = ViewModels.Utils.GetSexoLista();
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var role = Roles.RoleUtils.RegistSelectList.FirstOrDefault(s => s.Value == Input.RoleID.ToString());    
           
                var user = new Utilizador();

                if (role.Text == "Cliente"){
                   // Console.WriteLine("Cliente");
                 //   Console.WriteLine(String.Concat(Input.Nome.Where(c => !Char.IsWhiteSpace(c))));
                    user = new Cliente {Nome = Input.Nome, UserName = Input.Email , Email = Input.Email, Sexo = Input.Sexo, DataNascimento = Input.DataNascimento, Localidade = Input.Localidade, Morada = Input.Morada, Contacto = Input.Contacto, PhoneNumber = Input.Contacto , EmailConfirmed = true};
                }
                else if(role.Text == "Gestor"){
                    Console.WriteLine("gestor");
                   
                    user = new Gestor { Nome = Input.Nome, UserName = Input.Email , Email = Input.Email,  Sexo = Input.Sexo, DataNascimento = Input.DataNascimento, Localidade = Input.Localidade, Morada = Input.Morada, Contacto = Input.Contacto, PhoneNumber = Input.Contacto, EmailConfirmed = true };
                }
          
            
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                   

                    if (role!=null)
                        await _userManager.AddToRoleAsync(user, role.Text);

                    // return LocalRedirect(returnUrl);
                    return RedirectToPage("Login", new { email = Input.Email, returnUrl = returnUrl });


                    
                 
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
