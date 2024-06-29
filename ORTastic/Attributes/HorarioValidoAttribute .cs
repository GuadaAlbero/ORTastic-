using System;
using System.ComponentModel.DataAnnotations;


namespace ORTastic.Attributes
{
    public class HorarioValido: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime fecha = Convert.ToDateTime(value);
            TimeSpan hora = fecha.TimeOfDay;

            return hora >= new TimeSpan(16,0,0) && hora <= new TimeSpan(21,0,0);
        }
    }
}
