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
    public class ClienteModel
    {
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        //atributos de cliente for to view stock cliente
        [Required(ErrorMessage = "El ruc es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String ruc { get; set; }
        [Required(ErrorMessage = "Razon social es requerido")]
        public String razon_soc { get; set; }
        [Required(ErrorMessage = "La direccion es requerido")]
        public String direc { get; set; }
        [Required(ErrorMessage = "El telefono es requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")]
        public String telef { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        public String correo { get; set; }
        [Required(ErrorMessage = "La representacion legal es requerido")]
        public String rep_leg { get; set; }

        public String mensaje { get; set; }
     
        public List<SelectListItem> listadropcod { get; set; }
        public List<ClienteModel> listamodelcodigos { get; set; }
        public List<SelectListItem> listdropclientes { get; set; }
        public List<ClienteStockModel> listmodelclientes { get; set; }

        public ClienteModel()
        {
            var consulta1 = from a in contexto.semanas
                            select a.cod_semana;
            List<SelectListItem> codsemanas = new List<SelectListItem>();
            foreach (var q1 in consulta1)
            {

                codsemanas.Add(new SelectListItem() { Text = q1 + "" });
            }
            listadropcod = codsemanas;

            //here add cambios stock cliente
            var consulta2 = from a in contexto.clientes
                            select new { a.ruc, a.razon_social};
            List<SelectListItem> ruccliente = new List<SelectListItem>();
            foreach(var q2 in consulta2){
                ruccliente.Add(new SelectListItem() { Text = q2.ruc.Trim() + "-" + q2.razon_social.Trim().ToLower() + "", Value = q2.ruc.Trim() });
            }
            listdropclientes = ruccliente;


        }
        //METODO PARA LISTAR CLIENTES
        public List<cliente> Listarcliente()
        {
            List<cliente> lista = new List<cliente>();

            var consulta = contexto.pa_listarClientes();

            foreach (var cli in consulta)
            {
                cliente c = new cliente();
                c.ruc = cli.ruc;
                c.razon_social = cli.razon_social;
                c.direccion = cli.direccion;
                c.telefono = cli.telefono;
                c.correo = cli.correo;
                c.rep_legal = cli.rep_legal;

                lista.Add(c);
            }
            return lista;
        }
        //metodo para insertar cliente
        public int registrarCliente(ClienteModel c)
        {
            int resultado = 0;
            try
            {
                contexto.pa_registrarClientes(c.ruc, c.razon_soc, c.direc, c.telef, c.correo, c.rep_leg);
                resultado = 1;
                contexto.SubmitChanges();
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
            }
            return resultado;
        }
        //HERE
        public ClienteModel BuscarCliente(String ruc)
        {
            ClienteModel c = new ClienteModel();
            try
            {
                var consulta = contexto.pa_buscarClientes(ruc);
                foreach (var cliente in consulta)
                {
                   
                    c.ruc = cliente.ruc;
                    c.razon_soc = cliente.razon_social;
                    c.direc = cliente.direccion;
                    c.telef = cliente.telefono;
                    c.rep_leg = cliente.rep_legal;
                    c.correo = cliente.correo;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return c;

        }
        //metodo para eliminar cliente
        public string eliminarCliente(String ruc)
        {
            string resultado = String.Empty;
            try
            {
                contexto.pa_eliminarClientes(ruc);

                resultado = "ok";
                contexto.SubmitChanges();

            }
            catch (Exception ex)
            {
                resultado = ex.Message;

            }
            return resultado;
        }


       
    

        //nuevos cambios here
        //listar clientes en  view stock cliente
        public ClienteModel filtrarbusquedaCliente(String ruc)
        {
            ClienteModel cl = new ClienteModel();     
            try
            {
                var consulta = contexto.pa_filtrarCliente(ruc);
                foreach (var c in consulta)
                {
                    cl.ruc = c.ruc;
                    cl.razon_soc = c.razon_social;
                    cl.direc = c.direccion;
                    cl.correo = c.correo;
                    cl.telef = c.telefono;
                    cl.rep_leg = c.rep_legal;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return cl;

        }
        

        //metodo para actualizar cliente or editar

        public String actualizarCliente(String ruc, String rznsocial, String direc, String telef, String correo, String replegal)
        {
            String resultado = "";
            try
            {
                contexto.pa_actualizarCliente(ruc,rznsocial,direc,telef,correo,replegal);
                resultado = "ok";
                contexto.SubmitChanges();

            }
            catch (Exception ex)
            {
                resultado = ex.Message;

            }
            return resultado;
        }

        //listar stock productor
        public List<String[]> listaStocProductor(String ruc)
        {
            List<String[]> lista = new List<String[]>();
            var queryy = contexto.pa_stockActualProductor(ruc);
            foreach (var q in queryy)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, q.cantidad.ToString()});
            }
            return lista;
        }

      
       
    }
}
  