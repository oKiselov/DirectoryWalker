using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DirectoryWalker.Database.Helpers
{
    public class DataTableHelper
    {
        public static DataTable PrepareDataTableParameter(IList<string> pathes)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new[]{
                new DataColumn("Id", typeof(int)),
                new DataColumn("Path", typeof(string))});

            for (int i = 0; i < pathes.Count(); i++)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = i + 1;
                row["Path"] = pathes[i];
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
