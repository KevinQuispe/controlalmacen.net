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
    public class SemanaProduccion
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
        [Required(ErrorMessage = "Elija consdicion")]
        public short paletizado { get; set; }
        public String costo { get; set; }
        public List<SelectListItem> listadropclientes { get; set; }
        public List<InsumoModel> listamodelclientes { get; set; }
        public List<SelectListItem> listadropcodigoprodterm { get; set; }
        public List<InsumoModel> listamodelicodigoprodterm { get; set; }
        public List<SelectListItem> listadropsemana { get; set; }
        public List<InsumoModel> listamodelsemana { get; set; }
        public List<String[]> listSemProdInsumo { get; set; }
        public List<String> listadeFechas { get; set; }
        public String codinsumoeditar { get; set; }
        public String cantinsumorealeditar { get; set; }
        public String msjSemProduccion { get; set; }
        public SemanaProduccion()
        {
            var consulta1 = from a in contexto.clientes
                            select new { a.ruc, a.razon_social };
            listadropclientes = new List<SelectListItem>();
            foreach (var q1 in consulta1)
            {
                listadropclientes.Add(new SelectListItem() { Text = q1.ruc.Trim() + "-" + q1.razon_social.Trim() + "", Value = q1.ruc.Trim() });
            }
            var consulta2 = from c in contexto.pa_listaCodigoPterminado()
                            select new { c.codigoPT, c.descripcion };
            listadropcodigoprodterm = new List<SelectListItem>();
            foreach (var q2 in consulta2)
            {
                listadropcodigoprodterm.Add(new SelectListItem() { Text = q2.descripcion.Trim() + "", Value = q2.codigoPT.Trim() });
            }
            var consulta3 = (from s in contexto.semanas
                            where s.estado == 1
                            select new { s.cod_semana, s.anio }).First();
            semana = consulta3.cod_semana;
            anio = consulta3.anio+"";
            listSemProdInsumo = new List<string[]>();
            listadeFechas = new List<string>();
        }

        public int registrarSemProducc(String codsem,String codpt,String ruc,String codalm,int? cant,int? anio,bool? pal)
        {
            int rpta = 0;
            try
            {
                var comando = contexto.pa_regSemanaInsumoProduccion(codsem, codpt, ruc, codalm, cant, anio, pal);
                rpta = 1;
            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }            
            return rpta;
        }

        public List<String[]> listarInsumosSemProducc(String c,String ruc,String codsem,int? anio,String codalm)
        {
            List<String[]> lista = new List<String[]>();
            var query = contexto.pa_ListarSemanaProduccionInsumo(c, ruc, codsem, anio, codalm);
            foreach (var q in query)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.cantidadConsumo), String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.cantidadConsumoReal), String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.costo) });
            }
            return lista;
        }

        public List<String[]> listarInsumosSemProduccNCajas(String codpt, String codsm, String codalm, int? anio)
        {
            List<String[]> lista = new List<String[]>();
            var query = contexto.pa_ListarSemanaProduccionNroCajas(codpt, codsm, codalm, anio);
            foreach (var q in query)
            {
                lista.Add(new String[] { q.ruc, q.razon_social, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.CantidadCajas)});
            }
            return lista;
        }

        public String[] RetornaSemanayAnio(DateTime? fech)
        {
            String[] lista = new String[2];
            var query = contexto.pa_RetornaAnioCodSem(fech).First();
            lista[0]=query.codseman;
            lista[1]=query.aniio+"";
            return lista;
        }

        public int ActualizarConsumoReal(String codsem,String codins,String codal,int anio,String ruc,String codpt,decimal? cant)
        {
            int rpta = 0;
            try
            {
                var comando = contexto.pa_ActualizarConsumoRealProduccion(codins, codal, codsem, anio, ruc, codpt,cant);
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