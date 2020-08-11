using System.Data;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.IO
{
    public static class DataTableCreator
    {
        public static DataTable Create(TextAsset text, string tableName)
        {
            DataTable table = new DataTable(tableName);

            string[] lines = text.text.Split('\n');
            string[] columnNames = lines[0].Split(',');

            for (int i = 0; i < columnNames.Length; i++)
            {
                DataColumn column = new DataColumn(columnNames[i].TrimEnd('\r'));
                table.Columns.Add(column);
            }
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] == string.Empty)
                {
                    continue;
                }
                string[] parts = lines[i].Split(',');
                DataRow row = table.NewRow();
                for (int j = 0; j < parts.Length; j++)
                {
                    row[j] = parts[j];
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }

}