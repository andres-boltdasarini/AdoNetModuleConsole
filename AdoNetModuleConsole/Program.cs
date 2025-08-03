// Program.cs (новый файл для точки входа)
using System;
using System.Data;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdoNetLib
{
    class Program
    {
        static async Task Main(string[] args) // Асинхронная точка входа
        {
            var connector = new MainConnector();
            var data = new DataTable();

            if (await connector.ConnectAsync())
            {
                Console.WriteLine("Подключено успешно!");
                var db = new DbExecutor(connector);

                var tablename = "NetworkUser";

                Console.WriteLine("Получаем данные таблицы " + tablename);

                data = db.SelectAll(tablename);

                Console.WriteLine("Количество строк в " + tablename + ": " + data.Rows.Count);

                Console.WriteLine("Отключаем БД!");
                connector.DisconnectAsync();
                //if (await connector.ConnectAsync())
                //{
                //    Console.WriteLine("Количество строк в " + tablename + ": " + data.Rows.Count);
                //}
                foreach (DataColumn column in data.Columns)
                {
                    Console.Write($"{column.ColumnName}\t");
                }
                Console.WriteLine();
                foreach (DataRow row in data.Rows)
                {
                    var cells = row.ItemArray;
                    foreach (var cell in cells)
                    {
                        Console.Write($"{cell}\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }

            Console.ReadKey();
        }
    }
}