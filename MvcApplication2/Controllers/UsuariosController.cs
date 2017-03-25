using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//add this
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc.Html;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace almacen.Models
{
    public class UsuariosController : Controller
    {


        UsuarioModel model = new UsuarioModel();

        //accion para listar usuarios
        public ActionResult ListarUsuarios()
        {
            return View(model.ListarUsuarios());
        }


        //acction registrar ussuaruios
        public ActionResult registrausuarios()
        {
            UsuarioModel usu = new UsuarioModel();
            usu.login = "";
            usu.password = "";
            return View(usu);
        }
        [HttpPost]
        public ActionResult registrausuarios(UsuarioModel usu)
        {
            try
            {
                String mensaje = "";
                int comando = model.registrarUsuarios(usu);
                if (comando == 1)
                {
                    mensaje = "<span class=\"text-primary\"><i class=\"fa fa-thumbs-o-up fa-2x\" aria-hidden=\"true\"></i>Usuario creado correctamente</span>";
                }
                else
                {
                    if (comando == -2146232060)
                    {
                        mensaje = "<span class=\"text-danger\"><i class=\"fa fa-thumbs-down fa-2x\" aria-hidden=\"true\"></i> El login ingresado ya existe.<s/pan>";
                    }
                    else
                    {
                        mensaje = "<span class=\"text-danger\"><i class=\"fa fa-database fa-2x\" aria-hidden=\"true\"></i> Error de procesamiento.<s/pan>";
                    }
                }
                usu.msjUsuarioModel = mensaje;
                return View("registrausuarios", usu);
            }
            catch
            {
                usu.msjUsuarioModel = "Error inesperado...";
                return View(usu);
            }

        }
        // call method for delete user

        public ActionResult DeleteUser(int id)
        {
            var user = model.BuscarUsuario(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id, FormCollection collection)
        {
            try
            {
                if (model.EliminarUsuario(id).Equals("ok"))
                {

                    return RedirectToAction("ListarUsuarios");
                }
                else
                {
                    return RedirectToAction("DeleteUser");
                }
            }
            catch
            {
                return View();
            }

        }
        //metodo para editar datos de user
        public ActionResult EditarUser(int id)
        {
            var user = model.BuscarUsuario(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult EditarUser(int id, usuario u)
        {
            try
            {
                usuario user = new usuario();
                user.id = u.id;
                user.loggin = u.loggin;
                user.pass = u.pass;
                user.nombres = u.nombres;
                user.apellidos = u.apellidos;
                user.correo = u.correo;
                user.telefono = u.telefono;
                //call procedure 
                if (model.ActualizarUsuario(user).Equals("ok"))
                {

                    return RedirectToAction("ListarUsuarios");
                }
                else
                {
                    return RedirectToAction("EditarUser");
                }
            }
            catch
            {

                return View();
            }
        }

    }
}