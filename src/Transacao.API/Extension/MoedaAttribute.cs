using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Transacao.API.Extension
{
    public class MoedaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo("pt-Br"));
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda em formato inválido");
            }
            return ValidationResult.Success;
        }
    }
}
