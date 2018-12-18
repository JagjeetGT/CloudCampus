using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Service.Helpers;


namespace KRBAccounting.Service
{
    public interface ISecurityService
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User GetUser(string userName);
        User CheckLogin(string userName, string password, int branchId);
        void CreateUser(User user);
        void DeleteUser(string userName);
        void DeleteUser(int id);
        User UpdateUser(User user);
        User GetUserByName(string username);
        IEnumerable<Role> Roles();
        Role GetRole(int role);
        void CreateRole(Role role);
        void DeleteRole(int role);
        bool IsUserNameAvailable(string name);
        void Save();
        void AssignRole(int id, List<int> roleNames);
        void EditRole(Role role);
        bool IsRoleNameAvailable(string name);
        IEnumerable<User> GetUsersByCompanyId(int companyId);
       bool ChangePasswrod(User user);
    }
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._unitOfWork = unitOfWork;


        }
        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetAll();
            return users;
        }
        public bool ChangePasswrod(User user)
        {

            var result = this.UpdateUser(user);
            return result != null;
        }
        public IEnumerable<User> GetUsersByCompanyId(int companyId)
        {
            var users = _userRepository.GetMany(x => x.CompanyId == companyId && !x.IsDeleted).OrderBy(a => a.Username);
            return users;
        }

        /// <summary>
        /// Get User by User Name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUser(string userName)
        {
            var user = _userRepository.GetById(userName);
            return user;
        }
        public User GetUserByName(string username)
        {
            var user = _userRepository.GetById(x => x.Username == username);
            return user;
        }
        public User GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return user;
        }

        public User CheckLogin(string userName, string password,int branchId)
        {
            var user = _userRepository.GetById(x => x.Username == userName && x.IsActive&&!x.IsDeleted);
            if (user == null)
                return null;
            if (user.AllBranch)
            {
                if (!PasswordHelper.ValidatePassword(password, user.Password))
                    return null;
            }
            else
            {
                user = new User();
                 user =   _userRepository.GetById(
                        x => x.Username == userName && x.IsActive && !x.IsDeleted && x.BranchId == branchId);
                if (user == null)
                    return null;
                if (!PasswordHelper.ValidatePassword(password, user.Password))
                    return null;
            }
            return user;
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(string userName)
        {
            var user = GetUser(userName);
            _userRepository.Delete(user);
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
                _userRepository.Delete(user);
        }

        public User UpdateUser(User user)
        {
            _userRepository.Update(user);
            return user;
        }

        public IEnumerable<Role> Roles()
        {
            var roles = _roleRepository.GetAll();
            return roles;
        }

        public Role GetRole(int role)
        {
            var roleModel = _roleRepository.GetById(role);
            return roleModel;
        }

        public void CreateRole(Role role)
        {
            _roleRepository.Add(role);
            Save();
        }
        public void EditRole(Role role)
        {
            _roleRepository.Update(role);
            Save();
        }
        public void DeleteRole(int role)
        {
            var model = GetRole(role);
            _roleRepository.Delete(model);

        }
        public bool IsRoleNameAvailable(string roleName)
        {
            var data = _roleRepository.GetMany(x => x.RoleName == roleName).Any();
            return !data;
        }
        public bool IsUserNameAvailable(string name)
        {
            var user = _userRepository.GetMany(x => x.Username == name).Any();
            return !user;
        }

        public void AssignRole(int id, List<int> roleNames)
        {
            _userRepository.AssignRole(id, roleNames);

        }

        public void Save()
        {

            _unitOfWork.Commit();
        }
    }
}
