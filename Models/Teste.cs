using System;

namespace LabTestesOnline.Models
{
    public class Teste
    {

        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Resultado { get; set; }
        public int? ClienteId { get; set; }
        public int? TecnicoResponsavelId { get; set; }
        public int? CentroAnaliseId { get; set; }
        public Tecnico Tecnico { get; set; }
        public Cliente Cliente { get; set; }
        public CentroAnalise CentroAnalise { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
    }
}