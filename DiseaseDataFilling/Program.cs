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

        static string GetDatabaseConnectionString()
        {
            Console.WriteLine($"Write database connection string, value by default is {ConstantValues.DEFAULT_CONNECTION_STRING}");
            Console.WriteLine("If you need value by default press ENTER");

            var connectionString = Console.ReadLine();

            if (string.IsNullOrEmpty(connectionString))
            {
                return ConstantValues.DEFAULT_CONNECTION_STRING;
            }
            else
            {
                return connectionString;
            }
        }
    }
}
