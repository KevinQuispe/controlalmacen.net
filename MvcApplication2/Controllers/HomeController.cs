using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using almacen.Models;
using System.Threading.Tasks;
namespace almacen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AlmacenModel alm = new AlmacenModel();
           //INDEX DE LA PAGINA MAESTRA
            ViewBag.Message = "control de Almacen";

            return View(alm);
        }

         // LOGIN
        public ActionResult Login()
        {
            ViewBag.Message = "";

            return View();
        }

        // CADENA  DATA CONTEXT


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(UsuarioLogin u)
        {
            AlmacenModel alm = new AlmacenModel();
            if (ModelState.IsValid)
            {
                if (u.login() == true)
                {
                    Session["Username"] = u.log;
                    Session["bloquear"] = "pointer-events: none;";
                }
                else
                {
                    return View("Login");
                }
                return View("Index",alm);
            }
            else
            {
                return View("Login");
            }

        }

       
    }   
}
