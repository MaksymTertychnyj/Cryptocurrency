﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cryptocurrency.Helper
{
    public class DigitsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);

            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                    var regex = new Regex("[^0-9.-]+,"); 
                    var parsingOk = !regex.IsMatch(value.ToString()!);
                    if (!parsingOk)
                    {
                        validationResult = new ValidationResult(false, "Please Enter Numeric Value");
                    }
            }

            return validationResult;
        }
    }
}
