using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
namespace almacen.Models
{
    public class PaletizarModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();   
        public String codSem { get; set; }
        public String codInsumo { get; set; }
        public String codPT { get; set; }
        public String numguia { get; set; }
        public String cantCons { get; set; }
        public String cantConsReal { get; set; }
        public String ruc { get; set; }
        public List<String[]> listSemProdInsumo { get; set; }
        public String prodTerminado { get; set; }
        public String semana { get; set; } 
        public String anio { get; set; }
        public String codinsumoeditar { get; set; }
        public String cantinsumorealeditar { get; set; }
        public String msjPaletizar { get; set; }

        public PaletizarModel()
        {
            listSemProdInsumo = new List<String[]>();
        }



        public List<String[]> listarInsumosPaletiz(String codpt, String ruc, String codsem, int? anio, String codalm)
        {
            List<String[]> lista = new List<String[]>();
            var comando = contexto.pa_ListarPorPaletizar(codpt, ruc, codsem, anio, codalm);
            foreach (var q in comando)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.0000000}", q.cantidadConsumo), String.Format(CultureInfo.InvariantCulture, "{0:0.0000000}", q.cantidadConsumoReal)});
            }
            return lista;
        }


        public int ActualizarConsumoReal(String codsem, String codins, String codal, int anio, String ruc, String codpt, decimal? cant)
        {
            int rpta = 0;
            try
            {
                var comando = contexto.pa_ActualizarConsumoRealProduccion(codins, codal, codsem, anio, ruc, codpt, cant);
                rpta = 1;
            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }
            return rpta;
        }
        
    }
}