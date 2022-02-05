using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LabTestesOnline.Models
{
    public class Cliente : Utilizador
    {

        public Cliente()
        {
            Testes = new HashSet<Teste>();
        }

        public ICollection<Teste> Testes { get; set; }

    }




}
