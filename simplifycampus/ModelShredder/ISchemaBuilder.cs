using System;
using System.Data;

namespace ModelShredder
{
    public interface ISchemaBuilder
    {
        /// <summary>
        /// Adds columns to an empty DataTable that map to properties and fields on a Type.
        /// </summary>
        /// <param name="table">The empty table to which to add columns.</param>
        /// <param name="options">Controls the visible members and their order in the schema.</param>
        void BuildTableSchema(DataTable table, ShredderOptions options);
    }
}