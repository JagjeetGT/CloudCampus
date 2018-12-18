using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModelShredder
{
    public sealed class Shredder
    {
        private readonly IShredderOptionsProvider m_OptionsProvider;
        private readonly IObjectShredder m_ObjectShredder;
        private readonly ISchemaBuilder m_SchemaBuilder;
        
        private ShredderDelegate m_ShredderMethod;
        private ShredderOptions m_ShredderOptions;


        public Shredder(IShredderOptionsProvider provider, IObjectShredder shredder, ISchemaBuilder schemaBuilder)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            if (shredder == null) throw new ArgumentNullException("shredder");
            if (schemaBuilder == null) throw new ArgumentNullException("schemaBuilder");

            m_OptionsProvider = provider;
            m_ObjectShredder = shredder;
            m_SchemaBuilder = schemaBuilder;
        }

        public DataTable Shred(IEnumerable source)
        {
            DataTable table = new DataTable();

            // Begin enumeration
            IEnumerator e = source.GetEnumerator();

            // attempt to pick first element
            if (e.MoveNext())
            {
                Type srctype = e.Current.GetType();

                // reduce overhead of providing options and creating a shredder method
                if (m_ShredderOptions == null)
                {
                    m_ShredderOptions =m_OptionsProvider.ProvideShredderOptions(srctype);
                    m_ShredderMethod = m_ObjectShredder.GetShredderMethod(m_ShredderOptions);  
                }

                // Table must be initialized everytime
                m_SchemaBuilder.BuildTableSchema(table, m_ShredderOptions);

                // Load data into table
                table.BeginLoadData();

                do
                {
                    table.Rows.Add(m_ShredderMethod.Invoke(e.Current));
                } while (e.MoveNext());

                table.EndLoadData();
            }
            else
            {
                if (m_ShredderOptions != null)
                {
                    m_SchemaBuilder.BuildTableSchema(table, m_ShredderOptions);
                }
                else
                {
                    // source is empty, attempt to build schema through the generic IEnumerable implementation
                    Type t = e.GetType();

                    if (t.IsGenericType)
                    {
                        m_ShredderOptions = m_OptionsProvider.ProvideShredderOptions(t);
                        m_SchemaBuilder.BuildTableSchema(table, m_ShredderOptions);
                        return table;
                    }
                }


                // cant build any schema, non generic source and empty
                return null;
            }

            return table;
        }
    }
}
