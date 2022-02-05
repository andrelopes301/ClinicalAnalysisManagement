using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabTestesOnline.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using X.PagedList;

namespace LabTestesOnline.ViewModels
{


    public class LocalidadeComContagem
    {
        public string NomeDaLocalidade { get; set; }
        public int NumeroDeLabs { get; set; }

        public string NomeDaLocalidadeComContagem
        {
            get
            {
                return $"{NomeDaLocalidade} ({NumeroDeLabs.ToString()})";
            }
        }
    }



    public class TestesPossiveisViewModel
    {

        public string Localidade { get; set; }
        public string Procura { get; set; }


        public IPagedList<TestesPossiveis> TestesPossiveis { get; set; }

        public IEnumerable<LocalidadeComContagem> LocalidadesComContagem { get; set; }


        public string Ordem { get; set; }


        public TestesPossiveisViewModel() {



        }

        public IEnumerable<SelectListItem> LocalidadesSelectListItems
        {
            get
            {
                return LocalidadesComContagem.Select(
                        cc => new SelectListItem
                        {
                            Value = cc.NomeDaLocalidade,
                            Text = cc.NomeDaLocalidadeComContagem
                        }
                    );
            }
        }



        public void TestesComContagemDeLocalidades(IQueryable<TestesPossiveis> testesPossiveis)
        {
            LocalidadesComContagem = from p in testesPossiveis
                                    where p.CentroAnaliseId != null
                                    group p by p.CentroAnalise.Localidade into catGroup
                                    select new LocalidadeComContagem()
                                    {
                                        NomeDaLocalidade = catGroup.Key,
                                        NumeroDeLabs = catGroup.Count()
                                    };

            return;
        }

     
        public void testesPorPagina( IQueryable<TestesPossiveis> testesPossiveis, int page, int nreg)
        {
         
            TestesPossiveis = testesPossiveis.OrderBy(p => p.TipoTeste).ToPagedList(page, nreg);
        

        }




















    }
}
