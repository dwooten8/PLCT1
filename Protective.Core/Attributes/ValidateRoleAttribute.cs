using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protective.Core.Entity.Authentication;

namespace Protective.Core.Attributes
{
    public class ValidateRoleAttribute : ValidationAttribute
    {
        private readonly int _minElements;
        public ValidateRoleAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            List<IdentityRole> list = value as List<IdentityRole>;
            if (list != null)
            {
                var roleList = list.Where(r => r.Id != null).ToList();
                return roleList.Count >= _minElements;
            }
            return false;
        }
    }
}
