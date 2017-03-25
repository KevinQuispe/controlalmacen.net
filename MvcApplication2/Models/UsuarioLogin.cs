using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace almacen.Models
{
    public class UsuarioLogin
    {

        [Required(AllowEmptyStrings = false,ErrorMessage="Error")]
         public string log { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Error")]
        public string pass { get; set; }

        DatosAlmacenDataContext user = new DatosAlmacenDataContext();
        public bool login()
        {
            var consulta = from u in user.usuarios
                        where u.loggin ==log && u.pass == pass
                        select u;
            if (consulta.Count() >0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}   
