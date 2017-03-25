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
    public class ProductoresController : Controller
    {
        // GET: /Productores/
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        public ProductorModel model = new ProductorModel();

        //accion para mostrar el detalle de la guias
        public ActionResult detalleGuia()
        {
            if (Session["Username"] != null && Session["bloquear"] != null && Session["codigoalmacen"] != null)
            {
                List<GuiaClienteModel> lista = new List<GuiaClienteModel>();
                var ca = Session["codigoalmacen"].ToString();
                var consulta = from gc in contexto.pa_listaguiacliente(ca)
                               select gc;
                foreach (var insu in consulta)
                {
                    GuiaClienteModel g = new GuiaClienteModel();
                    g.fecha = String.Format("{0:dd/MM/yyyy}", insu.fecha);
                    g.numguia = insu.numeroguia;
                    g.ruc = insu.ruc;
                    g.persona_recepcion = insu.persona_recepcion;
                    g.observacion = insu.observacion;
                    lista.Add(g);
                }
                return View(lista);

            }
            else
            {
                return View("../Home/Login");
            }
        }
        //action for listar entrega de insumos a productores
        public ActionResult listaentregaInsumoProductor(String numg)
        {
            List<DetalleEntregaInsumo> lista = new List<DetalleEntregaInsumo>();
            var consulta = from ep in contexto.pa_listaEntregaInsumo(numg) select ep;

            foreach (var entrega in consulta)
            {
                DetalleEntregaInsumo e = new DetalleEntregaInsumo();
                e.nominsumo = entrega.nombre_insumo;

            }
            return View(lista);
        }
        //HERE MODIFIED 10-02-2017
        public ActionResult registrarguia()
        {
            ProductorModel res = new ProductorModel();
            List<ProductorModel> lista = new List<ProductorModel>();
            res.listamodelinsumos = lista;
            return View(res);
        }

        //metodo registrar guia de productor
        //day 10
        [HttpPost]
        public ActionResult registrarguia(ProductorModel guiap)
        {
            ModelState.Remove("cantidad");
            ModelState.Remove("fechaI");
            if (guiap.ruc == null)
            {
                ModelState.Remove("ruc");
            }
            if (guiap.salidaodevolucion == 2)
            {
                ModelState.Remove("numGuia");
                guiap.numGuia = "01";
            }
            String mensaje = "";
            var cmdo = contexto.pa_ExisteGuiaProductor(guiap.numGuia, guiap.ruc, retornaFecha(guiap.fecha),guiap.salidaodevolucion).First();
            int?[] comando = new int?[3];
            comando[0] = cmdo.estado;
            comando[1] = cmdo.rucexiste;
            comando[2] = cmdo.fechavalida;
            if (comando[0] == 0 && comando[1] != 0 && comando[2] == 1)
            {
                guiap.msjProductorModel = "<span class=\"text-primary\"><i class=\"fa fa-check-circle fa-2x\" aria-hidden=\"true\"></i> Llene la siguiente guía del productor.</span>";
                guiap.rznsocial = cmdo.razonsocial;
                guiap.numGuia = cmdo.coddevolgenerado;
                guiap.jsFormulario = "2";
            }
            else
            {
                if (comando[0] == 1)
                {
                    mensaje = "Esta guía ya existe";
                }
                if (comando[1] == 0)
                {
                    mensaje = mensaje + "-El Cliente no está registrado";
                }
                if (comando[2] == 0)
                {
                    mensaje = mensaje + "-La fecha debe pertenecer a la semana activa";
                }
                guiap.msjProductorModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>" + mensaje + "</span>";
                guiap.jsFormulario = "1";
            }            
            return View("registrarguia", guiap);
        }

        [HttpPost]
        public ActionResult agregarinsumo(ProductorModel guiap, String listainsumos = "")
        {
            DateTime? fechita = null;
            ModelState.Remove("ruc");
            try
            {
                fechita = Convert.ToDateTime(guiap.fecha);
            }
            catch (Exception)
            {
                fechita = null;
            }
            if (fechita != null)
            {
                int? comando = guiap.agregarInsumoProductor(guiap, fechita, Session["codigoalmacen"].ToString(), listainsumos);
                if (comando == 1)
                {
                    guiap.msjProductorModel = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> Insumo ingresado.</span>";
                }
                else
                {
                    if (comando == 2)
                    {
                        guiap.msjProductorModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>Error al insertar insumo, cantidad es superior a lo de Almacen.</span>";
                    }
                    else
                    {
                        if (comando == -2146232060)
                        {
                            guiap.msjProductorModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>Esta insumo ya existe en la guía</span>";
                        }
                        else
                        {
                            guiap.msjProductorModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>Error al insertar insumo.</span>";
                        }
                    }
                }
            }
            var query2 = contexto.pa_listarDetalleGuia(guiap.numGuia, Session["codigoalmacen"].ToString());
            foreach (var q in query2)
            {
                guiap.listainsumosguia.Add(new String[] { q.nombre_insumo, q.uni_des, String.Format(CultureInfo.InvariantCulture, "{0:0.00}", q.cantidad), String.Format(CultureInfo.InvariantCulture, "{0:0.00000}", q.costoinicial), String.Format(CultureInfo.InvariantCulture, "{0:0.00000}", q.total) });
            }
            guiap.jsFormulario = "2";
            return View("registrarguia", guiap);    
        }

        //metodo para elimnar guia del cliente
        public ActionResult Delete(String id)
        {
            var c = Session["codigoalmacen"].ToString();
            var guiacliente = model.BuscarGuiaCliente(id, c);
            guiacliente.numGuia = id;
            return View(guiacliente);
        }

        //metodo para elimnar guia del cliente
        [HttpPost]
        public ActionResult Delete(ProductorModel pr)
        {
            try
            {
                var c = Session["codigoalmacen"].ToString();
                if (model.EliminarguiaCliente(pr.numGuia, c).Equals("ok"))
                {

                    return RedirectToAction("detalleGuia");
                }
                else
                {
                    return RedirectToAction("Delete");
                }
            }
            catch
            {
                return View(pr);
            }
        }

        //Action list  para ver el detalle de la guia del cliente

        public ActionResult Detalle(String id)
        {
            List<detalleGuiaModel> lista = new List<detalleGuiaModel>();
            var consulta = contexto.pa_listarDetalleGuia(id, Session["codigoalmacen"].ToString());

            foreach (var insu in consulta)
            {
                detalleGuiaModel d = new detalleGuiaModel();
                d.codInsumo = insu.cod_insumo;
                d.nomInsumo = insu.nombre_insumo;
                d.unidad = insu.uni_des;
                d.numGuia = insu.numeroguia;
                
                d.cant = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", insu.cantidad);
                lista.Add(d);
            }
            return View(lista);
        }


        public ActionResult EditGuia(String id)
        {
            ProductorModel pr = new ProductorModel();
            var consulta = contexto.pa_RetornaGuiaPorNumeroGuia(id, Session["codigoalmacen"].ToString()).First();
            pr.numGuia = consulta.numeroguia;
            pr.personaRecep = consulta.persona_recepcion;
            pr.ruc = consulta.ruc;
            pr.fecha = String.Format("{0:dd-MM-yyyy}", consulta.fecha);
            pr.observ = consulta.observacion;
            return View(pr);
        }

        [HttpPost]
        public ActionResult EditGuia(ProductorModel pr)
        {
            String respuesta = "";
            DateTime fechaa;

            if (DateTime.TryParse(pr.fecha, out fechaa))
            {
                fechaa = DateTime.Parse(pr.fecha);
            }
            try
            {
                contexto.pa_ActualizarGuia(pr.numGuia, Session["codigoalmacen"].ToString(), fechaa, pr.observ, pr.personaRecep, pr.ruc);
                contexto.SubmitChanges();
                respuesta = "Se actualizó compra correctamente";
            }
            catch (Exception ex)
            {
                respuesta = "Error al actualizar datos de compra" + ex;
            }
            ModelState.AddModelError("msgEditGuia", respuesta);
            return View(pr);
        }

        public ActionResult EditDetalleGuia(String id)
        {
            String[] substrings = id.Split('a');
            detalleGuiaModel dg = new detalleGuiaModel();
            dg.codInsumo = substrings[0];
            dg.numGuia = substrings[1];
            var consulta = contexto.pa_RetornaUnDetallePorGuia(substrings[0], substrings[1], Session["codigoalmacen"].ToString()).FirstOrDefault();
            dg.cant = consulta.cantidad + "";
            dg.nomInsumo = consulta.nombre_insumo;
            return View(dg);
        }

        [HttpPost]
        public ActionResult EditDetalleGuia(detalleGuiaModel dg)
        {
            String respuesta = "";
            try
            {
                contexto.pa_ActualizarDetalleGuia(dg.numGuia, dg.codInsumo, Convert.ToDouble(dg.cant), Session["codigoalmacen"].ToString());
                contexto.SubmitChanges();
                respuesta = "Se actualizó datos de insumos correctamente";
            }
            catch (Exception ex)
            {
                respuesta = "Error al actualizar datos de insumo" + ex;
            }
            dg.comandojavascript = "document.getElementById('btnEditarDetGuia').disabled = true;";
            ModelState.AddModelError("msgEditDetGuia", respuesta);
            return View(dg);
        }
        //==============nuevos cambios==============
        //view delete detalle para la guia

        public ActionResult DeleteDetalle(String id)
        {
            detalleGuiaModel dg = new detalleGuiaModel();
            String[] substrings = id.Split('b');

            dg.codInsumo = substrings[0];
            dg.numGuia = substrings[1];

            var guia = model.BuscardetalleGuia(dg.numGuia, dg.codInsumo);

            return View(guia);
        }

        //accion para eliminar detalle de guia
        [HttpPost]
        public ActionResult DeleteDetalle(detalleGuiaModel dg)
        {
            try
            {
                String ca = Session["codigoalmacen"].ToString();

                if (model.Eliminardetalleguia(dg.numGuia, dg.codInsumo).Equals("ok"))
                {

                    return RedirectToAction("Detalle", new { id = dg.numGuia });
                }
                else
                {
                    return View("DeleteDetalle", dg);
                }

            }
            catch
            {
                return View(dg);
            }
        }


        public ActionResult agregadetalle(String id)
        {
            ProductorModel prod = new ProductorModel();
            prod.numGuia=id;
            return View(prod);

        }

        [HttpPost]
        public ActionResult agregadetalle(ProductorModel prod, String listainsumos)
        {
            int? cmd = prod.agregarDetalleGuiaProductor(prod.numGuia, listainsumos, retornaDouble(prod.cant), Session["codigoalmacen"].ToString());
            if (cmd == 1)
            {
                prod.msjProductorModel = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> Insumo agregado correctamente.</span>";
            }
            else
            {
                if (cmd == -2146232060)
                {
                    prod.msjProductorModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>Este insumo ya fue registrado anteriormente.</span>";
                }
                else
                {
                    prod.msjProductorModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>Error al insertar insumo.</span>";
                }
            }
            return View(prod);
        }


        //HERE MODIFIED 01- today
        public ActionResult registrarguia2()
        {
            ProductorModel cm = new ProductorModel();
            return View(cm);
        }

        [HttpPost]
        public ActionResult registrarguia2(ProductorModel pm)
        {
            ModelState.Remove("cantidad");
            ModelState.Remove("fechaI");
            if (pm.ruc == null)
            {
                ModelState.Remove("ruc");
            }
            String mensaje = "";
            var cmdo = contexto.pa_ExisteGuiaProductor(pm.numGuia, pm.ruc, retornaFecha(pm.fecha), 1).First();
            int?[] comando = new int?[3];
            comando[0] = cmdo.estado;
            comando[1] = cmdo.rucexiste;
            comando[2] = cmdo.fechavalida;
            if (comando[0] == 0 && comando[1] != 0 && comando[2] == 1)
            {
                pm.msjProductorModel = "<span class=\"text-primary\"><i class=\"fa fa-check-circle fa-2x\" aria-hidden=\"true\"></i> Llene la siguiente guía del productor.</span>";
                pm.rznsocial = cmdo.razonsocial;
                pm.numGuia = cmdo.coddevolgenerado;
                pm.estado = 2;
            }
            else
            {
                if (comando[0] == 1)
                {
                    mensaje = "Esta guía ya existe";
                }
                if (comando[1] == 0)
                {
                    mensaje = mensaje + "-El Cliente no está registrado";
                }
                if (comando[2] == 0)
                {
                    mensaje = mensaje + "-La fecha debe pertenecer a la semana activa";
                }
                pm.msjProductorModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>" + mensaje + "</span>";
                pm.estado = 1;
            }            
            return View(pm);

        }


        public ActionResult agregarinsumo2(ProductorModel pm, String sbAddInsum)
        {
            switch (sbAddInsum)
            {
                case "Confirmar":
                    pm.listainsqfaltan = pm.AlcanzaGuiaProdTerm(pm.codtipo, Session["codigoalmacen"].ToString(), Convert.ToInt32(pm.cantCajas), deShortaBool(pm.paletizado));
                    if (pm.listainsqfaltan.Any())
                    {
                        pm.msjerror = "<span class=\"text-primary\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> " + pm.listado + " →Los insumos siguientes exceden en cantidad al stock almacén.</span>";
                    }
                    else
                    {
                        int? res = pm.existeGuiaProdSinVal(pm.numGuia);                        
                        if (res == 1)
                        {
                            int? query = pm.RegistrarGuiaPTerminado(pm.numGuia, pm.codtipo, Convert.ToInt32(pm.cantCajas), deShortaBool(pm.paletizado));
                            if (query == 1)
                            {
                                int? rpta = pm.registrarDetallGuiav2(pm.numGuia,pm.codtipo,Convert.ToInt32(pm.cantCajas),deShortaBool(pm.paletizado));
                                if (rpta == 1)
                                {
                                    pm.listado = pm.listado + " ♦" + pm.codtipo + "-" + pm.cantCajas;
                                    pm.msjerror = "<span class=\"text-primary\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> " + pm.listado + "</span>";
                                }
                                else
                                {
                                    pm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> " + pm.listado + " → Error de inserción</span>";
                                }                                
                            }
                            else
                            {
                                pm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> " + pm.listado + " → Nota: Esta guía tiene asignada número de cajas del Producto seleccionado, seleccione otro</span>";
                            }
                            
                        }
                        else
                        {
                            String err = "";
                            int? q = pm.RegistrarGuiaPTerminado1eraVez(Session["codigoalmacen"].ToString(),pm.numGuia,retornaFecha(pm.fecha),pm.observ,pm.personaRecep,pm.ruc,pm.codtipo,Convert.ToInt32(pm.cantCajas),pm.fechatrastaldo,pm.puntopartida,pm.puntollegada,pm.razonsocialagro,pm.destinatario,pm.rucagro,pm.confi_vehic,pm.MTC,pm.licenciaconducir,pm.otrodoc,pm.salida,pm.codalmc2,deShortaBool(pm.paletizado));
                            if (q == 1)
                            {
                                int? rpta = pm.registrarDetallGuiav2(pm.numGuia, pm.codtipo, Convert.ToInt32(pm.cantCajas), deShortaBool(pm.paletizado));
                                if (rpta == 1)
                                {
                                    pm.listado = pm.listado + " ♦" + pm.codtipo + "-" + pm.cantCajas;
                                }
                                else
                                {
                                    err = "Error de inserción";
                                }   
                                pm.msjerror = "<span class=\"text-primary\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> " + pm.listado + "</span>";
                            }
                            else
                            {
                                pm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> Error¡¡¡</span>";
                            }                            
                        }                        
                    }
                    ModelState.Remove("listado");
                    pm.estado = 2;                    
                    break;
                case "Mostrar":
                    pm.estado = 3;
                    pm.listatipobyproduccion = pm.listaDetalleGuia(pm.numGuia);
                    break;
                case "Grabar":
                    int cmd = pm.actualizaDetelllGuiav2(pm.numGuia, pm.insumo, retornaDouble(pm.cantidad), Session["codigoalmacen"].ToString());
                    if (cmd == 1)
                    {
                        pm.msjerror = "<span class=\"text-primary\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> Se actualizó cantidad en detalle de Guia</span>";
                    }
                    else
                    {
                        pm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-archive fa-x\" aria-hidden=\"true\"></i> Probar otra cantidad, no debe superar stock-almacen</span>";
                    }
                    pm.listatipobyproduccion = pm.listaDetalleGuia(pm.numGuia);
                    pm.estado = 3;
                    break;

            }
            return View("registrarguia2", pm);
        }

        //metodo imprimir
        public ActionResult imprimirGuia(String id)
        {
            ProductorModel model = new ProductorModel();
            model.numGuia = id;
            try
            {
                var query = contexto.pa_listarDetalleGuia(id, Session["codigoalmacen"].ToString());
                foreach (var q in query)
                {
                    model.listainsumosguia.Add(new String[] { q.cod_insumo, q.nombre_insumo, String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.cantidad), q.uni_des });
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }


        private bool? deShortaBool(short n)
        {
            bool? val = null;
            if (n == 1)
            {
                val = true;
            }
            else
            {
                if (n == 2)
                {
                    val = false;
                }
                else
                {
                    val = null;
                }
            }
            return val;
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





