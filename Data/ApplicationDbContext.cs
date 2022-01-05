using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LabTestesOnline.Models;

namespace LabTestesOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<CentroAnalise> CentrosAnalises { get; set; }
        public virtual DbSet<Analise> Analises { get; set; }
        public virtual DbSet<Teste> Testes { get; set; }
        public virtual DbSet<Tecnico> Tecnicos { get; set; }
        public virtual DbSet<Gestor> Gestores { get; set; }




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
    }
}
