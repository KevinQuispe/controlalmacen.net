using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//ADD THIS
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace almacen.Models
{
    public class ProveedorModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        public string ruc { get; set; }
        public string raz_soc { get; set; }
        public string telefono { get; set; }
        public string fax { get; set; }
        public string rep_legal { get; set; }
        public string correo { get; set; }
        public String msjerror { get; set; }

        //metodo para registrar proveedor
        public int registrarProveedor(ProveedorModel pm)
        {
            int resultado = 0;
            try
            {
                contexto.pa_registrarProveedor(pm.ruc, pm.raz_soc, pm.telefono, pm.fax, pm.rep_legal,pm.correo);
                resultado = 1;
                contexto.SubmitChanges();
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
            }
            return resultado;
        }

        //metodo para buscar proveedor
        public proveedor buscarProveedor(String id)
        {

            proveedor pr = new proveedor();
            try
            {
                var consulta = contexto.pa_buscarProveedor(id);

                foreach (var c in consulta)
                {
                   
                    pr.ruc = c.ruc;
                    pr.razon_social = c.razon_social;
                    pr.telefono = c.telefono;
                    pr.fax = c.fax;
                    pr.rep_legal = c.rep_legal;
                    pr.correo = c.correo;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return pr;

        }

        //metodo para eliminar proveedor
        public string eliminarProveedor(String id)
        {
            string result = string.Empty;
           
            try
            {
                contexto.pa_eliminarProveedor(id);
                result = "ok";
                contexto.SubmitChanges();

            }
            catch (Exception ex)
            {
                result = ex.Message;

            }
            return result;
        }
        //metodo para editar proveedor
        public String actualizarProveedor(proveedor p)
        {
            string resultado = string.Empty;
            try
            {
                contexto.pa_updateProveedor(p.ruc, p.razon_social,p.telefono,p.fax, p.rep_legal,p.correo);
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


