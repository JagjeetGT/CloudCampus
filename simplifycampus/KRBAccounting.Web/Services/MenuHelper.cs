using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomFilters;

namespace KRBAccounting.Web.Services
{
    public class MenuHelper
    {
       
        public static List<MenuItem> MenuList(string userName,int categoryId)
        {
            DataContext _context = new DataContext();
            var menuItems = _context.MenuItems.Where(x => x.CategoryId == categoryId).ToList();
            var menuList = new List<MenuItem>();

            var user1 = _context.Users.Where(x => x.Username == userName).FirstOrDefault();
           if( user1.Roles.Where(x=>x.RoleName.ToLower() == "admin").Any()){
               return menuItems;
           }


            /********/
            foreach (var menu in menuItems)
            {
                
                if (CheckModulePermission.CheckModule(menu.ModuleKey) || menu.ModuleKey == "Accounting")
                {
                    var user = _context.Users.Where(x => x.Username == userName).FirstOrDefault();
                    var roles = user.Roles;
                    var properties = TypeDescriptor.GetProperties(typeof(SecurityRight));


                    foreach (var role in roles)
                    {
                        if (role.RoleName == "admin")
                        {
                            menuList.Add(menu);
                        }
                        else
                        {
                            var securityRight =
                                _context.SecurityRights.FirstOrDefault(
                                    x => x.Role == role.Id && x.ModuleId == menu.ModuleId);

                            PropertyDescriptor property = properties.Find("Navigate", false);
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
                            if (booleanValue)
                            {
                                menuList.Add(menu);
                            }
                        }
                    }
                }
            }
            return menuList.Distinct().ToList();
        }
      
        public static List<MenuItem> CustomMenuList(string userName)
        {
            DataContext _context = new DataContext();
            var menuItems = _context.MenuItems.ToList();
            var menuList = new List<MenuItem>();

            /********/
            foreach (var menu in menuItems)
            {

                if (CheckModulePermission.CheckModule(menu.ModuleKey) || menu.ModuleKey == "Accounting")
                {
                    var user = _context.Users.Where(x => x.Username == userName).FirstOrDefault();
                    var roles = user.Roles;
                    var properties = TypeDescriptor.GetProperties(typeof(SecurityRight));


                    foreach (var role in roles)
                    {
                        if (role.RoleName == "librarian")
                        {
                            menuList.Add(menu);
                        }
                        else
                        {
                            var securityRight =
                                _context.SecurityRights.FirstOrDefault(
                                    x => x.Role == role.Id && x.ModuleId == menu.ModuleId);

                            PropertyDescriptor property = properties.Find("Navigate", false);
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
                            if (booleanValue)
                            {
                                menuList.Add(menu);
                            }
                        }
                    }
                }
            }
            return menuList.Distinct().ToList();
        }

        public static List<MenuItem> HeaderMenu(string userName)
        {
            DataContext _context = new DataContext();
            var menuItems = _context.MenuItems.Where(x => x.ParentId == 0 && x.Action.ToLower() == "Dashboard").ToList();
            var menuList = new List<MenuItem>();

            /********/
            foreach (var menu in menuItems)
            {

                if (CheckModulePermission.CheckModule(menu.ModuleKey) || menu.ModuleKey == "Accounting")
                {
                    var user = _context.Users.Where(x => x.Username == userName).FirstOrDefault();
                    var roles = user.Roles;
                    var properties = TypeDescriptor.GetProperties(typeof(SecurityRight));


                    foreach (var role in roles)
                    {
                        if (role.RoleName == "admin")
                        {
                            menuList.Add(menu);
                        }
                        else
                        {
                            var securityRight =
                                _context.SecurityRights.FirstOrDefault(
                                    x => x.Role == role.Id && x.ModuleId == menu.ModuleId);

                            PropertyDescriptor property = properties.Find("Navigate", false);
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
                            if (booleanValue)
                            {
                                menuList.Add(menu);
                            }
                        }
                    }
                }
            }
            return menuList.Distinct().ToList();
        }
    }
}