using OtoSanayi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace OtoSanayi.DataAccessLayer
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T:class
    {
        private DbSet<T> _objectSet;
        public Repository()
        {
            _objectSet = context.Set<T>();
        }
        public List<T> List()
        {
            try
            {
                return _objectSet.ToList();
            }
            catch (Exception)
            {

                return new List<T>();
            }

        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            try
            {
                return _objectSet.Where(where).ToList();

            }
            catch (Exception)
            {

                return new List<T>();
            }


        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }

        public int Delete(T obj)
        {

            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            try
            {
                return context.SaveChanges();

            }
            catch (Exception)
            {

                return 0;
            }
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            try
            {
                return _objectSet.FirstOrDefault(where);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void Add(T obj)
        {
            _objectSet.Add(obj);
        }
    }
}
