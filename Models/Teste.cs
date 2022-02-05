using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabTestesOnline.Models
{
    public class Teste
    {
        public Teste()
        {
            Procedimentos = new HashSet<Procedimento>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Introduza o tipo de teste!")]
        [StringLength(100)]
        public string TipoTeste { get; set; }


        [Required(ErrorMessage = "Insira a data de início!")]
        [Display(Name = "Dada de início")]
        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }


        [Required(ErrorMessage = "Insira a data final!")]
        [Display(Name = "Data final")]
        [DataType(DataType.DateTime)]
        public DateTime DataFinal { get; set; }



        public string Resultado { get; set; }

        public bool Estado { get; set; }


        public int? CentroAnaliseId { get; set; }
        public CentroAnalise CentroAnalise { get; set; }

        public int? TecnicoResponsavelId { get; set; }
        public Tecnico TecnicoResponsavel { get; set; }

        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Procedimento> Procedimentos { get; set; }

    }
}