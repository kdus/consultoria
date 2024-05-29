using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comum
{
    public class Funcionalidades
    {
        public string FormatoDataMy(string vValor)
        {
            try
            {
                DateTime formata = DateTime.Parse(vValor);
                vValor = formata.ToString("yyyy-MM-dd");
            }
            catch
            {
                vValor = string.Empty;
            }

            return vValor;
        }


        public string FormatoData(string vValor)
        {
            try
            {
                DateTime formata = DateTime.Parse(vValor);
                vValor = formata.ToString("dd/MM/yyyy");
            }
            catch
            {
                vValor = string.Empty;
            }

            return vValor;
        }


    }
}