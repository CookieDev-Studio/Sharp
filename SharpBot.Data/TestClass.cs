using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

public class TestClass
{
    public static void TestDB()
    {
        using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=Frost bite7;Database=sharp"))
        {
            connection.Open();
            var values = connection.Query<Strike>("Select * from strikes").ToList();
            Console.WriteLine(values.First().userId);
        }
    }
}
