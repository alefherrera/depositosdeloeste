using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

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
    }
}
