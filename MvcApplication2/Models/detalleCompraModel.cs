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
    public class detalleCompraModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        public int idcompra { get; set; }
        public int idcom { get; set; }
        public String codinsumo { get; set; }
        public String insumo { get; set; }
        public String precio { get; set; }
        public String cantidad { get; set; }
        public String total { get; set; }
        public String Observacion { get; set; }
        public String codalma { get; set; }
        public String comandojavascript { get; set; }
        public detalleCompraModel()
        {
            idcompra = 0;
            insumo = "";
            codinsumo = "";
            cantidad = "";
            precio = "";
            total = "";
            Observacion = "";
            codalma = "";
            comandojavascript = "document.getElementById('btnEditarDetGuia').disabled = false;";
        }

    }
}