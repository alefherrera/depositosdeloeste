using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace BackEnd
{
    public class Base
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
    }
}
