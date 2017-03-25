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
    public class ClientesController : Controller
    {

        // GET: /Clientes/

        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();

        public ClienteModel model = new ClienteModel();

        public ClienteStockModel m = new ClienteStockModel();

        //metodo para listar clientes
        public ActionResult listaclientes()
        {
            return View(model.Listarcliente());
        }

        //metodo para crear el cliente
        public ActionResult agregarCliente()
        {
            ClienteModel c = new ClienteModel();
            return View(c);
        }
        // add this
        [HttpPost]
        public ActionResult agregarCliente(ClienteModel c)
        {
            try
            {
                int comando = model.registrarCliente(c);
                if (comando == 1)
                {
                    return RedirectToAction("listaclientes");
                }
                else
                {
                    if (comando == -2146232060)
                    {
                        c.mensaje = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Este RUC ya fue registrado anteriormente</span>";
                    }
                    else
                    {
                        c.mensaje = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Error inserción de datos</span>";
                    }
                    return View("agregarCliente", c);
                }
            }
            catch
            {
                return View();
            }
        }
        //metodo para eliminar cliente
        public ActionResult Delete(String id)
        {
            var cliente = model.filtrarbusquedaCliente(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            try
            {
                if (model.eliminarCliente(id).Equals("ok"))
                {

                    return RedirectToAction("listaclientes");
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
        //metodo para editar datos del cliente
        public ActionResult Editar(String id)
        {
            var cliente = model.filtrarbusquedaCliente(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Editar(ClienteModel cli)
        {
            try
            {
                //call procedure 
                if (model.actualizarCliente(cli.ruc, cli.razon_soc, cli.direc, cli.telef, cli.correo, cli.rep_leg).Equals("ok"))
                {

                    return RedirectToAction("listaclientes");
                }
                else
                {
                    return RedirectToAction("Editar");
                }
            }
            catch
            {

                return View();
            }
        }

        public ActionResult StockClienteInic()
        {
            ClienteStockModel cm = new ClienteStockModel();
            return View(cm);
        }

        //ADD NEW CHANGES
        public ActionResult listastockCliente()
        {
            ClienteStockModel cl = new ClienteStockModel();
            return View(cl);
        }



        [HttpPost]
        public ActionResult listastockCliente(ClienteStockModel cm)
        {
            cm.listastockcliente = cm.listaStocProductor(cm.ruc);
            if (!cm.listastockcliente.Any())
            {
                cm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-1x\" aria-hidden=\"true\"></i>Cliente no cuenta con stock</span>";
            }

            return View(cm);

        }

        public List<cliente> Listarcliente()
        {
            List<cliente> lista = new List<cliente>();

            var consulta = contexto.pa_listarClientes();

            foreach (var cli in consulta)
            {
                cliente c = new cliente();
                c.ruc = cli.ruc;
                c.razon_social = cli.razon_social;
                c.direccion = cli.direccion;
                c.telefono = cli.telefono;
                c.correo = cli.correo;
                c.rep_legal = cli.rep_legal;

                lista.Add(c);
            }
            return lista;
        }


        //HERE MODIFIED 01-02
        public ActionResult StockCliente()
        {
            ClienteStockModel cm = new ClienteStockModel();
            return View(cm);
        }

        [HttpPost]
        public ActionResult StockCliente(ClienteStockModel cm, String sem)
        {
            cm.listastockbysemana= cm.listaStockSemana(cm.ruc,cm.codsem);
            if (!cm.listastockbysemana.Any())
            {
                cm.msjerror = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-1x\" aria-hidden=\"true\"></i>Cliente no cuenta con stock</span>";
            }

            return View(cm);

        }
       


    }

}
