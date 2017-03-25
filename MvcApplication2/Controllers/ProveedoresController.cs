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
    public class ProveedoresController : Controller
    {

        // GET: /Proveedores/
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();

        ProveedorModel model = new ProveedorModel();

        //metodo para listar proveedores
        public ActionResult listaProveedores()
        {

            List<ProveedorModel> lista = new List<ProveedorModel>();
            var consulta = contexto.pa_listarProveedores();

            foreach (var q in consulta)
            {
                ProveedorModel pm = new ProveedorModel();

                pm.ruc = q.ruc;
                pm.raz_soc = q.razon_social;
                pm.telefono = q.telefono;
                pm.fax = q.fax;
                pm.rep_legal = q.rep_legal;
                pm.correo = q.correo;
                lista.Add(pm);
            }
            return View(lista);
        }

        public ActionResult RegistrarProveedor()
        {
            ProveedorModel pr = new ProveedorModel();
            return View(pr);
        }


        [HttpPost]
        public ActionResult RegistrarProveedor(ProveedorModel pr)
        {
            try
            {
                String mensaje="";

                int flag = model.registrarProveedor(pr);
                 
                if (flag == 1)
                {
                        mensaje = "<span class=\"text-primary\"><i class=\"fa fa-thumbs-o-up fa-2x\" aria-hidden=\"true\"></i> Se ha registrado correctamente!</span>";
                }
                else
                {
                    if (flag == -2146232060)
                    {
                        mensaje = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> Proveedor registrado anteriormente</span>";
                    }
                    else
                    {
                        mensaje = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\">error unknown</i></span>";
                    }
                }
                pr.msjerror = mensaje;      
            }
            catch (Exception)
            {
                String msj = "Error!";
                pr.msjerror = msj;
            }
            return View("RegistrarProveedor",pr);
        }


        //action for to delete proveedor

        public ActionResult DeleteProveedor(String id)
        {
            var pro = model.buscarProveedor(id);
            return View(pro);
        }

        [HttpPost]
        public ActionResult DeleteProveedor(String id, FormCollection collection)
        {
            try
            {
                if (model.eliminarProveedor(id).Equals("ok"))
                {

                    return RedirectToAction("listaProveedores");
                }
                else
                {
                    return RedirectToAction("DeleteProveedor");
                }
            }
            catch
            {
                return View();
            }
        }
          public ActionResult Editar(String id)
        {
            var pro = model.buscarProveedor(id);
            return View(pro);
        }

        [HttpPost]
        public ActionResult Editar(String id, proveedor pr)
        {
            try
            {
                proveedor p = new proveedor();      
                p.ruc = pr.ruc;
                p.razon_social = pr.razon_social;
                p.telefono = pr.telefono;
                p.fax=pr.fax;
                p.rep_legal = pr.rep_legal;
                p.correo = pr.correo;
       
                //call 
                if (model.actualizarProveedor(p).Equals("ok"))
                {

                    return RedirectToAction("listaProveedores");
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
    }
    }

