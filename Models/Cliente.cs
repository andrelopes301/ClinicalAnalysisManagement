using System;
using System.Collections.Generic;

namespace LabTestesOnline.Models
{
    public class Cliente
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public char Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }   
        public string Localidade { get; set; }
        public string Morada { get; set; }
        public int Contacto { get; set; }
        
        public virtual ICollection<Analise> Analises { get; set; }
        public virtual ICollection<Teste> Testes { get; set; }



    }
}
