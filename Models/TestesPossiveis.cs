using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabTestesOnline.Models
{
    public class TestesPossiveis
    {

        public TestesPossiveis()
        {
            Procedimentos = new HashSet<Procedimento>();
        }

        public int Id { get; set; }


        [Required(ErrorMessage = "Introduza o tipo de teste!")]
        [StringLength(100)]
        public string TipoTeste { get; set; }


        public int? CentroAnaliseId { get; set; }
        public CentroAnalise CentroAnalise { get; set; }
        public ICollection<Procedimento> Procedimentos { get; set; }

    }
}