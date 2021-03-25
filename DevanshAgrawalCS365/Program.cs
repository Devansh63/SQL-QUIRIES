using System;
using DevanshAgrawalCS365;
using System.Data.SqlClient;

namespace DevanshAgrawalCS365
{
    class Program
    {
        static void Main(string[] args)
        {
            Mainmenu cmd = new Mainmenu(CreateConnectionString());
            int inputOptionNumber = 0;

            do
            {
                cmd.showCommands();
                inputOptionNumber = Convert.ToInt32(takeUserInput("\n>> "));
                Console.WriteLine();
                cmd.switchAndSelectCommand(inputOptionNumber);           
            } while (inputOptionNumber != 10);

        }
        public static string takeUserInput(string _question)
        {
            Console.Write(_question);
            return Console.ReadLine();
        }
        public static string CreateConnectionString()
        {
            string question = "What is the name of the database you are trying to connect? ";
            string databaseName = takeUserInput(question);
            string connString = $"Server=DESKTOP-OLAGE94;Database={databaseName};Trusted_Connection=True;";
            return connString;
        }
    }
}
 