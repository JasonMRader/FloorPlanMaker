using Dapper;
using FloorplanClassLibrary;

namespace FloorPlanMaker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            SqliteDataAccess.CheckAndSetDatabaseLocation();
            SqliteDataAccess.BackupDatabase();
            SqliteDataAccess.DeleteOldBackups();
            
            Application.Run(new Form1 ());
        }
    }
}