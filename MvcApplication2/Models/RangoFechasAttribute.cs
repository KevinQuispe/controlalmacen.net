using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;

namespace almacen.Models
{
    public class RangoFechasAttribute : ValidationAttribute
    {
        private readonly int _rango;
        private readonly String _fecha2;        
        public RangoFechasAttribute(int rango, String otrafecha)
        {
            _rango = rango;
            _fecha2 = otrafecha;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fecha = value as String;            
            var property = validationContext.ObjectType.GetProperty(_fecha2);
            String resultad = null;
            if (property == null)
            {
                resultad = "Error en Fecha Inicial";
            }
            var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);
            var fecha2 = otherPropertyValue as String;
            DateTime fech1;
            DateTime fech2;            
            if (DateTime.TryParse(fecha, out fech1))
            {
                fech1 = DateTime.Parse(fecha);
            }
            else
            {
                resultad="Fecha Final formato incorrecto";
            }
            if (DateTime.TryParse(fecha2, out fech2))
            {
                fech2 = DateTime.Parse(fecha2);
            }
            else
            {
                resultad = "Fecha Inicial formato incorrecto";
            }
            //Real validation
            if (fech2.AddDays(_rango).Equals(fech1))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(resultad);
            }

            
        }
    }
}