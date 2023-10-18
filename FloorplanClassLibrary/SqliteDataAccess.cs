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
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        public static List<Table> LoadTables()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<Table>("SELECT * FROM DiningTable").ToList();
            }
        }
        public static int SaveTable(Table table)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                var parameters = new
                {
                    TableNumber = table.TableNumber,
                    MaxCovers = table.MaxCovers,
                    AverageCovers = table.AverageCovers,
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

        public static void UpdateTable(Table table)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var parameters = new
                {
                    ID = table.ID,
                    TableNumber = table.TableNumber,
                    MaxCovers = table.MaxCovers,
                    AverageCovers = table.AverageCovers,
                    DiningAreaID = table.DiningArea.ID,
                    XCoordinate = table.XCoordinate,
                    YCoordinate = table.YCoordinate,
                    Shape = (int)table.Shape,
                    Width = table.Width,
                    Height = table.Height
                    // If you have a TableID, include it in parameters
                    // TableID = table.TableID
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

                var tables = LoadTables();
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
                cnn.Execute("INSERT INTO Server (Name) VALUES (@Name)", server);
            }
        }
        public static List<Server> LoadServers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<Server>("SELECT * FROM Server").ToList();
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
                    section.Tables = cnn.Query<Table>("SELECT * FROM DiningTable WHERE ID IN (SELECT TableID FROM SectionTables WHERE SectionID = @SectionID)", new { SectionID = id }).ToList();
                    floorplan.Sections.Add(section);
                }

                // Populate Servers from FloorplanServers
                floorplan.Servers = cnn.Query<Server>("SELECT * FROM Server WHERE ID IN (SELECT ServerID FROM FloorplanServers WHERE FloorplanID = @FloorplanID)", new { FloorplanID = floorplan.ID }).ToList();

                return floorplan;
            }
        }
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
                    floorplan.DiningArea = diningArea;

                    // Populate Sections from FloorplanSections
                    var sectionIds = cnn.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @FloorplanID", new { FloorplanID = floorplan.ID });
                    foreach (var id in sectionIds)
                    {
                        var section = cnn.QuerySingle<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = id });
                        section.Tables = cnn.Query<Table>(
                            "SELECT * FROM DiningTable WHERE ID IN (SELECT TableID FROM SectionTables WHERE SectionID = @SectionID)",
                            new { SectionID = id }).ToList();
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
                            matchedSection.Server = servers.First(s => s.ID == ss.ServerID);
                        }
                    }

                    floorplan.Servers = servers;
                    floorplans.Add(floorplan);
                }
            }

            return floorplans;
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
                    // Populate Tables for each Section from SectionTables
                    section.Tables = cnn.Query<Table>(
                        "SELECT * FROM DiningTable WHERE ID IN (SELECT TableID FROM SectionTables WHERE SectionID = @SectionID)",
                        new { SectionID = id }).ToList();
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
                        matchedSection.Server = servers.First(s => s.ID == ss.ServerID);
                    }
                }

                // Add loaded servers to floorplan's Servers list\
                floorplan.Servers = servers;
                //foreach (var section in floorplan.Sections)
                //{
                //    if(section.Server != null)
                //    {
                //        floorplan.Servers.Add(section.Server);
                //    }
                //}



                return floorplan;
            }
        }

        public static void SaveFloorplanTemplate(FloorplanTemplate template)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute("INSERT INTO FloorplanTemplate (Name, DiningAreaID, ServerCount) VALUES (@Name, @DiningAreaID, @ServerCount)", template);

                template.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();

                foreach (Section section in template.Sections)
                {
                    SaveSection(section);
                    cnn.Execute("INSERT INTO TemplateSections (SectionID, TemplateID) VALUES (@SectionID, @TemplateID)", new { SectionID = section.ID, TemplateID = template.ID });
                }
            }

        }
        public static void SaveSection(Section section)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute("INSERT INTO Section (MaxCovers, AverageCovers, ServerID1, IsCloser, IsPre, TeamWait) VALUES (@MaxCovers, @AverageCovers, @ServerIDAlias, @IsCloser, @IsPre, @TeamWait)",
                new
                {
                    MaxCovers = section.MaxCovers,
                    AverageCovers = section.AverageCovers,
                    ServerIDAlias = section.ServerID,  // Map ServerID to ServerID1
                    IsCloser = section.IsCloser,
                    IsPre = section.IsPre,
                    TeamWait = section.IsTeamWait
                });

                section.ID = cnn.Query<int>("select last_insert_rowid()", new DynamicParameters()).Single();

                if (section.Tables == null) { return; }

                foreach (Table table in section.Tables)
                {
                    cnn.Execute("INSERT INTO SectionTables (SectionID, TableID) VALUES (@SectionID, @TableID)", new {SectionID = section.ID, TableID = table.ID});
                }
                cnn.Close();
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
                    template.Sections = cnn.Query<Section>("SELECT s.* FROM Section s JOIN TemplateSections ts ON s.ID = ts.SectionID WHERE ts.TemplateID = @TemplateID", new { TemplateID = template.ID }).ToList();

                    
                    foreach (var section in template.Sections)
                    {
                        section.Tables = cnn.Query<Table>("SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID", new { SectionID = section.ID }).ToList();
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


                    foreach (var section in template.Sections)
                    {
                        section.Tables = cnn.Query<Table>("SELECT t.* FROM DiningTable t JOIN SectionTables st ON t.ID = st.TableID WHERE st.SectionID = @SectionID", new { SectionID = section.ID }).ToList();
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
                    cnn.Execute("DELETE FROM Section WHERE ID = SectionID", new { ID = sectionId });
                }

                // 3. Delete entries from FloorplanSections
                
                
                // 4. Finally, delete the Floorplan
                cnn.Execute("DELETE FROM Floorplan WHERE ID = @ID", new { ID = existingFloorplan.ID });


            }
        }
        public static List<Floorplan> LoadFloorplanList()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                // Load all basic Floorplan details
                List<Floorplan> allFloorplans = connection.Query<Floorplan>("SELECT * FROM Floorplan").ToList();

                foreach (var floorplan in allFloorplans)
                {
                    // Load associated DiningArea
                   
                    floorplan.DiningArea = connection.QueryFirstOrDefault<DiningArea>("SELECT * FROM DiningArea WHERE ID = @ID", new { ID = floorplan.DiningAreaID });


                    // Load the associated Sections
                    var sectionIDs = connection.Query<int>("SELECT SectionID FROM FloorplanSections WHERE FloorplanID = @ID", new { ID = floorplan.ID });
                    foreach (var sectionID in sectionIDs)
                    {
                        Section section = connection.QueryFirstOrDefault<Section>("SELECT * FROM Section WHERE ID = @ID", new { ID = sectionID });

                        // Load Servers for the section
                        section.Server = connection.QueryFirstOrDefault<Server>("SELECT * FROM Server WHERE ID = @ID", new { ID = section.ServerID });

                        // Load DiningTables for the section
                        section.Tables = connection.Query<Table>("SELECT dt.* FROM DiningTable dt JOIN SectionTables st ON dt.ID = st.TableID WHERE st.SectionID = @ID", new { ID = sectionID }).ToList();

                        floorplan.Sections.Add(section);
                        floorplan.Servers.Add(section.Server);
                    }

                    // Set ServerCount and SectionCount
                    floorplan.ServerCount = floorplan.Servers.Count;
                    floorplan.SectionCount = floorplan.Sections.Count;
                }
                return allFloorplans;
            }


        }
        public static List<Shift> LoadShiftsForServer(Server server)
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

                List<Shift> allShifts = connection.Query<Shift, Section, DiningArea, Floorplan, Shift>(
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
        public static List<Shift> GetShiftsForServer(Server server)
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
                d.IsInside,
                sec.TeamWait AS IsTeamWait
            FROM Shift s
            INNER JOIN Floorplan f ON s.FloorplanID = f.ID
            INNER JOIN Section sec ON s.SectionID = sec.ID
            INNER JOIN DiningArea d ON s.DiningAreaID = d.ID
            WHERE s.ServerID = @ServerID";

                return dbConnection.Query<Shift>(query, new { ServerID = server.ID }).ToList();
                
            }
           
            
        }





    }
}
