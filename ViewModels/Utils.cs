using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LabTestesOnline.ViewModels
{
    public class Utils
    {


           
            public static  List<SelectListItem> GetSexoLista()   {
                  List<SelectListItem>  SexoList = new List<SelectListItem>();

                SexoList.Add(new SelectListItem{
                    Text = "Masculino",
                    Value = "M"
                });
                SexoList.Add(new SelectListItem {
                    Text = "Feminino",
                    Value = "F"
                });
            SexoList.Add(new SelectListItem
            {
                Text = "Outro",
                Value = "O"
            });
            return SexoList;
            }

            public static  List<SelectListItem> GetTiposTestes()   {
                  List<SelectListItem>  TiposTestes = new List<SelectListItem>();

                TiposTestes.Add(new SelectListItem{
                    Text = "Covid-19",
                    Value = "Covid-19"
                });
                TiposTestes.Add(new SelectListItem {
                    Text = "Hepatite-B",
                    Value = "Hepatite-B"
                });
                 TiposTestes.Add(new SelectListItem {
                    Text = "Hepatite-C",
                    Value = "Hepatite-C"
                });
                 TiposTestes.Add(new SelectListItem {
                    Text = "HIV",
                    Value = "HIV"
                });
                 TiposTestes.Add(new SelectListItem {
                    Text = "Herpes",
                    Value = "Herpes"
                });
                 TiposTestes.Add(new SelectListItem {
                    Text = "Gripe-A",
                    Value = "Gripe-A"
                });

            return TiposTestes;
            }


        public static List<SelectListItem> GetResultado()
        {
            List<SelectListItem> Resultados = new List<SelectListItem>();

            Resultados.Add(new SelectListItem
            {
                Text = "Positivo",
                Value = "Positivo"
            });
            Resultados.Add(new SelectListItem
            {
                Text = "Negativo",
                Value = "Negativo"
            });
            Resultados.Add(new SelectListItem
            {
                Text = "Inconclusivo",
                Value = "Inconclusivo"
            });
            Resultados.Add(new SelectListItem
            {
                Text = "Não Realizado",
                Value = "Não Realizado"
            });
            Resultados.Add(new SelectListItem
            {
                Text = "Em espera...",
                Value = "Em espera..."
            });
            Resultados.Add(new SelectListItem
            {
                Text = "Outro",
                Value = "Outro"
            });
  
            return Resultados;
        }





    }




 

}
