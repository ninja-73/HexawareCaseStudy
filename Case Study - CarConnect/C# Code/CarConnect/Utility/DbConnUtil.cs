using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;
using static CarConnect.Exceptions.CustomExceptions;

namespace CarConnect.Utility
{
    internal class DbConnUtil
    {
        private static IConfiguration _iconfiguration;

        static DbConnUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json");
                _iconfiguration = builder.Build();
            }
            catch (FileNotFoundException ex)
            {
                throw new DatabaseConnectionException("The appsettings.json file was not found.", ex);
            }
            catch (JsonReaderException ex)
            {
                throw new DatabaseConnectionException("Error reading the appsettings.json file.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("An unexpected error occurred while loading the configuration.", ex);
            }
        }

        public static string GetConnString()
        {
            try
            {
                string connString = _iconfiguration.GetConnectionString("LocalConnectionString");
                return connString;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Could not connect to the database. Please check your connection settings.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseConnectionException("An unexpected error occurred while trying to connect to the database.", ex);
            }
        }
    }
}
