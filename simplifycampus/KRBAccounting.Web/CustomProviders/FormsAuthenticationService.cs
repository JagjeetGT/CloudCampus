using System;
using System.Web;
using System.Web.Security;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;


namespace KRBAccounting.Web.CustomProviders
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        private readonly TimeSpan _contextexpirationTimeSpan;
        private readonly HttpContextBase _contexthttpContext;
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseFactory _databaseFactory;
        private User _contextcachedUser;
       

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>

        public FormsAuthenticationService(HttpContextBase httpContext)
        {
            _databaseFactory=new DatabaseFactory();
            _contexthttpContext = httpContext;
            _userRepository = new UserRepository(_databaseFactory);

            _contextexpirationTimeSpan = TimeSpan.FromHours(6);
        }

        #region IFormsAuthenticationService Members

        public virtual void SignIn(User user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                user.Username,
                now,
                now.Add(_contextexpirationTimeSpan),
                createPersistentCookie,
                user.Username,

                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _contexthttpContext.Response.Cookies.Add(cookie);
            _contextcachedUser = user;
        }
        public virtual void SetCategoryId(int Id)
        {


       
   
            _contexthttpContext.Request.Cookies.Remove("Category");
    
            var cookie = new HttpCookie("Category", Id.ToString());
            cookie.HttpOnly = true;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            _contexthttpContext.Request.Cookies.Add(cookie);
           
            
        }
        public virtual void SignOut()
        {
            _contextcachedUser = null;
            _contexthttpContext.Request.Cookies.Remove("Category");
       
            FormsAuthentication.SignOut();
        }

        public virtual User GetAuthenticatedUser()
        {
            if (_contextcachedUser != null)
                return _contextcachedUser;

            if (_contexthttpContext == null ||
                _contexthttpContext.Request == null ||
                !_contexthttpContext.Request.IsAuthenticated ||
                !(_contexthttpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_contexthttpContext.User.Identity;
            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null)
                _contextcachedUser = user;
            return _contextcachedUser;
        }
        public virtual int GetCachedCategorId()
        {

            

           

            if (_contexthttpContext == null ||
                _contexthttpContext.Request == null 
                )
            {
                return 0;
            }
            else
            {


                HttpCookie cookie = _contexthttpContext.Request.Cookies["Category"];
                if (cookie != null) 
                    return Convert.ToInt32(cookie.Value);
            }
  

             return 0;
        }




        #endregion

        public virtual User GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var username = ticket.UserData;

            if (String.IsNullOrWhiteSpace(username))
                return null;
            var customer = _userRepository.GetById(x => x.Username == username);
            return customer;
        }
        
    }
    public interface IFormsAuthenticationService
    {

        void SignIn(User user, bool createPersistentCookie);
        void SignOut();
        User GetAuthenticatedUser();
        int GetCachedCategorId();
      void  SetCategoryId(int Id);
    }
}