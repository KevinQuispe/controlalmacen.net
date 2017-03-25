using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace almacen.Models
{
    public class GuiaClienteModel
    {
        public string codIns { get; set; }
        public string codalmac { get; set; }
        public string fecha { get; set; }
        public string numguia { get; set; }
        public string ruc { get; set; }
        public string persona_recepcion { get; set; }
        public string observacion { get; set; }

    }
}
