using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations; 
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace almacen.Models
{
    public class UsuarioModel
    {
       //data context
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();

        [Required(ErrorMessage = "El  nombre es requerido")]
        public String nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public String  apellido { get; set; }
        [Required(ErrorMessage = "El telefono es requerido"),RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String telefono { get; set; }
        [EmailAddress(ErrorMessage = "El email no tiene el formato correcto")]
        [Required(ErrorMessage = "El correo requerido")]
        public String correo { get; set; }

        public String direcion { get; set; }
        [Required(ErrorMessage = "El login es requerido")]
       public String login { get; set; }
        [Required(ErrorMessage = "El password es requerido")]
        public String password { get; set; }
        public String msjUsuarioModel { get; set; }

        //acction listar usuarios
        public List<usuario> ListarUsuarios()
        {
            List<usuario> lista = new List<usuario>();

           

            var query = contexto.pa_listarusuarios();
            foreach (var q in query)
            {
                usuario usu = new usuario();
                usu.id = q.id;
                usu.nombres = q.nombres;
                usu.apellidos = q.apellidos;          
                usu.correo = q.correo;
                usu.telefono = q.telefono;
                usu.loggin = q.loggin;
                usu.pass = q.pass;         
                lista.Add(usu);
            }
            return lista;

        }
        //metodo para registrar usuarios
        public int registrarUsuarios(UsuarioModel u)
        {
            int resultado = 0;
            try
            {
              contexto.pa_insertarUsuarios(u.login,u.password,u.nombre,u.apellido,u.correo,u.telefono);
              resultado = 1;
              contexto.SubmitChanges();
            }
            catch(Exception ex){
                resultado = ex.HResult;
            }
            return resultado;
        }
        //medotodo para buscar usaurio
        public usuario BuscarUsuario(int id)
        {
            usuario um = new usuario();
            try
            {
                var consulta = contexto.pa_buscarUsuario(id);
                foreach (var usu in consulta)
                {
                    um.id = usu.id;
                    um.loggin = usu.loggin;
                    um.pass = usu.pass;
                    um.nombres = usu.nombres;
                    um.apellidos = usu.apellidos;
                    um.correo = usu.correo;
                    um.telefono = usu.telefono;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return um;

        }
        //metodo para eliminar usuario
        public String EliminarUsuario(int id)
        {
            string resultado = string.Empty;
            try
            {
                contexto.pa_eliminarUsuario(id);
                resultado = "ok";
                contexto.SubmitChanges();

            }
            catch (Exception ex)
            {
                resultado = ex.Message;

            }
            return resultado;
        }
        //metodo para actualizar usuario

        public String ActualizarUsuario(usuario u)
        {

          string resultado = string.Empty;
            try
            {
                contexto.pa_actualizarUsuario(u.id,u.loggin,u.pass,u.nombres,u.apellidos,u.correo,u.telefono);
                resultado = "ok";
                contexto.SubmitChanges();

            }
            catch (Exception ex)
            {
                resultado = ex.Message;

            }
            return resultado;
        }


    }
}
