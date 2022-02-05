using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabTestesOnline.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using X.PagedList;

namespace LabTestesOnline.ViewModels
{

        public class ClientViewModel
        {


        public class SexoItens
        {

            List<SelectListItem> SexoList;


            public SexoItens()
            {

                SexoList = new List<SelectListItem>();

                SexoList.Add(new SelectListItem
                {
                    Text = "Masculino",
                    Value = "m"
                });
                SexoList.Add(new SelectListItem
                {
                    Text = "Feminino",
                    Value = "f"
                });
            }
        }


    


}
}
