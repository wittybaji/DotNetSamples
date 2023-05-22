using System.Data;

namespace ExcelDataReaderTest
{
    public static class DataSetExtensions
    {
        public static DataSet ToAllStringFields(this DataSet ds)
        {
            var newDs = ds.Clone();
            foreach (DataTable table in newDs.Tables)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (col.DataType != typeof(string))
                        col.DataType = typeof(string);
                }
            }

            foreach (DataTable table in ds.Tables)
            {
                var targetTable = newDs.Tables[table.TableName];
                foreach (DataRow row in table.Rows)
                {
                    targetTable.ImportRow(row);
                }
            }

            return newDs;
        }
    }
}
