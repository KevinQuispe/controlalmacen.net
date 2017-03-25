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
    public class SemanaModel
    {
        //atributos de semanass
        [Required(AllowEmptyStrings = false, ErrorMessage = "El codigo es requerido")]
        public String codsemana { get; set; }

        [Required(ErrorMessage = "Año requerido"), RegularExpression("([0-9][0-9]*)", ErrorMessage = "valor no valido")] 
        public String anio { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]        
        public String fechaI { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha es requerida")]
        //[RangoFechas(6, "fechaI", ErrorMessage = "Diferencia en días debe ser 7 días")]
        public String fechaF { get; set; }
       
        public String observ { get; set; }

        public String estado { get; set; }

        public String jvscrpt { get; set; }

        public String msjrespuesta { get; set; }

        //datacontext
        DatosAlmacenDataContext contexto = new DatosAlmacenDataContext();    
       
        //select item para cadadropdown
        public List<SelectListItem> listadropcod { get; set; }
        public List<SelectListItem> listadropanio { get; set; }
   
        //List model para cadadropdown
        public List<SemanaModel> listamodelcodigos { get; set; }
        public List<SemanaModel> listamodelpanios { get; set; }
        public List<String[]> listaFaltaCerrarProduccion { get; set; }
     
        //Initialize
        public SemanaModel()
        {
            //listadropanio = null;
            var consulta1 = from a in contexto.semanas
                           select a.anio;
            codsemana = "";
            anio = "";
            fechaI="";
            fechaF = "";
            jvscrpt = "";
            listaFaltaCerrarProduccion = new List<string[]>();
            
        }        
        
        //Here regritra semana
        public int registrarSemana(String codsem, int año, DateTime? fechai, DateTime? fechaf,String observ ,int estate)
        {
  
            int resultado = 0;           
            try
            {
                contexto.pa_registrarSemanas(codsem, año, fechai, fechaf, observ, estate);
                resultado = 1;
                contexto.SubmitChanges();
            return resultado;
               
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
                return resultado;
            }
           
        }
        
        // metodo buscar semana
        public SemanaModel buscarSemana(String codigo)
        {
            SemanaModel s = new SemanaModel();         

            try
            {
                var consulta = contexto.pa_buscarSemana(codigo);
                

                foreach (var sem in consulta)
                {
                    s.codsemana = sem.cod_semana;
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return s;

        }     
        
        //metodo para activar semana
        public int? activarSemana(String codsem,int a,String codalm)
        {
            int? resultado = 0;
            try
            {
                var comando = contexto.pa_activarSemana(codsem, a, codalm).First();
                contexto.SubmitChanges();
                resultado = comando.respuesta;
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
            }
            return resultado;
        }
        
        //metodo para cerrar semana
        public int? cerrarsemana(String codsem,int a)
        {
            int? resultado = 0;
            try
            {
                var query = contexto.pa_CerrarSemana(codsem,a);
                resultado = 1;
            }
            catch (Exception ex)
            {
                resultado = ex.HResult;
            }            
            return resultado;
        }

        public List<String[]> listarProduccionFaltaCerrar(String codsem,int? anio)
        {
            List<String[]> lista = new List<string[]>();
            var query = contexto.pa_VerificarCierreProduccion(codsem,anio);
            foreach(var q  in query){
                lista.Add(new String[] { q.ruc,q.razon_social,q.descripcion });
            }
            return lista;
        }


        
    }

}
   