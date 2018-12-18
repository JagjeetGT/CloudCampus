using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.ComponentModel;
using KRBAccounting.Data;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomProviders;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckPermissionAttribute : CheckSessionAttribute
    {
        /// <summary>
        /// logical OR relationship for all the permission settings.
        /// means: if one of the permission is TRUE, the CheckPermission will return OK; 
        /// if all of them are FALSE, the CheckPermission will fail over
        /// 
        /// How to make logical AND permission checker? put each permission in separate CheckPermissionAttributes
        /// </summary>
        
        
        private List<string> m_permissionSplit;

        private string m_permissions;
        public string Permissions { get; set; }
        //public string Permissions
        //{
        //    get
        //    {
        //        return m_permissions ?? string.Empty;
        //    }
        //    set
        //    {
        //        m_permissions = value;
        //        m_permissionSplit = SplitPermissions(value);
        //    }
        //}

        public string Module { get; set; }

        private List<string> SplitPermissions(string value)
        {
            List<string> result = new List<string>();

            if (string.IsNullOrWhiteSpace(value))
            {
                return result;
            }

            string[] midValues = value.Split(new char[] { ',', ';' }, System.StringSplitOptions.RemoveEmptyEntries);
            result = (from mid in midValues
                      where !string.IsNullOrWhiteSpace(mid)
                      select mid.Trim()).ToList();
            return result;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            HttpContext httpcontex = HttpContext.Current;

            base.OnActionExecuting(filterContext);
            // if base filter attribute is filtered, the result is redirect result. Otherwise, it is null
            if (filterContext.Result == null)
            {
                bool finalResult = KRBAccounting.Web.CustomFilters.CheckPermission.GivePermission(Permissions,Module);
                //var action = filterContext.ActionDescriptor.ActionName.ToString();
                
                //var properties = TypeDescriptor.GetProperties(typeof(Role));

                //foreach (string permission in m_permissionSplit)
                //{
                //    PropertyDescriptor property = properties.Find(permission, false);
                //    // can't find the property
                //    if (null == property)
                //    {
                //        continue;
                //    }
                //    // property found, but not boolean type
                //    if (property.PropertyType != typeof(bool))
                //    {
                //        continue;
                //    }
                //    bool booleanValue = (bool)property.GetValue(roles);

                //    finalResult = finalResult || booleanValue;
                //}

                if (finalResult == false)
                {
                    Logon(filterContext);
                }
            }
        }

       
    }
}