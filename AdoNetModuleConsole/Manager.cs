using AdoNetLib;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

public class Manager 
{
  private MainConnector connector;
  private DbExecutor dbExecutor;
  private Table userTable;

public Manager() 
{
  connector = new MainConnector();

  userTable = new Table();
  userTable.Name = "NetworkUser";
  userTable.ImportantField = "Login";
  userTable.Fields.Add("Id");
  userTable.Fields.Add("Login");
  userTable.Fields.Add("Name");

}

  public void Connect() 
  {
    var result = connector.ConnectAsync();

    if (result.Result) 
    {
      Console.WriteLine("Подключено успешно!");

      dbExecutor = new DbExecutor(connector);
    } 
    else 
    {
      Console.WriteLine("Ошибка подключения!");
    }
  }

  public void Disconnect() 
  {
    Console.WriteLine("Отключаем БД!");
    connector.DisconnectAsync();
  }

public void ShowData() 
{
    var tablename = "NetworkUser";

    Console.WriteLine("Получаем данные таблицы " + tablename);

    using (var reader = dbExecutor.SelectAllCommandReader(tablename))
    {
        if (reader == null)
        {
            Console.WriteLine("Не удалось получить данные или таблица пуста");
            return;
        }

        // Выводим названия столбцов
        for (int i = 0; i < reader.FieldCount; i++)
        {
            Console.Write($"{reader.GetName(i)}\t");
        }
        Console.WriteLine();

        // Выводим данные строк
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader[i]}\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}

   public int DeleteUserByLogin(string value) 
 {
   return dbExecutor.DeleteByColumn(userTable.Name, userTable.ImportantField, value);
 }


   public int UpdateUserByLogin(string value, string newvalue) {
  return dbExecutor.UpdateByColumn(userTable.Name, userTable.ImportantField, value, userTable.Fields[2], newvalue);
}

  public void AddUser(string name, string login) 
 {
   dbExecutor.ExecProcedureAdding(name, login);
 }






}