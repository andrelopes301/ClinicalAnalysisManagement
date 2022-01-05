using System;
using System.Collections.Generic;

namespace LabTestesOnline.Models
{
    public class Analise
    {

        public int Id { get; set; }
        public string TipoAnalise { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public string Resultado { get; set; }

        public int? CentroAnaliseId { get; set; }
        public CentroAnalise CentroAnalise { get; set; }
        public int? TecnicoResponsavelId { get; set; }
        public Tecnico TecnicoResponsavel { get; set; }
        public int? ClienteId { get; set; }
        public Cliente Cliente { get; set; }



    }
}