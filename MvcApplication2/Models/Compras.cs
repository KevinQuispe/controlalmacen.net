using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//add this
//this para hacer uso de display
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace almacen.Models
{
    public class Compras
    {
        public int id { get; set; }
        public String formadepago { get; set; }
        public String numero { get; set; }
        public String razonsocial { get; set; }
        public String fechaemision { get; set; }
        public String fechavencimiento { get; set; }
        public String observacion { get; set; }
        public String valorventa { get; set; }
        public String impuesto { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Debe ser decimal")]
        public String total { get; set; }
        public String codalm { get; set; }
        public String comandojavascript { get; set; }

        public Compras()
        {
            id = 0;
            numero = "";
            razonsocial = "";
            fechaemision = "";
            fechavencimiento = "";
            observacion = "";
            valorventa = "";
            impuesto = "";
            total = "";
            codalm = "";
            comandojavascript = "document.getElementById('btnEditarCompra').disabled = false;";
        }
    }
}