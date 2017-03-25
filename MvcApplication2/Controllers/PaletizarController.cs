using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Collections;
using almacen.Models;
using almacen.Controllers;
using System.Globalization;
namespace almacen.Controllers
{
    public class PaletizarController : Controller
    {
        //
        // GET: /Paletizar/

        public ActionResult PorPaletizar(String id)
        {
            PaletizarModel pal = new PaletizarModel();
            String[] substrings = id.Split('a');
            pal.ruc = substrings[0];
            pal.anio = substrings[2];
            pal.semana = substrings[1];
            pal.prodTerminado = substrings[3];
            pal.listSemProdInsumo = pal.listarInsumosPaletiz(pal.prodTerminado, pal.ruc, pal.semana, Convert.ToInt32(pal.anio), Session["codigoalmacen"].ToString());
            return View(pal);
        }

        [HttpPost]
        public ActionResult PorPaletizar(PaletizarModel model)
        {
            int resultado = model.ActualizarConsumoReal(model.semana, model.codinsumoeditar, Session["codigoalmacen"].ToString(), Convert.ToInt32(model.anio), model.ruc, model.prodTerminado, retornaDecimal(model.cantinsumorealeditar));
            if (resultado == 1)
            {
                model.msjPaletizar = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> La cantidad de consumo real de este insumo ha sido actualizado</span>";
            }
            else
            {
                model.msjPaletizar = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> No se pudo actualizar la cantidad de consumo real</span>";
            }
            model.listSemProdInsumo = model.listarInsumosPaletiz(model.prodTerminado, model.ruc, model.semana, Convert.ToInt32(model.anio), Session["codigoalmacen"].ToString());
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

    }
}
