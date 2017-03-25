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
    public class ProduccionController : Controller
    {
        //
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        ProduccionModel model = new ProduccionModel();
        public ActionResult listaProdTerminado()
        {
           //List<ProduccionModel> listapdt = new List<ProduccionModel>();
            return View((model.ListarProdTerminado()));
        }


        public ActionResult regProdTerminado()
        {
            ProduccionModel model = new ProduccionModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult regProdTerminado(ProduccionModel model, String sbRegPT)
        {
            switch (sbRegPT)
            {
                case "Registrar":
                    ModelState.Remove("codigoInsumo");
                    ModelState.Remove("cantidad");
                    ModelState.Remove("tipo");
                    ModelState.Remove("codigopt");
                    bool? rpta = model.existeProdTermin(model.descripPT.ToUpper());
                    if (rpta == false)
                    {
                        model.msjPorduccionModel = "<span class=\"text-primary\"><i class=\"fa fa-check-circle fa-2x\" aria-hidden=\"true\"></i>Ahora registre insumos que conforman el producto.</span>";
                        model.estadoformulario = 2;
                    }
                    else
                    {
                        model.msjPorduccionModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Seleccione otro nombre de descripción del producto terminado</span>";
                        model.estadoformulario = 1;
                    }
                    return View(model);
                case "Agregar":
                    if (model.codigopt == null)
                    {
                        ModelState.Remove("codigopt");
                        model.codigopt = model.primerinsumYregistrarProdTerm(model.descripPT.ToUpper(), model.codigoInsumo, retornaDecimal(model.cantidad), deShortaBool(model.tipo));
                        model.msjPorduccionModel = "<span class=\"text-primary\"><i class=\"fa fa-check-circle fa-2x\" aria-hidden=\"true\"></i> Primer insumo registrado.</span>";
                    }
                    else
                    {
                        int? resultad = model.registrarInsEnProdTermCreado(model.codigopt, model.codigoInsumo, retornaDecimal(model.cantidad), deShortaBool(model.tipo));
                        if (resultad == 1)
                        {
                            model.msjPorduccionModel = "<span class=\"text-primary\"><i class=\"fa fa-check-circle fa-2x\" aria-hidden=\"true\"></i> El registro del insumo en el producto terminado fue exitoso.</span>";
                        }
                        else
                        {
                            if (resultad == -2146232060)
                            {
                                model.msjPorduccionModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Seleccione otro insumo, éste ya fue registrado anteriormente.</span>";
                            }
                            else
                            {
                                model.msjPorduccionModel = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Error al registrar insumo.</span>";
                            }
                        }
                    }
                    model.listainsumosProdTerm = model.listarInsumosenPT(model.codigopt);
                    model.estadoformulario = 2;
                    return View(model);
                default:
                    ProduccionModel modell = new ProduccionModel();
                    modell.msjPorduccionModel = "<span class=\"text-primary\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Error ¡¡¡</span>";
                    return View(modell);
            }
        }

        public ActionResult semanaProduccion()
        {
            SemanaProduccion model = new SemanaProduccion();
            return View(model);
        }

        [HttpPost]
        public ActionResult semanaProduccion(SemanaProduccion model)
        {
            int resultado = model.registrarSemProducc(model.semana, model.prodTerminado, model.ruc, Session["codigoalmacen"].ToString(), Convert.ToInt32(model.cant_cajas), Convert.ToInt32(model.anio), deShortaBool(model.paletizado));
            if (resultado == 1)
            {
                model.msjSemProduccion = "<span class=\"text-primary\"><i class=\"fa fa-check-circle fa-2x\" aria-hidden=\"true\"></i>Acontiuación lista de insumos del producto terminado seleccionado</span>";
            }
            else
            {
                if (resultado == -2146232060)
                {
                    model.msjSemProduccion = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Producción de la semana ya registrada anteriormente</span>";
                }
                else
                {
                    model.msjSemProduccion = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Error al registrar Semana Producción.</span>";
                }
            }
            model.listSemProdInsumo = model.listarInsumosSemProducc(model.prodTerminado, model.ruc, model.semana, Convert.ToInt32(model.anio), Session["codigoalmacen"].ToString());
            return View(model);
        }

        public ActionResult TablasemanaProduccion()
        {
            SemanaProduccion model = new SemanaProduccion();
            return View(model);
        }

        [HttpPost]
        public ActionResult TablasemanaProduccion(SemanaProduccion model)
        {
            model.listSemProdInsumo = model.listarInsumosSemProduccNCajas(model.prodTerminado, model.semana, Session["codigoalmacen"].ToString(), Convert.ToInt32(model.anio));
            return View(model);
        }

        public ActionResult DetalleTablaSemProd(String id)
        {
            SemanaProduccion model = new SemanaProduccion();
            String[] substrings = id.Split('a');
            model.ruc = substrings[0];
            model.anio = substrings[2];
            model.semana = substrings[1];
            model.prodTerminado = substrings[3];
            model.listSemProdInsumo = model.listarInsumosSemProducc(model.prodTerminado, model.ruc, model.semana, Convert.ToInt32(model.anio), Session["codigoalmacen"].ToString());
            return View(model);
        }

        [HttpPost]
        public ActionResult DetalleTablaSemProd(SemanaProduccion model)
        {
            int resultado = model.ActualizarConsumoReal(model.semana, model.codinsumoeditar, Session["codigoalmacen"].ToString(), Convert.ToInt32(model.anio), model.ruc, model.prodTerminado, retornaDecimal(model.cantinsumorealeditar));
            if (resultado == 1)
            {
                model.msjSemProduccion = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> La cantidad de consumo real de este insumo ha sido actualizado</span>";
            }
            else
            {
                model.msjSemProduccion = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> No se pudo actualizar la cantidad de consumo real</span>";
            }
            model.listSemProdInsumo = model.listarInsumosSemProducc(model.prodTerminado, model.ruc, model.semana, Convert.ToInt32(model.anio), Session["codigoalmacen"].ToString());
            return View(model);
        }

        public ActionResult TablasSemProduccIns()
        {
            SemanaProducc model = new SemanaProducc();
            try
            {
                model.listadropclientes = model.listarClientesConProducc(model.semana, Session["codigoalmacen"].ToString(), Convert.ToInt32(model.anio));
            }
            catch (Exception)
            {

            }            
            return View(model);
        }

        [HttpPost]
        public ActionResult TablasSemProduccIns(SemanaProducc model,String sbTablaSemProd)
        {
            switch (sbTablaSemProd)
            {
                case "Aceptar":
                    model.listSemProdInsumo = model.listarInsumosSemProducc(model.ruc, model.semana, Convert.ToInt32(model.anio));
                    model.estado = 2;
                    break;
                case "Grabar":
                    int queryy = model.ActualizarSemanaConsumoProduccion(model.ruc, model.semana, Convert.ToInt32(model.anio),model.codinsumoeditar,retornaDecimal(model.cantinsumorealeditar));
                    if (queryy == 1)
                    {
                        model.msjSemProducc = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> La cantidad de consumo real de este insumo ha sido actualizado</span>";
                    }
                    else
                    {
                        model.msjSemProducc = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> No se actualizó cantidad de consumo</span>";
                    }
                    model.listSemProdInsumo = model.listarInsumosSemProducc(model.ruc, model.semana, Convert.ToInt32(model.anio));
                    model.estado = 2;
                    break;
                case "Cerrar":
                    int cmdo=model.CerroProduccion(model.semana, Convert.ToInt32(model.anio),model.ruc);
                    if (cmdo == 1)
                    {
                        model.msjSemProducc = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> La producción de éste productor en la semana ha sido cerrada.</span>";
                    }
                    else
                    {
                        model.msjSemProducc = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i>Error: La producción de éste productor en la semana NO ha sido cerrada.</span>";
                    }                    
                    model.estado = 1;
                    break;
                case "Nuevo":
                    model.estado = 1;
                    break;
            }
            model.listadropclientes = model.listarClientesConProducc(model.semana, Session["codigoalmacen"].ToString(), Convert.ToInt32(model.anio));
            return View(model);
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

        //add day 28
        public ActionResult listadetalleProdTerm(String id)
        {
            List<detalleProdTerminado> lista = new List<detalleProdTerminado>();
            //var consulta = contexto.pa_listarDetalleGuia(id, Session["codigoalmacen"].ToString());
            var consulta = contexto.pa_listarDetalleProdTerm(id);
            foreach (var insu in consulta)
            {
                detalleProdTerminado d = new detalleProdTerminado();
                d.codInsumo = insu.cod_insumo;
                d.nomInsumo = insu.nombre_insumo;
                d.cant = insu.cantidad.ToString();
                lista.Add(d);
            }
            return View(lista);
        }
        

        //add day 31
        public ActionResult reportemermas(String id)
        {
            ProduccionModel prod = new ProduccionModel();
            prod.listareporteProdTerm = prod.ListarMermasProduccion(id.Trim());

            if (!prod.listareporteProdTerm.Any())
            {
                prod.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-1x\" aria-hidden=\"true\"></i>no hay merma</span>";
            }
            return View(prod);
        }

        //[HttpPost]
        //public ActionResult reportemermas(ProduccionModel prod)
        //{
        //    prod.listareporteProdTerm = prod.ListarMermasProduccion(prod.ruc);

        //    if (!prod.listareporteProdTerm.Any())
        //    {
        //        prod.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-1x\" aria-hidden=\"true\"></i>no hay merma</span>";
        //    }

        //    return View(prod);

        //}
        public ActionResult reportePDF()
        {
            return View();
        }
        

    }
}
