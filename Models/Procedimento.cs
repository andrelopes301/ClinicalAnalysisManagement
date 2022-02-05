using System.ComponentModel.DataAnnotations;

namespace LabTestesOnline.Models
{
    public class Procedimento
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Introduza o procedimento!")]
        [StringLength(200)]
        public string TipoProcedimento { get; set; }

        public bool isChecked { get; set; }

        public int? TestesPossiveisId { get; set; }
        public TestesPossiveis TestesPossiveis { get; set; }



    }
}
