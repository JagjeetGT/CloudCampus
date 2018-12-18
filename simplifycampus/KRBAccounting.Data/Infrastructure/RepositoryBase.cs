using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.EntityClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Pager;
using LinqKit;

namespace KRBAccounting.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private DataContext _dataContext;
        private readonly IDbSet<T> _dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
          
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected DataContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }

        public void AddToAddedStated(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Add(T entity)
        {
            try
            {
                _dbset.Add(entity);
                _dataContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                                               validationError.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public virtual void Update(T entity)
        {
            try
            {
                _dbset.Attach(entity);
                _dataContext.Entry(entity).State = EntityState.Modified;
                _dataContext.SaveChanges();
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                                               validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
            _dataContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
            _dataContext.SaveChanges();
        }

        public virtual T GetById(long id, string includeProperties = "")
        {
            return _dbset.Find(id);
        }

        public virtual T GetById(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.FirstOrDefault(predicate);
        }

        public virtual T GetById(string id, string includeProperties = "")
        {
            return _dbset.Find(id);
        }

        public virtual T GetById(int id, string includeProperties = "")
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll(string includeProperties = "")
        {
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includeProperties = "")
        {
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.Where(where);
        }

        //Return only the element list, not paged
        public virtual IQueryable<T> GetPaged(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var skip = pageSize * (pageIndex - 1);
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.Where(where).OrderBy(orderBy).Skip(skip).Take(pageSize).AsQueryable<T>();
        }

        public virtual IQueryable<T> GetPaged(Expression<Func<T, string>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var skip = pageSize * (pageIndex - 1);
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.OrderBy(orderBy).Skip(skip).Take(pageSize).AsQueryable<T>();
        }

        //Return paged list
        public virtual PagedList<T> GetPagedList(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            var items = this.GetPaged(where, orderBy, pageIndex, pageSize, includeProperties);
            var totalItemCount = this.Count();
            return new PagedList<T>(items, pageIndex, pageSize, totalItemCount, true);
        }

        public virtual PagedList<T> GetPagedList(Expression<Func<T, string>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            var items = this.GetPaged(orderBy, pageIndex, pageSize, includeProperties);
            var totalItemCount = this.Count();
            return new PagedList<T>(items, pageIndex, pageSize, totalItemCount, true);
        }
        
        public virtual IQueryable<T> GetPaged(Expression<Func<T, bool>> where, Expression<Func<T, int>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var skip = pageSize * (pageIndex - 1);
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.Where(where).OrderBy(orderBy).Skip(skip).Take(pageSize).AsQueryable<T>();
        }
       
        public virtual IQueryable<T> GetPaged(Expression<Func<T, int>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var skip = pageSize * (pageIndex - 1);
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.OrderBy(orderBy).Skip(skip).Take(pageSize).AsQueryable<T>();
        }

        public virtual PagedList<T> GetPagedList(Expression<Func<T, bool>> where, Expression<Func<T, int>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            var items = this.GetPaged(where, orderBy, pageIndex, pageSize, includeProperties);
            var totalItemCount = this.Count();
            return new PagedList<T>(items, pageIndex, pageSize, totalItemCount, true);
        }

        public virtual PagedList<T> GetPagedList(Expression<Func<T, int>> orderBy, int pageIndex, int pageSize, string includeProperties = "")
        {
            var items = this.GetPaged(orderBy, pageIndex, pageSize, includeProperties);
            var totalItemCount = this.Count();
            return new PagedList<T>(items, pageIndex, pageSize, totalItemCount, true);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = GetIncludeQuery(includeProperties);
            return query.Where(predicate).AsQueryable<T>();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).Count();
        }

        public virtual int Count()
        {
            return _dbset.Count();
        }

        public virtual IQueryable<T> GetExpandable(Expression<Func<T, bool>> predicate)
        {
            return _dbset.AsExpandable().Where(predicate).AsQueryable<T>();
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public IQueryable<T> GetIncludeQuery(string includeProperties)
        {
            IQueryable<T> query = _dbset;
            if (!String.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }
       
        public virtual bool DeleteException(Expression<Func<T, bool>> where)
        {
            try
            {
                var objects = _dbset.Where<T>(where).AsEnumerable();
                foreach (T obj in objects)
                    _dbset.Remove(obj);
                _dataContext.SaveChanges();
                return true;
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                return false;
            }
        }
       
        public virtual bool DeleteException(T entity)
        {
            try
            {
                _dbset.Remove(entity);
                _dataContext.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                return false;
            }

        }


    }
}
