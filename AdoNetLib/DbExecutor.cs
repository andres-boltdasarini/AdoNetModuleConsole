using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AdoNetLib
{
    public class DbExecutor
    {
        private MainConnector connector;
        public DbExecutor(MainConnector connector)
        {
            this.connector = connector;
        }


        public SqlDataReader SelectAllCommandReader(string table)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "select * from " + table,
                Connection = connector.GetConnection(),
            };

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return reader;
            }

            return null;
        }

        public int DeleteByColumn(string table, string column, string value) 
{
  var command = new SqlCommand 
  {
    CommandType = CommandType.Text,
      CommandText = "delete from " + table + " where " + column + " = '" + value + "';",
      Connection = connector.GetConnection(),
  };

  return command.ExecuteNonQuery();

}

        public int AddUser(string tableName, string name, string login)
        {
            using var command = new SqlCommand(
                $"INSERT INTO {tableName} (Name, Login) VALUES (@name, @login)",
                connector.GetConnection()
            );

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@login", login);

            return command.ExecuteNonQuery();
        }

        public int UpdateByColumn(string table, string columntocheck, string valuecheck, string columntoupdate, string valueupdate) 
{
  var command = new SqlCommand 
  {
    CommandType = CommandType.Text,
      CommandText = "update   " + table + " set " + columntoupdate + " = '" + valueupdate + "'  where " + columntocheck + " = '" + valuecheck + "';",
      Connection = connector.GetConnection(),
  };

  return command.ExecuteNonQuery();

}


    }
}
