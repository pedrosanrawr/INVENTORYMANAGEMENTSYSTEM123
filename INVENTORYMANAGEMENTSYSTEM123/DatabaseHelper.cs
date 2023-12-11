using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

public static class DatabaseHelper
{
    private const string ConnectionString = "Data Source=inventorysystemdb.db;Version=3;";

    public static void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                command.ExecuteNonQuery();
            }
        }
    }


    public static DataTable ExecuteQuery(string query)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}