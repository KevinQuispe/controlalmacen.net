using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace almacen.Models
{
    public class TipoCambioModel : Controller
    {

        // GET: /TipoCambioModel
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        //atributos para cambio moneda
        public string fecha { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar este dato")]
        public string compra { get; set; }
        [Required(ErrorMessage="Es obligatorio ingresar este dato")]
        public string venta { get; set; }
        public int id { get; set; }
   

        //metodo para buscar tipo de cambio
        public Cambio_moneda BuscarTipoCambio(DateTime id)
        {
            Cambio_moneda tc = new Cambio_moneda();
            try
            {
                var consulta = contexto.pa_BuscartipoCambio(id);
                foreach (var cambio in consulta)
                {
                    String date = cambio.Fecha_Cambio.ToString();
                    // String fecha = String.Format(CultureInfo.InvariantCulture, "{0:yyyy/MM/dd}",c.fecha);

                    tc.Fecha_Cambio = cambio.Fecha_Cambio;
                    tc.Compra = cambio.Compra;
                    tc.Venta = cambio.Venta;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tc;
        }
        //actualizar
        public String actualizarTipoCambio(Cambio_moneda c)
        {
            String resultado = "";
            try
            {
                string date = c.Fecha_Cambio.ToShortTimeString();

                contexto.pa_updateCotizacionDolar(c.Fecha_Cambio, c.Compra, c.Venta);
                resultado = "ok";
                contexto.SubmitChanges();
            }
            catch (Exception ex)
            {
                resultado = ex.Message;

            }
            return resultado;
        }
        public int inserarNuevaCotizacion(Cambio_moneda c)
        {
            int retorno = 0;
            try
            {
                contexto.pa_insertarNewCotizacion(c.Fecha_Cambio, c.Compra,c.Venta);
                retorno = 1;
                contexto.SubmitChanges();
            }
            catch (Exception ex)
            {
                retorno = ex.HResult;
            }
            return retorno;
        }
    }
}
    

