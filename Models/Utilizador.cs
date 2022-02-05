using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LabTestesOnline.Models
{
    public class Utilizador: IdentityUser<int>
    {


        [Required(ErrorMessage = "Introduza o seu nome!")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Escolha o seu sexo!")]
        public char Sexo { get; set; }


        [Required(ErrorMessage = "Coloque a sua data de nascimento!")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }


        [Required(ErrorMessage = "Introduza a sua localidade!")]
        [StringLength(100)]
        public string Localidade { get; set; }

        [Required(ErrorMessage = "Introduza a sua morada!")]
        [StringLength(100)]
        public string Morada { get; set; }
   

        [Required(ErrorMessage = "Introduza um número telefónico!")]
        [Display(Name = "Telefone")]
        [StringLength(9, ErrorMessage = "O telefone deve possuir 9 números!", MinimumLength = 9)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Número de Telefone inválido!")]
        [Phone]
        public String Contacto { get; set; }


    }


}
