using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//ADD THIS
using System.Collections;
using almacen.Models;
using almacen.Controllers;
using System.Globalization;
namespace almacen.Controllers
{
    public class InsumosController : Controller
    {
        //
        // GET: /Insumos/
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        InsumoModel model = new InsumoModel();

        public ActionResult registrarinsumos()
        {
            try
            {
                InsumoModel res = new InsumoModel();
                List<InsumoModel> lista = new List<InsumoModel>();
                res.listamodelunidad = lista;
                res.listamodelfamilia = lista;
                return View(res);
            }
            catch (Exception)
            {
                return View();
            }
        }
        //New cambios
        public ActionResult ListadeInsumos()
        {
            List<detalleInsumosModel> lista = new List<detalleInsumosModel>();

            var query = contexto.pa_listarInsumos();
            foreach (var q in query)
            {
                detalleInsumosModel ins = new detalleInsumosModel();
                ins.codInsumo = q.cod_insumo;
                ins.nomInsumo = q.nombre_insumo;
                ins.famInsumo = q.fam_des;
                ins.uniInsumo = q.uni_des;
                lista.Add(ins);
            }
            return View(lista);
        }
        //metodo registrar insumo
        [HttpPost]
        public ActionResult registrarinsumos(InsumoModel insumo, String listunidad = "", String listafamilia = "")
        {
            try
            {
                //lista para las unidades 
                var query1 = (from inidad in contexto.unidads
                              where inidad.uni_des == listunidad
                              select inidad).First();

                Int32 coduni = query1.cod_unidad;

                //lista para las unidades 
                var query2 = (from fami in contexto.familias
                              where fami.fam_des == listafamilia
                              select fami).First();

                Int32 codfam = query2.cod_familia;

                String codFami = query2.cod_familia.ToString();
                String mensaje = "";
                int comando = model.registrarInsumo(insumo, coduni, codfam);
                if (comando == 1)
                {
                    mensaje = "<span class=\"text-primary\"><i class=\"fa fa-thumbs-o-up fa-2x\" aria-hidden=\"true\"></i> Se ha registrado correctamente el insumo</span>";
                }
                else
                    if (comando == -2146232060)
                    {
                        mensaje = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Este insumo fue registrado anteriormente</span>";
                    }
                insumo.msjInsumoModel = mensaje;
            }

            catch (Exception)
            {
                String msj = "Error¡¡¡";
                insumo.msjInsumoModel = msj;
            }
            return View("registrarinsumos", insumo);
        }

        public ActionResult compras()
        {
            if (Session["codigoalmacen"] != null)
            {
                List<Compras> lc = new List<Compras>();
                var query = contexto.pa_ListarCompras(Session["codigoalmacen"].ToString());
                foreach (var q in query)
                {
                    Compras c = new Compras();
                    c.id = q.id;
                    c.formadepago = q.forma_pago;
                    c.numero = q.numero;
                    c.razonsocial = q.razon_social;
                    c.fechaemision = String.Format("{0:dd/MM/yyyy}", q.fecha_emision);
                    c.fechavencimiento = String.Format("{0:dd/MM/yyyy}", q.fecha_vencimiento);
                    c.observacion = q.observacion;
                    c.valorventa = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", q.valor_venta);
                    c.impuesto = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", q.impuesto);
                    c.total = q.total + "";
                    lc.Add(c);
                }
                return View(lc);
            }
            else
            {
                return View("../Home/Login");
            }

        }

        public ActionResult comprainsumos()
        {
            try
            {
                InsumoModel res = new InsumoModel();
                Session.Remove("idcompra");
                Session["idcompra"] = null;
                List<InsumoModel> lista = new List<InsumoModel>();
                res.listamodelinsumos = lista;
                return View(res);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult comprainsumos(InsumoModel insumo, String btnguardarcompra)
        {
            if (btnguardarcompra == null)
            {
                ModelState.Clear();
                if (insumo.compraoventa == 2 || insumo.compraoventa == 1)
                {
                    if (ModelState.IsValidField("fecha_emi"))
                    {
                        DateTime? fecha = retornaFecha(insumo.fecha_emi);
                        if (fecha != null)
                        {
                            var comando = contexto.pa_retornarTipoCambio(fecha).First();
                            if (insumo.compraoventa == 1)
                            {
                                insumo.tipodecambio = String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", comando.Venta);
                            }
                            else
                            {
                                insumo.tipodecambio = String.Format(CultureInfo.InvariantCulture, "{0:0,0.000}", comando.Compra);
                            }                            
                            insumo.igvisvisible = "visibility:hidden";
                        }
                        else
                        {
                            insumo.msjInsumoModel = "<span class=\"text-danger\"><i class=\"fa fa-exclamation-triangle fa-2\" aria-hidden=\"true\"> Seleccione antes fecha de emisión</i></span>";
                        }
                    }
                }
                insumo.jsFormulario = "1"; 
                return View("comprainsumos", insumo);
            }
            else
            {
                if ((insumo.forma_pago == "1" || insumo.forma_pago == "2") && (insumo.compraoventa == 1 || insumo.compraoventa == 2))
                {
                    try
                    {
                        Session["idcompra"] = null;
                        String mensaje = "";
                        ModelState.Remove("cant");
                        ModelState.Remove("precio");
                        ModelState.Remove("cant");
                        ModelState.Remove("observdetalle");
                        if (insumo.forma_pago == "1")
                        {
                            insumo.fecha_venc = "";
                            ModelState.Remove("fecha_venc");
                        }
                        int? estado = insumo.registrarcompraInsumo(insumo, Session["codigoalmacen"].ToString());
                        if (estado == 0)
                        {
                            mensaje = "<span class=\"text-primary\"><i class=\"fa fa-thumbs-o-up fa-2x\" aria-hidden=\"true\"></i> Ahora, ingrese insumos a la compra.</span>";
                            insumo.jsFormulario = "2";
                            insumo.numcomp = insumo.numcompI + "-" + insumo.numcompII;
                        }
                        else
                        {
                            mensaje = "<span class=\"text-danger\"><i class=\"fa fa-fa-exclamation-triangle fa-2\" aria-hidden=\"true\"> Ingrese otros valores</i></span>";
                            if (estado == 1)
                            {
                                ModelState.AddModelError("numcomp", "Ingrese otro número de compra, éste ya existe");
                            }
                            else
                            {
                                if (estado == 2)
                                {
                                    ModelState.AddModelError("ruc", "Este ruc de proveedor no está registrado");
                                }
                                else
                                {
                                    if (estado == 3)
                                    {
                                        mensaje = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2\" aria-hidden=\"true\"> Fecha de vencimiento debe ser mayor</i></span>";
                                    }
                                    else
                                    {
                                        if (estado == 4)
                                        {
                                            mensaje = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2\" aria-hidden=\"true\"> Error en fechas</i></span>";
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("valor_venta", "Ingrese valor venta correcto");
                                        }
                                    }
                                }
                            }
                        }
                        insumo.msjInsumoModel = mensaje;
                        return View("comprainsumos", insumo);
                    }
                    catch (Exception ex)
                    {
                        return Content("<p>Error!!!!" + ex.ToString() + "</p>");
                    }
                }
                else
                {
                    InsumoModel res = new InsumoModel();
                    return View("comprainsumos", res);
                }

            }

        }

        //metodo para agregar insuumo a detalle_compras
        [HttpPost]
        public ActionResult detallecompraInsumo(InsumoModel insu, String insumos = "", String sbinsumoencompra = "")
        {
            ModelState.Remove("valorventacalculado");
            ModelState.Remove("total");
            ModelState.Remove("total_adic");
            ModelState.Remove("Fecha_emi");
            ModelState.Remove("Fecha_venc");
            ModelState.Remove("ruc");
            ModelState.Remove("numGuia");
            var cmd = contexto.pa_RetornaTotales(Convert.ToInt32(Session["idcompra"])).First();
            Decimal? valorcalculado = cmd.totalinsumos;
            Decimal? valorcalculadoo = cmd.totalgastoscompra;
            switch (sbinsumoencompra)
            {
                case "agregar":
                    try
                    {
                        int idinicial = Convert.ToInt32(Session["idcompra"]);
                        Decimal? cantidadaagregar = (retornaDecimal(insu.precio) * retornaDecimal(insu.cant));
                        if (valorcalculado + valorcalculadoo + cantidadaagregar <= retornaDecimal(insu.valor_venta))
                        {
                            int?[] rpta = model.registrardetalleCompra(insu, idinicial, Session["codigoalmacen"].ToString(), insumos);
                            if (rpta[0] != 0 && rpta[1] == 1)
                            {
                                insu.valorventacalculado = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", (valorcalculado + cantidadaagregar));
                                insu.msjInsumoModel = "<span class=\"text-primary\"><i class=\"fa fa-thumbs-o-up fa-2x\" aria-hidden=\"true\"></i> El insumo fue agregado correctamente en la compra</span>";
                            }
                            else
                            {
                                insu.valorventacalculado = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", valorcalculado);
                                insu.msjInsumoModel = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Este insumo fue registrado anteriormente en esta compra</span>";
                            }
                            Session["idcompra"] = rpta[0];
                        }
                        else
                        {
                            insu.msjInsumoModel = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Las cantidades ingresadas superan el valor de venta</span>";
                        }
                        insu.total = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", (valorcalculado + (retornaDecimal(insu.impuesto) * valorcalculado)));
                        insu.listagastoscompra = insu.listadegastoscompra(Convert.ToInt32(Session["idcompra"]));
                        insu.listacomprainsumos = insu.listadetallesdecompra(Session["codigoalmacen"].ToString(), Convert.ToInt32(Session["idcompra"]));
                        insu.jsFormulario = "2";
                        return View("comprainsumos", insu);
                    }
                    catch (Exception ex)
                    {
                        return Content("<p>Error!!!!" + ex.ToString() + "</p>");
                    }
                case "grabar":
                    ModelState.Remove("cant");
                    ModelState.Remove("observdetalle");
                    ModelState.Remove("precio");
                    Decimal? cantidadaagregaar = (retornaDecimal(insu.prec_adic) * retornaDecimal(insu.cant_adic));
                    if (valorcalculado + valorcalculadoo + cantidadaagregaar <= retornaDecimal(insu.valor_venta))
                    {
                        int comando = insu.insertarGastosCompra(Convert.ToInt32(Session["idcompra"]), insu.cuenta, insu.desc_adic, retornaDouble(insu.cant_adic), retornaDecimal(insu.prec_adic), retornaDecimal(insu.total_adic));
                        if (comando == 1)
                        {
                            insu.total_adic = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", (valorcalculadoo + cantidadaagregaar));
                            insu.msjInsumoModel = "<span class=\"text-primary\"><i class=\"fa fa-thumbs-o-up fa-2x\" aria-hidden=\"true\"></i> El costo adicional fue agregado correctamente en la compra</span>";
                        }
                        else
                        {
                            if (comando == -2146232060)
                            {
                                insu.msjInsumoModel = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Este costo adicional fue registrado anteriormente en esta compra</span>";
                            }
                            else
                            {
                                insu.msjInsumoModel = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Error!!!</span>";
                            }
                            insu.total_adic = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", valorcalculadoo);                            
                        }
                    }
                    else
                    {
                        insu.msjInsumoModel = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Las cantidades ingresadas superan el valor de venta</span>";                        
                    }
                    insu.listagastoscompra = insu.listadegastoscompra(Convert.ToInt32(Session["idcompra"]));
                    insu.listacomprainsumos = insu.listadetallesdecompra(Session["codigoalmacen"].ToString(), Convert.ToInt32(Session["idcompra"]));
                    insu.jsFormulario = "2";
                    return View("comprainsumos", insu);
                case "asignar":
                    ModelState.Remove("cant");
                    ModelState.Remove("observdetalle");
                    ModelState.Remove("precio");
                    var query = contexto.pa_AsignarCostosAdicionales(Convert.ToInt32(Session["idcompra"]),retornaDecimal(insu.total_adic));
                    insu.listacomprainsumos = insu.listadetallesdecompra(Session["codigoalmacen"].ToString(), Convert.ToInt32(Session["idcompra"]));
                    insu.listagastoscompra = insu.listadegastoscompra(Convert.ToInt32(Session["idcompra"]));
                    insu.jsFormulario = "var fields = document.getElementById(\"form1\").getElementsByTagName('*');for(var i = 0; i < fields.length; i++){fields[i].disabled = true;}document.getElementById(\"botonasignar\").disabled=true;document.getElementById(\"formcostosadicionales\").style.display='none';";
                    return View("comprainsumos", insu);
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return Content("<p>Este no es un error de programación sino de asp.net que es limitado</p>");
            }
        }

        //metodo para realizar consulta de insumos_compra

        public ActionResult detallecompras(int id)
        {
            List<detalleCompraModel> lista = new List<detalleCompraModel>();
            var consulta = contexto.pa_listarDetalleCompras(Session["codigoalmacen"].ToString(), id);
            foreach (var insu in consulta)
            {
                detalleCompraModel d = new detalleCompraModel();
                d.idcompra = id;
                d.codinsumo = insu.cod_insumo;
                d.insumo = insu.nombre_insumo;
                d.precio = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", insu.precio);
                d.cantidad = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", insu.cantidad);
                d.total = insu.total.ToString();
                d.Observacion = insu.observacion;
                lista.Add(d);
            }
            return View(lista);
        }

        public ActionResult editarcompras(int id)
        {
            String codalma = Session["codigoalmacen"].ToString();
            var query = contexto.pa_ListarCompras(Session["codigoalmacen"].ToString()).Where(x => x.id == id).First();
            Compras c = new Compras();
            c.numero = query.numero;
            c.razonsocial = query.razon_social;
            c.fechaemision = String.Format("{0:dd-MM-yyyy}", query.fecha_emision);
            if (query.fecha_emision != null)
            {
                c.fechavencimiento = String.Format("{0:dd-MM-yyyy}", query.fecha_vencimiento);
            }
            else
            {
                c.fechavencimiento = null;
            }           
            c.valorventa = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", query.valor_venta);
            c.observacion = query.observacion;
            c.impuesto = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", query.impuesto);
            c.total = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", query.total);
            return View(c);
        }

        [HttpPost]
        public ActionResult editarcompras(Compras c)
        {
            String respuesta = "";
            try
            {
                contexto.pa_ActualizarCompra(c.id, c.numero, retornaFecha(c.fechaemision), retornaFecha(c.fechavencimiento), Convert.ToDecimal(c.valorventa), c.observacion, Session["codigoalmacen"].ToString(), Convert.ToDecimal(c.impuesto));
                contexto.SubmitChanges();
                respuesta = "Se actualizó compra correctamente";
            }
            catch (Exception ex)
            {
                respuesta = "Error al actualizar datos de compra" + ex;
            }
            c.comandojavascript = "document.getElementById('btnEditarCompra').disabled = true;";
            ModelState.AddModelError("msgEditCompra", respuesta);
            return View(c);
        }

        public ActionResult editarDetalleCompras(String id)
        {
            String[] substrings = id.Split('a');
            detalleCompraModel det = new detalleCompraModel();
            det.idcompra = Convert.ToInt32(substrings[1]);
            det.codinsumo = substrings[0];

            var consulta = contexto.pa_DetalleCompraPorEditar(Session["codigoalmacen"].ToString(), det.idcompra, det.codinsumo).First();
            det.insumo = consulta.nombre_insumo;
            det.precio = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", consulta.precio);
            det.cantidad = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", consulta.cantidad);
            det.Observacion = consulta.observacion;
            return View(det);
        }

        [HttpPost]
        public ActionResult editarDetalleCompras(detalleCompraModel det)
        {
            String respuesta = "";
            try
            {
                contexto.pa_ActualizarDetalleCompra(det.idcompra, det.codinsumo, Convert.ToDecimal(det.precio), Convert.ToDouble(det.cantidad), Session["codigoalmacen"].ToString(), det.Observacion);
                contexto.SubmitChanges();
                respuesta = "Se actualizó datos de insumos correctamente";
            }
            catch (Exception ex)
            {
                respuesta = "Error al actualizar datos de insumo" + ex;
            }
            det.comandojavascript = "document.getElementById('btnEditarDetCompra').disabled = true;";
            ModelState.AddModelError("msgEditDetCompra", respuesta);
            return View(det);
        }


        //Action for view stock de insumos
        public ActionResult StockInsumo(String codInsu, String codalmac)
        {
            if (Session["Username"] != null && Session["bloquear"] != null && Session["codigoalmacen"] != null)
            {
                InsumoModel ins = new InsumoModel();
                ins = ins.stockInsumo(ins, Session["codigoalmacen"].ToString(), 0);
                return View(ins);
            }
            else
            {
                return View("../Home/Login");
            }
        }


        [HttpPost]
        public ActionResult StockInsumo(InsumoModel ins)
        {
            int estado = 1;
            if (ins.elegido == null)
            {
                estado = 0;
            }
            ins = ins.stockInsumo(ins, Session["codigoalmacen"].ToString(), estado);
            return View(ins);
        }


        //action para el eliminar compra
        public ActionResult Delete(int id)
        {
            var compra = model.BuscarCompra(id);
            return View(compra);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (model.Eliminarcompra(id).Equals("ok"))
                {

                    return RedirectToAction("compras");
                }
                else
                {
                    return RedirectToAction("Delete");
                }

            }
            catch
            {
                return View();
            }
        }



        //==============nuevos cambios==============
        //view delete detalle para las compras

        public ActionResult DeleteDetalle(String id)
        {
            detalleCompraModel det = new detalleCompraModel();
            String[] substrings = id.Split('b');
            det.codinsumo = substrings[0];
            String ca = Session["codigoalmacen"].ToString();
            det.idcompra = Convert.ToInt32(substrings[1]);

            var compra = model.BuscardetalleCompra(ca, det.codinsumo, det.idcompra);
          
            return View(compra);
        }

        //accion para eliminar detalle de la compra
        [HttpPost]
        public ActionResult DeleteDetalle(detalleCompraModel det)
        {
            try
            {
                String ca = Session["codigoalmacen"].ToString();
                if (model.Eliminardetallecompra(ca, det.codinsumo, det.idcompra).Equals("ok"))
                {
                    int idcompra = det.idcompra;
                    return RedirectToAction("detallecompras", new { id = det.idcompra });//detallecompras", det.idcompra
                }
                else
                {
                    return View("DeleteDetalle", det);
                }

            }
            catch
            {
                return View("DeleteDetalle", det);
            }
        }
        // action para inventario  inicial
        public ActionResult inventarioinicial()
        {
            detalleInsumosModel det = new detalleInsumosModel();
            det.listadropinsumos = det.listadropdeinsumos();
            det.estado = 1;
            return View(det);
        }

        [HttpPost]
        public ActionResult inventarioinicial(detalleInsumosModel det, String sbinventario = "")
        {
            String cad = det.idbotonclickeado;
            switch (cad)
            {
                case "Confirmar":
                    det.listadropinsumos = det.listadropdeinsumos();
                    DateTime? fechainventario = retornaFecha(det.fecha);
                    if (fechainventario != null)
                    {
                        det.javascriptadicional = "document.getElementById('btnRegistrar').disabled = false;"
                                                    + "document.getElementById('btnConfirmar').disabled = true;";
                        det.estado = 2;
                        det.listainsumosinventini = det.ListaInventarioInicial(det, fechainventario, Session["codigoalmacen"].ToString());
                    }
                    return View(det);
                case "Registrar":
                    fechainventario = retornaFecha(det.fecha);
                    det.listadropinsumos = det.listadropdeinsumos();
                    det.javascriptadicional = "document.getElementById('btnRegistrar').disabled = false;"
                                            + "document.getElementById('btnConfirmar').disabled = true;";
                    double? cantactual = retornaDouble(det.cantActual);
                    decimal? costinicial = retornaDecimal(det.costo_uni);
                    if (det.codInsumo != null && costinicial != null && cantactual != null)
                    {
                        try
                        {
                            contexto.pa_RegistrarInsumoInventarioInicial(det.codInsumo, Session["codigoalmacen"].ToString(), fechainventario, cantactual, costinicial);
                            contexto.SubmitChanges();
                            det.MensajeInventario = "<span class=\"text-primary\"><i class=\"fa fa-check-circle fa-2x\" aria-hidden=\"true\"></i> Se agregó insumo al inventario correctamente</span>";
                        }
                        catch (Exception ex)
                        {
                            if (ex.HResult == -2146232060)
                            {
                                det.MensajeInventario = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Este insumo ya existe en este inventario</span>";
                            }
                        }
                    }
                    else
                    {
                        det.MensajeInventario = "<span class=\"text-danger\"><i class=\"fa fa-exclamation-triangle fa-2x\" aria-hidden=\"true\"></i> Ingrese valores</span>";
                    }
                    det.estado = 2;
                    det.listainsumosinventini = det.ListaInventarioInicial(det, fechainventario, Session["codigoalmacen"].ToString());
                    return View(det);
                default:
                    return Content("<p>Error¡ Error¡¡¡</p>");
            }

        }


        public ActionResult retornaInfoAlmacen(String id = "")
        {
            InfoAlmacenModel iam = new InfoAlmacenModel();
            iam.descripcion = (from a in contexto.almacens
                               where a.cod_almacen == id
                               select a.descripcion).First();
            Session["bloquear"] = "pointer-events: fill;";
            iam.codigo = id;
            return View("../Almacenes/InfoAlmacen", iam);
        }

        public ActionResult cambioMoneda()
        {
            return View();
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




