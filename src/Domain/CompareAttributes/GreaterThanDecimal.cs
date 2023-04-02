using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cegeka.Auction.Domain.CompareAttributes;

public class GreaterThanDecimal : CompareAttribute
{
    public GreaterThanDecimal(string otherProperty) : base(otherProperty)
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyValue = validationContext.ObjectType.GetProperty(OtherProperty).GetValue(validationContext.ObjectInstance, null);
        var decimalValue = Convert.ToDecimal(value);
        var otherDecimalValue = Convert.ToDecimal(otherPropertyValue);

        if (decimalValue > otherDecimalValue)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {OtherPropertyDisplayName}.");
    }
}
