using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace BackEnd
{
    public class BusinessObject<T>
    {
        public virtual void TestMethod()
        {
            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();

            ITransaction tx = session.BeginTransaction();
            Menu d = new Menu();
            d.Id = 1;
            d.IdPadre = 0;
            d.Link = "/Default.aspx";
            d.Nombre = "HOME";
            session.Save(d);
            tx.Commit();

            session.Close();
        }

        public virtual void Save()
        {
            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();
            ITransaction tx = session.BeginTransaction();

            session.Save(this);

            tx.Commit();

            session.Close();
        }

        public virtual void Delete()
        {
            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();
            ITransaction tx = session.BeginTransaction();

            session.Delete(this);

            tx.Commit();
            session.Close();
        }



        public virtual T Clone()
        {
            return this.Clone();
        }

        public virtual List<T> Select()
        {
            List<T> rtnList = new List<T>();

            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();
            
            rtnList = (List<T>)session.CreateCriteria(typeof(T)).List<T>();

            session.Close();
            return rtnList;
        }

        public virtual List<T> Select(List<List<string>> lista)
        {
            List<T> rtnList = new List<T>();

            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();

            ICriteria criteria = session.CreateCriteria(typeof(T));
            foreach (List<string> item in lista)
            {
                item.ToArray();
                criteria.Add(Restrictions.Eq(item[0],item[1]));
            }

            rtnList = (List<T>)criteria.List<T>();

            session.Close();
            return rtnList;
        }

        public virtual List<T> Select(string query)
        {
            List<T> rtnList = new List<T>();

            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();
            IQuery squery = session.CreateQuery(query);

            rtnList = (List<T>)squery.List<T>();

            session.Close();
            return rtnList;
        }

    }
}
