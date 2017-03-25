using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.Web.Mvc;
using almacen.Models;
namespace almacen.Controllers
{
    public class SemanasController : Controller
    {

        // Data contex
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        //agregamos el modelo semanas
        public SemanaModel model = new SemanaModel();


        //action registrar semanas
        public ActionResult registrarsemana()
        {
            SemanaModel sem = new SemanaModel();
            var comando = contexto.pa_RetornaUltimaFecha().First();
            if (comando.existe == 1)
            {
                sem.fechaI = String.Format("{0:dd/MM/yyyy}", comando.fechaInic);
                sem.fechaF = String.Format("{0:dd/MM/yyyy}", comando.FechaFinal);
                sem.jvscrpt = "document.getElementById(\"fechaI\").disabled=true;";
            }
            return View(sem);
        }

        //metodo para insertar semanas
        [HttpPost]
        public ActionResult registrarsemana(SemanaModel sem)
        {
            if ((ModelState.IsValidField("anio")) && (ModelState.IsValidField("fechaI")) && (ModelState.IsValidField("fechaF")) && (ModelState.IsValidField("codsemana")))
            {
                int? comando = 0;
                comando = model.registrarSemana(sem.codsemana, Convert.ToInt32(sem.anio), retornaFecha(sem.fechaI), retornaFecha(sem.fechaF), sem.observ, Convert.ToInt32(sem.estado));
                if (comando == 1)
                {
                    sem.msjrespuesta = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> La semana ha registrado exitosamente</span>";
                }
                else
                {
                    if (comando == -2146232060)
                    {
                        sem.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Código de semana repetido en el año</span>";
                    }
                    else
                    {
                        sem.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> No se puede registrar semana</span>";
                    }
                }
            }
            else
            {
                sem.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Ingrese datos correctos</span>";
            }
            sem.jvscrpt = "document.getElementById(\"fechaI\").disabled=true;document.getElementById(\"fechaF\").disabled=true;document.getElementById(\"codsemana\").disabled=true;document.getElementById(\"anio\").disabled=true;document.getElementById(\"btnsubmit\").disabled=true;";
            return View("registrarsemana", sem);
        }

        //activar semana
        public ActionResult activarsemana()
        {
            SemanaModel res = new SemanaModel();
            try
            {
                var query = contexto.pa_SemanaPorActivar().First();
                res.codsemana = query.cod_semana;
                res.anio = query.anio + "";
                res.fechaI = String.Format("{0:dd/MM/yyyy}", query.fecha_inicio);
                res.fechaF = String.Format("{0:dd/MM/yyyy}", query.fecha_fin);
            }
            catch (Exception)
            {
                res.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> No hay semana por activar</span>";
            }
            return View(res);
        }

        //metodo activar semana   
        [HttpPost]
        public ActionResult activarsemana(SemanaModel sem, String SemanaActivar)
        {
            int? comando = model.activarSemana(sem.codsemana, Convert.ToInt32(sem.anio), Session["codigoalmacen"].ToString());
            if (comando == 1)
            {
                sem.msjrespuesta = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> Semana '" + sem.codsemana + "' activada</span>";
            }
            else
            {
                if (comando == 2)
                {
                    sem.msjrespuesta = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> Debe cerrar la actual semana activa</span>";
                }
                else
                {
                    sem.msjrespuesta = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> Error Activación" + comando + "' activada</span>";
                }

            }
            sem.jvscrpt = "document.getElementById(\"btnsubmit\").disabled=true;";
            return View(sem);
        }
        //accion view  cerrarsemana
        public ActionResult cerrarsemana()
        {
            try
            {
                String faltacerrar = "";
                SemanaModel res = new SemanaModel();
                var query = (contexto.pa_SemanaPorCerrar()).First();
                res.codsemana = query.cod_semana;
                res.anio = query.anio + "";
                res.fechaI = String.Format("{0:dd/MM/yyyy}", query.fecha_inicio);
                res.fechaF = String.Format("{0:dd/MM/yyyy}", query.fecha_fin);
                res.listaFaltaCerrarProduccion=(res.listarProduccionFaltaCerrar(res.codsemana, Convert.ToInt32(res.anio)));
                if (res.listaFaltaCerrarProduccion.Any())
                {
                    foreach (var q in res.listaFaltaCerrarProduccion)
                    {
                        faltacerrar =faltacerrar+ "→Productor: " + q[0] + "(" + q[1] + ") en el Almacen: " + q[2].ToUpper() + ".<br />";
                    }
                    res.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> <u>Advertencia:</u> Falta cerrar producción<br />" + faltacerrar + "</span>";
                }
                return View(res);
            }
            catch (Exception)
            {
                SemanaModel res = new SemanaModel();
                res.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> No hay semana por cerrar</span>";
                return View(res);
            }
        }

        //metodo post para cerrarsemana
        [HttpPost]
        public ActionResult cerrarsemana(SemanaModel sem)
        {
            String faltacerrar = "";
            sem.listaFaltaCerrarProduccion = sem.listarProduccionFaltaCerrar(sem.codsemana, Convert.ToInt32(sem.anio));
            if (sem.listarProduccionFaltaCerrar(sem.codsemana, Convert.ToInt32(sem.anio)).Any())
            {
                foreach (var q in sem.listarProduccionFaltaCerrar(sem.codsemana, Convert.ToInt32(sem.anio)))
                {
                    faltacerrar = faltacerrar+"→Productor: " + q[0] + "(" + q[1] + ") en el Almacen: " + q[2].ToUpper() + ".<br />";
                }
                sem.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> <u>No cerró semana porque falta cerrar producción: </u> <br />" + faltacerrar + "</span>";
            }
            else
            {
                int? comando = model.cerrarsemana(sem.codsemana, Convert.ToInt32(sem.anio));
                if (comando == 1)
                {
                    sem.msjrespuesta = "<span class=\"text-primary\"><i class=\"fa fa-check fa-2x\" aria-hidden=\"true\"></i> La semana se cerró correctamente</span>";
                }
                else
                {
                    sem.msjrespuesta = "<span class=\"text-danger\"><i class=\"fa fa-ban fa-2x\" aria-hidden=\"true\"></i> Error al cerrar semana</span>";
                }                
            }
            return View(sem);
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
