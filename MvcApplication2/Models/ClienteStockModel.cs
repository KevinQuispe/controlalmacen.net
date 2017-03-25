using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
namespace almacen.Models
{
    public class ClienteStockModel
    {
        //data context 
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();
        //atributos de cliente
        public String ruc { get; set; }
        public String razon_soc { get; set; }
        public String direc { get; set; }
        public String correo { get; set; }
        public String telef { get; set; }
        public String rep_legal { get; set; }
        public String codsem { get; set; }

         // add this cambios 27-01
        public String cant { get; set; }
        public String nominsumo { get; set; }
        public List<String[]> listastcokinsumos { get; set; }

        public List<SelectListItem> listadropinsumos = new List<SelectListItem>();
        public List<InsumoModel> listamodelinsumos = new List<InsumoModel>();

        public String msjerror { get; set; }
        //declare variables list for semanas
        public List<SelectListItem> listdropcodsemanas { get; set; }
        public List<ClienteStockModel> listmodelcodsemanas { get; set; }

  
        //Atributo de stockinicial
        public String codinsumo { get; set; }
        public String producto { get; set; }
        public String stockinicial { get; set; }
        public String costo { get; set; }

        public List<SelectListItem> listaclientes { get; set; }
        public List<SelectListItem> listainsumos { get; set; }
        public List<String[]> listastockcliente { get; set; }

        public List<String[]> listastockbysemana { get; set; }

        //here load semanasz
        public ClienteStockModel()
        {
            var consulta1 = from a in contexto.semanas
                            select a.cod_semana;
            List<SelectListItem> codsemanas = new List<SelectListItem>();
            listainsumos = new List<SelectListItem>();
            listaclientes = new List<SelectListItem>();

            foreach (var q1 in consulta1)
            {
                codsemanas.Add(new SelectListItem() { Text = q1 + "" });
            }
            listdropcodsemanas = codsemanas;


            var otraconsulta = contexto.pa_listarClientes();
            foreach (var q1 in otraconsulta)
            {
                listaclientes.Add(new SelectListItem() { Text = q1.ruc.Trim() + "-" + q1.razon_social.Trim().ToLower() + "", Value = q1.ruc.Trim() });
            }
            var consulta = from a in contexto.insumos
                            select new { a.nombre_insumo, a.cod_insumo };
         
            foreach (var q1 in consulta)
            {
                listainsumos.Add(new SelectListItem() { Text = q1.cod_insumo.Trim() + "-" + q1.nombre_insumo.Trim() + "", Value = q1.cod_insumo.Trim() });
            }

            listastockcliente = new List<String[]>();

            listastockbysemana = new List<String[]>();

            msjerror = "";

            //here cambios adicionales

            //lista de insumos para consultar por almacen
            List<String[]> listastock = new List<String[]>();
            listastock.Add(new String[] { "", "", "" });
            listastcokinsumos = listastock;

        }



        public int? registrarEnStockCliente(String ruc,String codinsumo,double? cant,decimal? costo)
        {
            int? respuesta = 0;
            try
            {
                var comando = contexto.pa_RegistrarStockClienteInic(ruc, codinsumo, cant, costo).First();
                respuesta = comando.resultado;
            }catch(Exception ex){
                respuesta = ex.HResult;
            }
            return respuesta;
        }

        public List<String[]> listaStockClientePorProductor(String ruc)
        {
            List<String[]> lista = new List<String[]>();
            var queryy = contexto.pa_listaSctockClienteXProductor(ruc);
            foreach (var q in queryy)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, q.ruc, String.Format(CultureInfo.InvariantCulture, "{0:0.00}", q.cantidad), String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", q.costo) });
            }
            return lista;
        }


        //add changes
        public List<String[]> listaStocProductor(String ruc)
        {
            List<String[]> lista = new List<String[]>();
            var queryy = contexto.pa_stockActualProductor(ruc);
            foreach (var q in queryy)
            {
                lista.Add(new String[] { q.cod_insumo, q.nombre_insumo, q.cantidad.ToString() });
            }
            return lista;
        }


        //modified day 01-02

        public List<String[]> listaStockSemana(String ruc, String sem)
        {
            List<String[]> lista = new List<String[]>();
            var queryy = contexto.pa_consultarStockSemana(ruc, sem);
            foreach (var q in queryy)
            {
                lista.Add(new String[] { q.nombre_insumo, q.saldo_inicial.ToString(), q.entrega.ToString(), q.consumo_produccion.ToString(), q.saldoFin.ToString() });
            }
            return lista;
        }
    }
}