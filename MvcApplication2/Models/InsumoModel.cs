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
    public class InsumoModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        AlmacenModel almacen = new AlmacenModel();


        public String elegido { get; set; }

        public short compraoventa { get; set; } //compra=1, venta=2
        //atributos para insumo
        //[StringLength(7, MinimumLength = 7,ErrorMessage = "deben ser 7 digitos")]  
        [Required(ErrorMessage = "El codigo de insumo es requerido")]
        public string codInsumo { get; set; }

        [Required(ErrorMessage = "El nombre de insumo es requerido")]
        public string nombreInsumo { get; set; }
        //[RegularExpression("\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}\b", ErrorMessage = "Año no valido")]
        [Required(ErrorMessage = "El codigo de insumo es requerido")]
        public string cod_uni { get; set; }

        [Required(ErrorMessage = "El codigo de insumo es requerido")]
        public string cod_fam { get; set; }

        // atributos for compra d guia de remision
        [Required(ErrorMessage = "El codigo compra es requerido")]
        public string codCompra { get; set; }

        [Required(ErrorMessage = "Ruc es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public string ruc { get; set; }

        public string forma_pago { get; set; }

        [Required(ErrorMessage = "Fecha es requerido")]
        public string fecha_emi { get; set; }

        [Required(ErrorMessage = "Fecha es requerido")]
        public string fecha_venc { get; set; }

        [Required(ErrorMessage = "El codigo es requerido")]
        public string razon_social { get; set; }

        [Required(ErrorMessage = "Razon social es requerido")]
        public string numGuia { get; set; }

        [Required(ErrorMessage = "Observación requerida")]
        public string observ { get; set; }

        // atributos for compra_detalle 
        public Int32 id { get; set; }
        public string impuesto { get; set; }
        public string cod_almac { get; set; }

        [MaxLength(11, ErrorMessage = "Deben ser 11 caracteres")]
        [MinLength(11, ErrorMessage = "Deben ser 11 caracteres")]
        public string numcomp { get; set; }


        public int moneda { get; set; }

        public String valor_venta { get; set; }

        public String tipodecambio { get; set; }

        public String igv { get; set; }

        public String valorventacalculado { get; set; }
        //limite
        [MaxLength(11, ErrorMessage = "Deben ser 11 caracteres")]
        public string numcompI { get; set; }

        [MaxLength(11, ErrorMessage = "Deben ser 11 caracteres")]
        public string numcompII { get; set; }
        //limite


        public string total { get; set; }
        [Required(ErrorMessage = "Observación de detalle requerida")]
        public string observdetalle { get; set; }
        [Required(ErrorMessage = "Cantidad requerida"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public string cant { get; set; }
        [Required(ErrorMessage = "El Precio es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public string precio { get; set; }


        public List<SelectListItem> listadropalmacen { get; set; }
        public List<AlmacenModel> listamodelalmacen { get; set; }


        public List<SelectListItem> listadropinsumos = new List<SelectListItem>();
        public List<InsumoModel> listamodelinsumos = new List<InsumoModel>();

        public List<SelectListItem> listadropfamilia = new List<SelectListItem>();
        public List<InsumoModel> listamodelfamilia = new List<InsumoModel>();

        public List<SelectListItem> listadrodunidad = new List<SelectListItem>();
        public List<InsumoModel> listamodelunidad = new List<InsumoModel>();


        //NUEVOS CAMBIOS ADD
        //lista para dropdown forma de pago
        public List<InsumoModel> listafechas { get; set; }
        [Required(ErrorMessage = "La  cuenta es requerida")]
        public string cuenta { get; set; }
        [Required(ErrorMessage = "La cantidad es requerida")]
        public string cant_adic { get; set; }
        [Required(ErrorMessage = "El Precioes requerido")]
        public string prec_adic { get; set; }
        [Required(ErrorMessage = "El total es requerido")]
        public string total_adic { get; set; }
        [Required(ErrorMessage = "El descripción del adicional es requerido")]
        public string desc_adic { get; set; }

        public List<String[]> listagastoscompra { get; set; }
        //CLOSE CAMBIOS ADD

        //lista para dropdown forma de pago
        public String isvisible { get; set; }

        //Add for lista compra de insumos
        public List<String[]> listacomprainsumos { get; set; }
        public String jsFormulario { get; set; }

        //lista stock de insumos 
        public List<String[]> listastcokinsumos { get; set; }

        //lista para agregar unidad y familia
        public List<String[]> listastinsumoreg { get; set; }

        public String msjInsumoModel { get; set; }

        public String igvisvisible { get; set; }
        //Constructor load data
        public InsumoModel()
        {
            forma_pago = "2";
            compraoventa = 1;
            isvisible = "visibility:visible";
            igvisvisible = "visibility:visible";
            elegido = "";
            igv = "0.18";
            impuesto = "0.18";
            valorventacalculado = "0";
            total_adic = "0";
            var consulta1 = from a in contexto.insumos
                            select new { a.nombre_insumo, a.cod_insumo };

            List<SelectListItem> listacompradeinsumos = new List<SelectListItem>();
            foreach (var q1 in consulta1)
            {
                listacompradeinsumos.Add(new SelectListItem() { Text = q1.cod_insumo.Trim() + "-" + q1.nombre_insumo.Trim() + "", Value = q1.cod_insumo.Trim() });
            }
            listadropinsumos = listacompradeinsumos;

            //here use lista compra insumos
            listacomprainsumos = new List<String[]>();
            listagastoscompra = new List<String[]>();
            jsFormulario = "1";

            //lista de insumos para consultar por almacen
            List<String[]> listastock = new List<String[]>();
            listastock.Add(new String[] { "", "", "" });
            listastcokinsumos = listastock;


            //consulta para llenar descripcion de unidades en registro insumo
            var consulta3 = from uni in contexto.unidads select uni.uni_des;
            List<SelectListItem> listaunidades = new List<SelectListItem>();

            foreach (var q2 in consulta3)
            {
                listaunidades.Add(new SelectListItem() { Text = q2 + "" });
            }
            listadrodunidad = listaunidades;

            // consulta para descripcion de familia

            var consulta4 = from fa in contexto.familias select fa.fam_des;
            List<SelectListItem> listafamilias = new List<SelectListItem>();
            foreach (var q3 in consulta4)
            {

                listafamilias.Add(new SelectListItem() { Text = q3 + "" });
            }
            listadropfamilia = listafamilias;

            //cargar nombre de almacenes
            var consulta5 = from al in contexto.almacens select al.descripcion;
            List<SelectListItem> listaalmacen = new List<SelectListItem>();
            foreach (var q4 in consulta5)
            {

                listaalmacen.Add(new SelectListItem() { Text = q4 + "" });
            }
            listadropalmacen = listaalmacen;


            //captura unidad y familia de insumos  para registrar insumo
            List<String[]> listareg = new List<String[]>();
            listareg.Add(new String[] { "", "" });
            listastinsumoreg = listareg;


        }

        //metodo  registrar insumo
        public int registrarInsumo(InsumoModel ins, Int32 listuni, Int32 listfam)
        {
            int valor = 0;
            try
            {
                contexto.pa_registrarInsumos(ins.codInsumo, ins.nombreInsumo, listuni, listfam);
                valor = 1;
                contexto.SubmitChanges();
            }
            catch (Exception ex)
            {
                valor = ex.HResult;

            }
            return valor;
        }


        //metodo para comprar insumo
        public int? registrarcompraInsumo(InsumoModel insu, String codAlmacen)
        {
            int? id = null;
            bool ifestado1 = false;
            bool ifestado2 = false;
            DateTime? fechain = retornaFecha(insu.fecha_emi);
            DateTime? fechafin = retornaFecha(insu.fecha_venc);
            ifestado1 = (insu.forma_pago.Equals("1"));
            ifestado2 = (insu.forma_pago.Equals("2"));
            Decimal? valorventa = retornaDecimal(insu.valor_venta);
            if (valorventa != null && valorventa > 0)
            {
                if ((ifestado1 && fechain != null) || (ifestado2 && fechain != null && fechafin != null && (fechafin > fechain)))
                {
                    var comando = contexto.pa_ExisteCompraGuia(insu.numcompI + "-" + insu.numcompII, insu.ruc).First();
                    id = comando.estado;
                }
                else
                {
                    if (ifestado2 && fechain != null && fechafin != null && (fechafin < fechain))
                    {
                        id = 3;
                    }
                    else
                    {
                        id = 4;
                    }
                }
            }
            else
            {
                id = 5;
            }
            return id;

        }

        //metodo para registrar insumo a detalle de compra
        public int?[] registrardetalleCompra(InsumoModel insu, int id, String codalmacen, String selectinsumo)
        {
            int?[] resultado = new int?[2];
            try
            {
                if (id == 0)
                {
                    var comando1 = contexto.pa_insertarCompraoVentaRC(insu.numcomp, insu.ruc, retornaFecha(insu.fecha_emi), insu.forma_pago, retornaFecha(insu.fecha_venc), retornaDecimal(insu.valor_venta), retornaDecimal(insu.igv), insu.numGuia, insu.observ, codalmacen, retornaDecimal(insu.tipodecambio), insu.moneda,insu.compraoventa).First();
                    contexto.SubmitChanges();
                    resultado[0] = comando1.ID;
                }
                else
                {
                    resultado[0] = id;
                }
                var comando2 = contexto.pa_RegistrarInsumosCompraoVenta(resultado[0], selectinsumo, retornaDecimal(insu.precio), retornaDecimal(insu.cant), insu.observdetalle, codalmacen).First();
                resultado[1] = comando2.siseregistro;
                return resultado;

            }
            catch (Exception)
            {
                resultado[0] = 0;
                resultado[1] = 0;
                return resultado;
            }

        }

        public List<String[]> listadetallesdecompra(String codalmacen, int? rpta)
        {
            List<String[]> lista = new List<string[]>();
            var query2 = contexto.pa_listarDetalleCompras(codalmacen, rpta);
            foreach (var q in query2)
            {
                lista.Add(new String[] { q.nombre_insumo, q.uni_des, q.observacion, String.Format(CultureInfo.InvariantCulture, "{0:0.00000}", q.cantidad), String.Format(CultureInfo.InvariantCulture, "{0:0.00000}", q.precio), String.Format(CultureInfo.InvariantCulture, "{0:0.00}", (retornaDecimal(q.cantidad + "") * retornaDecimal(q.precio + ""))) });
            }
            return lista;

        }

        public List<String[]> listadegastoscompra(int? rpta)
        {
            List<String[]> lista = new List<string[]>();
            var query = contexto.pa_ListarGastosCompraPorCompra(rpta);
            foreach (var q in query)
            {
                lista.Add(new String[] { q.Cuenta, q.Descripcion, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.Cantidad), String.Format(CultureInfo.InvariantCulture, "{0:0.00}", q.Precio), String.Format(CultureInfo.InvariantCulture, "{0:0.00}", q.Total) });
            }
            return lista;

        }
        //metodo para buscar insumo
        public List<String[]> buscarInsumo(String codIns, String almac)
        {
            List<String[]> lista = new List<String[]>();

            try
            {
                var consulta = contexto.pa_consultarStock(codIns, almac);


                foreach (var query in consulta)
                {
                    lista.Add(new String[] { query.cod_insumo + "", query.nombre_insumo + "", query.cantidad + "" });
                }

            }
            catch (Exception)
            {

                throw;
            }
            return lista;

        }

        public InsumoModel stockInsumo(InsumoModel ins, String ca, int estado)
        {
            ins.listastcokinsumos = new List<String[]>();
            ins.listadropinsumos = new List<SelectListItem>();
            var query = contexto.pa_listarInsumoConStock(ca);
            foreach (var q in query)
            {
                ins.listadropinsumos.Add(new SelectListItem() { Text = q.nombre_insumo + "", Value = q.cod_insumo.Trim() });
                if (estado == 0)
                {
                    ins.listastcokinsumos.Add(new String[] { q.cod_insumo.Trim() + "", q.nombre_insumo + "", q.cantidad + "" });
                }
            }
            if (estado == 1)
            {
                ins.listastcokinsumos = buscarInsumo(ins.elegido, ca);
            }
            return ins;
        }

        //metodo para buscar nueva compra
        public InsumoModel BuscarCompra(int codigo)
        {
            InsumoModel c = new InsumoModel();

            try
            {
                //heare pa_ buscar compra
                var consulta = contexto.pa_buscarcompra(codigo);
                foreach (var com in consulta)
                {
                    c.id = com.id;
                    c.fecha_emi = String.Format("{0:dd/MM/yyyy}", com.fecha_emision);
                    c.numcomp = com.numero;
                    c.observ = com.observacion;
                    c.razon_social = com.razon_social;
                    c.impuesto = com.impuesto.ToString();
                    c.valor_venta = com.valor_venta.ToString();
                    c.total = com.total.ToString();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return c;

        }
        //metodo para eliminar la compra
        public string Eliminarcompra(int id)
        {

            string resultado = string.Empty;
            try
            {
                contexto.pa_eliminarcompra(id);
                resultado = "ok";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        //==============nuevos cambios===================
        //metodo para buscar detalle compra
        public detalleCompraModel BuscardetalleCompra(String coda, String codinsu, int id)
        {
            
            detalleCompraModel c = new detalleCompraModel();
            try
            {
                //here pa_ buscar compra
                var consulta = contexto.pa_buscaDetalleCompra(coda, codinsu, id);
                foreach (var com in consulta)
                {
                    c.idcompra = com.id;
                    c.codinsumo = com.cod_insumo;
                    c.insumo = com.nombre_insumo;
                    c.precio = com.precio.ToString();
                    c.cantidad = com.cantidad.ToString();
                    c.Observacion = com.observacion;
                    c.total = com.total.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return c;

        }
        //metodo para eliminar detalle de la compra
        public String Eliminardetallecompra(String coda, String ins, int id)
        {
            String resultado = "";
            try
            {
                contexto.pa_eliminarDetalleCompra(coda, ins, id);
                resultado = "ok";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }

        public int insertarGastosCompra(int id, String cuenta, String descrp, double? cantid, decimal? price, decimal? total)
        {
            int resultado = 0;
            try
            {
                contexto.pa_InsertarGastosCompra(id, cuenta, descrp, cantid, price, total);
                resultado = 1;
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
            }
            return resultado;
        }


        private DateTime? retornaFecha(String fecha)
        {
            DateTime? fechainventario = null;
            try
            {
                fechainventario = DateTime.Parse(fecha);
            }
            catch (Exception)
            {
                fechainventario = null;
            }
            return fechainventario;
        }


        private double? retornaDouble(String cantidad)
        {
            double? cant = null;
            try
            {
                cant = Double.Parse(cantidad);
            }
            catch (Exception)
            {
                cant = null;
            }
            return cant;
        }

        private decimal? retornaDecimal(String cantidad)
        {
            decimal? cant = null;
            try
            {
                cant = Decimal.Parse(cantidad);
            }
            catch (Exception)
            {
                cant = null;
            }
            return cant;
        }

    }

}



