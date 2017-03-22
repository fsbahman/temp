using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteSql
{
	class Program
	{
		static void Main(string[] args)
		{
			var connection = new SqlConnection("Data Source=.;Initial Catalog=629409_Customer_01;Integrated Security=True");
			GetSchemaInfo(connection);
		}

		static void GetSchemaInfo(SqlConnection connection)
		{
			using (connection)
			{
				SqlCommand command = new SqlCommand(
				  "SELECT * FROM EventMessages;",
				  connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				DataTable schemaTable = reader.GetSchemaTable();
				//Console.WriteLine(schemaTable.Columns["ID"].Ordinal); 

				foreach (DataRow row in schemaTable.Rows)
				{
					foreach (DataColumn column in schemaTable.Columns)
					{
						Console.WriteLine(String.Format("{0} = {1}",
						   column.ColumnName, row[column]));
					}
				}
			}
		}
	}
}
