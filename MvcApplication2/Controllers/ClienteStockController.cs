using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using almacen.Models;
namespace almacen.Controllers
{
    public class ClienteStockController : Controller
    {
        //
        // GET: /ClienteStock/

        public ActionResult StockClienteInic()
        {
            ClienteStockModel csm = new ClienteStockModel();
            return View(csm);
        }

        [HttpPost]
        public ActionResult StockClienteInic(ClienteStockModel csm)
        {
            int? comando = csm.registrarEnStockCliente(csm.ruc, csm.codinsumo, retornaDouble(csm.stockinicial), retornaDecimal(csm.costo));
            if (comando == 1)
            {
                csm.msjerror = "<span class=\"text-primary\"><i class=\"fa fa-thumbs-o-up fa-2x\" aria-hidden=\"true\"></i> Se ha agregado correctamente el insumo en el stock del cliente</span>";
            }
            else
            {
                if (comando == 0)
                {
                    csm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> El RUC del cliente ingresado no está registrado</span>";
                }
                else
                {
                    if (comando == 2)
                    {
                        csm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Este cliente (RUC) ya tiene el insumo asignado</span>";
                    }
                    else
                    {
                        csm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Error inesperado</span>";
                    }
                }
            }
            csm.listastockcliente = csm.listaStockClientePorProductor(csm.ruc);
            return View(csm);
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
