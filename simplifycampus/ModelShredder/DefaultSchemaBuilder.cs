using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using ModelShredder.Extensions;

namespace ModelShredder
{
    public sealed class DefaultSchemaBuilder : ISchemaBuilder
    {
        /// <summary>
        /// Adds columns to an empty DataTable that map to properties and fields on a Type.
        /// </summary>
        /// <param name="table">The empty table to which to add columns.</param>
        /// <param name="options">Controls the visible members and their order in the schema.</param>
        /// <returns><see cref="ShredderOptions"/> which indicate which Fields and Properties were mapped (in order).</returns>
        public void BuildTableSchema(DataTable table, ShredderOptions options)
        {
            foreach (MemberInfo member in options.Members)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = member.Name;

                // Member info is either PropertyInfo or FieldInfo, PropertyInfo is more likely.
                PropertyInfo p = member as PropertyInfo;
                if (p != null)
                {
                    if (p.PropertyType.IsNullable())
                    {
                        dc.DataType = p.PropertyType.GetGenericArguments()[0];
                        dc.AllowDBNull = true;
                    }
                    else
                    {
                        dc.DataType = p.PropertyType;
                    }
                }
                else
                {
                    FieldInfo f = (FieldInfo)member; // makes sure exception is thrown when cast is invalid. Should never happen.
                    if (f.FieldType.IsNullable())
                    {
                        dc.DataType = f.FieldType.GetGenericArguments()[0];
                        dc.AllowDBNull = true;
                    }
                    else
                    {
                        dc.DataType = f.FieldType;
                    }
                }

                // Add column to table
                table.Columns.Add(dc);
            }
        }
    }
}