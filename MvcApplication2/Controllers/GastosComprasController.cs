using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// add
using almacen.Models;
using almacen.Controllers;
namespace almacen.Controllers
{
    public class GastosComprasController : Controller
    {
        
        // GET: /GastosCompras/

        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        GastosComprasModel model = new GastosComprasModel();
     
        public ActionResult listagastoscompras()
        {
            List<GastosComprasModel> lista = new List<GastosComprasModel>();
            String ca = Session["codigoalmacen"].ToString();
            
            var query = contexto.pa_listarGastosCompras(ca);
            foreach (var q in query)
            {
                GastosComprasModel g = new GastosComprasModel();
                g.codCompra = q.cod_compra;
                g.cuenta = q.Cuenta;
                g.desc_adic = q.Descripcion;
                g.cant_adic = q.Cantidad.ToString();
                g.prec_adic = q.Precio.ToString();
                g.total_adic = q.Total.ToString();
                lista.Add(g);
            }
            return View(lista);
        }


        public ActionResult EditarGastos(String id )
        {

            String[] substrings = id.Split('a');
            var gastos = model.BuscarGastosCompras(substrings[1], substrings[0]);
            return View(gastos);
         }

        //acction para editar gastos
        [HttpPost]
        public ActionResult EditarGastos(String id, Gastos_Compra gc)
        {
            try
            {
                Gastos_Compra gastos = new Gastos_Compra();
                gastos.cod_compra = gc.cod_compra;
                gastos.Cuenta = gc.Cuenta;
                gastos.Descripcion = gc.Descripcion;
                gastos.Cantidad = gc.Cantidad;
                gastos.Precio = gc.Precio;
                gastos.Total = gc.Total;
                
                if (model.EditarGastosCompras(gastos).Equals("ok"))
                {

                    return RedirectToAction("listagastoscompras");
                }
                else
                {
                    return RedirectToAction("EditarGastos");
                }
            }
            catch
            {

                return View();
            }
        }
        
        public ActionResult EliminarGastos(String id)
        {
            String[] substrings = id.Split('b');
            var gastos = model.BuscarGastosCompras(substrings[0], substrings[1]);      
       
            return View(gastos);
        }

        [HttpPost]
        public ActionResult EliminarGastos(String codCompra, String cuenta, FormCollection collection)
        {
            try
            {
                if (model.eliminarGastosCompras(codCompra,cuenta).Equals("ok"))
                {

                    return RedirectToAction("listagastoscompras");
                }
                else
                {
                    return RedirectToAction("EliminarGastos");
                }
            }
            catch
            {
                return View();
            }
        }
        
    }
}
