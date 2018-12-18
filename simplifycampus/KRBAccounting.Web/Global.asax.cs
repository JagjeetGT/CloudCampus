using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KRBAccounting.Data;
using KRBAccounting.Service.CustomValidations;
using Quartz.Impl;
using Quartz;

namespace KRBAccounting.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "AccountGroup", // Route name
                "account-group", // URL with parameters
                new { controller = "Master", action = "AccountGroup", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
           "LicenseExpired", // Route name
           "license-expired", // URL with parameters
           new { controller = "Error", action = "LicenseExpired", id = UrlParameter.Optional } // Parameter defaults
       );
            #region School routes

            routes.MapRoute(
                "GenerateStudentCode", // Route name
                "School/GenerateStudentCode/{studentName}", // URL with parameters
                new { controller = "School", action = "GenerateStudentCode" }
                // Parameter defaults
                );

            #endregion



            #region student Profile
            routes.MapRoute(
               "class-routine", // Route name
               "student-profile/class-routine", // URL with parameters
               new { controller = "StudentProfile", action = "ClassRoutine", name = UrlParameter.Optional } // Parameter defaults
           );
            routes.MapRoute(
                "student-profile", // Route name
                "student-profile/{name}", // URL with parameters
                new { controller = "StudentProfile", action = "Profile", name = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "studentprofile", // Route name
                "student-dashboard", // URL with parameters
                new { controller = "StudentProfile", action = "index" } // Parameter defaults
            );
            #endregion routes.MapRoute(
            #region Teacher
            routes.MapRoute(
               "teacher-subject", // Route name
               "teacher-subject/{subjectid}/{name}", // URL with parameters
               new { controller = "Teacher", action = "TeacherSubject", subjectid = UrlParameter.Optional, name = UrlParameter.Optional } // Parameter defaults
           );

            #endregion
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapHubs();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Custom Validation for Decimal values
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            //System.Data.SqlClient.SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);
        }

        void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            KRBAccounting.Data.DataContext.DatabaseName = "Academy_V3";
                var test=FirstRequestInitialisation.Initialise(context);
        }

        class FirstRequestInitialisation
        {
            private static string host = null;

            private static Object s_lock = new Object();

            // Initialise only on the first request
            public static string Initialise(HttpContext context)
            {
                if (string.IsNullOrEmpty(host))
                {
                    lock (s_lock)
                    {
                        if (string.IsNullOrEmpty(host))
                        {
                            Uri uri = HttpContext.Current.Request.Url;
                            host = uri.Host;
                        }
                    }
                }

                return host;
            }
        }


    }
}