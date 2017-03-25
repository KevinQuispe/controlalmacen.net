using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace almacen.Models
{
    public class detalleInsumosModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();

        public String codInsumo {get;set;}
         
        public String nomInsumo {get;set;}

        public String famInsumo {get;set;}

        public String uniInsumo {get;set;}

        public String fecha { get; set; }

        public String cantActual { get; set; }

        public String costo_uni { get; set; }

        public List<String[]> listainsumosinventini { get; set; }

        public int estado { get; set; }

        public String idbotonclickeado { get; set; }

        public String javascriptadicional { get; set; }

        public List<SelectListItem> listadropinsumos { get; set; }

        public String MensajeInventario { get; set; }

        public detalleInsumosModel()
        {
            listadropinsumos = new List<SelectListItem>();
            listainsumosinventini = new List<String[]>();
            estado = 1;
            idbotonclickeado = "";
            javascriptadicional = "document.getElementById('btnRegistrar').disabled = true;";//document.getElementById('btnRegistrar').disabled = true;
        }

        public List<SelectListItem> listadropdeinsumos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            var query = contexto.pa_listarInsumos();
            foreach (var q in query)
            {
                lista.Add(new SelectListItem() { Text = q.cod_insumo.Trim()+"-"+q.nombre_insumo + "", Value = q.cod_insumo.Trim() });
            }
            return lista;
        }

        public List<String[]> ListaInventarioInicial(detalleInsumosModel d,DateTime? fech,String codalmacen)
        {
            var query = contexto.pa_ListarInventarioInicial(fech,codalmacen);
            foreach (var q in query)
            {
                d.listainsumosinventini.Add(new String[] { q.cod_insumo.Trim() + "", q.nombre_insumo + "", String.Format(CultureInfo.InvariantCulture, "{0:0.00000}", q.costoinicial), String.Format(CultureInfo.InvariantCulture, "{0:0.00}", q.cant_actual), String.Format("{0:dd-MM-yyyy}", q.Fecha) });
            }
            return d.listainsumosinventini;
        }
    }
}