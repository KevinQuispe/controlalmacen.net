using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations; 
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Threading.Tasks;
using System.Globalization;
namespace almacen.Models
{
    public class AlmacenModel
    {
        // get and set for almacenes
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Deben ser 2 digitos")]
        [Required(ErrorMessage = "Codigo de almacen requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String codigo { get; set; }
       
        [Required(ErrorMessage = "Direccion de almacen requerido")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Descripcion de almacen requerido")]
        public string descripcion { get; set; }

        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();

        public List<SelectListItem> listadropalmacenes { get; set; }
        public List<AlmacenModel> listamodelalmacenes { get; set; }

        //add this
        public List<SelectListItem> listadropinsumos = new List<SelectListItem>();
        public List<InsumoModel> listamodelinsumos = new List<InsumoModel>();

        [Required(ErrorMessage = "Codigo de almacen requerido")]
        public String codigoReceptor { get; set; }

        [Required(ErrorMessage = "Cantidad requerida")]
        public String cantidadRecibir { get; set; }

        [Required(ErrorMessage = "Insumo requerido")]
        public String insumo { get; set; }

        [Required(ErrorMessage = "Guia requerido")]
        public String guia { get; set; }

        public int estado { get; set; }

        public List<String []> tablansumoscant { get; set; }

        public String msjAlmacen { get; set; }
        //load data almacen
        public AlmacenModel()
        {

            estado = 1;
            var consulta1 = from a in contexto.almacens
                            select new {a.descripcion,a.cod_almacen };
            listadropalmacenes = new List<SelectListItem>();
            listadropinsumos = new List<SelectListItem>();
            tablansumoscant = new List<String[]>();
            foreach (var q1 in consulta1)
            {

                listadropalmacenes.Add(new SelectListItem() { Text = q1.descripcion + "", Value = q1.cod_almacen + "-" + q1.descripcion });
            }

            var consulta2 = from a in contexto.insumos
                            select new { a.nombre_insumo, a.cod_insumo };
            //lista de insumos            
            foreach (var q1 in consulta2)
            {
                listadropinsumos.Add(new SelectListItem() { Text = q1.cod_insumo.Trim() + "-" + q1.nombre_insumo.Trim() + "", Value = q1.cod_insumo.Trim() });
            }            
        }
       
        public String resistraralmacen(AlmacenModel a)
        {
          
            //create a new object
            string resultado = string.Empty;

            try
            {
                contexto.pa_registrarAlmacen(a.codigo, a.descripcion,a.direccion);
                resultado = "ok";
                contexto.SubmitChanges();
                return resultado;

            }
            catch (Exception ex)
            {
                resultado = ex.Message;
                return resultado;
            }

        }

        public List<SelectListItem> listaAlmacenesMovimientos(String codalm)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            var consulta1 = from a in contexto.almacens
                            where a.cod_almacen != codalm
                            select new { a.descripcion, a.cod_almacen };
            foreach (var q1 in consulta1)
            {
                lista.Add(new SelectListItem() { Text = q1.descripcion + "", Value = q1.cod_almacen + "-" + q1.descripcion });
            }
            return lista;
        }

        public String nombreAlmacen(String codalm)
        {
            String nombre = "";
            var consulta = (from a in contexto.almacens
                           where a.cod_almacen == codalm
                           select new { a.descripcion }).First();
            nombre = codalm+" - "+consulta.descripcion;
            return nombre;
        }

        public int? existeGuia(String numg)
        {
            int? rpta = 0;
            try
            {
                var comando = contexto.pa_MovimientosAlmacen(numg).First();
                rpta = comando.existe;
            }catch(Exception ex){
                rpta = ex.HResult;
            }
            return rpta;
        }


        public int?[] InsercionTrasladoAlm(String Ce,String Cr,String ng,String ci,double? cant)
        {
            int?[] rpta=new int?[2];
            try
            {
                var comando = contexto.pa_RegGuiaTrasladoAlmacen(Ce, Cr, ng, ci, cant).First();
                rpta[0] = comando.excede;
                rpta[1] = comando.inserto;
            }
            catch (Exception)
            {
                rpta[0] = 1;
                rpta[1] = 0;
            }
            return rpta;
        }

        public List<String[]> listaInsumosGTA(String numg)
        {
            List<String[]> lista = new List<string[]>();
            var query = contexto.pa_listarInsumGuiaTrasAlm(numg);
            foreach (var q in query)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.00000}", q.cantidad) });
            }
            return lista;
        }

    }
  }
