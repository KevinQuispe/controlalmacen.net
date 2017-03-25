using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
// add
using almacen.Models;
using almacen.Controllers;
namespace almacen.Controllers
{
    public class TipoCambioController : Controller
    {
        // GET: /TipoCambio/
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();

        public TipoCambioModel model = new TipoCambioModel();

        //action for to load tipo de cambio
        public ActionResult tipo_de_cambio()
        {
            List<TipoCambioModel> lista = new List<TipoCambioModel>();
            var consulta = contexto.pa_listarCotizacion();
            foreach (var q in consulta)
            {
                TipoCambioModel pm = new TipoCambioModel();
                //pm.fecha =String.Format("{0:dd/MM/yyyy}", q.Fecha_Cambio);                //
                pm.fecha = String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", q.Fecha_Cambio);
                //pm.fecha = String.Format(CultureInfo.InvariantCulture, "{0:yyyy/MM/dd}", q.Fecha_Cambio);
                pm.compra = q.Compra.ToString();
                pm.venta = q.Venta.ToString();
                lista.Add(pm);
            }
            return View(lista);
        }
        //action
        public ActionResult Editar(DateTime id)
        {
            var tipo = model.BuscarTipoCambio(id);
            return View(tipo);
        }

        [HttpPost]
        public ActionResult Editar(DateTime id, Cambio_moneda c)
        {
            try
            {
                TipoCambioModel cambio = new TipoCambioModel();
                if (cambio.actualizarTipoCambio(c).Equals("ok"))
                {

                    return RedirectToAction("tipo_de_cambio");
                }
                else
                {
                    ModelState.AddModelError("Venta", "Error base de datos");
                    return View("Editar",c);
                }
            }
            catch
            {

                return View(c);
            }
        }
        //Registrar nueva cotizacion
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(Cambio_moneda c)
        {
            try
            {
                int comando = model.inserarNuevaCotizacion(c);
                if (comando == 1)
                {
                    return RedirectToAction("tipo_de_cambio");
                }
                else
                {
                    if (comando == -2146232060)
                    {
                        ModelState.AddModelError("Venta", "Esta fecha ya tiene datos de cotización ingresados");
                    }
                    else
                    {
                        ModelState.AddModelError("Venta", "Error en base de datos");
                    }
                    return View("Nuevo", c);
                }
            }
            catch
            {
                return View();
            }
        }
    }

}