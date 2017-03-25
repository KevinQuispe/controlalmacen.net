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
    public class ProduccionModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        [Required(ErrorMessage = "El codigo es requerido")]
        public String codigopt { get; set; }
        [Required(ErrorMessage = "La descripción del Producto terminado es requerido")]
        public String descripPT { get; set; }
        [Required(ErrorMessage = "El ruc es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String ruc { get; set; }
        [Required(ErrorMessage = "Codigo es requerido")]
        public String codigoInsumo { get; set; }
        [Required(ErrorMessage = "La cantidad es requerida")]
        public String cantidad { get; set; }
        [Required(ErrorMessage = "El tipo es requerido")]
        public short tipo { get; set; }
        public short estadoformulario { get; set; }
        public List<SelectListItem> listadropinsumos = new List<SelectListItem>();
        public List<InsumoModel> listamodelinsumos = new List<InsumoModel>();
        public List<SelectListItem> listadropcodigoconsumo = new List<SelectListItem>();
        public List<InsumoModel> listamodelicodigoconsumo = new List<InsumoModel>();
        public String msjPorduccionModel { get; set; }
        public List<String[]> listainsumosProdTerm { get; set; }

       //add day 28
       public String nominsumo { get; set; }

       //add day 31
       public List<String[]> listareporteProdTerm { get; set; }
       public String msjerror { get; set; }
     

        public ProduccionModel()
        {
            //add list
            listareporteProdTerm = new List<String[]>();

            estadoformulario = 1;
            var consulta1 = from a in contexto.insumos
                            select new { a.nombre_insumo, a.cod_insumo };
            listadropinsumos = new List<SelectListItem>();
            foreach (var q1 in consulta1)
            {
                listadropinsumos.Add(new SelectListItem() { Text = q1.cod_insumo.Trim() + "-" + q1.nombre_insumo.Trim() + "", Value = q1.cod_insumo.Trim() });
            }
            var consulta2 = from c in contexto.pa_listaCodigoPterminado()
                            select new { c.codigoPT, c.descripcion };
            listadropcodigoconsumo = new List<SelectListItem>();
          
            foreach (var q2 in consulta2)
            {
                listadropcodigoconsumo.Add(new SelectListItem() { Text = q2.descripcion.Trim() + "", Value = q2.codigoPT.Trim() });
            }
            listainsumosProdTerm = new List<String[]>();
        }

        public bool? existeProdTermin(String desc)
        {
            bool? rpta = null;
            try
            {
                var comando = contexto.pa_ExistePTsegunDescrip(desc).First();
                rpta = comando.existe;

            }
            catch (Exception)
            {
                rpta = null;
            }
            return rpta;
        }

        public List<String[]> listarInsumosenPT(String codpt)
        {
            List<String[]> lista = new List<String[]>();
            var comando = contexto.pa_ListInsumosProdTerminado(codpt);
            foreach (var q in comando)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.cantidad) });
            }
            return lista;
        }

        public string primerinsumYregistrarProdTerm(String descrip,String codins,decimal? cantid,bool? tipo)
        {
            string codigo=null;
            try
            {
                var comando = contexto.pa_RegistrarProductoTerminado(descrip, codins, cantid, tipo).First();
                codigo = comando.codPTerm;
            }
            catch (Exception)
            {
                codigo = null;
            }
            return codigo;
        }

        public int? registrarInsEnProdTermCreado(String codig,String codins,decimal? cnt,bool? tipo)
        {
            int? rpta = null;
            try
            {
                var comando = contexto.pa_SoloRegistrarInsumoProdTerm(codig, codins, cnt, tipo);
                rpta = 1;
            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }
            return rpta;
        }

        //here  lista producto temrinado
        public List<PTerminado> ListarProdTerminado()
        {
            List<PTerminado> lista = new List<PTerminado>();

            var consulta = contexto.pa_listaTiposProdTerminado();

            foreach (var pt in consulta)
            {
                PTerminado prod = new PTerminado();

                prod.codigoPT = pt.codigoPT;
                prod.descripcion = pt.descripcion;
                lista.Add(prod);

            }
            return lista;
        }

        // here modified

        public List<String[]> ListarMermasProduccion(String ruc)
        {
            List<String[]> lista = new List<String[]>();
            var comando = contexto.pa_reportarMermas(ruc);
            foreach (var q in comando)
            {
                lista.Add(new String[] { q.ruc, q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.merma), String.Format(CultureInfo.InvariantCulture, "{0:0.000000}",q.costo)});
            }
            return lista;
        }
    }

}