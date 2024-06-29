using System;
using System.ComponentModel.DataAnnotations;


namespace ORTastic.Attributes
{
    public class FechaMinimaAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime fecha = Convert.ToDateTime(value); 

            return fecha >= DateTime.Now.AddDays(30);
        }
    }
}
