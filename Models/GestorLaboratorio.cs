using System.Collections.Generic;

namespace LabTestesOnline.Models
{
    public class Gestor
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CentroAnalise> CentrosAnalises { get; set; }

    }
}