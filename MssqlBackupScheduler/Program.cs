#region
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
#endregion

namespace MssqlBackupScheduler
{
    class Program
    {
        private static readonly string ConnectionString = $"Data Source=.,3423;Integrated Security=True;Connection Timeout={TimeoutSecond}";

        /// <summary>
        /// A folder where all .bak files will be saved
        /// </summary>
        private const string BackupFolder = @"F:\db backup\";

        /// <summary>
        /// Days for valid .bak file. ex) all .bak files which is older than 7 days will be deleted
        /// </summary>
        private const int ValidDay = 7;

        private const int TimeoutSecond = 300;

        static void Main(string[] args)
        {
            var names = GetDatabaseNames();

            foreach (var name in names)
            {
                Console.WriteLine(name);

                Backup(name);

                DeleteOldArchive(name);
            }
        }

        private static void DeleteOldArchive(string name)
        {
            var files = Directory.GetFiles(BackupFolder, $"{name}_*.bak");
            foreach (var file in files)
            {
                var creationTime = File.GetCreationTime(file);
                if (creationTime.AddDays(ValidDay) < DateTime.Today)
                    File.Delete(file);
            }
        }

        private static void Backup(string name)
        {
            // F:\db backup\EastOfCathy_20190326_190642.bak
            string fileName = $"{name}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            var path = Path.Combine(BackupFolder, fileName);

            var commandText = $@"BACKUP DATABASE {name} TO DISK = '{path}'";

            var command = CreateCommand(commandText);
            command.CommandTimeout = TimeoutSecond;
            command.ExecuteNonQuery();
        }

        private static SqlCommand CreateCommand(string commandText)
        {
            var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand(commandText, connection);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return command;
        }

        /// <summary>
        /// Get non-system DB names
        /// </summary>
        /// <returns></returns>
        private static List<string> GetDatabaseNames()
        {
            var commandText = "SELECT [name] FROM master.dbo.sysdatabases WHERE dbid > 4";
            var command = CreateCommand(commandText);

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            var names = new List<string>();
            while (reader.Read())
            {
                names.Add(reader.GetString(0));
            }

            return names;
        }
    }
}