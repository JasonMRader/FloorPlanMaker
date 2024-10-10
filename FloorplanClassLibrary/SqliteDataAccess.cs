using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Serialization;

namespace FloorplanClassLibrary
{

    public class SqliteDataAccess
    {

        //private static string LoadConnectionString(string id = "Default")
        //{
        //    return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        //}
        private static string LoadConnectionString(string id = "Default")
        {
            var connectionString = ConfigurationManager.ConnectionStrings[id].ConnectionString;
            var dbPath = ConfigurationManager.AppSettings["DatabasePath"];

            // Assuming your database file has a fixed name, e.g., "FloorplanMakerDB.db"
            string dbFileName = "FloorplanMakerDB.db";

            // Construct the full path to the database file
            string fullPath = string.IsNullOrEmpty(dbPath)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName) // Default location
                : Path.Combine(dbPath, dbFileName);

            connectionString = connectionString.Replace(".\\FloorplanMakerDB.DB", fullPath);

            return connectionString;
        }
        public static void SaveTableStat(List<TableStat> tableStats)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                cnn.Open();

                using (var transaction = cnn.BeginTransaction()) {
                    foreach (TableStat tableStat in tableStats) {
                        // 1. Check for existing TableStat record
                        string checkSql = @"SELECT COUNT(*) FROM TableStats 
                                    WHERE TableStatNumber = @TableStatNumber 
                                    AND IsLunch = @IsLunch 
                                    AND Date = @Date";
                        int count = cnn.Query<int>(checkSql, new {
                            TableStatNumber = tableStat.TableStatNumber,
                            IsLunch = tableStat.IsLunch,
                            Date = tableStat.Date.ToString("yyyy-MM-dd")
                        }, transaction: transaction).Single();

                        if (count == 0) {
                            // 1.1 Insert new TableStat record
                            var insertTableStatSql = @"INSERT INTO TableStats 
                                               (TableStatNumber, DayOfWeek, Date, IsLunch, Sales, Orders, DiningAreaID) 
                                               VALUES 
                                               (@TableStatNumber, @DayOfWeek, @Date, @IsLunch, @Sales, @Orders, @DiningAreaID)";
                            cnn.Execute(insertTableStatSql, new {
                                TableStatNumber = tableStat.TableStatNumber,
                                DayOfWeek = tableStat.DayOfWeek.ToString(),
                                Date = tableStat.Date.ToString("yyyy-MM-dd"),
                                IsLunch = tableStat.IsLunch,
                                Sales = tableStat.Sales,
                                Orders = tableStat.Orders,
                                DiningAreaID = tableStat.DiningAreaID
                            }, transaction: transaction);
                        }

                        // 2. Calculate total sales for the DiningAreaID, IsLunch, and Date
                        var totalSales = cnn.Query<float>(@"SELECT SUM(Sales) FROM TableStats 
                                                    WHERE DiningAreaID = @DiningAreaID 
                                                    AND IsLunch = @IsLunch 
                                                    AND Date = @Date",
                           new {
                               DiningAreaID = tableStat.DiningAreaID,
                               IsLunch = tableStat.IsLunch,
                               Date = tableStat.Date.ToString("yyyy-MM-dd")
                           }, transaction: transaction).Single();

                        // 3. Get the ServerCount from Floorplan, or set it to 0 if no matching floorplan exists
                        var serverCount = cnn.Query<int?>(@"SELECT ServerCount FROM Floorplan 
                                                    WHERE DiningAreaID = @DiningAreaID 
                                                    AND IsLunch = @IsLunch 
                                                    AND Date = @Date",
                           new {
                               DiningAreaID = tableStat.DiningAreaID,
                               IsLunch = tableStat.IsLunch,
                               Date = tableStat.Date.ToString("yyyy-MM-dd")
                           }, transaction: transaction).FirstOrDefault() ?? 0;

                        // 4. Check if DiningAreaRecord exists for the Date, IsLunch, and DiningAreaID
                        var diningAreaRecordID = cnn.Query<int?>(@"SELECT ID FROM DiningAreaRecord 
                                                           WHERE DiningAreaID = @DiningAreaID 
                                                           AND IsAm = @IsLunch 
                                                           AND Date = @Date",
                           new {
                               DiningAreaID = tableStat.DiningAreaID,
                               IsLunch = tableStat.IsLunch,
                               Date = tableStat.Date.ToString("yyyy-MM-dd")
                           }, transaction: transaction).FirstOrDefault();

                        if (diningAreaRecordID == null) {
                            try {
                                // 4.1 Insert new DiningAreaRecord
                                var insertDiningAreaSql = @"INSERT INTO DiningAreaRecord 
                                                    (DiningAreaID, Date, IsAm, Sales, ServerCount, PercentageOfSales) 
                                                    VALUES 
                                                    (@DiningAreaID, @Date, @IsLunch, @Sales, @ServerCount, 0.0)";
                                cnn.Execute(insertDiningAreaSql, new {
                                    DiningAreaID = tableStat.DiningAreaID,
                                    Date = tableStat.Date.ToString("yyyy-MM-dd"),
                                    IsLunch = tableStat.IsLunch,
                                    Sales = totalSales,
                                    ServerCount = serverCount
                                }, transaction: transaction);

                                // 4.2 Retrieve the new DiningAreaRecordID
                                diningAreaRecordID = cnn.Query<int>("SELECT last_insert_rowid()", transaction: transaction).Single();
                            }
                            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint) {
                                // Handle uniqueness constraint violation
                                // Another process might have inserted the record after the initial check
                                // Retrieve the existing DiningAreaRecordID
                                diningAreaRecordID = cnn.Query<int?>(@"SELECT ID FROM DiningAreaRecord 
                                                               WHERE DiningAreaID = @DiningAreaID 
                                                               AND IsAm = @IsLunch 
                                                               AND Date = @Date",
                                   new {
                                       DiningAreaID = tableStat.DiningAreaID,
                                       IsLunch = tableStat.IsLunch,
                                       Date = tableStat.Date.ToString("yyyy-MM-dd")
                                   }, transaction: transaction).FirstOrDefault();

                                if (diningAreaRecordID == null) {
                                    // If still null, rethrow the exception
                                    throw;
                                }
                            }
                        }
                        else {
                            // 4.3 Update the existing DiningAreaRecord
                            var updateDiningAreaSql = @"UPDATE DiningAreaRecord 
                                                SET Sales = @Sales, ServerCount = @ServerCount 
                                                WHERE ID = @DiningAreaRecordID";
                            cnn.Execute(updateDiningAreaSql, new {
                                Sales = totalSales,
                                ServerCount = serverCount,
                                DiningAreaRecordID = diningAreaRecordID
                            }, transaction: transaction);
                        }

                        // 5. Update the TableStat record with the DiningAreaRecordID
                        var updateTableStatSql = @"UPDATE TableStats 
                                           SET DiningAreaRecordID = @DiningAreaRecordID 
                                           WHERE TableStatNumber = @TableStatNumber 
                                           AND IsLunch = @IsLunch 
                                           AND Date = @Date";
                        cnn.Execute(updateTableStatSql, new {
                            DiningAreaRecordID = diningAreaRecordID,
                            TableStatNumber = tableStat.TableStatNumber,
                            IsLunch = tableStat.IsLunch,
                            Date = tableStat.Date.ToString("yyyy-MM-dd")
                        }, transaction: transaction);
                    }

                    transaction.Commit();
                }
            }
        }


        public static int? GetDiningAreaIDByTableNumber(string tableNumber)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = @"SELECT DiningAreaID FROM DiningTableRecord WHERE TableNumber = @TableNumber";
                return cnn.Query<int?>(query, new { TableNumber = tableNumber }).FirstOrDefault();
            }
        }
        public static List<Table> GetCountedTablesForDiningArea(DiningArea diningArea)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var tables = cnn.Query<Table>("SELECT * FROM DiningTableRecord WHERE DiningAreaID = @ID AND IsIncluded = 1",
                    new { ID = diningArea.ID, }).ToList();

                foreach (var table in tables)
                {
                    if (diningArea != null)
                    {
                        table.DiningArea = diningArea;
                    }
                }

                return tables;
            }
        }
        public static void SaveTablesCounted(List<Table> tableRecords)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                cnn.Open();
                foreach (var table in tableRecords) {
                    // Log the table information for debugging
                    Console.WriteLine($"Processing Table: {table.TableNumber} with IsIncluded: {table.IsIncluded}");

                    // Check if a record with the same TableNumber already exists in DiningTableRecord
                    string checkSql = @"SELECT COUNT(*) FROM DiningTableRecord WHERE TableNumber = @TableNumber";
                    int count = cnn.Query<int>(checkSql, new { TableNumber = table.TableNumber }).Single();

                    if (count == 0) {
                        // Insert a new record into DiningTableRecord if no existing record is found
                        string insertSql = @"INSERT INTO DiningTableRecord (TableNumber, DiningAreaID, IsIncluded) 
                                     VALUES (@TableNumber, @DiningAreaID, @IsIncluded)";
                        cnn.Execute(insertSql, new {
                            TableNumber = table.TableNumber,
                            DiningAreaID = table.DiningAreaId,
                            IsIncluded = table.IsIncluded
                        });
                    }
                    else {
                        // Update the IsIncluded field in DiningTableRecord if the record already exists
                        string updateSql = @"UPDATE DiningTableRecord SET IsIncluded = @IsIncluded WHERE TableNumber = @TableNumber";
                        cnn.Execute(updateSql, new {
                            TableNumber = table.TableNumber,
                            IsIncluded = table.IsIncluded
                        });
                    }

                    // Now update the IsIncluded field in DiningTable for the same TableNumber
                    string updateDiningTableSql = @"UPDATE DiningTable 
                                            SET IsIncluded = @IsIncluded 
                                            WHERE TRIM(TableNumber) = TRIM(@TableNumber)";
                    int rowsAffected = cnn.Execute(updateDiningTableSql, new {
                        TableNumber = table.TableNumber.Trim(), // Ensure TableNumber has no extra spaces
                        IsIncluded = table.IsIncluded
                    });

                    // Log if the DiningTable update was successful
                    if (rowsAffected > 0) {
                        Console.WriteLine($"Updated DiningTable for {table.TableNumber} successfully.");
                    }
                    else {
                        Console.WriteLine($"Failed to update DiningTable for {table.TableNumber}. Check if the TableNumber exists.");
                    }
                }
            }
        }



        public static List<Table> GetExcludedTablesForDiningArea(DiningArea diningArea)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var tables = cnn.Query<Table>("SELECT * FROM DiningTableRecord WHERE DiningAreaID = @ID AND IsIncluded = 0",
                    new { ID = diningArea.ID }).ToList();

                foreach (var table in tables)
                {
                    if (diningArea != null)
                    {
                        table.DiningArea = diningArea;
                    }
                }

                return tables;
            }
        }


        public static List<TableStat> LoadTableStats()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var queryResult = cnn.Query(@"SELECT * FROM TableStats").ToList();

                var tableStatsList = queryResult.Select(row => new TableStat
                {
                    // Assign other properties as necessary
                    TableStatNumber = row.TableStatNumber,
                    DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.DayOfWeek), // Convert string to DayOfWeek
                    Date = DateOnly.Parse(row.Date), // Convert string to DateOnly
                    IsLunch = Convert.ToBoolean(row.IsLunch), // Convert long to bool
                    Sales = row.Sales != null ? (float?)Convert.ToDouble(row.Sales) : null, // Convert double to float?
                    Orders = Convert.ToInt32(row.Orders) // Convert long to int
                }).ToList();

                return tableStatsList;
            }
        }

        public static List<TableStat> LoadTableStatsByDateAndLunch(bool isLunch, DateOnly date)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Prepare your query with parameters
                var query = @"SELECT * FROM TableStats WHERE IsLunch = @IsLunch AND Date = @Date";

                // Execute the query with the provided parameters
                var queryResult = cnn.Query(query, new { IsLunch = isLunch, Date = date.ToString("yyyy-MM-dd") }).ToList();

                var tableStatsList = queryResult.Select(row => new TableStat
                {
                    // Assign other properties as necessary
                    TableStatNumber = row.TableStatNumber,
                    DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.DayOfWeek),
                    Date = DateOnly.Parse(row.Date),
                    IsLunch = Convert.ToBoolean(row.IsLunch),
                    Sales = row.Sales != null ? (float?)Convert.ToDouble(row.Sales) : null,
                    Orders = Convert.ToInt32(row.Orders), 
                    DiningAreaID = row.DiningAreaID != null? (int?)row.DiningAreaID : 0
                }).ToList();

                return tableStatsList;
            }
        }
        public static List<TableStat> LoadTableStatsByDateAllDay( DateOnly date)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Prepare your query with parameters
                var query = @"SELECT * FROM TableStats WHERE Date = @Date";

                // Execute the query with the provided parameters
                var queryResult = cnn.Query(query, new { Date = date.ToString("yyyy-MM-dd") }).ToList();

                var tableStatsList = queryResult.Select(row => new TableStat
                {
                    // Assign other properties as necessary
                    TableStatNumber = row.TableStatNumber,
                    DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.DayOfWeek),
                    Date = DateOnly.Parse(row.Date),
                    IsLunch = Convert.ToBoolean(row.IsLunch),
                    Sales = row.Sales != null ? (float?)Convert.ToDouble(row.Sales) : null,
                    Orders = Convert.ToInt32(row.Orders)
                }).ToList();

                return tableStatsList;
            }
        }
        public static List<TableStat> LoadTableStatsByDateListAndLunch(bool isLunch, List<DateOnly> dates)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Convert the list of DateOnly objects to a list of formatted date strings
                var formattedDates = dates.Select(date => date.ToString("yyyy-MM-dd")).ToList();

                // Prepare your query with parameters
                var query = @"SELECT * FROM TableStats WHERE IsLunch = @IsLunch AND Date IN @Dates";

                // Execute the query with the provided parameters
                var queryResult = cnn.Query(query, new { IsLunch = isLunch, Dates = formattedDates }).ToList();

                var tableStatsList = queryResult.Select(row => new TableStat
                {
                    // Assign other properties as necessary
                    TableStatNumber = row.TableStatNumber,
                    DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.DayOfWeek),
                    Date = DateOnly.Parse(row.Date),
                    IsLunch = Convert.ToBoolean(row.IsLunch),
                    Sales = row.Sales != null ? (float?)Convert.ToDouble(row.Sales) : null,
                    Orders = Convert.ToInt32(row.Orders)
                }).ToList();
                tableStatsList = CalculateAverageSales(tableStatsList, dates.Count);
                return tableStatsList;
            }
        }
       private static List<TableStat> CalculateAverageSales(List<TableStat> tableStats, int numberOfDays)
        {
            // Group the table stats by TableStatNumber
            var groupedStats = tableStats
                .GroupBy(ts => ts.TableStatNumber)
                .Select(group => new
                {
                    TableStatNumber = group.Key,
                    TotalSales = group.Sum(g => g.Sales ?? 0)
                });

            // Calculate average sales and create new TableStat objects
            var averageStats = groupedStats
                .Select(g => new TableStat
                {
                    TableStatNumber = g.TableStatNumber,
                    Sales = g.TotalSales / numberOfDays
                }).ToList();

            return averageStats;
        }



        public static List<DateOnly> GetMissingSalesDates(DateOnly startDate, DateOnly endDate)
        {
            List<DateOnly> missingDates = new List<DateOnly>();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Generate a list of all dates in the range
                for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    missingDates.Add(date);
                }

                // Query the database for dates in the range that have records
                string sql = @"SELECT DISTINCT Date FROM TableStats WHERE Date BETWEEN @StartDate AND @EndDate";
                var existingDateStrings = cnn.Query<string>(sql, new { StartDate = startDate.ToString("yyyy-MM-dd"), EndDate = endDate.ToString("yyyy-MM-dd") }).ToList();

                // Convert the existing dates from strings to DateOnly objects
                var existingDates = existingDateStrings.Select(dateStr => DateOnly.Parse(dateStr)).ToList();

                // Remove the dates found in the database from the missingDates list
                missingDates = missingDates.Except(existingDates).ToList();
            }

            return missingDates;
        }
        public static List<DateOnly> GetMissingWeatherDates(DateOnly startDate, DateOnly endDate)
        {
            List<DateOnly> missingDates = new List<DateOnly>();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Generate a list of all dates in the range
                for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    missingDates.Add(date);
                }

                // Query the database for dates in the range that have records
                string sql = @"SELECT DISTINCT Date FROM HourlyWeatherData WHERE Date BETWEEN @StartDate AND @EndDate";
                var existingDateStrings = cnn.Query<string>(sql, new { StartDate = startDate.ToString("yyyy-MM-dd"), EndDate = endDate.ToString("yyyy-MM-dd") }).ToList();

                // Convert the existing dates from strings to DateOnly objects
                var existingDates = existingDateStrings.Select(dateStr => DateOnly.Parse(dateStr)).ToList();

                // Remove the dates found in the database from the missingDates list
                missingDates = missingDates.Except(existingDates).ToList();
            }

            return missingDates;
        }


        public static void SelectNewDatabaseLocation(string newLocation)
        {
            try
            {
                string dbFileName = "FloorplanMakerDB.db"; // Replace with your actual database file name
                string currentDbPath = ConfigurationManager.AppSettings["DatabasePath"];

                // Determine the current full path of the database file
                string fullCurrentPath = string.IsNullOrEmpty(currentDbPath)
                    ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName) // Default location
                    : Path.Combine(currentDbPath, dbFileName);

                string fullNewPath = Path.Combine(newLocation, dbFileName);

                if (File.Exists(fullCurrentPath))
                {
                    File.Copy(fullCurrentPath, fullNewPath, true); // true to overwrite if file already exists
                    UpdateDatabaseLocation(fullNewPath);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, possibly inform the user
            }
        }
        public static void CheckAndSetDatabaseLocation()
        {
            string dbFileName = "FloorplanMakerDB.db";
            string configPath = ConfigurationManager.AppSettings["DatabasePath"];

            // Check if the database location is already set
            if (string.IsNullOrEmpty(configPath))
            {
                using (var folderBrowser = new FolderBrowserDialog())
                {
                    folderBrowser.Description = "Select the folder to store the database:";

                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        // Set the database location in the config
                        string selectedPath = folderBrowser.SelectedPath;
                        UpdateDatabaseLocation(selectedPath);

                        // Optionally move the database file to the selected location
                        // Ensure the default DB exists or create it if necessary
                        string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);
                        string destinationPath = Path.Combine(selectedPath, dbFileName);
                        if (!File.Exists(destinationPath))
                        {
                            if (File.Exists(sourcePath))
                            {
                                File.Copy(sourcePath, destinationPath);
                            }
                            else
                            {
                                
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No folder selected, the application will close.");
                        Application.Exit();
                    }
                }
            }
        }

        private static void UpdateDatabaseLocation(string newLocation)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DatabasePath"].Value = newLocation;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        public static void LoadDatabaseTables(string dbPath)
        {
            string connectionString = $"Data Source={dbPath};Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM sqlite_master WHERE type='table';";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader.GetString(0);
                            // Now, you can load data from this table
                            LoadTableData(connection, tableName);
                        }
                    }
                }
                connection.Close();
            }
        }
        private static void LoadTableData(SQLiteConnection connection, string tableName)
        {
            using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {tableName}", connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Process data from the table
                    // For example, you can load it into a data structure or display it in your application
                }
            }
        }
        public static void BackupDatabase()
        {
            string connectionString = LoadConnectionString();
            var builder = new System.Data.SQLite.SQLiteConnectionStringBuilder(connectionString);
            string databasePath = builder.DataSource;

            string backupDirectory = Path.Combine(Path.GetDirectoryName(databasePath), "BackUpDBs");
            Directory.CreateDirectory(backupDirectory); // Ensure the backup directory exists

            string backupFileName = $"backup_{DateTime.Now:yyyy_MM_dd}.db";
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);

            System.IO.File.Copy(databasePath, backupFilePath, overwrite: true);
            
        }
        public static void DeleteOldBackups()
        {
            try
            {
                
                string connectionString = LoadConnectionString();
               
                var builder = new System.Data.SQLite.SQLiteConnectionStringBuilder(connectionString);
                string databasePath = builder.DataSource;
                               
                string backupDirectory = Path.Combine(Path.GetDirectoryName(databasePath), "BackUpDBs");
               
                if (!Directory.Exists(backupDirectory))
                {
                    MessageBox.Show("Backup directory does not exist.");
                    return;
                }
                // Get all backup files in the directory
                string[] backupFiles = Directory.GetFiles(backupDirectory, "backup_*.db");

                // Sort the backup files by date in descending order
                Array.Sort(backupFiles, (x, y) => File.GetCreationTime(y).CompareTo(File.GetCreationTime(x)));

                // Determine the cutoff date for old backups
                DateTime cutoffDate = DateTime.Now.AddMonths(-2);

                // Filter out backups older than the cutoff date
                List<string> oldBackups = new List<string>();
                foreach (string backupFile in backupFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(backupFile);
                    string dateString = fileName.Replace("backup_", "");

                    if (DateTime.TryParseExact(dateString, "yyyy_MM_dd", null, System.Globalization.DateTimeStyles.None, out DateTime backupDate))
                    {
                        if (backupDate < cutoffDate)
                        {
                            oldBackups.Add(backupFile);
                        }
                    }
                }

                // Delete old backups if there will be at least 10 backups remaining
                if (backupFiles.Length - oldBackups.Count >= 10)
                {
                    foreach (string oldBackup in oldBackups)
                    {
                        File.Delete(oldBackup);
                        Console.WriteLine($"Deleted old backup: {oldBackup}");
                    }
                }
                else
                {
                    Console.WriteLine("Not enough backups would remain after deletion, so no files were deleted.");
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error during deleting old backups: {ex.Message}");
            }
        }

        public static List<Table> LoadTables(List<DiningArea> diningAreas)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var tables = cnn.Query<Table>("SELECT * FROM DiningTable").ToList();

                foreach (var table in tables)
                {
                    // Assuming DiningAreaID is a property of Table
                    var diningArea = diningAreas.FirstOrDefault(da => da.ID == table.DiningAreaId);
                    if (diningArea != null)
                    {
                        table.DiningArea = diningArea;
                    }
                    var pairedTables = cnn.Query<string>(
                        "SELECT TableNumber FROM InheritedTablePairs WHERE MainTableNumber = @MainTableNumber",
                new { MainTableNumber = table.TableNumber }).ToList();                    
                    table.InheritedTables.AddRange(pairedTables);
                }

                return tables;
            }
        }
        public static List<Table> LoadTables(DiningArea diningArea)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var tables = cnn.Query<Table>("SELECT * FROM DiningTable WHERE DiningAreaID = @ID", new { ID = diningArea.ID,}).ToList();

                foreach (var table in tables)
                {
                    
                    
                    if (diningArea != null)
                    {
                        table.DiningArea = diningArea;
                    }
                }

                return tables;
            }
        }
        // TODO: Remove unused methods
        public static int SaveTable(Table table)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                // Check if a table with the same TableNumber already exists
                string checkSql = "SELECT COUNT(*) FROM DiningTable WHERE TableNumber = @TableNumber";
                int count = cnn.Query<int>(checkSql, new { TableNumber = table.TableNumber }).Single();

                if (count > 0)
                {
                    // Table with the same TableNumber exists
                    var result = MessageBox.Show("A table with this number already exists. Do you want to replace it?", "Table Exists", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // User wants to replace the existing table, update it
                        UpdateTable(table);
                        SaveInheritedTablePairs(table);
                        return table.ID;
                    }
                    else
                    {
                        // User doesn't want to replace the table, return -1 or another specific value
                        return -1; // You might want to return a specific value to indicate no record was inserted
                    }
                }
                else
                {
                    // Table with the same TableNumber does not exist, proceed with insertion
                    var parameters = new
                    {
                        TableNumber = table.TableNumber,
                        MaxCovers = table.MaxCovers,
                        AverageCovers = table.AverageSales,
                        DiningAreaID = table.DiningArea.ID,
                        XCoordinate = table.XCoordinate,
                        YCoordinate = table.YCoordinate,
                        Shape = (int)table.Shape,
                        Width = table.Width,
                        Height = table.Height
                    };

                    using (var transaction = cnn.BeginTransaction())
                    {
                        string sql = @"INSERT INTO DiningTable 
                    (TableNumber, MaxCovers, AverageCovers, DiningAreaID, XCoordinate, 
                    YCoordinate, Shape, Width, Height) 
                    VALUES 
                    (@TableNumber, @MaxCovers, @AverageCovers, @DiningAreaID, @XCoordinate, 
                    @YCoordinate, @Shape, @Width, @Height)";

                        cnn.Execute(sql, parameters, transaction);

                        int insertedId = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();

                        table.ID = insertedId;
                        SaveInheritedTablePairs(table);

                        transaction.Commit();

                        return insertedId;
                    }
                }
            }
        }
        public static void SaveInheritedTablePairs(Table table)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                // First, remove existing pairs for the table
                cnn.Execute("DELETE FROM InheritedTablePairs WHERE MainTableNumber = @MainTableNumber",
                            new { MainTableNumber = table.TableNumber });

                // Now, insert the new pairs
                foreach (var inheritedTable in table.InheritedTables) {
                    string sql = @"INSERT INTO InheritedTablePairs (TableNumber, MainTableNumber)
                           VALUES (@TableNumber, @MainTableNumber)";
                    cnn.Execute(sql, new { TableNumber = inheritedTable, MainTableNumber = table.TableNumber });
                }
            }
        }

        public static void UpdateTable(Table table)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new
                {
                    ID = table.ID,
                    TableNumber = table.TableNumber,
                    MaxCovers = table.MaxCovers,
                    AverageCovers = table.AverageSales,
                    DiningAreaID = table.DiningArea.ID,
                    XCoordinate = table.XCoordinate,
                    YCoordinate = table.YCoordinate,
                    Shape = (int)table.Shape,
                    Width = table.Width,
                    Height = table.Height,
                    IsIncluded = table.IsIncluded
                   
                };

                string sql = @"
            UPDATE DiningTable 
            SET TableNumber = @TableNumber,
                MaxCovers = @MaxCovers, 
                AverageCovers = @AverageCovers,
                DiningAreaID = @DiningAreaID,
                XCoordinate = @XCoordinate,
                YCoordinate = @YCoordinate,
                Shape = @Shape,
                Width = @Width,
                Height = @Height,
                IsIncluded = @IsIncluded
            WHERE ID = @ID";

                cnn.Execute(sql, parameters);
            }
        }
        public static void DeleteTable(Table table)
        {
            //int tableID = table.ID;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "DELETE FROM DiningTable WHERE ID = @ID";
                cnn.Execute(sql, new { ID = table.ID });
            }
        }
        public static void DeleteTableStatsByDateRange(DateTime startDate, DateTime endDate)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                // Define the SQL query for deleting records within the date range
                string deleteSql = @"DELETE FROM TableStats 
                             WHERE Date >= @StartDate 
                             AND Date <= @EndDate";

                // Execute the delete query with the provided date range
                cnn.Execute(deleteSql, new
                {
                    StartDate = startDate.ToString("yyyy-MM-dd"),
                    EndDate = endDate.ToString("yyyy-MM-dd")
                });
            }
        }

        public static void DeleteTablesByDiningArea(DiningArea diningArea)
        {
            int diningAreaID = diningArea.ID;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "DELETE FROM DiningTable WHERE DiningAreaID = @ID";
                cnn.Execute(sql, new { ID = diningAreaID });
            }
        }
        public static List<DiningArea> LoadDiningAreas()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var diningAreas = cnn.Query<DiningArea>("SELECT * FROM DiningArea").ToList();

                var tables = LoadTables(diningAreas);
                if (tables == null)
                {
                    throw new InvalidOperationException("LoadTables returned null.");
                }

                foreach (var diningArea in diningAreas)
                {
                    diningArea.Tables = tables.Where(t => t.DiningAreaId == diningArea.ID).ToList();
                }
                

                return diningAreas;
            }
        }
        public static DiningArea LoadDiningAreaByID(int Id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                var diningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = Id });


                return diningArea;
            }
        }
        public static void SaveDiningArea(DiningArea diningArea)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO DiningArea (Name, IsInside) VALUES (@Name, @IsInside)", diningArea);
            }
        }
        public static int SaveNewServer(Server server)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = "INSERT INTO Server (Name, Archived, DisplayName) VALUES (@Name, @Archived, @DisplayName)";
                        cnn.Execute(insertQuery, server, transaction);

                        int insertedId = cnn.Query<int>("SELECT last_insert_rowid()", transaction: transaction).Single();
                        transaction.Commit();

                        return insertedId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static List<Server> LoadServers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<Server>("SELECT * FROM Server").ToList();
            }
        }
        public static void UpdateServer(Server server)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var query = "UPDATE Server SET Name = @Name, Archived = @Archived, DisplayName = @DisplayName, " +
                            "CocktailPreference = @CocktailPreference, CloseFrequency = @CloseFrequency, " +
                            "TeamWaitFrequency = @TeamWaitFrequency, OutsideFrequency = @OutsideFrequency, " +
                            "PreferedSectionWeight = @PreferedSectionWeight, " +
                            "HSID = @HSID WHERE ID = @ID";

                cnn.Execute(query, new
                {
                    Name = server.Name,
                    Archived = server.Archived,
                    DisplayName = server.DisplayName,
                    CocktailPreference = server.CocktailPreference,
                    CloseFrequency = server.CloseFrequency,
                    TeamWaitFrequency = server.TeamWaitFrequency,
                    OutsideFrequency = server.OutsideFrequency,
                    PreferedSectionWeight = server.PreferedSectionWeight,
                    HSID = server.HSID,
                    ID = server.ID
                });
            }
        }

        public static List<Server> LoadActiveServers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                List<Server> servers = cnn.Query<Server>("SELECT * FROM Server WHERE Archived = 0").ToList();
                //List<Server> currentBartenders = servers
                // .Where(s => s.Name.StartsWith("BAR"))
                // .OrderBy(s => s.Name)
                // .ToList();
                //foreach (Server server in currentBartenders)
                //{
                //    server.IsBartender = true;
                //}
                return servers;
            }
        }

        public static List<Server> LoadArchivedServers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<Server>("SELECT * FROM Server WHERE Archived = 1").ToList();
            }
        }

        public static void SaveFloorplan(Floorplan floorplan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Insert into Floorplan table
                var sql = "INSERT INTO Floorplan (Date, IsLunch, DiningAreaID) VALUES (@Date, @IsLunch, @DiningAreaId); SELECT last_insert_rowid();";
                floorplan.ID = cnn.ExecuteScalar<int>(sql, new { floorplan.Date, floorplan.IsLunch, DiningAreaId = floorplan.DiningArea.ID });

                // Insert into FloorplanSections
                foreach (var section in floorplan.Sections)
                {
                    cnn.Execute("INSERT INTO FloorplanSections (FloorplanID, SectionID) VALUES (@FloorplanID, @SectionID)", new { FloorplanID = floorplan.ID, SectionID = section.ID });
                }

                // Insert into FloorplanServers
                foreach (var server in floorplan.Servers)
                {
                    cnn.Execute("INSERT INTO FloorplanServers (FloorplanID, ServerID) VALUES (@FloorplanID, @ServerID)", new { FloorplanID = floorplan.ID, ServerID = server.ID });
                }
            }
        }

        public static Floorplan LoadFloorplan(int floorplanId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var floorplan = cnn.QuerySingle<Floorplan>("SELECT * FROM Floorplan WHERE ID = @ID", new { ID = floorplanId });

                // Populate DiningArea
                floorplan.DiningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = floorplan.DiningArea.ID });

                // Populate Sections from FloorplanSections
                var sectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID });
                foreach (var id in sectionIds)
                {
                    var section = cnn.QuerySingle<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = id });
                    // Populate Tables for each Section from SectionTables
                    section.SetTableList( cnn.Query<Table>("SELECT * FROM DiningTable WHERE ID IN (SELECT TableID FROM SectionTables WHERE SectionID = @SectionID)", new { SectionID = id }).ToList());
                    floorplan.Sections.Add(section);
                }

                // Populate Servers from FloorplanServers
                //floorplan.Servers = cnn.Query<Server>("SELECT * FROM Server WHERE ID IN (SELECT ServerID FROM FloorplanServers WHERE FloorplanID = @FloorplanID)", new { FloorplanID = floorplan.ID }).ToList();

                return floorplan;
            }
        }
        //TODO: instead of loading tables from DB, get them from dining area
        public static List<Floorplan> LoadFloorplansByDateAndShift(DateOnly date, bool isLunch)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            List<Floorplan> floorplans = new List<Floorplan>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Query Floorplans based on given criteria
                var fetchedFloorplans = cnn.Query<Floorplan>(
                    "SELECT * FROM Floorplan WHERE Date = @Date AND IsLunch = @IsLunch",
                    new
                    {
                        Date = dateString,
                        IsLunch = isLunch
                    }).ToList();

                foreach (var floorplan in fetchedFloorplans)
                {
                    // You might need to adjust this based on the structure of your DiningArea table
                    var diningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = floorplan.DiningAreaID });
                    //diningArea.Tables = LoadTables(diningArea);
                    floorplan.DiningArea = diningArea;
                    diningArea.Tables = LoadTables(diningArea);


                    // Populate Sections from FloorplanSections
                    var sectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID });
                    foreach (var id in sectionIds)
                    {
                        var section = cnn.QuerySingle<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = id });

                        // Get Table IDs for each Section from SectionTables
                        var tableIdsInSection = cnn.Query<int>("SELECT TableID FROM SectionTables WHERE SectionID = @SectionID", new { SectionID = id });

                        // Match tables from DiningArea with the ones in Section
                        var tablesInSection = diningArea.Tables.Where(table => tableIdsInSection.Contains(table.ID)).ToList();
                        section.SetTableList(tablesInSection);

                        floorplan.AddSection(section);
                    }


                    var servers = new List<Server>();

                    // Fetch the server-section relationships directly as anonymous types
                    var serverSections = cnn.Query("SELECT * FROM ServerSections WHERE SectionID IN @SectionIds", new { SectionIds = sectionIds })
                                             .Select(x => new { ServerID = (int)x.ServerID, SectionID = (int)x.SectionID })
                                             .ToList();
                    var relevantServerIds = serverSections.Select(ss => ss.ServerID).Distinct().ToList();
                    var allRelevantServers = cnn.Query<Server>("SELECT * FROM Server WHERE ID IN @ServerIDs", new { ServerIDs = relevantServerIds }).ToList();



                    foreach (var ss in serverSections)
                    {
                        var server = allRelevantServers.FirstOrDefault(s => s.ID == ss.ServerID);
                        var matchedSection = floorplan.Sections.FirstOrDefault(s => s.ID == ss.SectionID);

                        if (server != null && matchedSection != null)
                        {
                            matchedSection.ServerTeam.Add(server);
                        }
                    }

                    // After adding all servers, set IsTeamWait appropriately
                    foreach (var section in floorplan.Sections)
                    {
                        if (section.ServerTeam.Count > 1)
                        {
                            section.MakeTeamWait();
                        }
                    }

                    //floorplan.Servers = servers;
                    floorplans.Add(floorplan);
                }
            }

            return floorplans;
        }
        //TODO: instead of loading tables from DB, get them from dining area
        public static Shift LoadShift(DiningArea diningAreaSelected, DateOnly date, bool isLunch)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            List<Floorplan> allFloorplans = new List<Floorplan>();
            Shift shift = new Shift(diningAreaSelected, date, isLunch);  
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                allFloorplans = cnn.Query<Floorplan>("SELECT * FROM Floorplan WHERE Date = @Date AND IsLunch = @IsLunch",
                    new
                    {

                        Date = dateString,
                        IsLunch = isLunch
                    }
                    ).ToList();
                if (allFloorplans == null)
                {
                    return null;
                }
                
            }
            
            foreach (Floorplan floorplan in allFloorplans)
            {
                shift.AddFloorplanAndServers(LoadFloorplanByCriteria(floorplan.DiningAreaID, date, isLunch));
            }
            shift.SetSelectedFloorplan(date, isLunch, diningAreaSelected.ID);
            shift.PickupSectionUpdate();
            shift.PairBothBarSections();
            return shift;

        }
        public static Shift LoadShift(DateOnly date, bool isLunch)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            List<Floorplan> allFloorplans = new List<Floorplan>();
            Shift shift = new Shift(date, isLunch);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                allFloorplans = cnn.Query<Floorplan>("SELECT * FROM Floorplan WHERE Date = @Date AND IsLunch = @IsLunch",
                    new
                    {

                        Date = dateString,
                        IsLunch = isLunch
                    }
                    ).ToList();
                if (allFloorplans == null)
                {
                    return null;
                }

            }

            foreach (Floorplan floorplan in allFloorplans)
            {
                shift.AddFloorplanAndServers(LoadFloorplanByCriteria(floorplan.DiningAreaID, date, isLunch));
            }
           
            return shift;

        }
        public static Floorplan LoadFloorplanByCriteria(int diningAreaID, DateOnly date, bool isLunch)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Query Floorplan based on given criteria
                var floorplan = cnn.QuerySingleOrDefault<Floorplan>(
                    "SELECT * FROM Floorplan WHERE DiningAreaID = @DiningAreaID AND Date = @Date AND IsLunch = @IsLunch",
                    new
                    {
                        DiningAreaID = diningAreaID,
                        Date = dateString,
                        IsLunch = isLunch
                    });

                if (floorplan == null)
                {
                    return null;
                }

                //var diningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = floorplan.DiningAreaID });
                var diningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new {ID = diningAreaID});

                var tables = LoadTables(diningArea);
                if (tables == null)
                {
                    throw new InvalidOperationException("LoadTables returned null.");
                }

               
                diningArea.Tables = tables.Where(t => t.DiningAreaId == diningArea.ID).ToList();
                floorplan.DiningArea = diningArea;              


                

                // Populate Sections from FloorplanSections
                var sectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID });
                foreach (var id in sectionIds)
                {
                    var section = cnn.QuerySingle<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = id });

                    // Get Table IDs for each Section from SectionTables
                    var tableIdsInSection = cnn.Query<int>("SELECT TableID FROM SectionTables WHERE SectionID = @SectionID", new { SectionID = id });

                    // Match tables from DiningArea with the ones in Section
                    var tablesInSection = diningArea.Tables.Where(table => tableIdsInSection.Contains(table.ID)).ToList();
                    section.SetTableList(tablesInSection);

                    floorplan.AddSection(section);
                }


                var servers = new List<Server>();

                // Fetch the server-section relationships directly as anonymous types
                var serverSections = cnn.Query("SELECT * FROM ServerSections WHERE SectionID IN @SectionIds", new { SectionIds = sectionIds })
                                         .Select(x => new { ServerID = (int)x.ServerID, SectionID = (int)x.SectionID })
                                         .ToList();
                var relevantServerIds = serverSections.Select(ss => ss.ServerID).Distinct().ToList();
                var allRelevantServers = cnn.Query<Server>("SELECT * FROM Server WHERE ID IN @ServerIDs", new { ServerIDs = relevantServerIds }).ToList();



                foreach (var ss in serverSections)
                {
                    var server = allRelevantServers.FirstOrDefault(s => s.ID == ss.ServerID);
                    var matchedSection = floorplan.Sections.FirstOrDefault(s => s.ID == ss.SectionID);

                    if (server != null && matchedSection != null)
                    {
                        matchedSection.ServerTeam.Add(server);
                        server.CurrentSection = matchedSection;
                    }
                }

                // After adding all servers, set IsTeamWait appropriately
                foreach (var section in floorplan.Sections)
                {
                    if(section.Server != null)
                    {
                        if (section.Server.Name.StartsWith("BAR"))
                        {
                            section.SetToBarSection();
                        }
                        section.Server.CurrentSection = section;
                    }
                    if (section.ServerTeam.Count > 1)
                    {
                        section.MakeTeamWait();
                    }
                    section.SetServerCount();
                }

                return floorplan;


            }
        }

        public static Floorplan LoadFloorplanByCriteria(DiningArea diningArea, DateOnly date, bool isLunch)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Query Floorplan based on given criteria
                var floorplan = cnn.QuerySingleOrDefault<Floorplan>(
                    "SELECT * FROM Floorplan WHERE DiningAreaID = @DiningAreaID AND Date = @Date AND IsLunch = @IsLunch",
                    new
                    {
                        DiningAreaID = diningArea.ID,
                        Date = dateString,
                        IsLunch = isLunch
                    });

                if (floorplan == null)
                {
                    return null;
                }

                // Populate DiningArea
                floorplan.DiningArea = diningArea;

                // Populate Sections from FloorplanSections
                var sectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID });
                foreach (var id in sectionIds)
                {
                    var section = cnn.QuerySingle<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = id });

                    // Get Table IDs for each Section from SectionTables
                    var tableIdsInSection = cnn.Query<int>("SELECT TableID FROM SectionTables WHERE SectionID = @SectionID", new { SectionID = id });

                    // Match tables from DiningArea with the ones in Section
                    var tablesInSection = diningArea.Tables.Where(table => tableIdsInSection.Contains(table.ID)).ToList();
                    section.SetTableList(tablesInSection);

                    floorplan.AddSection(section);
                }


                var servers = new List<Server>();

                // Fetch the server-section relationships directly as anonymous types
                var serverSections = cnn.Query("SELECT * FROM ServerSections WHERE SectionID IN @SectionIds", new { SectionIds = sectionIds })
                                         .Select(x => new { ServerID = (int)x.ServerID, SectionID = (int)x.SectionID })
                                         .ToList();
                var relevantServerIds = serverSections.Select(ss => ss.ServerID).Distinct().ToList();
                var allRelevantServers = cnn.Query<Server>("SELECT * FROM Server WHERE ID IN @ServerIDs", new { ServerIDs = relevantServerIds }).ToList();

                

                foreach (var ss in serverSections)
                {
                    var server = allRelevantServers.FirstOrDefault(s => s.ID == ss.ServerID);
                    var matchedSection = floorplan.Sections.FirstOrDefault(s => s.ID == ss.SectionID);

                    if (server != null && matchedSection != null)
                    {
                        matchedSection.ServerTeam.Add(server);
                    }
                }

                // After adding all servers, set IsTeamWait appropriately
                foreach (var section in floorplan.Sections)
                {
                    if (section.ServerTeam.Count > 1)
                    {
                        section.MakeTeamWait();
                    }
                }

                return floorplan;

               
            }
        }

        //public static void SaveFloorplanTemplate(FloorplanTemplate template)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        cnn.Open();
        //        cnn.Execute("INSERT INTO FloorplanTemplate (Name, DiningAreaID, ServerCount, HasTeamWait, HasPickUp)" +
        //            " VALUES (@Name, @DiningAreaID, @ServerCount, @HasTeamWait, @HasPickUp)", template);

        //        template.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();

        //        foreach (Section section in template.Sections)
        //        {
        //            SaveSection(section);
        //            cnn.Execute("INSERT INTO TemplateSections (SectionID, TemplateID) VALUES (@SectionID, @TemplateID)", new { SectionID = section.ID, TemplateID = template.ID });
        //        }
        //    }

        //}
        public static void SaveFloorplanTemplate(FloorplanTemplate template)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute("INSERT INTO FloorplanTemplate (Name, DiningAreaID, ServerCount, HasTeamWait, HasPickUp)" +
                    " VALUES (@Name, @DiningAreaID, @ServerCount, @HasTeamWait, @HasPickUp)", template);

                template.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();

                foreach (Section section in template.Sections)
                {
                    section.IsCloser = false;
                    section.IsPre = false;
                    SaveSection(section);
                    cnn.Execute("INSERT INTO TemplateSections (SectionID, TemplateID) VALUES (@SectionID, @TemplateID)", new { SectionID = section.ID, TemplateID = template.ID });
                }

                foreach (var line in template.floorplanLines)
                {
                    cnn.Execute("INSERT INTO FloorplanLines (TemplateID, StartX, StartY, EndX, EndY) VALUES (@TemplateID, @StartX, @StartY, @EndX, @EndY)",
                        new { TemplateID = template.ID, StartX = line.StartPoint.X, StartY = line.StartPoint.Y, EndX = line.EndPoint.X, EndY = line.EndPoint.Y });
                }
            }
        }
        public static void TestDeleteLines(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                var sql = "DELETE FROM FloorplanLines WHERE TemplateID = @Id;";
                cnn.Execute(sql, new { Id = id });

            }
        }
        public static void UpdateTemplateLines(int id, List<FloorplanLine> lines)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                var sql = "DELETE FROM FloorplanLines WHERE TemplateID = @Id;";
                cnn.Execute(sql, new { Id = id });

                foreach (var line in lines)
                {
                    cnn.Execute("INSERT INTO FloorplanLines (TemplateID, StartX, StartY, EndX, EndY) VALUES (@TemplateID, @StartX, @StartY, @EndX, @EndY)",
                        new { TemplateID = id, StartX = line.StartPoint.X, StartY = line.StartPoint.Y, EndX = line.EndPoint.X, EndY = line.EndPoint.Y });
                }
            }
        }
        public static void DeleteFloorplanTemplate(int templateId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // First, delete records from linking tables
                        cnn.Execute("DELETE FROM FloorplanLines WHERE TemplateID = @TemplateID", new { TemplateID = templateId }, transaction);
                        cnn.Execute("DELETE FROM TemplateSections WHERE TemplateID = @TemplateID", new { TemplateID = templateId }, transaction);

                        // Then, delete the template itself
                        cnn.Execute("DELETE FROM FloorplanTemplate WHERE ID = @ID", new { ID = templateId }, transaction);

                        transaction.Commit();
                        MessageBox.Show($"Template with ID of {templateId} Deleted");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        // section, , server section,
        //public static void DeleteFloorplan(Floorplan floorplan)
        //{
        //    int floorplanId = floorplan.ID;

        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        cnn.Open();
        //        using (var transaction = cnn.BeginTransaction())
        //        {
        //            try
        //            {
                        
        //                cnn.Execute("DELETE FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplanId }, transaction);
        //                cnn.Execute("DELETE FROM Shift WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplanId }, transaction);


        //                cnn.Execute("DELETE FROM Floorplan WHERE ID = @ID", new { ID = floorplanId }, transaction);
        //                foreach(Section section in floorplan.Sections)
        //                {
        //                    cnn.Execute("DELETE FROM ServerSections WHERE SectionID = @SectionID", new { SectionID = section.ID }, transaction);
        //                    cnn.Execute("DELETE FROM Section WHERE ID = @ID", new { ID = section.ID }, transaction);
        //                }

        //                transaction.Commit();
        //            }
        //            catch
        //            {
        //                transaction.Rollback();
        //                throw;
        //            }
        //        }
        //    }
        //}
        public static void SaveSection(Section section)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute("INSERT INTO Section (MaxCovers, AverageCovers, IsCloser, IsPre, TeamWait, IsPickUp, ServerCount, IsBarSection) " +
                    "VALUES (@MaxCovers, @AverageCovers, @IsCloser, @IsPre, @TeamWait, @IsPickUp, @ServerCount, @IsBarSection)",
                new
                {
                    MaxCovers = section.MaxCovers,
                    AverageCovers = section.ExpectedTotalSales,                    
                    IsCloser = section.IsCloser,
                    IsPre = section.IsPre,
                    TeamWait = section.IsTeamWait,
                    IsPickUp = section.IsPickUp,
                    ServerCount = section.ServerCount,
                    IsBarSection = section.IsBarSection
                });
                section.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();
                if (section.Tables == null) { return; }

                foreach (Table table in section.Tables)
                {
                    cnn.Execute("INSERT INTO SectionTables (SectionID, TableID) VALUES (@SectionID, @TableID)", new {SectionID = section.ID, TableID = table.ID});
                }
                foreach (Server server in section.ServerTeam)
                {
                    cnn.Execute("INSERT INTO ServerSections (ServerID, SectionID) VALUES (@ServerID, @SectionID)", new {ServerID = server.ID, SectionID = section.ID});
                }
                cnn.Close();
            }
        }
        public static Section LoadSectionForShiftHistory(int sectionId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                var section = cnn.QuerySingleOrDefault<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = sectionId });

                if (section != null)
                {
                    var tables = cnn.Query<Table>("SELECT t.* FROM DiningTable t INNER JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID", new { SectionID = sectionId }).ToList();
                    section.SetTableList(tables);

                    //var servers = cnn.Query<Server>("SELECT s.* FROM Server s INNER JOIN ServerSections ss ON s.ID = ss.ServerID WHERE ss.SectionID = @SectionID", new { SectionID = sectionId }).ToList();
                    //section.ServerTeam = servers;
                }

                cnn.Close();
                return section;
            }
        }


        public static List<FloorplanTemplate> oldLoadAllFloorplanTemplates()
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                var templates = cnn.Query<FloorplanTemplate>("SELECT * FROM FloorplanTemplate").ToList();


                foreach (var template in templates)
                {
                    if (template.DiningAreaID == null) break;
                    template.DiningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = template.DiningAreaID });
                    template.Sections = cnn.Query<Section>("SELECT s.* FROM Section s JOIN TemplateSections ts ON s.ID = ts.SectionID WHERE ts.TemplateID = @TemplateID", new { TemplateID = template.ID }).ToList();


                    foreach (var section in template.Sections)
                    {
                        section.SetTableList(cnn.Query<Table>("SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID", new { SectionID = section.ID }).ToList());
                    }
                }

                return templates;
            }
        }
        public static List<FloorplanTemplate> LoadAllFloorplanTemplates()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var templates = cnn.Query<FloorplanTemplate>("SELECT * FROM FloorplanTemplate").ToList();

                foreach (var template in templates)
                {
                    if (template.DiningAreaID == null) break;
                    template.DiningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = template.DiningAreaID });

                    // Include ServerCount in the query for Section
                    template.Sections = cnn.Query<Section>(
                        "SELECT s.*, s.ServerCount FROM Section s " +
                        "JOIN TemplateSections ts ON s.ID = ts.SectionID " +
                        "WHERE ts.TemplateID = @TemplateID",
                            new { TemplateID = template.ID }
                            ).ToList();

                    foreach (var section in template.Sections)
                    {
                        // Set TemplateTeamWait based on the Section table
                        var teamWaitValue = cnn.ExecuteScalar<bool>(
                            "SELECT TeamWait FROM Section WHERE ID = @SectionID",
                            new { SectionID = section.ID }
                        );
                        section.SetTemplateTeamWait(teamWaitValue);
                        if(teamWaitValue)
                        {
                            template.SetTeamWaitValue(true);
                        }
                        // Set TemplateServerCount using the ServerCount column
                        section.TemplateServerCount = section.ServerCount; // Assuming ServerCount is part of the Section model

                        // Set TemplatePickUp based on the Section table
                        section.TemplatePickUp = section.IsPickUp;
                        if (section.IsPickUp)
                        {
                            template.SetPickUpValue(true);
                        }

                        // Set table list for each section
                        section.SetTableList(cnn.Query<Table>(
                            "SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID",
                            new { SectionID = section.ID }
                        ).ToList());
                    }
                    template.floorplanLines = cnn.Query<FloorplanLine>("SELECT StartX, StartY, EndX, EndY FROM FloorplanLines WHERE TemplateID = @TemplateID", new { TemplateID = template.ID })
                      .Select(line => new FloorplanLine(new Point(line.StartX, line.StartY), new Point(line.EndX, line.EndY)))
                      .ToList();
                }
               

                return templates;
            }
        }



        public static List<FloorplanTemplate> LoadTemplatesByDiningArea(DiningArea diningArea)
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                var templates = cnn.Query<FloorplanTemplate>("SELECT * FROM FloorplanTemplate WHERE DiningAreaID = @DiningAreaID", 
                    new {DiningAreaID = diningArea.ID}).ToList();


                foreach (var template in templates)
                {
                    if (template.DiningAreaID == null) break;
                    template.DiningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = template.DiningAreaID });

                    // Include ServerCount in the query for Section
                    template.Sections = cnn.Query<Section>(
                        "SELECT s.*, s.ServerCount, s.IsBarSection FROM Section s " +
                        "JOIN TemplateSections ts ON s.ID = ts.SectionID " +
                        "WHERE ts.TemplateID = @TemplateID",
                            new { TemplateID = template.ID }
                            ).ToList();

                    foreach (var section in template.Sections)
                    {
                        // Set TemplateTeamWait based on the Section table
                        var teamWaitValue = cnn.ExecuteScalar<bool>(
                            "SELECT TeamWait FROM Section WHERE ID = @SectionID",
                            new { SectionID = section.ID }
                        );
                        section.SetTemplateTeamWait(teamWaitValue);
                        var isBarWaitValue = cnn.ExecuteScalar<bool>(
                            "SELECT isBarSection FROM Section WHERE ID = @SectionID",
                            new { SectionID = section.ID }
                        );
                        section.SetTemplateBarSection(isBarWaitValue);
                        if (teamWaitValue)
                        {
                            template.SetTeamWaitValue(true);
                        }
                        // Set TemplateServerCount using the ServerCount column
                        section.TemplateServerCount = section.ServerCount; // Assuming ServerCount is part of the Section model

                        // Set TemplatePickUp based on the Section table
                        section.TemplatePickUp = section.IsPickUp;
                        if (section.IsPickUp)
                        {
                            template.SetPickUpValue(true);
                        }
                        if(section.TemplateBarSection)
                        {
                            template.SetBarSectionValue(true);
                            section.SetToBarSection();
                        }

                        // Set table list for each section
                        section.SetTableList(cnn.Query<Table>(
                            "SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID",
                            new { SectionID = section.ID }
                        ).ToList());
                    }
                    template.floorplanLines = cnn.Query<FloorplanLine>("SELECT StartX, StartY, EndX, EndY FROM FloorplanLines WHERE TemplateID = @TemplateID", new { TemplateID = template.ID })
                      .Select(line => new FloorplanLine(new Point(line.StartX, line.StartY), new Point(line.EndX, line.EndY)))
                      .ToList();
                }

                return templates;
            }
        }
        //public static List<FloorplanTemplate> LoadTemplatesByDiningAreaAndServerCount(DiningArea diningArea, int serverCount)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var templates = cnn.Query<FloorplanTemplate>(
        //            "SELECT * FROM FloorplanTemplate WHERE DiningAreaID = @DiningAreaID AND ServerCount = @ServerCount",
        //            new { DiningAreaID = diningArea.ID, ServerCount = serverCount }
        //        ).ToList();

        //        foreach (var template in templates)
        //        {
        //            if (template.DiningAreaID == null) break;
        //            //template.DiningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = template.DiningAreaID });
        //            template.DiningArea = diningArea;
        //            // Include ServerCount in the query for Section
        //            template.Sections = cnn.Query<Section>(
        //                "SELECT s.*, s.ServerCount FROM Section s " +
        //                "JOIN TemplateSections ts ON s.ID = ts.SectionID " +
        //                "WHERE ts.TemplateID = @TemplateID",
        //                    new { TemplateID = template.ID }
        //                    ).ToList();

        //            foreach (var section in template.Sections)
        //            {
        //                // Set TemplateTeamWait based on the Section table
        //                var teamWaitValue = cnn.ExecuteScalar<bool>(
        //                    "SELECT TeamWait FROM Section WHERE ID = @SectionID",
        //                    new { SectionID = section.ID }
        //                );
        //                section.SetTeamWait(teamWaitValue);
        //                if (teamWaitValue)
        //                {
        //                    template.SetTeamWaitValue(true);
        //                }
        //                // Set TemplateServerCount using the ServerCount column
        //                section.TemplateServerCount = section.ServerCount; // Assuming ServerCount is part of the Section model

        //                // Set TemplatePickUp based on the Section table
        //                section.TemplatePickUp = section.IsPickUp;
        //                if (section.IsPickUp)
        //                {
        //                    template.SetPickUpValue(true);
        //                }

        //                // Set table list for each section
        //                section.SetTableList(cnn.Query<Table>(
        //                    "SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID",
        //                    new { SectionID = section.ID }
        //                ).ToList());
        //                foreach(Table table in section.Tables)
        //                {
        //                    Table diningTable = diningArea.Tables.FirstOrDefault(t=> t.ID == table.ID);
        //                    table.AverageSales = diningTable.AverageSales;
        //                }
        //            }
        //        }

        //        return templates;
        //    }
        //}
        public static List<FloorplanTemplate> LoadTemplatesByDiningAreaAndServerCount(DiningArea diningArea, int serverCount)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var templates = cnn.Query<FloorplanTemplate>(
                    "SELECT * FROM FloorplanTemplate WHERE DiningAreaID = @DiningAreaID AND ServerCount = @ServerCount",
                    new { DiningAreaID = diningArea.ID, ServerCount = serverCount }
                ).ToList();

                foreach (var template in templates)
                {
                    if (template.DiningAreaID == null) break;
                    template.DiningArea = diningArea;
                    template.Sections = cnn.Query<Section>(
                        "SELECT s.*, s.ServerCount FROM Section s " +
                        "JOIN TemplateSections ts ON s.ID = ts.SectionID " +
                        "WHERE ts.TemplateID = @TemplateID",
                        new { TemplateID = template.ID }
                    ).ToList();

                    foreach (var section in template.Sections)
                    {
                        var teamWaitValue = cnn.ExecuteScalar<bool>(
                            "SELECT TeamWait FROM Section WHERE ID = @SectionID",
                            new { SectionID = section.ID }
                        );
                        section.SetTemplateTeamWait(teamWaitValue);
                        if (teamWaitValue)
                        {
                            template.SetTeamWaitValue(true);
                        }
                        section.TemplateServerCount = section.ServerCount;
                        section.TemplatePickUp = section.IsPickUp;
                        if (section.IsPickUp)
                        {
                            template.SetPickUpValue(true);
                        }

                        section.SetTableList(cnn.Query<Table>(
                            "SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID",
                            new { SectionID = section.ID }
                        ).ToList());

                        foreach (Table table in section.Tables)
                        {
                            Table diningTable = diningArea.Tables.FirstOrDefault(t => t.ID == table.ID);
                            table.SetTableSales(diningTable?.AverageSales ?? 0);
                        }
                    }

                    template.floorplanLines = cnn.Query<FloorplanLine>("SELECT StartX, StartY, EndX, EndY FROM FloorplanLines WHERE TemplateID = @TemplateID", new { TemplateID = template.ID })
                        .Select(line => new FloorplanLine(new Point(line.StartX, line.StartY), new Point(line.EndX, line.EndY)))
                        .ToList();
                }

                return templates;
            }
        }

        public static void UpdateAllFloorplanDates()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                // Set the time part of all dates in the Floorplan table to midnight (which effectively removes the time component)
                cnn.Execute("UPDATE Floorplan SET Date = date(Date)");

                cnn.Close();
            }
        }
        public static void SaveOrUpdateFloorplanAndSections(Floorplan floorplan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                var existingFloorplan = cnn.QueryFirstOrDefault<Floorplan>(
                "SELECT * FROM Floorplan WHERE Date = @Date AND IsLunch = @IsLunch AND DiningAreaID = @DiningAreaID",
                new
                {
                    Date = floorplan.Date.Date.ToString("yyyy-MM-dd"),
                    IsLunch = floorplan.IsLunch,
                    DiningAreaID = floorplan.DiningArea.ID
                });

                if (existingFloorplan != null)
                {
                    // 1. Update the Floorplan itself
                   // cnn.Execute("UPDATE Floorplan SET ... WHERE ID = @ID", new { ID = existingFloorplan.ID, ... });

                    // 2. Delete the related sections and their associated data
                    var relatedSectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID }).ToList();
                    foreach (var sectionId in relatedSectionIds)
                    {
                        cnn.Execute("DELETE FROM ServerSections WHERE SectionID = @SectionID", new { SectionID = sectionId });
                        cnn.Execute("DELETE FROM Shift WHERE SectionID = @SectionID", new { SectionID = sectionId });
                    }
                    cnn.Execute("DELETE FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID });

                    floorplan.ID = existingFloorplan.ID; // We'll reuse this ID when inserting new data
                }
                else
                {
                    cnn.Execute("INSERT INTO Floorplan (Date, IsLunch, DiningAreaID) VALUES (date(@Date), @IsLunch, @DiningAreaID)",
                    new
                    {
                        Date = floorplan.Date,
                        IsLunch = floorplan.IsLunch,
                        DiningAreaID = floorplan.DiningArea.ID
                    });

                    floorplan.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();
                }

                // Save (or update) each section
                foreach (Section section in floorplan.Sections)
                {
                    SaveSection(section); // Assuming SaveSection does a save or update logic too

                    // Link this section with the floorplan
                    cnn.Execute("INSERT INTO FloorplanSections (FloorplanID, SectionID) VALUES (@FloorplanID, @SectionID)",
                    new
                    {
                        FloorplanID = floorplan.ID,
                        SectionID = section.ID
                    });

                    if (section.Server != null)
                    {
                        cnn.Execute("INSERT OR IGNORE INTO ServerSections (ServerID, SectionID) VALUES (@ServerID, @SectionID)",
                        new
                        {
                            ServerID = section.Server.ID,
                            SectionID = section.ID
                        });

                        cnn.Execute("INSERT OR IGNORE INTO Shift (ServerID, SectionID, FloorplanID, DiningAreaID) VALUES (@ServerID, @SectionID, @FloorplanID, @DiningAreaID)",
                        new
                        {
                            ServerID = section.Server.ID,
                            SectionID = section.ID,
                            FloorplanID = floorplan.ID,
                            DiningAreaID = floorplan.DiningArea.ID
                        });
                    }
                }
                cnn.Close();
            }
        }
        public static bool CheckIfFloorplanExistsForDate(DateOnly dateOnly, bool isLunch, int diningAreaID)
        {
            string fpDateOnlyString =dateOnly.ToString("yyyy-MM-dd");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                var existingFloorplan = cnn.QueryFirstOrDefault<Floorplan>(
            "SELECT * FROM Floorplan WHERE Date = @Date AND IsLunch = @IsLunch AND DiningAreaID = @DiningAreaID",
            new
            {
                Date = fpDateOnlyString,
                IsLunch = isLunch,
                DiningAreaID = diningAreaID
            });

                if (existingFloorplan != null)
                { 
                        cnn.Close();
                        return true;                    
                }
                cnn.Close();
                return false;
            

            }
        }
        public static int GetFloorplanCountsForShift(DateOnly dateOnly, bool isAM)
        {
            string dateOnlyString = dateOnly.ToString("yyyy-MM-dd"); // Convert DateOnly to string format for querying

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                // SQL query to count the floorplans that match the given date and IsLunch/IsAM value
                string query = @"
                    SELECT COUNT(*)
                    FROM Floorplan
                    WHERE Date = @Date AND IsLunch = @IsLunch";

                // Execute the query and return the count
                int count = cnn.QuerySingle<int>(query, new
                {
                    Date = dateOnlyString,
                    IsLunch = isAM
                });

                return count;
            }
        }


        public static bool SaveFloorplanAndSections(Floorplan floorplan)
        {
           
            string fpDateOnlyString = DateOnly.FromDateTime(floorplan.Date).ToString("yyyy-MM-dd");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                var existingFloorplan = cnn.QueryFirstOrDefault<Floorplan>(
            "SELECT * FROM Floorplan WHERE Date = @Date AND IsLunch = @IsLunch AND DiningAreaID = @DiningAreaID",
            new
            {
                Date = fpDateOnlyString,  
                IsLunch = floorplan.IsLunch,
                DiningAreaID = floorplan.DiningArea.ID
            });

                if (existingFloorplan != null)
                {
                    DialogResult result = MessageBox.Show("A floorplan for this area and shift already exists. Do you want to replace it?",
                                                 "Replace Floorplan?",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        cnn.Close();
                        return false;
                    }

                    var relatedSectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID }).ToList();

                    foreach (var sectionId in relatedSectionIds)
                    {
                        cnn.Execute("DELETE FROM ServerSections WHERE SectionID = @SectionID", new { SectionID = sectionId });
                        cnn.Execute("DELETE FROM Shift WHERE SectionID = @SectionID", new { SectionID = sectionId });
                    }
                    cnn.Execute("DELETE FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID });                   
                    cnn.Execute("DELETE FROM Floorplan WHERE ID = @ID", new { ID = existingFloorplan.ID });
                }
                
                cnn.Execute("INSERT INTO Floorplan (Date, IsLunch, DiningAreaID, ServerCount) VALUES (date(@Date), @IsLunch, @DiningAreaID, @ServerCount)",
                new
                {
                    Date = floorplan.Date,
                    IsLunch = floorplan.IsLunch,
                    DiningAreaID = floorplan.DiningArea.ID,
                    ServerCount = floorplan.Servers.Count,
                });

                floorplan.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();

               
                foreach (Section section in floorplan.Sections)
                {
                    SaveSection(section); 
                    cnn.Execute("INSERT INTO FloorplanSections (FloorplanID, SectionID) VALUES (@FloorplanID, @SectionID)",
                    new
                    {
                        FloorplanID = floorplan.ID,
                        SectionID = section.ID
                    });                  
                    if (section.ServerTeam.Count >= 1)
                    {                       
                        foreach (Server server in section.ServerTeam)
                        {
                            cnn.Execute("INSERT OR IGNORE INTO Shift (ServerID, SectionID, FloorplanID, DiningAreaID) " +
                                "VALUES (@ServerID, @SectionID, @FloorplanID, @DiningAreaID)",
                                           new
                                           {
                                               ServerID = server.ID,
                                               SectionID = section.ID,
                                               FloorplanID = floorplan.ID,
                                               DiningAreaID = floorplan.DiningArea.ID
                                           }); 
                        }
                    }
                }

                cnn.Close();
            }
            IncrementTemplatesUsed(floorplan);
            return true;
        }

        private static void IncrementTemplatesUsed(Floorplan floorplan)
        {
            FloorplanTemplate template = new FloorplanTemplate(floorplan);
            FloorplanTemplate duplicateTemplate = template.duplicateTemplate();

            if (duplicateTemplate != null)
            {
                using (var connection = new SQLiteConnection(LoadConnectionString()))
                {
                    string updateQuery = "UPDATE FloorplanTemplate SET TimesUsed = TimesUsed + 1 WHERE ID = @ID";
                    connection.Execute(updateQuery, new { ID = duplicateTemplate.ID });
                }
            }
        }

        public static void DeleteFloorplan(Floorplan floorplan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var existingFloorplan = cnn.QueryFirstOrDefault<Floorplan>(
                    "SELECT * FROM Floorplan WHERE ID = @ID", new {ID = floorplan.ID});
               
                var relatedSectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID }).ToList();
                cnn.Execute("DELETE FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID });
                // 2. Delete entries from ServerSections and Shift using the related SectionIDs
                foreach (var sectionId in relatedSectionIds)
                {
                    cnn.Execute("DELETE FROM ServerSections WHERE SectionID = @SectionID", new { SectionID = sectionId });
                    cnn.Execute("DELETE FROM Shift WHERE SectionID = @SectionID", new { SectionID = sectionId });
                    cnn.Execute("DELETE FROM Section WHERE ID = @ID", new { ID = sectionId });

                }

                // 3. Delete entries from FloorplanSections


                // 4. Finally, delete the Floorplan
                cnn.Execute("DELETE FROM Floorplan WHERE ID = @ID", new { ID = existingFloorplan.ID });


            }
        }
        public static void DeleteAllFloorplans()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // 1. Retrieve all floorplans
                var allFloorplans = cnn.Query<Floorplan>("SELECT * FROM Floorplan").ToList();

                foreach (var floorplan in allFloorplans)
                {
                    // 2. Delete associated records for each floorplan
                    var relatedSectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID }).ToList();

                    cnn.Execute("DELETE FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID });

                    foreach (var sectionId in relatedSectionIds)
                    {
                        cnn.Execute("DELETE FROM ServerSections WHERE SectionID = @SectionID", new { SectionID = sectionId });
                        cnn.Execute("DELETE FROM Shift WHERE SectionID = @SectionID", new { SectionID = sectionId });
                        cnn.Execute("DELETE FROM Section WHERE ID = @ID", new { ID = sectionId });
                    }
                }

                // 3. Finally, delete all floorplan records
                cnn.Execute("DELETE FROM Floorplan");
            }
        }

        public static List<Floorplan> LoadFloorplanList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Load all basic Floorplan details
                List<Floorplan> allFloorplans = cnn.Query<Floorplan>("SELECT * FROM Floorplan").ToList();

                foreach (var floorplan in allFloorplans)
                {
                    // Load associated DiningArea
                   
                    floorplan.DiningArea = cnn.QueryFirstOrDefault<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = floorplan.DiningAreaID });


                   
                    var sectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID });
                    foreach (var id in sectionIds)
                    {
                        var section = cnn.QuerySingle<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = id });
                        // Populate Tables for each Section from SectionTables
                        section.SetTableList(cnn.Query<Table>(
                            "SELECT * FROM DiningTable WHERE ID IN (SELECT TableID FROM SectionTables WHERE SectionID = @SectionID)",
                            new { SectionID = id }).ToList());
                        floorplan.AddSection(section);
                    }

                    var servers = new List<Server>();

                    // Fetch the server-section relationships directly as anonymous types
                    var serverSections = cnn.Query("SELECT * FROM ServerSections WHERE SectionID IN @SectionIds", new { SectionIds = sectionIds })
                                             .Select(x => new { ServerID = (int)x.ServerID, SectionID = (int)x.SectionID })
                                             .ToList();

                    foreach (var ss in serverSections)
                    {
                        // Load server details only once per server
                        if (!servers.Any(s => s.ID == ss.ServerID))
                        {
                            var server = cnn.QuerySingle<Server>("SELECT * FROM Server WHERE ID = @ID", new { ID = ss.ServerID });
                            servers.Add(server);
                            //floorplan.Servers.Add(server);
                        }

                        // Associate server with their respective section
                        var matchedSection = floorplan.Sections.FirstOrDefault(s => s.ID == ss.SectionID);
                        if (matchedSection != null)
                        {
                            matchedSection.AddServer(servers.First(s => s.ID == ss.ServerID));
                        }
                    }

                    // Add loaded servers to floorplan's Servers list\
                    //floorplan.Servers = servers;
                    foreach (var section in floorplan.Sections)
                    {
                        if (section.Server != null)
                        {
                            floorplan.Servers.Add(section.Server);
                        }
                    }
                }
                return allFloorplans;
            }


        }

       public static List<EmployeeShift> GetShiftsForServer(Server server)
{
    using IDbConnection dbConnection = new SQLiteConnection(LoadConnectionString());
    {
        dbConnection.Open();

        string query = @"
        SELECT 
            s.ID,
            f.Date,
            f.IsLunch,
            s.FloorplanID,
            s.SectionID,
            s.ServerID,
            s.DiningAreaID,
            sec.IsCloser,
            sec.IsPre,
            d.IsInside,
            d.IsCockTail,
            sec.TeamWait AS IsTeamWait,
            sec.IsPickUp
        FROM Shift s
        INNER JOIN Floorplan f ON s.FloorplanID = f.ID
        INNER JOIN Section sec ON s.SectionID = sec.ID
        INNER JOIN DiningArea d ON s.DiningAreaID = d.ID
        WHERE s.ServerID = @ServerID";

        var shifts = dbConnection.Query<EmployeeShift>(query, new { ServerID = server.ID }).ToList();

        // Group shifts by Date and IsLunch
        var groupedShifts = shifts
            .GroupBy(shift => new { shift.Date, shift.isLunch })
            .Select(g =>
            {
                var mainShift = g.FirstOrDefault(s => !s.HasPickUp);
                if (mainShift != null)
                {
                    mainShift.HasPickUp = g.Any(s => s.HasPickUp && s != mainShift);
                    return mainShift;
                }
                return g.First(); // In case all shifts are PickUp (fallback)
            })
            .ToList();

        return groupedShifts;
    }
}


        public static void SaveIgnoredPair(string pair)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "INSERT INTO IgnoredPairs (Pair) VALUES (@Pair);";
                cnn.Execute(sql, new { Pair = pair });
            }            
        }
        public static void DeleteIgnoredPair(string pair)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "DELETE FROM IgnoredPairs WHERE Pair = @Pair;";
                cnn.Execute(sql, new { Pair = pair });
            }            
        }
        public static HashSet<string> LoadAllIgnoredPairs()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "SELECT Pair FROM IgnoredPairs;";
                return new HashSet<string>(cnn.Query<string>(sql));

            }
           
        }
        public static HashSet<string> LoadAllCustomPairs()
        {
            HashSet<string> customPairs = new HashSet<string>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Load CustomRightLeftNeighbors
                string sqlRightLeft = "SELECT RightBorder, LeftBorder FROM CustomRightLeftNeighbors;";
                var rightLeftPairs = cnn.Query<(string RightBorder, string LeftBorder)>(sqlRightLeft);
                foreach (var pair in rightLeftPairs)
                {
                    string pairKey = GetPairKey(pair.RightBorder, pair.LeftBorder);
                    customPairs.Add(pairKey);
                }

                // Load CustomTopBottomNeighbors
                string sqlTopBottom = "SELECT TopBorder, BottomBorder FROM CustomTopBottomNeighbors;";
                var topBottomPairs = cnn.Query<(string TopBorder, string BottomBorder)>(sqlTopBottom);
                foreach (var pair in topBottomPairs)
                {
                    string pairKey = GetPairKey(pair.TopBorder, pair.BottomBorder);
                    customPairs.Add(pairKey);
                }
            }

            return customPairs;
        }
        public static HashSet<string> LoadTopBotCustomPairs()
        {
            HashSet<string> customPairs = new HashSet<string>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
               
                // Load CustomTopBottomNeighbors
                string sqlTopBottom = "SELECT TopBorder, BottomBorder FROM CustomTopBottomNeighbors;";
                var topBottomPairs = cnn.Query<(string TopBorder, string BottomBorder)>(sqlTopBottom);
                foreach (var pair in topBottomPairs)
                {
                    string pairKey = GetPairKey(pair.TopBorder, pair.BottomBorder);
                    customPairs.Add(pairKey);
                }
            }

            return customPairs;
        }
        public static HashSet<string> LoadRightLeftCustomPairs()
        {
            HashSet<string> customPairs = new HashSet<string>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Load CustomRightLeftNeighbors
                string sqlRightLeft = "SELECT RightBorder, LeftBorder FROM CustomRightLeftNeighbors;";
                var rightLeftPairs = cnn.Query<(string RightBorder, string LeftBorder)>(sqlRightLeft);
                foreach (var pair in rightLeftPairs)
                {
                    string pairKey = GetPairKey(pair.RightBorder, pair.LeftBorder);
                    customPairs.Add(pairKey);
                }

            }

            return customPairs;
        }
        private static string GetPairKey(string borderOne, string borderTwo)
        {
            return borderOne.CompareTo(borderTwo) < 0 ? borderOne + "-" + borderTwo : borderTwo + "-" + borderOne;
        }
        public static TopBottomNeighbor LoadCustomTopBottomNeighbor(TableEdgeBorders topBorder, TableEdgeBorders bottomBorder)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = @"
            SELECT MidLocation, StartPoint, EndPoint 
            FROM CustomTopBottomNeighbors 
            WHERE TopBorder = @TopBorder AND BottomBorder = @BottomBorder;";

                var record = cnn.QueryFirstOrDefault<(int MidLocation, int StartPoint, int EndPoint)>(sql, new { TopBorder = topBorder.Table.TableNumber, BottomBorder = bottomBorder.Table.TableNumber });

                if (record != default)
                {
                    return new TopBottomNeighbor(record.MidLocation, record.StartPoint, record.EndPoint, topBorder, bottomBorder);
                }

                return null; // Or handle the case where no record is found
            }
        }
        public static RightLeftNeighbor LoadCustomRightLeftNeighbor(TableEdgeBorders rightBorder, TableEdgeBorders leftBorder)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = @"
            SELECT MidLocation, StartPoint, EndPoint 
            FROM CustomRightLeftNeighbors 
            WHERE RightBorder = @RightBorder AND LeftBorder = @LeftBorder;";

                var record = cnn.QueryFirstOrDefault<(int MidLocation, int StartPoint, int EndPoint)>(sql, new { RightBorder = rightBorder.Table.TableNumber, LeftBorder = leftBorder.Table.TableNumber });

                if (record != default)
                {
                    return new RightLeftNeighbor(record.MidLocation, record.StartPoint, record.EndPoint, rightBorder, leftBorder);
                }

                return null; // Or handle the case where no record is found
            }
        }


        public static void SaveTopBottomNeighbor(TopBottomNeighbor neighbor)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string checkSql = "SELECT Id FROM CustomTopBottomNeighbors WHERE TopBorder = @TopBorder AND BottomBorder = @BottomBorder;";
                var existingId = cnn.Query<int?>(checkSql, new { TopBorder = neighbor.TopNeighbor.Table.TableNumber, BottomBorder = neighbor.BottomNeighbor.Table.TableNumber }).FirstOrDefault();

                if (existingId.HasValue)
                {
                    // Update existing record
                    var updateSql = @"
                UPDATE CustomTopBottomNeighbors 
                SET MidLocation = @MidLocation, StartPoint = @StartPoint, EndPoint = @EndPoint 
                WHERE Id = @Id;";
                    cnn.Execute(updateSql, new { MidLocation = neighbor.MidPoint, StartPoint = neighbor.Start, EndPoint = neighbor.End, Id = existingId.Value });
                }
                else
                {
                    // Insert new record
                    var insertSql = @"
                INSERT INTO CustomTopBottomNeighbors (MidLocation, StartPoint, EndPoint, TopBorder, BottomBorder) 
                VALUES (@MidLocation, @StartPoint, @EndPoint, @TopBorder, @BottomBorder);";
                    cnn.Execute(insertSql, new { MidLocation = neighbor.MidPoint, StartPoint = neighbor.Start, EndPoint = neighbor.End, TopBorder = neighbor.TopNeighbor.Table.TableNumber, BottomBorder = neighbor.BottomNeighbor.Table.TableNumber });
                }
            }
        }
        public static void SaveRightLeftNeighbor(RightLeftNeighbor neighbor)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string checkSql = "SELECT Id FROM CustomRightLeftNeighbors WHERE RightBorder = @RightBorder AND LeftBorder = @LeftBorder;";
                var existingId = cnn.Query<int?>(checkSql, new { RightBorder = neighbor.RightNeighbor.Table.TableNumber, LeftBorder = neighbor.LeftNeighbor.Table.TableNumber }).FirstOrDefault();

                if (existingId.HasValue)
                {
                    // Update existing record
                    var updateSql = @"
                UPDATE CustomRightLeftNeighbors 
                SET MidLocation = @MidLocation, StartPoint = @StartPoint, EndPoint = @EndPoint 
                WHERE Id = @Id;";
                    cnn.Execute(updateSql, new { MidLocation = neighbor.MidPoint, StartPoint = neighbor.Start, EndPoint = neighbor.End, Id = existingId.Value });
                }
                else
                {
                    // Insert new record
                    var insertSql = @"
                INSERT INTO CustomRightLeftNeighbors (MidLocation, StartPoint, EndPoint, RightBorder, LeftBorder) 
                VALUES (@MidLocation, @StartPoint, @EndPoint, @RightBorder, @LeftBorder);";
                    cnn.Execute(insertSql, new { MidLocation = neighbor.MidPoint, StartPoint = neighbor.Start, EndPoint = neighbor.End, RightBorder = neighbor.RightNeighbor.Table.TableNumber, LeftBorder = neighbor.LeftNeighbor.Table.TableNumber });
                }
            }
        }


        //public static void SaveRightLeftNeighbor(RightLeftNeighbor neighbor)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var sql = @"
        //    INSERT INTO CustomRightLeftNeighbors (MidLocation, StartPoint, EndPoint, RightBorder, LeftBorder) 
        //    VALUES (@MidLocation, @StartPoint, @EndPoint, @RightBorder, @LeftBorder);";

        //        var parameters = new
        //        {
        //            MidLocation = neighbor.MidPoint,
        //            StartPoint = neighbor.Start,
        //            EndPoint = neighbor.End,
        //            RightBorder = neighbor.RightNeighbor.Table.TableNumber,
        //            LeftBorder = neighbor.LeftNeighbor.Table.TableNumber
        //        };

        //        cnn.Execute(sql, parameters);
        //    }
        //}


        public static void DeleteTopBottomNeighbor(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "DELETE FROM CustomTopBottomNeighbors WHERE Id = @Id;";
                cnn.Execute(sql, new { Id = id });
            }
        }

        public static void DeleteRightLeftNeighbor(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "DELETE FROM CustomRightLeftNeighbors WHERE Id = @Id;";
                cnn.Execute(sql, new { Id = id });
            }
        }
        public static List<TopBottomNeighbor> LoadAllTopBottomNeighbors(List<TableEdgeBorders> tableEdgeBorders)
        {
            List<TopBottomNeighbor> neighbors = new List<TopBottomNeighbor>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM CustomTopBottomNeighbors";
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Assume columns are similar to RightLeftNeighbors, replace with actual column names
                            int midLocation = reader.GetInt32(reader.GetOrdinal("MidLocation"));
                            int startPoint = reader.GetInt32(reader.GetOrdinal("StartPoint"));
                            int endPoint = reader.GetInt32(reader.GetOrdinal("EndPoint"));
                            string topBorder = reader.GetString(reader.GetOrdinal("TopBorder"));
                            string bottomBorder = reader.GetString(reader.GetOrdinal("BottomBorder"));

                            TableEdgeBorders topBorderObj = tableEdgeBorders.FirstOrDefault(t => t.Table.TableNumber == topBorder);
                            TableEdgeBorders bottomBorderObj = tableEdgeBorders.FirstOrDefault(t => t.Table.TableNumber == bottomBorder);

                            if (topBorderObj != null && bottomBorderObj != null)
                            {
                                TopBottomNeighbor neighbor = new TopBottomNeighbor(midLocation, startPoint, endPoint, topBorderObj, bottomBorderObj);
                                topBorderObj.AddNeighbor(neighbor);
                                bottomBorderObj.AddNeighbor(neighbor);
                                neighbors.Add(neighbor);
                            }
                        }
                    }
                }
            }

            return neighbors;
        }


        public static List<RightLeftNeighbor> LoadAllRightLeftNeighbors(List<TableEdgeBorders> tableEdgeBorders)
        {
            List<RightLeftNeighbor> neighbors = new List<RightLeftNeighbor>();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM CustomRightLeftNeighbors";
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int midLocation = reader.GetInt32(reader.GetOrdinal("MidLocation"));
                            int startPoint = reader.GetInt32(reader.GetOrdinal("StartPoint"));
                            int endPoint = reader.GetInt32(reader.GetOrdinal("EndPoint"));
                            string rightBorder = reader.GetString(reader.GetOrdinal("RightBorder"));
                            string leftBorder = reader.GetString(reader.GetOrdinal("LeftBorder"));

                            TableEdgeBorders rightBorderObj = tableEdgeBorders.FirstOrDefault(t => t.Table.TableNumber == rightBorder);
                            TableEdgeBorders leftBorderObj = tableEdgeBorders.FirstOrDefault(t => t.Table.TableNumber == leftBorder);

                            if (rightBorderObj != null && leftBorderObj != null)
                            {
                                RightLeftNeighbor neighbor = new RightLeftNeighbor(midLocation, startPoint, endPoint, rightBorderObj, leftBorderObj);
                                rightBorderObj.AddNeighbor(neighbor);
                                leftBorderObj.AddNeighbor(neighbor);
                                neighbors.Add(neighbor);
                            }
                        }
                    }
                }
            }

            return neighbors;
        }
        public static void SaveNewEventDate(SpecialEventDate specialEvent)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = @"
            INSERT INTO SpecialEventDates (DateOnly, Type, ShouldIgnoreSales, Name) 
            VALUES (@DateOnly, @Type, @ShouldIgnoreSales, @Name)";

                var parameters = new
                {                   
                    DateOnly = specialEvent.DateOnly.ToString("yyyy-MM-dd"),
                    Type = specialEvent.Type.ToString(),
                    ShouldIgnoreSales = specialEvent.ShouldIgnoreSales,
                    Name = specialEvent.Name 
                };

                cnn.Execute(sql, parameters);
            }
        }
        public static List<SpecialEventDate> LoadSpecialEvents()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var specialEventDates = cnn.Query<dynamic>("SELECT Id, DateOnly, Type, ShouldIgnoreSales, Name FROM SpecialEventDates").ToList();

                List<SpecialEventDate> results = new List<SpecialEventDate>();

                foreach (var row in specialEventDates)
                {
                    long id = row.Id;
                    DateOnly dateOnly = DateOnly.Parse((string)row.DateOnly);
                    SpecialEventDate.OutlierType type = Enum.Parse<SpecialEventDate.OutlierType>((string)row.Type);
                    bool shouldIgnoreSales = row.ShouldIgnoreSales == 1;
                    string name = row.Name;

                    results.Add(new SpecialEventDate((int)id, dateOnly, type, shouldIgnoreSales, name));
                }

                return results;
            }
        }
        public static SpecialEventDate GetEventByDate(DateOnly date)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "SELECT Id, DateOnly, Type, ShouldIgnoreSales, Name FROM SpecialEventDates WHERE DateOnly = @DateOnly LIMIT 1";
                var parameters = new { DateOnly = date.ToString("yyyy-MM-dd") };

                var result = cnn.QueryFirstOrDefault(sql, parameters);

                if (result != null)
                {
                    return new SpecialEventDate(
                        (int)result.Id,
                        DateOnly.Parse(result.DateOnly),
                        Enum.Parse<SpecialEventDate.OutlierType>(result.Type),
                        result.ShouldIgnoreSales == 0,
                        result.Name
                    );
                }
                return null;
            }
        }

        public static void SaveNewWeatherData(WeatherData weatherData)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = @"
            INSERT INTO WeatherData (
                Date, TempHi, TempLow, TempAvg, FeelsLikeHi, FeelsLikeLow, FeelsLikeAvg,
                CloudCover, Precipitation, PrecipitationCover, PrecipitationType,
                WindSpeedMax, WindSpeedAvg
            ) 
            VALUES (
                @Date, @TempHi, @TempLow, @TempAvg, @FeelsLikeHi, @FeelsLikeLow, @FeelsLikeAvg,
                @CloudCover, @Precipitation, @PrecipitationCover, @PrecipitationType,
                @WindSpeedMax, @WindSpeedAvg
            )";

                var parameters = new
                {
                    Date = weatherData.DateOnly.ToString("yyyy-MM-dd"), // Use DateOnly for conversion
                    weatherData.TempHi,
                    weatherData.TempLow,
                    weatherData.TempAvg,
                    weatherData.FeelsLikeHi,
                    weatherData.FeelsLikeLow,
                    weatherData.FeelsLikeAvg,
                    weatherData.CloudCover,
                    weatherData.Precipitation,
                    weatherData.PrecipitationCover,
                    weatherData.PrecipitationType,
                    weatherData.WindSpeedMax,
                    weatherData.WindSpeedAvg
                };

                cnn.Execute(sql, parameters);
            }
        }



        public static WeatherData LoadWeatherDataByDate(DateOnly date)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.QuerySingleOrDefault<WeatherData>("SELECT * FROM WeatherData WHERE Date = @Date",
                                                      new { Date = date.ToString("yyyy-MM-dd") });
            }
        }

        public static List<WeatherData> LoadAllWeatherData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<WeatherData>("SELECT * FROM WeatherData").ToList();
                                             //, new { Date = date.ToString("yyyy-MM-dd") }).ToList();
            }
        }

        public static List<WeatherData> LoadWeatherDataByDateRange(DateOnly startDate, DateOnly endDate)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<WeatherData>("SELECT * FROM WeatherData WHERE Date BETWEEN @StartDate AND @EndDate",
                                              new
                                              {
                                                  StartDate = startDate.ToString("yyyy-MM-dd"),
                                                  EndDate = endDate.ToString("yyyy-MM-dd")
                                              }).ToList();
            }
        }

        public static List<WeatherData> LoadWeatherDataByDateTimes(List<DateTime> dateTimes)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var dateStrings = dateTimes.Select(dateTime => dateTime.ToString("yyyy-MM-dd")).ToList();
                string sql = $"SELECT * FROM WeatherData WHERE Date IN ({string.Join(",", dateStrings.Select((s, i) => $"@Date{i}"))})";

                var parameters = new DynamicParameters();
                for (int i = 0; i < dateStrings.Count; i++)
                {
                    parameters.Add($"@Date{i}", dateStrings[i]);
                }

                var weatherDataList = cnn.Query<dynamic>(sql, parameters).ToList();

                List<WeatherData> results = new List<WeatherData>();

                foreach (var row in weatherDataList)
                {
                    var weatherData = new WeatherData
                    {
                        ID = (int)row.ID,
                        Date = row.Date,
                        TempHi = (int)row.TempHi,
                        TempLow = (int)row.TempLow,
                        TempAvg = (int)row.TempAvg,
                        FeelsLikeHi = (int)row.FeelsLikeHi,
                        FeelsLikeLow = (int)row.FeelsLikeLow,
                        FeelsLikeAvg = (int)row.FeelsLikeAvg
                    };

                    results.Add(weatherData);
                }

                return results;
            }
        }


        public static List<WeatherData> LoadWeatherDataByDates(List<DateOnly> dates)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Convert the list of dates to a list of strings in the 'yyyy-MM-dd' format
                var dateStrings = dates.Select(date => date.ToString("yyyy-MM-dd")).ToList();

                // Generate a SQL query with the appropriate number of parameters
                string sql = $"SELECT * FROM WeatherData WHERE Date IN ({string.Join(",", dateStrings.Select((s, i) => $"@Date{i}"))})";

                // Create a dynamic parameters object
                var parameters = new DynamicParameters();
                for (int i = 0; i < dateStrings.Count; i++)
                {
                    parameters.Add($"@Date{i}", dateStrings[i]);
                }

                return cnn.Query<WeatherData>(sql, parameters).ToList();
            }
        }
        public static void SaveOrUpdateWeatherData(WeatherData weatherData)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var existingData = cnn.Query<WeatherData>("SELECT * FROM WeatherData WHERE Date = @Date", new { Date = weatherData.DateOnly.ToString("yyyy-MM-dd") }).FirstOrDefault();

                if (existingData == null)
                {
                    SaveNewWeatherData(weatherData); // Use the existing SaveNewWeatherData method
                }
                else
                {
                    string sql = @"
                UPDATE WeatherData 
                SET 
                    TempHi = @TempHi, 
                    TempLow = @TempLow, 
                    TempAvg = @TempAvg, 
                    FeelsLikeHi = @FeelsLikeHi, 
                    FeelsLikeLow = @FeelsLikeLow, 
                    FeelsLikeAvg = @FeelsLikeAvg,
                    CloudCover = @CloudCover,
                    Precipitation = @Precipitation,
                    PrecipitationCover = @PrecipitationCover,
                    PrecipitationType = @PrecipitationType,
                    WindSpeedMax = @WindSpeedMax,
                    WindSpeedAvg = @WindSpeedAvg
                WHERE Date = @Date";

                    var parameters = new
                    {
                        Date = weatherData.DateOnly.ToString("yyyy-MM-dd"), // Use DateOnly for conversion
                        weatherData.TempHi,
                        weatherData.TempLow,
                        weatherData.TempAvg,
                        weatherData.FeelsLikeHi,
                        weatherData.FeelsLikeLow,
                        weatherData.FeelsLikeAvg,
                        weatherData.CloudCover,
                        weatherData.Precipitation,
                        weatherData.PrecipitationCover,
                        weatherData.PrecipitationType,
                        weatherData.WindSpeedMax,
                        weatherData.WindSpeedAvg
                    };

                    cnn.Execute(sql, parameters);
                }
            }
        }

       
        public static void SaveOrUpdateHourlyWeatherData(List<HourlyWeatherData> hourlyWeatherDatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = @"
                INSERT OR REPLACE INTO HourlyWeatherData (
                    Date, Time, TempHi, TempLow, TempAvg, FeelsLikeHi, FeelsLikeLow, FeelsLikeAvg,
                    CloudCover, PrecipitationAmount, SnowAmount, PrecipitationType,
                    WindSpeedMax, WindSpeedAvg
                ) 
                VALUES (
                    @Date, @Time, @TempHi, @TempLow, @TempAvg, @FeelsLikeHi, @FeelsLikeLow, @FeelsLikeAvg,
                    @CloudCover, @PrecipitationAmount, @SnowAmount, @PrecipitationType,
                    @WindSpeedMax, @WindSpeedAvg
                )";

                foreach (var hourlyData in hourlyWeatherDatas)
                {
                    var parameters = new
                    {
                        Date = hourlyData.Date.ToString("yyyy-MM-dd"), // Use Date for the date part
                        Time = hourlyData.Date.ToString("HH:mm"), // Extract time as HH:mm
                        TempHi = (object)hourlyData.TempHi ?? DBNull.Value,
                        TempLow = (object)hourlyData.TempLow ?? DBNull.Value,
                        TempAvg = (object)hourlyData.TempAvg ?? DBNull.Value,
                        FeelsLikeHi = (object)hourlyData.FeelsLikeHi ?? DBNull.Value,
                        FeelsLikeLow = (object)hourlyData.FeelsLikeLow ?? DBNull.Value,
                        FeelsLikeAvg = (object)hourlyData.FeelsLikeAvg ?? DBNull.Value,
                        CloudCover = (object)hourlyData.CloudCover ?? DBNull.Value,
                        PrecipitationAmount = (object)hourlyData.PrecipitationAmount ?? DBNull.Value,
                        SnowAmount = (object)hourlyData.SnowAmount_CM ?? DBNull.Value,
                        PrecipitationType = (object)hourlyData.PrecipitationType ?? DBNull.Value,
                        WindSpeedMax = (object)hourlyData.WindSpeedMax ?? DBNull.Value,
                        WindSpeedAvg = (object)hourlyData.WindSpeedAvg ?? DBNull.Value
                    };

                    cnn.Execute(sql, parameters);
                }
            }
        }

        public static ShiftWeather LoadHourlyWeatherData(DateOnly dateOnly, bool isLunch)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {                
                string timeStart = isLunch ? "11:00" : "16:00"; 
                string timeEnd = isLunch ? "15:59" : "23:59"; 
                
                string date = dateOnly.ToString("yyyy-MM-dd");
                
                string sql = @"
                    SELECT * FROM HourlyWeatherData
                    WHERE Date = @Date AND Time BETWEEN @TimeStart AND @TimeEnd
                    ORDER BY Time";
               
                var parameters = new
                {
                    Date = date,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd
                };

                var data = cnn.Query(sql, parameters).ToList();

                var result = data.Select(item => new HourlyWeatherData
                {
                    ID = Convert.ToInt32(item.ID), // Explicitly convert from long to int
                    Date = DateTime.Parse($"{item.Date} {item.Time}"),
                    TempHi = item.TempHi != null ? Convert.ToInt32(item.TempHi) : 0, // Ensure conversion
                    TempLow = item.TempLow != null ? Convert.ToInt32(item.TempLow) : 0,
                    TempAvg = item.TempAvg != null ? Convert.ToInt32(item.TempAvg) : 0,
                    FeelsLikeHi = item.FeelsLikeHi != null ? Convert.ToInt32(item.FeelsLikeHi) : 0,
                    FeelsLikeLow = item.FeelsLikeLow != null ? Convert.ToInt32(item.FeelsLikeLow) : 0,
                    FeelsLikeAvg = item.FeelsLikeAvg != null ? Convert.ToInt32(item.FeelsLikeAvg) : 0,
                    CloudCover = item.CloudCover != null ? Convert.ToSingle(item.CloudCover) : 0f,
                    PrecipitationAmount = item.PrecipitationAmount != null ? Convert.ToSingle(item.PrecipitationAmount) : 0f,
                    PrecipitationChance = item.PrecipitationChance != null ? Convert.ToSingle(item.PrecipitationChance) : 0f,
                    SnowAmount_CM = item.SnowAmount != null ? Convert.ToSingle(item.SnowAmount) : 0f,
                    PrecipitationType = item.PrecipitationType != null ? Convert.ToString(item.PrecipitationType) : "",
                    WindSpeedMax = item.WindSpeedMax != null ? Convert.ToInt32(item.WindSpeedMax) : 0,
                    WindSpeedAvg = item.WindSpeedAvg != null ? Convert.ToInt32(item.WindSpeedAvg) : 0
                }).ToList();
                ShiftWeather shiftWeather = new ShiftWeather(result);
                return shiftWeather;
            }
        }

        public static ShiftRecord LoadShiftRecord(DateOnly date, bool isLunch)
        {
            ShiftRecord shiftRecord = new ShiftRecord
            {
                //ID = GenerateShiftID(date, isLunch), // Assuming you have a method to generate or retrieve the ID
                dateOnly = date,
                IsAm = isLunch,
                //Reservations = LoadReservations(date, isLunch), // Assuming you have a method to load reservations
                SpecialEventDate = GetEventByDate(date), // Assuming you have a method to load special event date
                //Sales = LoadTotalSales(date, isLunch), // Assuming you have a method to load total sales
            };

            shiftRecord.ShiftWeather = LoadHourlyWeatherData(date, isLunch);
            shiftRecord.DiningAreaRecords = LoadDiningAreaRecordsByDateAndLunch(date, isLunch);
            shiftRecord.tableStats = LoadTableStatsByDateAndLunch(isLunch, date);
            shiftRecord.Sales = (float)shiftRecord.tableStats.Where(ts => (ts.DiningAreaID != 6 && ts.DiningAreaID != 0)).ToList().Sum(ts => ts.Sales);

            return shiftRecord;
        }
        public static List<DiningAreaRecord> LoadDiningAreaRecordsByDateAndLunch(DateOnly date, bool isAm)
        {
            var tableStats = LoadTableStatsByDateAndLunch(isAm, date);

            // Dictionary to store ServerCount for each DiningAreaID
            var serverCounts = LoadServerCountsByDateAndLunch(date, isAm);

            // Calculate the total sales excluding Banquet (DiningAreaID = 6)
            float totalSales = tableStats
                .Where(ts => ts.DiningAreaID.HasValue && (ts.DiningAreaID != 6 || ts.DiningAreaID != 0))
                .Sum(ts => ts.Sales ?? 0);

            // Group tableStats by DiningAreaID and create DiningAreaRecords
            var diningAreaRecords = tableStats
                .Where(ts => ts.DiningAreaID.HasValue)
                .GroupBy(ts => ts.DiningAreaID.Value)
                .Select(g => new DiningAreaRecord {
                    DiningAreaID = g.Key,
                    DateOnly = date,
                    IsAm = isAm,
                    Sales = g.Sum(ts => ts.Sales ?? 0), // Sum of sales for this dining area
                    TableStats = g.ToList(),
                    ServerCount = serverCounts.ContainsKey(g.Key) ? serverCounts[g.Key] : 0,
                    PercentageOfSales = totalSales > 0 ? (g.Sum(ts => ts.Sales ?? 0) / totalSales) * 100 : 0 // Calculate percentage of total sales
                })
                .ToList();

            return diningAreaRecords;
        }

        public static Dictionary<int, int> LoadServerCountsByDateAndLunch(DateOnly date, bool isLunch) {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                string dateString = date.ToString("yyyy-MM-dd");

                var floorplanData = cnn.Query<(int DiningAreaID, int ServerCount)>(
                    @"SELECT DiningAreaID, ServerCount 
              FROM Floorplan 
              WHERE Date = @Date AND IsLunch = @IsLunch",
                    new { Date = dateString, IsLunch = isLunch }).ToList();

                return floorplanData.ToDictionary(x => x.DiningAreaID, x => x.ServerCount);
            }
        }

        public static List<DiningAreaRecord> LoadFloorplanRecordsByDateAndLunch(DateOnly date, bool isLunch)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string dateString = date.ToString("yyyy-MM-dd");

                var floorplanRecords = cnn.Query<DiningAreaRecord>(
                    @"SELECT ID, DiningAreaID, Date, Sales, IsLunch, ServerCount 
                      FROM Floorplan 
                      WHERE Date = @Date AND IsLunch = @IsLunch",
                    new { Date = dateString, IsLunch = isLunch }).ToList();

                foreach (var floorplan in floorplanRecords)
                {
                    floorplan.TableStats = LoadTableStatsByDiningAreaAndDate(floorplan.DiningAreaID, date, isLunch);
                    floorplan.Sales = (float)floorplan.TableStats.Sum(ts => ts.Sales);
                    floorplan.DateOnly = date;
                }

                return floorplanRecords;
            }
        }
        public static List<DiningAreaRecord> LoadDiningAreaRecords(DateOnly date, bool isAm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                string dateString = date.ToString("yyyy-MM-dd");
                string query = @"SELECT * FROM DiningAreaRecord WHERE Date = @Date AND IsAm = @IsAm";
                var queryResult = cnn.Query(query, new { Date = dateString, IsAm = isAm});
                var diningAreaRecords = queryResult.Select(row => new DiningAreaRecord {
                    ID = Convert.ToInt32(row.ID),
                    DiningAreaID = Convert.ToInt32(row.DiningAreaID),
                    DateOnly = DateOnly.Parse(row.Date),
                    IsAm = Convert.ToBoolean(row.IsAm),
                    Sales = Convert.ToSingle(row.Sales),
                    ServerCount = Convert.ToInt32(row.ServerCount),
                    PercentageOfSales = Convert.ToSingle(row.PercentageOfSales),
                    TableStats = new List<TableStat>()
                }).ToList();


                return diningAreaRecords;
            }
        }
        public static List<TableStat> LoadTableStatsByDiningAreaAndDate(int DiningAreaID, DateOnly date, bool isLunch)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Prepare your query with parameters
                var query = @"SELECT * FROM TableStats WHERE DiningAreaID = @DiningAreaID AND IsLunch = @IsLunch AND Date = @Date";

                // Execute the query with the provided parameters
                var queryResult = cnn.Query(query, new {DiningAreaID = DiningAreaID, IsLunch = isLunch, Date = date.ToString("yyyy-MM-dd") }).ToList();

                var tableStatsList = queryResult.Select(row => new TableStat
                {
                    // Assign other properties as necessary
                    TableStatNumber = row.TableStatNumber,
                    DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.DayOfWeek),
                    Date = DateOnly.Parse(row.Date),
                    IsLunch = Convert.ToBoolean(row.IsLunch),
                    Sales = row.Sales != null ? (float?)Convert.ToDouble(row.Sales) : null,
                    Orders = Convert.ToInt32(row.Orders)
                }).ToList();

                return tableStatsList;
            }
        }

        public static List<DiningAreaRecord> LoadDiningAreaRecords()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                // Load all DiningAreaRecords
                //string diningAreaSql = @"SELECT * FROM DiningAreaRecord WHERE IsAm = @IsAm";
                //var queryResult = cnn.Query(diningAreaSql, new {IsAm = isLunch});
                string diningAreaSql = @"SELECT * FROM DiningAreaRecord";
                var queryResult = cnn.Query(diningAreaSql);

                var diningAreaRecords = queryResult.Select(row => new DiningAreaRecord {
                    ID = Convert.ToInt32(row.ID),
                    DiningAreaID = Convert.ToInt32(row.DiningAreaID),
                    DateOnly = DateOnly.Parse(row.Date),
                    IsAm = Convert.ToBoolean(row.IsAm),
                    Sales = Convert.ToSingle(row.Sales),
                    ServerCount = Convert.ToInt32(row.ServerCount),
                    PercentageOfSales = Convert.ToSingle(row.PercentageOfSales),
                    TableStats = new List<TableStat>()
                }).ToList();

                // Load all DiningTableRecords and organize them by DiningAreaID
                string diningTableSql = @"SELECT * FROM DiningTableRecord";
                var allDiningTableRecords = cnn.Query(diningTableSql);

                var diningTableRecordsByArea = allDiningTableRecords.GroupBy(t => t.DiningAreaID)
                                                                    .ToDictionary(g => (int)g.Key, g => g.ToList());

                foreach (var record in diningAreaRecords) {
                    // Load existing TableStats for the DiningAreaRecord
                    //string tableStatsSql = @"SELECT * FROM TableStats WHERE DiningAreaRecordID = @DiningAreaRecordID AND IsLunch = @IsLunch";
                    //var queryTableResult = cnn.Query(tableStatsSql, new { DiningAreaRecordID = record.ID, IsLunch = isLunch });
                    string tableStatsSql = @"SELECT * FROM TableStats WHERE DiningAreaRecordID = @DiningAreaRecordID";
                    var queryTableResult = cnn.Query(tableStatsSql, new { DiningAreaRecordID = record.ID });

                    var tableStatsList = queryTableResult.Select(row => new TableStat {
                        TableStatNumber = row.TableStatNumber,
                        DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row.DayOfWeek),
                        Date = DateOnly.Parse(row.Date),
                        IsLunch = Convert.ToBoolean(row.IsLunch),
                        Sales = row.Sales != null ? (float?)Convert.ToDouble(row.Sales) : null,
                        Orders = Convert.ToInt32(row.Orders),
                        DiningAreaID = row.DiningAreaID != null ? (int?)Convert.ToInt32(row.DiningAreaID) : null,
                        DiningAreaRecordID = row.DiningAreaRecordID != null ? (int?)Convert.ToInt32(row.DiningAreaRecordID) : null
                    }).ToList();

                    // Ensure a TableStat exists for every table in the dining area
                    if (diningTableRecordsByArea.TryGetValue(record.DiningAreaID, out var diningTableRecords)) {
                        foreach (var tableRecord in diningTableRecords) {
                            string tableNumber = tableRecord.TableNumber;

                            // Check if a TableStat already exists for this table
                            bool exists = tableStatsList.Any(ts => ts.TableStatNumber == tableNumber);
                            if (!exists) {
                                // Create a new TableStat with $0 sales
                                var newTableStat = new TableStat {
                                    TableStatNumber = tableNumber,
                                    DayOfWeek = record.DateOnly.DayOfWeek,
                                    Date = record.DateOnly,
                                    IsLunch = record.IsAm,
                                    Sales = 0.0f,
                                    Orders = 0,
                                    DiningAreaID = record.DiningAreaID,
                                    DiningAreaRecordID = record.ID
                                };
                                tableStatsList.Add(newTableStat);
                            }
                        }
                    }
                    

                    // Assign the updated TableStats list to the record
                    record.TableStats = tableStatsList;
                }

                return diningAreaRecords;
            }
        }

        public static void UpdateDiningTableRecords(List<TablePercentageRecord> tableRecords)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                // Begin a transaction for batch update
                cnn.Open();
                using (var transaction = cnn.BeginTransaction()) {
                    // Prepare the SQL update statement with parameters
                    string updateSql = @"
                UPDATE DiningTableRecord SET
                    LessThan1k = @LessThan1k,
                    [1kTo2k] = @1kTo2k,
                    [2kTo3k] = @2kTo3k,
                    [3kTo4k] = @3kTo4k,
                    [4kTo5k] = @4kTo5k,
                    [5kTo6k] = @5kTo6k,
                    [6kTo7k] = @6kTo7k,
                    [7kTo8k] = @7kTo8k,
                    [8kTo9k] = @8kTo9k,
                    [9kTo10k] = @9kTo10k,
                    [10kTo11k] = @10kTo11k,
                    [11kTo12k] = @11kTo12k,
                    [12kTo13k] = @12kTo13k,
                    [13kTo14k] = @13kTo14k,
                    [14kTo15k] = @14kTo15k,
                    [15kTo16k] = @15kTo16k,
                    [16kTo17k] = @16kTo17k,
                    [17kTo18k] = @17kTo18k,
                    [18kTo19k] = @18kTo19k,
                    [19kTo20k] = @19kTo20k,
                    [20kTo21k] = @20kTo21k,
                    [21kTo22k] = @21kTo22k,
                    [22kTo23k] = @22kTo23k,
                    [23kTo24k] = @23kTo24k,
                    [24kTo25k] = @24kTo25k,
                    [25kTo26k] = @25kTo26k,
                    [26kTo27k] = @26kTo27k,
                    [27kTo28k] = @27kTo28k,
                    [28kTo29k] = @28kTo29k,
                    [29kTo30k] = @29kTo30k,
                    GreaterThan30k = @GreaterThan30k
                WHERE ID = @ID";

                    foreach (var record in tableRecords) {
                        // Prepare parameters for the update
                        var parameters = new DynamicParameters();
                        parameters.Add("@ID", record.ID);
                        parameters.Add("@LessThan1k", record.LessThan1k);
                        parameters.Add("@1kTo2k", record._1kTo2k);
                        parameters.Add("@2kTo3k", record._2kTo3k);
                        parameters.Add("@3kTo4k", record._3kTo4k);
                        parameters.Add("@4kTo5k", record._4kTo5k);
                        parameters.Add("@5kTo6k", record._5kTo6k);
                        parameters.Add("@6kTo7k", record._6kTo7k);
                        parameters.Add("@7kTo8k", record._7kTo8k);
                        parameters.Add("@8kTo9k", record._8kTo9k);
                        parameters.Add("@9kTo10k", record._9kTo10k);
                        parameters.Add("@10kTo11k", record._10kTo11k);
                        parameters.Add("@11kTo12k", record._11kTo12k);
                        parameters.Add("@12kTo13k", record._12kTo13k);
                        parameters.Add("@13kTo14k", record._13kTo14k);
                        parameters.Add("@14kTo15k", record._14kTo15k);
                        parameters.Add("@15kTo16k", record._15kTo16k);
                        parameters.Add("@16kTo17k", record._16kTo17k);
                        parameters.Add("@17kTo18k", record._17kTo18k);
                        parameters.Add("@18kTo19k", record._18kTo19k);
                        parameters.Add("@19kTo20k", record._19kTo20k);
                        parameters.Add("@20kTo21k", record._20kTo21k);
                        parameters.Add("@21kTo22k", record._21kTo22k);
                        parameters.Add("@22kTo23k", record._22kTo23k);
                        parameters.Add("@23kTo24k", record._23kTo24k);
                        parameters.Add("@24kTo25k", record._24kTo25k);
                        parameters.Add("@25kTo26k", record._25kTo26k);
                        parameters.Add("@26kTo27k", record._26kTo27k);
                        parameters.Add("@27kTo28k", record._27kTo28k);
                        parameters.Add("@28kTo29k", record._28kTo29k);
                        parameters.Add("@29kTo30k", record._29kTo30k);
                        parameters.Add("@GreaterThan30k", record.GreaterThan30k);

                        // Execute the update command
                        cnn.Execute(updateSql, parameters, transaction: transaction);
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
            }
        }

        public void SaveSectionColor(int sectionNumber, Color backColor, Color foreColor, bool isDefault)
        {
            using (var connection = new SQLiteConnection(LoadConnectionString())) {
                connection.Open();

                // First, check if a record already exists
                string checkQuery = "SELECT COUNT(*) FROM SectionColors WHERE Number = @Number AND IsDefault = @IsDefault";
                var recordCount = connection.ExecuteScalar<int>(checkQuery, new { Number = sectionNumber, IsDefault = isDefault ? 1 : 0 });

                if (recordCount > 0) {
                    // If record exists, update it
                    string updateQuery = @"UPDATE SectionColors 
                                   SET R = @R, G = @G, B = @B, 
                                       ForeR = @ForeR, ForeG = @ForeG, ForeB = @ForeB 
                                   WHERE Number = @Number AND IsDefault = @IsDefault";
                    connection.Execute(updateQuery, new {
                        R = backColor.R,
                        G = backColor.G,
                        B = backColor.B,
                        ForeR = foreColor.R,
                        ForeG = foreColor.G,
                        ForeB = foreColor.B,
                        Number = sectionNumber,
                        IsDefault = isDefault ? 1 : 0
                    });
                }
                else {
                    // If no record exists, insert a new one
                    string insertQuery = @"INSERT INTO SectionColors (Number, R, G, B, ForeR, ForeG, ForeB, IsDefault) 
                                   VALUES (@Number, @R, @G, @B, @ForeR, @ForeG, @ForeB, @IsDefault)";
                    connection.Execute(insertQuery, new {
                        Number = sectionNumber,
                        R = backColor.R,
                        G = backColor.G,
                        B = backColor.B,
                        ForeR = foreColor.R,
                        ForeG = foreColor.G,
                        ForeB = foreColor.B,
                        IsDefault = isDefault ? 1 : 0
                    });
                }

                connection.Close();
            }
        }

        public ColorPair GetSectionColor(int sectionNumber, bool isDefault)
        {
            using (var connection = new SQLiteConnection("YourConnectionString")) {
                connection.Open();

                string query = @"SELECT R, G, B, ForeR, ForeG, ForeB
                         FROM SectionColors 
                         WHERE Number = @Number AND IsDefault = @IsDefault";

                var result = connection.QueryFirstOrDefault(query, new { Number = sectionNumber, IsDefault = isDefault ? 1 : 0 });

                if (result != null) {
                    // Construct background and font colors from the retrieved RGB values
                    Color backgroundColor = Color.FromArgb(result.R, result.G, result.B);
                    Color fontColor = Color.FromArgb(result.ForeR, result.ForeG, result.ForeB);

                    // Return the ColorPair
                    return new ColorPair(backgroundColor, fontColor);
                }

                // Return default color pair if no record is found
                return new ColorPair(Color.White, Color.Black); // Or any other default color values
            }
        }
        public static List<TablePercentageRecord> LoadEmptyTableRecords()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                // Define the SQL query to select only the required columns
                string sql = @"
            SELECT
                ID,
                TableNumber,
                DiningAreaID,
                IsIncluded
            FROM
                DiningTableRecord";

                // Execute the query and map the results to the TableRecord class
                var tableRecords = cnn.Query<TablePercentageRecord>(sql).ToList();

                return tableRecords;
            }
        }
        public static List<TablePercentageRecord> LoadTablePercentageRecords()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                // Load all DiningTableRecords
                string diningTableSql = @"SELECT * FROM DiningTableRecord";
                var diningTableQueryResult = cnn.Query(diningTableSql);

                var tablePercentageRecords = diningTableQueryResult.Select(row => {
                    var rowDict = (IDictionary<string, object>)row;

                    return new TablePercentageRecord {
                        ID = Convert.ToInt32(rowDict["ID"]),
                        TableNumber = Convert.ToString(rowDict["TableNumber"]),
                        DiningAreaID = Convert.ToInt32(rowDict["DiningAreaID"]),
                        IsIncluded = Convert.ToBoolean(rowDict["IsIncluded"]),
                        LessThan1k = Convert.ToDouble(rowDict["LessThan1k"]),
                        _1kTo2k = Convert.ToDouble(rowDict["1kTo2k"]),
                        _2kTo3k = Convert.ToDouble(rowDict["2kTo3k"]),
                        _3kTo4k = Convert.ToDouble(rowDict["3kTo4k"]),
                        _4kTo5k = Convert.ToDouble(rowDict["4kTo5k"]),
                        _5kTo6k = Convert.ToDouble(rowDict["5kTo6k"]),
                        _6kTo7k = Convert.ToDouble(rowDict["6kTo7k"]),
                        _7kTo8k = Convert.ToDouble(rowDict["7kTo8k"]),
                        _8kTo9k = Convert.ToDouble(rowDict["8kTo9k"]),
                        _9kTo10k = Convert.ToDouble(rowDict["9kTo10k"]),
                        _10kTo11k = Convert.ToDouble(rowDict["10kTo11k"]),
                        _11kTo12k = Convert.ToDouble(rowDict["11kTo12k"]),
                        _12kTo13k = Convert.ToDouble(rowDict["12kTo13k"]),
                        _13kTo14k = Convert.ToDouble(rowDict["13kTo14k"]),
                        _14kTo15k = Convert.ToDouble(rowDict["14kTo15k"]),
                        _15kTo16k = Convert.ToDouble(rowDict["15kTo16k"]),
                        _16kTo17k = Convert.ToDouble(rowDict["16kTo17k"]),
                        _17kTo18k = Convert.ToDouble(rowDict["17kTo18k"]),
                        _18kTo19k = Convert.ToDouble(rowDict["18kTo19k"]),
                        _19kTo20k = Convert.ToDouble(rowDict["19kTo20k"]),
                        _20kTo21k = Convert.ToDouble(rowDict["20kTo21k"]),
                        _21kTo22k = Convert.ToDouble(rowDict["21kTo22k"]),
                        _22kTo23k = Convert.ToDouble(rowDict["22kTo23k"]),
                        _23kTo24k = Convert.ToDouble(rowDict["23kTo24k"]),
                        _24kTo25k = Convert.ToDouble(rowDict["24kTo25k"]),
                        _25kTo26k = Convert.ToDouble(rowDict["25kTo26k"]),
                        _26kTo27k = Convert.ToDouble(rowDict["26kTo27k"]),
                        _27kTo28k = Convert.ToDouble(rowDict["27kTo28k"]),
                        _28kTo29k = Convert.ToDouble(rowDict["28kTo29k"]),
                        _29kTo30k = Convert.ToDouble(rowDict["29kTo30k"]),
                        GreaterThan30k = Convert.ToDouble(rowDict["GreaterThan30k"]),


                    };
                }).ToList();
                return tablePercentageRecords;
            }
        }
        public static List<TablePercentageRecord> LoadTablePercentageRecordsWithTableStats()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                // Load all DiningTableRecords
                string diningTableSql = @"SELECT * FROM DiningTableRecord";
                var diningTableQueryResult = cnn.Query(diningTableSql);

                var tablePercentageRecords = diningTableQueryResult.Select(row =>
                {
                    var rowDict = (IDictionary<string, object>)row;

                    return new TablePercentageRecord {
                        ID = Convert.ToInt32(rowDict["ID"]),
                        TableNumber = Convert.ToString(rowDict["TableNumber"]),
                        DiningAreaID = Convert.ToInt32(rowDict["DiningAreaID"]),
                        IsIncluded = Convert.ToBoolean(rowDict["IsIncluded"]),
                        LessThan1k = Convert.ToDouble(rowDict["LessThan1k"]),
                        _1kTo2k = Convert.ToDouble(rowDict["1kTo2k"]),
                        _2kTo3k = Convert.ToDouble(rowDict["2kTo3k"]),
                        _3kTo4k = Convert.ToDouble(rowDict["3kTo4k"]),
                        _4kTo5k = Convert.ToDouble(rowDict["4kTo5k"]),
                        _5kTo6k = Convert.ToDouble(rowDict["5kTo6k"]),
                        _6kTo7k = Convert.ToDouble(rowDict["6kTo7k"]),
                        _7kTo8k = Convert.ToDouble(rowDict["7kTo8k"]),
                        _8kTo9k = Convert.ToDouble(rowDict["8kTo9k"]),
                        _9kTo10k = Convert.ToDouble(rowDict["9kTo10k"]),
                        _10kTo11k = Convert.ToDouble(rowDict["10kTo11k"]),
                        _11kTo12k = Convert.ToDouble(rowDict["11kTo12k"]),
                        _12kTo13k = Convert.ToDouble(rowDict["12kTo13k"]),
                        _13kTo14k = Convert.ToDouble(rowDict["13kTo14k"]),
                        _14kTo15k = Convert.ToDouble(rowDict["14kTo15k"]),
                        _15kTo16k = Convert.ToDouble(rowDict["15kTo16k"]),
                        _16kTo17k = Convert.ToDouble(rowDict["16kTo17k"]),
                        _17kTo18k = Convert.ToDouble(rowDict["17kTo18k"]),
                        _18kTo19k = Convert.ToDouble(rowDict["18kTo19k"]),
                        _19kTo20k = Convert.ToDouble(rowDict["19kTo20k"]),
                        _20kTo21k = Convert.ToDouble(rowDict["20kTo21k"]),
                        _21kTo22k = Convert.ToDouble(rowDict["21kTo22k"]),
                        _22kTo23k = Convert.ToDouble(rowDict["22kTo23k"]),
                        _23kTo24k = Convert.ToDouble(rowDict["23kTo24k"]),
                        _24kTo25k = Convert.ToDouble(rowDict["24kTo25k"]),
                        _25kTo26k = Convert.ToDouble(rowDict["25kTo26k"]),
                        _26kTo27k = Convert.ToDouble(rowDict["26kTo27k"]),
                        _27kTo28k = Convert.ToDouble(rowDict["27kTo28k"]),
                        _28kTo29k = Convert.ToDouble(rowDict["28kTo29k"]),
                        _29kTo30k = Convert.ToDouble(rowDict["29kTo30k"]),
                        GreaterThan30k = Convert.ToDouble(rowDict["GreaterThan30k"]),
                        // Initialize the dictionary
                        tableStatPercentagesByCategory = new Dictionary<string, List<TableStat>>()
                        {
                            { "LessThan1k", new List<TableStat>() },
                            { "1kTo2k", new List<TableStat>() },
                            { "2kTo3k", new List<TableStat>() },
                            { "3kTo4k", new List<TableStat>() },
                            { "4kTo5k", new List<TableStat>() },
                            { "5kTo6k", new List<TableStat>() },
                            { "6kTo7k", new List<TableStat>() },
                            { "7kTo8k", new List<TableStat>() },
                            { "8kTo9k", new List<TableStat>() },
                            { "9kTo10k", new List<TableStat>() },
                            { "10kTo11k", new List<TableStat>() },
                            { "11kTo12k", new List<TableStat>() },
                            { "12kTo13k", new List<TableStat>() },
                            { "13kTo14k", new List<TableStat>() },
                            { "14kTo15k", new List<TableStat>() },
                            { "15kTo16k", new List<TableStat>() },
                            { "16kTo17k", new List<TableStat>() },
                            { "17kTo18k", new List<TableStat>() },
                            { "18kTo19k", new List<TableStat>() },
                            { "19kTo20k", new List<TableStat>() },
                            { "20kTo21k", new List<TableStat>() },
                            { "21kTo22k", new List<TableStat>() },
                            { "22kTo23k", new List<TableStat>() },
                            { "23kTo24k", new List<TableStat>() },
                            { "24kTo25k", new List<TableStat>() },
                            { "25kTo26k", new List<TableStat>() },
                            { "26kTo27k", new List<TableStat>() },
                            { "27kTo28k", new List<TableStat>() },
                            { "28kTo29k", new List<TableStat>() },
                            { "29kTo30k", new List<TableStat>() },
                            { "GreaterThan30k", new List<TableStat>() }
                        }
                    };
                }).ToList();

                // Load all TableStat records
                string tableStatSql = @"SELECT * FROM TableStats";
                var tableStatQueryResult = cnn.Query(tableStatSql);

                // Organize TableStat records by TableNumber
                var tableStatsByTableNumber = tableStatQueryResult.GroupBy(ts => ts.TableStatNumber)
                                                                  .ToDictionary(g => g.Key, g => g.ToList());

                // Now, for each TablePercentageRecord, associate the relevant TableStat entries
                foreach (var tpr in tablePercentageRecords) {
                    if (tableStatsByTableNumber.TryGetValue(tpr.TableNumber, out var tableStats)) {
                        // For each TableStat, determine the sales category and add it to the appropriate list
                        foreach (var tsRow in tableStats) {
                            var tableStat = new TableStat {
                                TableStatNumber = tsRow.TableStatNumber,
                                DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), tsRow.DayOfWeek),
                                Date = DateOnly.Parse(tsRow.Date),
                                IsLunch = Convert.ToBoolean(tsRow.IsLunch),
                                Sales = tsRow.Sales != null ? (float?)Convert.ToDouble(tsRow.Sales) : null,
                                Orders = Convert.ToInt32(tsRow.Orders),
                                DiningAreaID = tsRow.DiningAreaID != null ? (int?)Convert.ToInt32(tsRow.DiningAreaID) : null,
                                DiningAreaRecordID = tsRow.DiningAreaRecordID != null ? (int?)Convert.ToInt32(tsRow.DiningAreaRecordID) : null
                            };

                            // Determine the sales category
                            string category = GetSalesCategory(tableStat.Sales ?? 0);

                            // Add the TableStat to the appropriate category list
                            if (tpr.tableStatPercentagesByCategory.ContainsKey(category)) {
                                tpr.tableStatPercentagesByCategory[category].Add(tableStat);
                            }
                            else {
                                // Handle unexpected categories if necessary
                            }
                        }
                    }
                    else {
                        // No TableStat records found for this table number
                        // Optionally, you might want to handle this case
                    }
                }

                return tablePercentageRecords;
            }
        }

        // Helper method to determine the sales category based on sales amount
        private static string GetSalesCategory(double sales)
        {
            if (sales < 1000)
                return "LessThan1k";
            else if (sales >= 1000 && sales < 2000)
                return "1kTo2k";
            else if (sales >= 2000 && sales < 3000)
                return "2kTo3k";
            else if (sales >= 3000 && sales < 4000)
                return "3kTo4k";
            else if (sales >= 4000 && sales < 5000)
                return "4kTo5k";
            else if (sales >= 5000 && sales < 6000)
                return "5kTo6k";
            else if (sales >= 6000 && sales < 7000)
                return "6kTo7k";
            else if (sales >= 7000 && sales < 8000)
                return "7kTo8k";
            else if (sales >= 8000 && sales < 9000)
                return "8kTo9k";
            else if (sales >= 9000 && sales < 10000)
                return "9kTo10k";
            else if (sales >= 10000 && sales < 11000)
                return "10kTo11k";
            else if (sales >= 11000 && sales < 12000)
                return "11kTo12k";
            else if (sales >= 12000 && sales < 13000)
                return "12kTo13k";
            else if (sales >= 13000 && sales < 14000)
                return "13kTo14k";
            else if (sales >= 14000 && sales < 15000)
                return "14kTo15k";
            else if (sales >= 15000 && sales < 16000)
                return "15kTo16k";
            else if (sales >= 16000 && sales < 17000)
                return "16kTo17k";
            else if (sales >= 17000 && sales < 18000)
                return "17kTo18k";
            else if (sales >= 18000 && sales < 19000)
                return "18kTo19k";
            else if (sales >= 19000 && sales < 20000)
                return "19kTo20k";
            else if (sales >= 20000 && sales < 21000)
                return "20kTo21k";
            else if (sales >= 21000 && sales < 22000)
                return "21kTo22k";
            else if (sales >= 22000 && sales < 23000)
                return "22kTo23k";
            else if (sales >= 23000 && sales < 24000)
                return "23kTo24k";
            else if (sales >= 24000 && sales < 25000)
                return "24kTo25k";
            else if (sales >= 25000 && sales < 26000)
                return "25kTo26k";
            else if (sales >= 26000 && sales < 27000)
                return "26kTo27k";
            else if (sales >= 27000 && sales < 28000)
                return "27kTo28k";
            else if (sales >= 28000 && sales < 29000)
                return "28kTo29k";
            else if (sales >= 29000 && sales < 30000)
                return "29kTo30k";
            else
                return "GreaterThan30k";
        }

        public static void SaveReservations(List<ReservationRecord> reservations)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                cnn.Open();
                using (var transaction = cnn.BeginTransaction()) {
                    try {
                        string insertReservationQuery = @"
                    INSERT INTO ReservationRecords (
                        Covers, DateTime, TimeCreated, CheckTotal, Server, Request, Name, Origin, State
                    ) VALUES (
                        @Covers, @DateTime, @TimeCreated, @CheckTotal, @Server, @Request, @Name, @Origin, @State
                    );
                    SELECT last_insert_rowid();";

                        string insertTableNumberQuery = @"
                    INSERT INTO ReservationTableNumbers (ReservationId, TableNumber)
                    VALUES (@ReservationId, @TableNumber);";

                        string insertVisitTagQuery = @"
                    INSERT INTO ReservationVisitTags (ReservationId, Tag)
                    VALUES (@ReservationId, @Tag);";

                        foreach (var reservation in reservations) {
                            // Insert into ReservationRecords and get the generated Id
                            var reservationId = cnn.ExecuteScalar<long>(insertReservationQuery, new {
                                Covers = reservation.Covers,
                                DateTime = reservation.DateTime.ToString("o"),
                                TimeCreated = reservation.TimeCreated.ToString("o"),
                                CheckTotal = reservation.CheckTotal,
                                Server = reservation.Server,
                                Request = reservation.Request,
                                Name = reservation.Name,
                                Origin = reservation.Origin.ToString(),
                                State = reservation.State
                            }, transaction);

                            // Insert TableNumbers
                            foreach (var tableNumber in reservation.TableNumbers) {
                                cnn.Execute(insertTableNumberQuery, new {
                                    ReservationId = reservationId,
                                    TableNumber = tableNumber
                                }, transaction);
                            }

                            // Insert VisitTags
                            foreach (var tag in reservation.VisitTags) {
                                cnn.Execute(insertVisitTagQuery, new {
                                    ReservationId = reservationId,
                                    Tag = tag
                                }, transaction);
                            }
                        }

                        transaction.Commit();
                    }
                    catch {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }



        //public static void SaveTopBottomNeighbor(string key, string value)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var sql = "INSERT OR REPLACE INTO TopBottomNeighbors (Key, Value) VALUES (@Key, @Value);";
        //        cnn.Execute(sql, new { Key = key, Value = value });
        //    }
        //}

        //public static void SaveRightLeftNeighbor(string key, string value)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var sql = "INSERT OR REPLACE INTO RightLeftNeighbors (Key, Value) VALUES (@Key, @Value);";
        //        cnn.Execute(sql, new { Key = key, Value = value });
        //    }
        //}
        //public static void DeleteTopBottomNeighbor(string key)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var sql = "DELETE FROM TopBottomNeighbors WHERE Key = @Key;";
        //        cnn.Execute(sql, new { Key = key });
        //    }
        //}

        //public static void DeleteRightLeftNeighbor(string key)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var sql = "DELETE FROM RightLeftNeighbors WHERE Key = @Key;";
        //        cnn.Execute(sql, new { Key = key });
        //    }
        //}
        //public static Dictionary<string, string> LoadAllTopBottomNeighbors()
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var sql = "SELECT Key, Value FROM TopBottomNeighbors;";
        //        return cnn.Query(sql).ToDictionary(row => (string)row.Key, row => (string)row.Value);
        //    }
        //}

        //public static Dictionary<string, string> LoadAllRightLeftNeighbors()
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        var sql = "SELECT Key, Value FROM RightLeftNeighbors;";
        //        return cnn.Query(sql).ToDictionary(row => (string)row.Key, row => (string)row.Value);
        //    }
        //}





    }
}
