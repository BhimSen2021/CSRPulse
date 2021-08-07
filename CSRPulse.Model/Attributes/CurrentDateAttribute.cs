using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model.Attributes
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            
            var dt = (DateTime)value;
            if (dt <= DateTime.UtcNow.Date)
            {
                return true;
            }
            return false;
        }
       
    }
}
