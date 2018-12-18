using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Web.CustomProviders
{
    public class CustomRoleProvider : RoleProvider
    {

        #region Variables
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseFactory _databaseFactory;

        #endregion

        #region Constructors

        public CustomRoleProvider()
        {
            _databaseFactory = new DatabaseFactory();
            this._userRepository = new UserRepository(_databaseFactory);
        }

        #endregion

        #region Implemented RoleProvider Methods

        public override bool IsUserInRole(string userName, string roleName)
        {
            User user = _userRepository.GetById(x => x.Username == userName);
            if (user == null)
                return false;

            foreach (var i in user.Roles)
            {
                if (i.RoleName == roleName)
                {
                    return true;
                }
            }

            return false;
        }

        public override string[] GetRolesForUser(string userName)
        {
            User user = _userRepository.GetById(x => x.Username == userName);
            if (user.Roles.Count == 0)
                return new string[] {string.Empty};
            var i = 0;

            var roles = user.Roles.Select(x => x.RoleName);

            return roles.ToArray();
        }

        #endregion

        #region Not Implemented RoleProvider Methods

        #region Properties

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}