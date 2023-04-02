using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Domain.CompareAttributes;

public class GreaterThanOrEqualToDate : CompareAttribute
{
    public GreaterThanOrEqualToDate(string otherProperty) : base(otherProperty)
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyValue = validationContext.ObjectType.GetProperty(OtherProperty).GetValue(validationContext.ObjectInstance, null);
        var dateValue = (DateTime)value;
        var otherDateValue = (DateTime)otherPropertyValue;

        if (dateValue >= otherDateValue)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than or equal to {OtherPropertyDisplayName}.");
    }
}
