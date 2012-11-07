using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Utils;
using System.Data;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Transform;
using System.Collections;

namespace BackEnd
{
    public class Compartimiento : BusinessObject<Compartimiento>
    {
        public virtual int Id { get; set; }
        public virtual int NroEstanteria { get; set; }
        public virtual int Nivel { get; set; }
        public virtual int NroCompartimiento { get; set; }
        public virtual int Actividad { get; set; }
        public virtual int Estado { get; set; }
        public virtual int IdArticulo { get; set; }
        public virtual int Cantidad { get; set; }
        public virtual int Cantidad_Guardar { get; set; }
        public virtual int cantidad_maxima(Articulo articulo)
        {
            //TODO: Hacer el enum de esto y deshardcodear esta negrada
            int peso_max = 0;
            if (Nivel == 1)
                peso_max = 1200000;
            else
                peso_max = 700000;

            List<int> art_dimensiones = new List<int>();
            art_dimensiones.Add(articulo.Largo);
            art_dimensiones.Add(articulo.Ancho);
            art_dimensiones.Add(articulo.Alto);

            int cantidadTotal = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int h = 0; h < 3; h++)
                    {
                        if (!(i == j || i == h || j == h))
                        {
                            int cantidadTemp = (int)Enums.Pallet.Largo / art_dimensiones[h] * (int)Enums.Pallet.Ancho / art_dimensiones[j] * (int)Enums.Pallet.Alto / art_dimensiones[i];
                            if (cantidadTemp > cantidadTotal)
                                cantidadTotal = cantidadTemp;
                        }
                    }
                }
            }

            int peso_total = cantidadTotal * articulo.Peso;
            if (peso_total > peso_max)
                cantidadTotal = peso_max / articulo.Peso;

            return cantidadTotal;
        }
        public virtual DataTable Select_Detalles(int cliente)
        {
            List<Compartimiento> rtnList = new List<Compartimiento>();

            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory factory = config.BuildSessionFactory();
            ISession session = factory.OpenSession();
            IQuery squery = session.CreateSQLQuery("call compartimientos_reservas(" + this.Id + ", " + this.NroEstanteria + ", " + cliente + ", " + this.Estado + ")");
            
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

    }
}

