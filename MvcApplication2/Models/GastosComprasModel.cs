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
    public class GastosComprasModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        public List<InsumoModel> listafechas { get; set; }

        [Required(ErrorMessage = "La  cuenta es requerida")]
        public string cuenta { get; set; }
        [Required(ErrorMessage = "La  cuenta es requerida")]
        public string codCompra { get; set; }
        [Required(ErrorMessage = "La cantidad es requerida")]
        public string cant_adic { get; set; }
        [Required(ErrorMessage = "El Precioes requerido")]
        public string prec_adic { get; set; }
        [Required(ErrorMessage = "El total es requerido")]
        public string total_adic { get; set; }
        [Required(ErrorMessage = "El descripción del adicional es requerido")]
        public string desc_adic { get; set; }


        //clase para lista de gastos de insumos

        public Gastos_Compra BuscarGastosCompras(String codcomp, String cuenta)
        {
            Gastos_Compra gc = new Gastos_Compra();
            try
            {
                var consulta = contexto.pa_busrcarGastosCompras(codcomp, cuenta);
                foreach (var gastos in consulta)
                {
                    gc.cod_compra = gastos.cod_compra;
                    gc.Cuenta = gastos.Cuenta;
                    gc.Descripcion = gastos.Descripcion;
                    gc.Cantidad = gastos.Cantidad;
                    gc.Precio = gastos.Precio;
                    gc.Total = gastos.Total;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return gc;

        }

       public String EditarGastosCompras(Gastos_Compra c)
       {
           String resultado = String.Empty;
           try
           {
               //contexto.pa_editarGastosCompras(c.codCompra, c.cuenta, c.desc_adic, Convert.ToDouble(c.cant_adic), Convert.ToDecimal(c.prec_adic), Convert.ToDecimal(c.total_adic));
               contexto.pa_editarGastosCompras(c.cod_compra, c.Cuenta, c.Descripcion, c.Cantidad, c.Precio, c.Total);
               resultado ="ok";
               contexto.SubmitChanges();
           }
           catch (Exception ex)
           {
               resultado = ex.Message;

           }
           return resultado;
       }

    
       //metodo para eliminar cliente
       public String eliminarGastosCompras(String codCompra, String cuenta)
       {
           string resultado = String.Empty;
           try
           {
               contexto.pa_eliminarGastosCompras(codCompra, cuenta);
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