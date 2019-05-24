using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace database
{
    class Program
    {
        static string connectionString = "server=127.0.0.1;user id=root;password=123456;port=3306;database=test;";
        
        static void Main(string[] args)
        {
            ConnectDatabase();

            var personContext = new PersonContext();
            List<Person> persons = personContext.Person.ToList();
            persons.ForEach(person => Console.WriteLine(person.Name));
        }

        private static void ConnectDatabase()
        {
            using (var connection =
                new MySqlConnection(connectionString))
            using (var mySqlCommand = connection.CreateCommand())
            {
                connection.Open();
                mySqlCommand.CommandText = "select * from person";

                var mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Console.WriteLine(mySqlDataReader.GetString("name"));
                }
            }
        }
    }

    public class PersonContext : DbContext
    {
        static string connectionString = "server=127.0.0.1;user id=root;password=123456;port=3306;database=test;";
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);
        }
        
        public DbSet<Person> Person { get; set; }
        
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
            
    }
}
