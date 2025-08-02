// Program.cs (новый файл для точки входа)
using System;
using System.Threading.Tasks;

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
            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }

            Console.ReadKey();
        }
    }
}