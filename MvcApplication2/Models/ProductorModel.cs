using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//ADD THIS
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
namespace almacen.Models
{
    public class ProductorModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        InsumoModel insu = new InsumoModel();
        //atributos detalle insumo
        public short estado { get; set; }
        public String nomInsumo { get; set; }
        public String UnidadMed { get; set; }
        public String cant { get; set; }
        public String precio { get; set; }
        public String total { get; set; }
        public String produccion { get; set; }

        AlmacenModel almac = new AlmacenModel();
        //atributas para la guia
        [Required(ErrorMessage = "El numero guia es requerido")]
        public String numGuia { get; set; }
        public short salidaodevolucion { get; set; } //1 salida 2 devolución

        public String rznsocial { get; set; }

        [Required(ErrorMessage = "El ruc es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String ruc { get; set; }

        [Required(ErrorMessage = "La  fecha es requerida"),]
        public String fecha { get; set; }

        [Required(ErrorMessage = "La  fecha es requerida"),]
        public String fechaT { get; set; }

        [Required(ErrorMessage = "La persona recepcion es requerida"),]
        public String personaRecep { get; set; }

        public String observ { get; set; }

        [Required(ErrorMessage = "El insumo es requerido")]
        public String insumo { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public String cantidad { get; set; }

        public String codAlmacen { get; set; }
        //agregar inusumos
        public List<SelectListItem> listadropinsumos { get; set; }
        public List<ProductorModel> listamodelinsumos { get; set; }

        //lista de  tipo de produccion
        public List<SelectListItem> listadropproduccion { get; set; }
        public List<ProductorModel> listamodelproduccion { get; set; }


        public List<String[]> listainsumosguia { get; set; }
        public String jsFormulario { get; set; }
        public String msjProductorModel { get; set; }
        public String listado { get; set; }
        public List<String[]> listatipobyproduccion { get; set; }
        public String codtipo { get; set; }
        public String msjerror { get; set; }

        //neew cmabios for linea
        public List<SelectListItem> listadropliena { get; set; }
        public List<ProductorModel> listamodellinea { get; set; }
        public List<String[]> listainsqfaltan { get; set; }
        public String codlinea { get; set; }
        public String cantCajas { get; set; }
        public short paletizado { get; set; }

        // nuevos atributos para guia
        public DateTime fechatrastaldo { get; set; }
        public String puntopartida { get; set; }
        public String puntollegada { get; set; }
        public String razondestinatario { get; set; }
        public String ruccliente { get; set; }
        public String marcaplacavh { get; set; }
        public String otrodoc { get; set; }
        public String confvehicular { get; set; }
        public String MTC { get; set; }
        public String licenciaconducir { get; set; }
        public String razonsocialagro { get; set; }
        public String destinatario { get; set; }
        public String rucagro { get; set; }
        public String confi_vehic { get; set; }
        public short salida { get; set; }
        public String codalmc2 { get; set; }

        //load insumos in constructor
        public ProductorModel()
        {
            salidaodevolucion = 1;
            estado = 1;           
            var consulta1 = from a in contexto.insumos
                            select new { a.nombre_insumo, a.cod_insumo };

            listadropinsumos = new List<SelectListItem>();
            listadropproduccion = new List<SelectListItem>();
            listatipobyproduccion = new List<String[]>();
            listainsqfaltan = new List<String[]>();
            listainsumosguia = new List<string[]>();
            foreach (var q1 in consulta1)
            {
                listadropinsumos.Add(new SelectListItem() { Text = q1.cod_insumo.Trim() + "-" + q1.nombre_insumo.Trim() + "", Value = q1.cod_insumo });
            }            
            jsFormulario = "1";

            //producion
            var consulta2 = from q in contexto.PTerminados
                            select new { q.codigoPT, q.descripcion};
            foreach (var q2 in consulta2)
            {
                listadropproduccion.Add(new SelectListItem() { Text = q2.codigoPT + "-" + q2.descripcion + "", Value = q2.codigoPT });
            }

            //linea produccion
            var consulta3 = from q in contexto.lineaPTs
                            select new { q.cod_linea,q.descripcion };
            listadropliena = new List<SelectListItem>();
            foreach (var q3 in consulta3)
            {
                listadropliena.Add(new SelectListItem() { Text = q3.descripcion + "", Value = q3.cod_linea.ToString() });
            }

        }
        //agregar insumos a productor
        public int? agregarInsumoProductor(ProductorModel prod,DateTime? fecha, String codalmcen, String insumoescogido)
        {
            int? resultado = 0;
            try
            {
                var comando=contexto.pa_AgregarInsumoaGuia(insumoescogido, prod.numGuia, Convert.ToDouble(prod.cantidad), fecha, prod.personaRecep, prod.observ, prod.ruc,prod.salidaodevolucion,codalmcen).First();
                resultado=comando.respuesta;
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
            }            
            return resultado;

        }
        //Buscar guia de cliente 
        public ProductorModel BuscarGuiaCliente(String numguia, String codalm)
        {

            ProductorModel g = new ProductorModel();

            try
            {
                //heare pa_ buscar guia del cliente

                var consulta = contexto.pa_buscarGuiaCliente(numguia, codalm);
                foreach (var com in consulta)
                {
                    g.fecha = String.Format("{0:dd/MM/yyyy}", com.fecha);
                    g.numGuia = com.numeroguia;
                    g.ruc = com.ruc;
                    g.personaRecep = com.persona_recepcion;
                    g.observ = com.observacion;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return g;
        }
        //metodo para eliminar la guia del cliente
        public String EliminarguiaCliente(String numg, String coda)
        {

            String resultado = "";
            try
            {
                contexto.pa_eliminarGuiaCliente(numg, coda);
                resultado = "ok";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        //==============nuevos cambios===================
        //metodo para buscar detalle guia
        public detalleGuiaModel BuscardetalleGuia(String numGuia, String codInsu)
        {
            detalleGuiaModel dg = new detalleGuiaModel();

            try
            {

                var consulta = contexto.pa_buscarDetalleGuia(numGuia, codInsu);
                foreach (var com in consulta)
                {

                    dg.nomInsumo = com.nombre_insumo;
                    dg.numGuia = com.numeroguia.Trim();
                    dg.codInsumo = codInsu;
                    dg.cant = com.cantidad.ToString();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return dg;

        }

     
        //metodo para eliminar detalle de la compra
        public String Eliminardetalleguia(String numGuia, String codInsu)
        {
            String resultado = "";
            try
            {
                contexto.pa_eliminarDetalleGuia(numGuia, codInsu);
                contexto.SubmitChanges();
                resultado = "ok";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public int? agregarDetalleGuiaProductor(String numg,String codins,double? cant,String codalm)
        {
            int? resultado = 0;
            try
            {
                var comando = contexto.pa_SoloAgregarInsumoaGuia(numg, codins, cant, codalm);
                resultado = 1;
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
            }
            return resultado;
        }

        //modified day 01-02 day 10
        public List<String[]> listaTipoProduccion(String tipo)
        {
            List<String[]> lista = new List<String[]>();
            var queryy = contexto.pa_listarProdTipoProduccion(tipo);
            foreach (var q in queryy)
            {
                lista.Add(new String[] { q.nombre_insumo, q.uni_des, q.cantidad.ToString() });
            }
            return lista;
        }


        public List<String[]> AlcanzaGuiaProdTerm(String codpt, String codalm, int? cajas, bool? tipo)
        {
            List<String[]> lista = new List<String[]>();
            try
            {
                var query1 = contexto.pa_AlcanzaStockAlmacen(codpt,codalm,cajas, tipo);
                foreach (var q in query1)
                {
                    lista.Add(new String[] { q.codinsumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.falta)});
                }
            }catch(Exception){
            }
            return lista;
        }

        public int? RegistrarGuiaPTerminado1eraVez(String codalm, String numg, DateTime? fech, String obs, String persrec, String ruc, String codpt, int ncajas, DateTime fechtras, String puntop, String puntoplleg, String razoagro, String destinat, String rucagro, String configv, String cert_mtc, String licencia,String otrodoc,short salid,String codalm2, bool? pal)
        {
            int? rpta = 0;
            try
            {
                //var query = contexto.pa_RegistraGuiaPTerminado(codalm, numg, fech, obs, persrec, ruc, codpt, ncajas, pal).First();
                //rpta = query.inserto;
                var query = contexto.pa_RegistraGuiaPTerminadok(codalm, numg, fech, obs, persrec, ruc, codpt, ncajas, fechtras, puntop, puntoplleg, razoagro, destinat, rucagro, configv, cert_mtc, licencia, otrodoc, salid,codalm2,pal).First();
                rpta = query.inserto;

            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }
            return rpta;
        }

        public int? existeGuiaProdSinVal(String numg)
        {
            int? rpta = 0;
            try
            {
                var query = contexto.pa_ExistGuiaProductorSinVal(numg).First();
                rpta = query.existe;
            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }
            return rpta;
        }

        public int? RegistrarGuiaPTerminado(String numg, String codpt, int ncajas, bool? pal)
        {
            int? rpta = 0;
            try
            {
                var query = contexto.pa_RegistraGuiaPTerminado2(numg, codpt, ncajas, pal).First();
                rpta = query.inserto;
            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }
            return rpta;
        }

        public List<String[]> listaDetalleGuia(String ng)
        {
            List<String[]> lista = new List<string[]>();
            try
            {
                var comd = contexto.pa_ListarDetalleGuiav2(ng);
                foreach (var q in comd)
                {
                    lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.cantidad) });
                }
            }catch(Exception){

            }
            return lista;
        }


        public int? registrarDetallGuiav2(String ng,String ct,int? nc,bool? pal)
        {
            int? rpta = 0;
            try
            {
                var comando = contexto.pa_InsertarDetalleGuiaProductorv2(ng, ct, nc,pal);
                rpta = 1;
            }
            catch (Exception ex)
            {
                rpta = ex.HResult;
            }
            return rpta;
        }

        public int actualizaDetelllGuiav2(String ng,String ci,double? cant,String codalm)
        {
            int rpta = 0;
            try
            {
                var comando = contexto.pa_ActualizarDetalleGuia(ng, ci, cant, codalm);
                rpta = 1;
            }catch(Exception ex){
                rpta = ex.HResult;
            }
            return rpta;
        }

    }
}
       
      
   
