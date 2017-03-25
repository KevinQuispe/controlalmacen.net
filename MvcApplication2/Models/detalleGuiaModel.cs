using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace almacen.Models
{
    public class detalleGuiaModel
    {
        public String codInsumo { get; set; }
        public String nomInsumo { get; set; }
        public String numGuia { get; set; }
        public String cant { get; set; }
        public String ruc { get; set; }
        public String observacion { get; set; }
        public String fecha { get; set; }
        public String persona { get; set; }
        public String unidad { get; set; }
        public String comandojavascript { get; set; }

  

        public detalleGuiaModel()
        {
            codInsumo = "";
            nomInsumo = "";
            numGuia = "";
            cant = "";
            ruc = "";
            observacion = "";
            fecha = "";
            persona = "";
            comandojavascript = "document.getElementById('btnEditarDetGuia').disabled = false;";
        }
    }
}