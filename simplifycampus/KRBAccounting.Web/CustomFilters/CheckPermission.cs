using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using KRBAccounting.Data;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomProviders;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckPermission
    {

        
        public readonly IFormsAuthenticationService _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
        public static bool GivePermission(string permission, string modules)
        {
            var checkPermission = new CheckPermission();
            var _context = new DataContext();
            var user = checkPermission._authentication.GetAuthenticatedUser();
            var roles = user.Roles;
            bool finalResult = false;
            var module = _context.ModuleLists.Where(x => x.ShortName == modules).FirstOrDefault();
            return true;
            var properties = TypeDescriptor.GetProperties(typeof(SecurityRight));
            foreach (var role in roles)
            {
                if(role.RoleName=="admin")
                {
                    return true;
                }
                var securityRight =
                    _context.SecurityRights.FirstOrDefault(x => x.Role == role.Id && x.ModuleId == module.Id);
                      
                PropertyDescriptor property = properties.Find(permission, false);
                // can't find the property
                if (null == property)
                {
                    continue;
                }
                // property found, but not boolean type
                if (property.PropertyType != typeof(bool))
                {
                    continue;
                }
                var value = property.GetValue(securityRight);
                bool booleanValue = value != null && (bool)value;

                finalResult = finalResult || booleanValue;
                if (finalResult)
                    return finalResult;

            }
            return false;
        }
    }
}