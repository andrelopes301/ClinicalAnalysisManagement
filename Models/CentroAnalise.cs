using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LabTestesOnline.Models
{
    public class CentroAnalise
    {

        public CentroAnalise()
        {
            TestesPossiveis = new HashSet<TestesPossiveis>();
            Tecnicos = new HashSet<Tecnico>();
        }


        public int Id { get; set; }

        [Required(ErrorMessage = "Introduza o nome do centro!")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza a localidade!")]
        [Display(Name = "Localidade")]
        [StringLength(100)]
        public string Localidade { get; set; }

     
        [Required(ErrorMessage = "Insira o limite de testes (1-100)!")]
        [Display(Name = "Nº Limite de Testes")]
        [Range(1, 100, ErrorMessage = "Insira um valor entre 1 e 100!")]
        public int NumLimiteTestes { get; set; }




        [Required(ErrorMessage = "Insira a hora de abertura!")]
        [Display(Name = "Hora de Abertura")]
        [DataType(DataType.DateTime)]
        public DateTime HorarioAbertura { get; set; }




        [Required(ErrorMessage = "Insira a hora de encerramento!")]
        [Display(Name = "Hora de Encerramento")]
        [DataType(DataType.DateTime)]
        public DateTime HorarioEncerramento { get; set; }

        public  ICollection<TestesPossiveis> TestesPossiveis { get; set; }
        public  ICollection<Tecnico> Tecnicos { get; set; }
        public int? GestorId { get; set; }
        public Gestor Gestor { get; set; }





    }
}