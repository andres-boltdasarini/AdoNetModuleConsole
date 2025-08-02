// MainConnector.cs
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AdoNetLib
{
    public class MainConnector
    {
        private SqlConnection? connection; // Добавляем поле

        public async Task<bool> ConnectAsync()
        {
            try
            {
                connection = new SqlConnection(ConnectionString.MsSqlConnection);
                await connection.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DisconnectAsync() // Заменяем void на Task
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
    }
}