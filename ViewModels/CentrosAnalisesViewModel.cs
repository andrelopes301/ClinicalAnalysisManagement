using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabTestesOnline.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using X.PagedList;


namespace LabTestesOnline.ViewModels
{
    public class CentrosAnalisesViewModel
    {



        public IPagedList<CentroAnalise> CentrosAnalises { get; set; }



        public void paginacao(IQueryable<CentroAnalise> centroAnalises, int page, int nreg)
        {

                 CentrosAnalises = centroAnalises.ToPagedList(page, nreg);
                
  
            return;
        }




    }
}
