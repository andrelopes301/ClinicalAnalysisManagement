namespace LabTestesOnline.Models
{
    public class Tecnico
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CentroAnalise CentroAnalise { get; set; }


    }
}