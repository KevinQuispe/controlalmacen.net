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
    public class AlmacenesController : Controller
    {
        //
        // GET: /Almacenes/      
        AlmacenModel model = new AlmacenModel();

        DatosAlmacenDataContext dbo = new DatosAlmacenDataContext();

        public ActionResult InfoAlmacen(String listaalm = "")
        {
            if (Session["Username"] != null)
            {
                InfoAlmacenModel iam = new InfoAlmacenModel();
                iam.codigo = listaalm.Substring(0, 2);
                Session["codigoalmacen"] = iam.codigo;
                iam.descripcion = listaalm.Substring(3);
                Session["bloquear"] = "pointer-events: fill;";
                return View("../Almacenes/InfoAlmacen", iam);
            }
            else
            {
                return View("../Home/Login");
            }

        }


        public ActionResult agregaralmacen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult agregaralmacen(AlmacenModel a)
        {
            try
            {

                if (model.resistraralmacen(a).Equals("ok"))
                {
                    return RedirectToAction("agregaralmacen");
                }
                else
                {
                    return RedirectToAction("agregaralmacen");
                }
            }
            catch
            {

                return View(a);
            }

        }
        //add

        public ActionResult listalmacenmov(String listaalmcen = "")
        {

            return View();

        }
        public ActionResult movimientos()
        {
            AlmacenModel alm = new AlmacenModel();
            alm.codigo = alm.nombreAlmacen(Session["codigoalmacen"].ToString());
            alm.listadropalmacenes = new List<SelectListItem>();
            alm.listadropalmacenes = alm.listaAlmacenesMovimientos(Session["codigoalmacen"].ToString());
            return View(alm);
        }

        [HttpPost]
        public ActionResult movimientos(AlmacenModel alm, String sbMovimAlm = "")
        {
            switch (sbMovimAlm)
            {
                case "Confirmar":
                    ModelState.Remove("insumo");
                    ModelState.Remove("cantidadRecibir");
                    int? cmd = alm.existeGuia(alm.guia);
                    if (cmd == 0)
                    {
                        alm.estado = 2;
                        alm.msjAlmacen = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> NOTA: Ahora agregue insumos, al primer insumo agregado de forma exitosa a la guía, ésta se crea automáticamente.</span>";
                    }
                    else
                    {
                        if (cmd == 1)
                        {
                            alm.estado = 1;
                            alm.msjAlmacen = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Digite otro número de guía</span>";
                        }
                        else
                        {
                            alm.estado = 1;
                            alm.msjAlmacen = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Error¡¡¡</span>";
                        }
                    }               
                    return View(alm);
                case "Registrar":
                    if (alm.existeGuia(alm.guia) == 0)
                    {
                        int?[] resultado = new int?[2];
                        resultado = alm.InsercionTrasladoAlm(Session["codigoalmacen"].ToString(), alm.codigoReceptor, alm.guia, alm.insumo, retornaDouble(alm.cantidadRecibir));
                        if (resultado[0] == 1)
                        {
                            alm.estado = 2;
                            alm.msjAlmacen = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> -La cantidad excede la cantidad actual del stock almacen emisor</span>";
                        }
                        else
                        {
                            if (resultado[0] == 0 && resultado[1] == 1)
                            {
                                alm.estado = 2;
                                alm.msjAlmacen = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> La Guía fue registrada</span>";
                            }
                        }
                    }
                    else
                    {
                        if (alm.existeGuia(alm.guia) == 1)
                        {
                            int?[] resultado = new int?[2];
                            resultado = alm.InsercionTrasladoAlm(Session["codigoalmacen"].ToString(), alm.codigoReceptor, alm.guia, alm.insumo, retornaDouble(alm.cantidadRecibir));
                            alm.estado = 2;
                            String mensaje = "";
                            if (resultado[1] == 2)
                            {
                                mensaje = "-Insumo fue registrado antes en ésta guía";
                            }
                            if (resultado[0] == 1)
                            {
                               
                                alm.msjAlmacen = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Cantidad excede lo del stock almacen emisor "+mensaje+"</span>";
                            }
                            else
                            {
                                if (resultado[1] == 1)
                                {
                                    alm.msjAlmacen = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> Insumo agregado exitosamente</span>";
                                }
                                else
                                {
                                    alm.msjAlmacen = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Error¡¡¡ "+mensaje+"</span>";
                                }
                            }
                        }
                    }
                    alm.tablansumoscant = alm.listaInsumosGTA(alm.guia);
                    return View(alm);
                default:
                    return View(alm);
            }
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