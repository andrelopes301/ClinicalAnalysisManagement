using System;
using System.Collections.Generic;

namespace LabTestesOnline.Models
{
    public class CentroAnalise
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Localidade { get; set; }
        public int numLimiteTestes { get; set; }
        public int numLimiteAnalises { get; set; }

        public DateTime HorarioAbertura { get; set; }

        public DateTime HorarioEncerramento { get; set; }

        public virtual ICollection<TestesPossiveis> TestesPossiveis { get; set; }
        public virtual ICollection<AnalisesPossiveis> AnalisesPossiveis { get; set; }
        public virtual ICollection<Tecnico> Tecnicos { get; set; }
        public int? GestorId { get; set; }
        public Gestor Gestor { get; set; }





    }
}