using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeAnalyzer.Web.Attribute
{
    public class CheckFileAttribute : ValidationAttribute, IClientModelValidator
    {
        private int _maxLength;

        public CheckFileAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Type objectType = context.ObjectType;

            PropertyInfo pInfo = objectType.GetProperty(context.MemberName);

            IFormFile file = (IFormFile)pInfo.GetValue(context.ObjectInstance);

            Regex reg = new Regex("^.*(.cs|.txt)$");

            if(file == null)
            {
                return new ValidationResult("Select file.");
            }
            if (!reg.IsMatch(file.FileName))
            {
                return new ValidationResult("Invalid file format. Only 'txt' and 'cs' files are allowed.");
            }
            if (file.Length > _maxLength*1024)
            {
                return new ValidationResult($"Maximum file length is {_maxLength}kb");
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-NotSelected"] = "Select file.";
            context.Attributes["data-val-FileFormat"] = "Invalid file format. Only 'txt' and 'cs' files are allowed.";
            context.Attributes["data-val-FileSize"] = $"Maximum file size is {_maxLength}kb";
        }
    }
}
