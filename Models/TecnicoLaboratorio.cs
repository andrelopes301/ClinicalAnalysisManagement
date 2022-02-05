namespace LabTestesOnline.Models
{
    public class Tecnico : Utilizador
    {
        public CentroAnalise CentroAnalise { get; set; }
        public int? CentroAnaliseId { get; set; }

    }
}