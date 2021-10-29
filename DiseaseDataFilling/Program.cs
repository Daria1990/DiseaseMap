using DiseaseDataFilling.DataFilling;
using System;

namespace DiseaseDataFilling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connectionString = GetDatabaseConnectionString();

                var mongoDataFilling = new MongoDataFilling();
                mongoDataFilling.FillDiseaseDb(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Метод получет строку соединения с базой данных
        /// </summary>
        /// <returns>строка подключния к БД</returns>
        static string GetDatabaseConnectionString()
        {
            Console.WriteLine($"Write database connection string, value by default is {ConstantValues.DefaultConnectionString}");
            Console.WriteLine("If you need value by default press ENTER");

            var connectionString = Console.ReadLine();

            if (string.IsNullOrEmpty(connectionString))
            {
                return ConstantValues.DefaultConnectionString;
            }
            else
            {
                return connectionString;
            }
        }
    }
}
