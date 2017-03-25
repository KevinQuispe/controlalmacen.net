using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//add this
//this para hacer uso de display
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
namespace almacen.Models
{
    public class SemanaProducc
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        [Required(ErrorMessage = "El ruc es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String ruc { get; set; }
        [Required(ErrorMessage = "Producto es requerido")]
        public String prodTerminado { get; set; }
        [Required(ErrorMessage = "Cantidad es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String cant_cajas { get; set; }
        public String semana { get; set; }
        public String anio { get; set; }
        [Required(ErrorMessage = "Elija condicion")]
        public short paletizado { get; set; }
        public List<SelectListItem> listadropclientes { get; set; }
        //public List<SelectListItem> listadropsemana { get; set; }
        public List<InsumoModel> listamodelsemana { get; set; }
        public List<String[]> listSemProdInsumo { get; set; }
        public String codinsumoeditar { get; set; }
        public String cantinsumorealeditar { get; set; }
        public String msjSemProducc { get; set; }
        public short estado { get; set; }
        public String razon { get; set; }

        public SemanaProducc()
        {
            estado = 1;
            listadropclientes = new List<SelectListItem>();
            listSemProdInsumo = new List<String[]>();
            try
            {
                var query = contexto.pa_SemanaPorCerrar().First();
                semana = query.cod_semana;
                anio = query.anio + "";
            }
            catch (Exception)
            {
                semana = "";
                anio = "";
            }

        }

        public List<String[]> listarInsumosSemProducc(String ruc, String codsem, int? anio)
        {
            List<String[]> lista = new List<String[]>();
            var query = contexto.pa_ListarSemanaConsumoProduccion(ruc, codsem, anio);
            foreach (var q in query)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.Consumo), String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.ConsumoReal)});
            }
            return lista;
        }

        public int ActualizarSemanaConsumoProduccion(String ruc,String codsem,int? anio,String codins,decimal? consreal)
        {
            int rpta = 0;
            try
            {
                var comando = contexto.pa_ActualizarSemanaConsumProducc(ruc, codsem, anio, codins, consreal);
                rpta = 1;
            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }
            return rpta;
        }

        public List<SelectListItem> listarClientesConProducc(String codsem,String codalm,int? anio)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            var query = contexto.pa_ListarClientProduccAbierta(codsem,codalm,anio);
            foreach (var q in query)
            {
                lista.Add(new SelectListItem() { Text = q.ruc.Trim() + "-" + q.razon_social.Trim().ToLower() + "", Value = q.ruc.Trim() });
            }
            return lista;
        }

        public int CerroProduccion(String codsem, int? anio, String ruc)
        {
            int rpta = 0;
            try
            {
                var query = contexto.pa_CerrarProduccion(codsem, anio, ruc);
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