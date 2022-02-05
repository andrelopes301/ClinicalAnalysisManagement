using System.Collections.Generic;

namespace LabTestesOnline.Models
{
    public class Gestor : Utilizador
    {
        public Gestor()
        {
            CentrosAnalises = new HashSet<CentroAnalise>();
        }

        public ICollection<CentroAnalise> CentrosAnalises { get; set; }

    }
}