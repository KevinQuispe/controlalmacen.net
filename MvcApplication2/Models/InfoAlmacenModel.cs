using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace almacen.Models
{
    public class InfoAlmacenModel
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public InfoAlmacenModel()
        {
            codigo = "";
            descripcion = "";
            direccion = "";
        }
    }
}