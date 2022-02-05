using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LabTestesOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LabTestesOnline.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Utilizador> _userManager;
        private readonly SignInManager<Utilizador> _signInManager;

        public IndexModel(
            UserManager<Utilizador> userManager,
            SignInManager<Utilizador> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [DataType(DataType.PhoneNumber)]
            [StringLength(9, ErrorMessage = "O telefone deve possuir 9 números!", MinimumLength = 9)]
            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Número de Telefone inválido!")]
            [Display(Name = "Telefone")]


            public string Telefone { get; set; }

        }

        private async Task LoadAsync(Utilizador user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var telefone = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            Input = new InputModel
            {
                Telefone = telefone
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var telefone = await _userManager.GetPhoneNumberAsync(user);
            if (Input.Telefone != telefone)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.Telefone);
                if (!setPhoneResult.Succeeded)
                {
                   
                    StatusMessage = "Erro ao tentar atualizar o número de Telefone.";
                    return RedirectToPage();
                }
            }

            user.Contacto = Input.Telefone;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado!";
            return RedirectToPage();
        }
    }
}
