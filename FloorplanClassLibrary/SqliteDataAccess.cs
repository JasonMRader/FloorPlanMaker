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
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                foreach(TableStat tableStat in  tableStats)
                {
                    // Check for existing record
                    string checkSql = @"SELECT COUNT(*) FROM TableStats 
                            WHERE TableStatNumber = @TableStatNumber 
                            AND IsLunch = @IsLunch 
                            AND Date = @Date";
                    int count = cnn.Query<int>(checkSql, new
                    {
                        TableStatNumber = tableStat.TableStatNumber,
                        IsLunch = tableStat.IsLunch,
                        Date = tableStat.Date.ToString("yyyy-MM-dd")
                    }).Single();

                    if (count == 0)
                    {
                        // No existing record, proceed with insertion
                        var sql = @"INSERT INTO TableStats 
                        (TableStatNumber, DayOfWeek, Date, IsLunch, Sales, Orders) 
                        VALUES 
                        (@TableStatNumber, @DayOfWeek, @Date, @IsLunch, @Sales, @Orders)";

                        cnn.Execute(sql, new
                        {
                            TableStatNumber = tableStat.TableStatNumber,
                            DayOfWeek = tableStat.DayOfWeek.ToString(),
                            Date = tableStat.Date.ToString("yyyy-MM-dd"),
                            IsLunch = tableStat.IsLunch,
                            Sales = tableStat.Sales,
                            Orders = tableStat.Orders
                        });
                    }
                }
               
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
                    Orders = Convert.ToInt32(row.Orders)
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



        public static List<DateOnly> GetMissingDates(DateOnly startDate, DateOnly endDate)
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

                        transaction.Commit();

                        return insertedId;
                    }
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
                    Height = table.Height
                   
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
                Height = @Height
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
        public static void SaveDiningArea(DiningArea diningArea)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO DiningArea (Name, IsInside) VALUES (@Name, @IsInside)", diningArea);
            }
        }
        public static void SaveNewServer(Server server)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO Server (Name, Archived, DisplayName) VALUES (@Name, @Archived, @DisplayName)", server);
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
                var query = "UPDATE Server SET Name = @Name, Archived = @Archived, DisplayName = @DisplayName WHERE ID = @ID";
                cnn.Execute(query, new { Name = server.Name, Archived = server.Archived, DisplayName = server.DisplayName, ID = server.ID });
            }
        }
        public static List<Server> LoadActiveServers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<Server>("SELECT * FROM Server WHERE Archived = 0").ToList();
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
                        section.SetTableList(cnn.Query<Table>(
                            "SELECT * FROM DiningTable WHERE ID IN (SELECT TableID FROM SectionTables WHERE SectionID = @SectionID)",
                            new { SectionID = id }).ToList());
                        floorplan.AddSection(section);
                    }

                    var servers = new List<Server>();
                    var serverSections = cnn.Query("SELECT * FROM ServerSections WHERE SectionID IN @SectionIds", new { SectionIds = sectionIds })
                                            .Select(x => new { ServerID = (int)x.ServerID, SectionID = (int)x.SectionID })
                                            .ToList();

                    foreach (var ss in serverSections)
                    {
                        if (!servers.Any(s => s.ID == ss.ServerID))
                        {
                            var server = cnn.QuerySingle<Server>("SELECT * FROM Server WHERE ID = @ID", new { ID = ss.ServerID });
                            servers.Add(server);
                        }

                        var matchedSection = floorplan.Sections.FirstOrDefault(s => s.ID == ss.SectionID);
                        if (matchedSection != null)
                        {
                            matchedSection.AddServer(servers.First(s => s.ID == ss.ServerID));
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
                    SaveSection(section);
                    cnn.Execute("INSERT INTO TemplateSections (SectionID, TemplateID) VALUES (@SectionID, @TemplateID)", new { SectionID = section.ID, TemplateID = template.ID });
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
                        cnn.Execute("DELETE FROM TemplateSections WHERE TemplateID = @TemplateID", new { TemplateID = templateId }, transaction);

                        // Then, delete the template itself
                        cnn.Execute("DELETE FROM FloorplanTemplate WHERE ID = @ID", new { ID = templateId }, transaction);

                        transaction.Commit();
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
                cnn.Execute("INSERT INTO Section (MaxCovers, AverageCovers, IsCloser, IsPre, TeamWait, IsPickUp) VALUES (@MaxCovers, @AverageCovers, @IsCloser, @IsPre, @TeamWait, @IsPickUp)",
                new
                {
                    MaxCovers = section.MaxCovers,
                    AverageCovers = section.AverageSales,                    
                    IsCloser = section.IsCloser,
                    IsPre = section.IsPre,
                    TeamWait = section.IsTeamWait,
                    IsPickUp = section.IsPickUp
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
                        section.SetTeamWait(teamWaitValue);
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
                    template.Sections = cnn.Query<Section>("SELECT s.* FROM Section s JOIN TemplateSections ts ON s.ID = ts.SectionID WHERE ts.TemplateID = @TemplateID", new { TemplateID = template.ID }).ToList();
                    //template.AssignSectionNumbers();

                    foreach (var section in template.Sections)
                    {
                        section.SetTableList(cnn.Query<Table>("SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID", new { SectionID = section.ID }).ToList());
                    }
                }

                return templates;
            }
        }
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
                    //template.DiningArea = cnn.QuerySingle<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = template.DiningAreaID });
                    template.DiningArea = diningArea;
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
                        section.SetTeamWait(teamWaitValue);
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

                        // Set table list for each section
                        section.SetTableList(cnn.Query<Table>(
                            "SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID",
                            new { SectionID = section.ID }
                        ).ToList());
                    }
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

        public static void SaveFloorplanAndSections(Floorplan floorplan)
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
                    DialogResult result = MessageBox.Show("A floorplan with the same criteria already exists. Do you want to replace it?",
                                                 "Replace Floorplan?",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        // User does not want to replace, so exit without saving.
                        cnn.Close();
                        return;
                    }

                    // 1. Retrieve all related SectionIDs
                    var relatedSectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID }).ToList();

                    // 2. Delete entries from ServerSections and Shift using the related SectionIDs
                    foreach (var sectionId in relatedSectionIds)
                    {
                        cnn.Execute("DELETE FROM ServerSections WHERE SectionID = @SectionID", new { SectionID = sectionId });
                        cnn.Execute("DELETE FROM Shift WHERE SectionID = @SectionID", new { SectionID = sectionId });
                    }

                    // 3. Delete entries from FloorplanSections
                    cnn.Execute("DELETE FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = existingFloorplan.ID });

                    // 4. Finally, delete the Floorplan
                    cnn.Execute("DELETE FROM Floorplan WHERE ID = @ID", new { ID = existingFloorplan.ID });
                }
                
                cnn.Execute("INSERT INTO Floorplan (Date, IsLunch, DiningAreaID) VALUES (date(@Date), @IsLunch, @DiningAreaID)",
                new
                {
                    Date = floorplan.Date,
                    IsLunch = floorplan.IsLunch,
                    DiningAreaID = floorplan.DiningArea.ID
                });

                floorplan.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();

                // Save each section
                foreach (Section section in floorplan.Sections)
                {
                    SaveSection(section); // Reusing your existing method

                    // Link this section with the floorplan
                    cnn.Execute("INSERT INTO FloorplanSections (FloorplanID, SectionID) VALUES (@FloorplanID, @SectionID)",
                    new
                    {
                        FloorplanID = floorplan.ID,
                        SectionID = section.ID
                    });

                    // Assuming section has a List<Server> Servers to represent all servers in that section
                    if (section.ServerTeam.Count >= 1)
                    {

                        //cnn.Execute("INSERT OR IGNORE INTO ServerSections (ServerID, SectionID) VALUES (@ServerID, @SectionID)",
                        //new
                        //{
                        //    ServerID = section.Server.ID,
                        //    SectionID = section.ID
                        //});
                        foreach (Server server in section.ServerTeam)
                        {
                            cnn.Execute("INSERT OR IGNORE INTO Shift (ServerID, SectionID, FloorplanID, DiningAreaID) VALUES (@ServerID, @SectionID, @FloorplanID, @DiningAreaID)",
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
        public static List<EmployeeShift> LoadShiftsForServer(Server server)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
               
                // Load shifts along with details from Section and DiningArea
                var sql = @"
                    SELECT s.*, sec.IsCloser, sec.TeamWait, da.IsInside, fp.Date as FloorplanDate
                    FROM Shift s
                    INNER JOIN Section sec ON s.SectionID = sec.ID
                    INNER JOIN DiningArea da ON s.DiningAreaID = da.ID
                    INNER JOIN Floorplan fp ON s.FloorplanID = fp.ID
                    WHERE s.ServerID = @ID";

                List<EmployeeShift> allShifts = connection.Query<EmployeeShift, Section, DiningArea, Floorplan, EmployeeShift>(
           sql,
           (shift, section, diningArea, floorplan) =>
           {
               shift.IsCloser = section.IsCloser;
               shift.IsTeamWait = section.IsTeamWait;
               shift.IsInside = diningArea.IsInside;
               shift.Date = floorplan.Date; // Assign the date from the Floorplan to the Shift object
               return shift;
           },
           splitOn: "IsCloser,IsInside,FloorplanDate",
           param: new { ID = server.ID }
       ).ToList();
                var rawData = connection.Query<dynamic>(sql, new { ID = server.ID }).ToList();
                foreach (var data in rawData)
                {
                    Console.WriteLine(data.FloorplanDate);
                }

                return allShifts;
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
                sec.TeamWait AS IsTeamWait
            FROM Shift s
            INNER JOIN Floorplan f ON s.FloorplanID = f.ID
            INNER JOIN Section sec ON s.SectionID = sec.ID
            INNER JOIN DiningArea d ON s.DiningAreaID = d.ID
            WHERE s.ServerID = @ServerID";

                return dbConnection.Query<EmployeeShift>(query, new { ServerID = server.ID }).ToList();
                
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
