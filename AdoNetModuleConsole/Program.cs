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

            if (await connector.ConnectAsync())
            {
                Console.WriteLine("Подключено успешно!");
                var db = new DbExecutor(connector);

                var tablename = "NetworkUser";

                Console.WriteLine("Получаем данные таблицы " + tablename);

                var reader = db.SelectAllCommandReader(tablename);

                if (reader != null)
                {
                    var columnList = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        columnList.Add(name);
                    }
                    for (int i = 0; i < columnList.Count; i++)
                    {
                        Console.Write($"{columnList[i]}\t");
                    }
                    Console.WriteLine();
                    while (reader.Read())
                    {
                        for (int i = 0; i < columnList.Count; i++)
                        {
                            var value = reader[columnList[i]];
                            Console.Write($"{value}\t");
                        }

                        Console.WriteLine();
                    }
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