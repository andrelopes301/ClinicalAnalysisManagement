using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LabTestesOnline.Models;
using LabTestesOnline.Roles;
using Microsoft.AspNetCore.Identity;

namespace LabTestesOnline.Models
{
    public class ApplicationDbContext : IdentityDbContext<Utilizador, IdentityRole<int>, int>
    {

        public  DbSet<Cliente> Clientes { get; set; }
        public  DbSet<CentroAnalise> CentrosAnalises { get; set; }
        public  DbSet<Teste> Testes { get; set; }
        public  DbSet<Tecnico> Tecnicos { get; set; }
        public  DbSet<Gestor> Gestores { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder{
        //    optionsBuilder.EnableSensitiveDataLogging();
        //  }



        
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Cliente>().HasMany(e => e.Testes).WithOne(t => t.Cliente).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<CentroAnalise>().HasMany(c => c.Tecnicos).WithOne(t => t.CentroAnalise).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<CentroAnalise>().HasOne(c => c.Gestor).WithMany(t=> t.CentrosAnalises).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Teste>().HasOne(e => e.Cliente).WithMany(t => t.Testes).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Tecnico>().HasOne(e => e.CentroAnalise).WithMany(t => t.Tecnicos).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Gestor>().HasMany(e => e.CentrosAnalises).WithOne(t => t.Gestor).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Procedimento>().HasOne(e => e.TestesPossiveis).WithMany(e => e.Procedimentos).OnDelete(DeleteBehavior.SetNull);
           



            base.OnModelCreating(builder);
            this.SeedRoles(builder);
            this.SeedUsers(builder);
            this.SeedUserRoles(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            string email = "admin@gmail.com";
            Utilizador admin = new Utilizador()
            {
                Id = 1,
                UserName = email,
                Email = email,
                Nome = "Deus",
                Contacto = "931686474",
                Localidade ="Céu",
                Morada ="Inferno",
                NormalizedUserName = email.ToUpper(),
                NormalizedEmail = email.ToUpper(),
                LockoutEnabled = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<Utilizador> passwordHasher = new PasswordHasher<Utilizador>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin_123");

            builder.Entity<Utilizador>().HasData(admin);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<int>>().HasData(RoleUtils.All);
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
            );
        }

        public DbSet<LabTestesOnline.Models.TestesPossiveis> TestesPossiveis { get; set; }
        



    }
}