using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace KRBAccounting.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {

        T Get(Expression<Func<T, bool>> where);
        bool DeleteException(Expression<Func<T, bool>> where);
        bool DeleteException(T entity);
        void Add(T entity);
        void AddToAddedStated(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id, string includeProperties = "");
        T GetById(string id, string includeProperties = "");
        T GetById(Expression<Func<T, bool>> predicate, string includeProperties = "");
        T GetById(int id, string includeProperties = "");
        IEnumerable<T> GetAll(string includeProperties = "");
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includeProperties = "");
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate, string includeProperties = "");
        IQueryable<T> GetPaged(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderBy, int page, int pageSize, string includeProperties = "");
        IQueryable<T> GetPaged(Expression<Func<T, string>> orderBy, int page, int pageSize, string includeProperties = "");
        PagedList<T> GetPagedList(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderBy, int pageIndex, int pageSize, string includeProperties = "");
        PagedList<T> GetPagedList(Expression<Func<T, string>> orderBy, int pageIndex, int pageSize, string includeProperties = "");
        IQueryable<T> GetPaged(Expression<Func<T, bool>> where, Expression<Func<T, int>> orderBy, int page, int pageSize, string includeProperties = "");
        IQueryable<T> GetPaged(Expression<Func<T, int>> orderBy, int page, int pageSize, string includeProperties = "");
        PagedList<T> GetPagedList(Expression<Func<T, bool>> where, Expression<Func<T, int>> orderBy, int pageIndex, int pageSize, string includeProperties = "");
        PagedList<T> GetPagedList(Expression<Func<T, int>> orderBy, int pageIndex, int pageSize, string includeProperties = "");
        int Count(Expression<Func<T, bool>> predicate);
        int Count();
        IQueryable<T> GetExpandable(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
