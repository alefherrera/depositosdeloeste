using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using System.Collections;
using NHibernate.Transform;
using System.Data;

namespace BackEnd
{
    public class BusinessObject<T>
    {

        public virtual bool Loaded { get; set; }

        public BusinessObject()
        {
            ResetearParametros(this);
        }

        protected void ResetearParametros(object o)
        {
            foreach (System.Reflection.PropertyInfo item in o.GetType().GetProperties())
            {
                if (Dic.ContainsKey(item.PropertyType))
                {
                    item.SetValue(o, Dic[item.PropertyType]);
                }
            }
        }

        private static Dictionary<System.Type, object> Dic = new Dictionary<Type, object> {
        {typeof(string),string.Empty},
        {typeof(int),-1},
        {typeof(Int64),-1},
        {typeof(DateTime),Convert.ToDateTime("1900-01-01")},
        {typeof(bool),false}
        };

 
        public class Parameter
        {
            public Parameter(string name, object value, Type type)
            {
                Name = name;
                Value = value;
                Tipo = type;
            }
            public string Name { get; set; }
            public object Value { get; set; }
            public Type Tipo { get; set; }
        }


        public virtual List<T> Select()
        {
            List<Parameter> param = new List<Parameter>();
            foreach (System.Reflection.PropertyInfo item in this.GetType().GetProperties())
            {
                var valor = item.GetValue(this);

                if (valor != null && Dic.ContainsKey(valor.GetType()) && valor.ToString() != Dic[valor.GetType()].ToString())
                {
                    param.Add(new Parameter(item.Name, valor, valor.GetType()));
                }
            }
            return this.Select(param.ToArray());
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

        public virtual void BajaLogica()
        {
            this.GetType().GetProperty("Activo").SetValue(this, false);
            Update();
        }

        public virtual void Update()
        {
            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();
            ITransaction tx = session.BeginTransaction();

            session.Update(this);

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

        public virtual List<T> Select(bool a)
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

        protected virtual List<T> Select(params Parameter[] parametros)
        {
            List<T> rtnList = new List<T>();

            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();

            ICriteria criteria = session.CreateCriteria(typeof(T));
            foreach (Parameter item in parametros)
            {
                criteria.Add(Restrictions.Eq(item.Name, item.Value));
            }

            rtnList = (List<T>)criteria.List<T>();
            
            session.Close();
            return rtnList;
        }

        public virtual DataTable Select(string query)
        {
            List<T> rtnList = new List<T>();

            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();
            IQuery squery = session.CreateQuery(query);
            
            var listResult = squery.SetResultTransformer(Transformers.AliasToEntityMap).List<Hashtable>();
            session.Close();

            DataTable datatable = new DataTable();
            if (listResult.Count > 0)
            {
                Hashtable htable = listResult[0];
                foreach (DictionaryEntry entry in htable)
                {
                    datatable.Columns.Add(entry.Key.ToString());
                }

            }
            foreach (Hashtable htable in listResult)
            {
                DataRow row = datatable.NewRow();
                foreach (DictionaryEntry entry in htable)
                {
                    row[entry.Key.ToString()] = entry.Value;
                }
                datatable.Rows.Add(row);
            }

            return datatable;
        }

        public virtual bool Load()
        {
            try
            {
                var obj = this.Select()[0];
                foreach (System.Reflection.PropertyInfo item in this.GetType().GetProperties())
                {
                    item.SetValue(this, obj.GetType().GetProperty(item.Name).GetValue(obj));
                }
                Loaded = true;
            }
            catch
            {
                Loaded = false;
            }
            return Loaded;
        }
    }
}
