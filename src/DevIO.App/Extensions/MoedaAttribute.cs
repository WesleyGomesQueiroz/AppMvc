using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DevIO.App.Extensions
{
    public class MoedaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo(name: "pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult(errorMessage: "Moeda em formato inválido");
            }

            return ValidationResult.Success;
        }
    }

    public class MoedaAttributeAdapter : AttributeAdapterBase<MoedaAttribute>
    {
        public MoedaAttributeAdapter(MoedaAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentNullException(paramName: nameof(context));

            MergeAttribute(context.Attributes, key: "data-val", value: "true");
            MergeAttribute(context.Attributes, key: "data-val-moeda", value: GetErrorMessage(context));
            MergeAttribute(context.Attributes, key: "data-val-number", value: GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "Moeda em formato inválido";
        }

        public class MoedaValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
        {
            private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

            public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
            {
                if (attribute is MoedaAttribute moedaAttribute)
                {
                    return new MoedaAttributeAdapter(moedaAttribute, stringLocalizer);
                }

                return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
            }
        }

    }
}
