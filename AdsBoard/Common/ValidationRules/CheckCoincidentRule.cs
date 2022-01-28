using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdsBoard.Common.ValidationRules
{
    class CheckCoincidentRule : ValidationRule
    {
        public TextBox ComparedTextBox { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var retryPassword = (string)value;

            if(ComparedTextBox.Text == retryPassword)
            {
                return new ValidationResult(true,"");
            }
            else
            {
                return new ValidationResult(false, "Пароли не совпадают");
            }
            
        }
    }
}
