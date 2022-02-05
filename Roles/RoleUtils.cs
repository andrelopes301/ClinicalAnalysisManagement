using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LabTestesOnline.Roles
{
    static public class RoleUtils
    {

        private static List<IdentityRole<int>> roles = new List<IdentityRole<int>>
        {
            new IdentityRole<int>{  Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "1"},
            new IdentityRole<int>{  Id = 2, Name = "Gestor", NormalizedName = "GESTOR", ConcurrencyStamp = "2"},
            new IdentityRole<int>{  Id = 3, Name = "Tecnico", NormalizedName = "TECNICO", ConcurrencyStamp = "3"},
            new IdentityRole<int>{  Id = 4, Name = "Cliente", NormalizedName = "CLIENTE", ConcurrencyStamp = "4"},
        };

        static public List<IdentityRole<int>> All
        {
            get
            {
                //Devolver lista de roles
                return roles;
            }
        }

        static public IEnumerable<SelectListItem> RegistSelectList
        {
            get
            {
                //Apenas se podem registar clientes e tecnicos
                return roles.Where(r => r.Id == 2 || r.Id == 4)
                   .Select(r => new SelectListItem()
                    {
                        Text = r.Name,
                        Value = r.Id.ToString()
                    });
            }
        }
    }
}
