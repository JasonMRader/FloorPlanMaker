﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class SectionColorManager
    {
        public static Dictionary<int, ColorPair> SectionColors { get; private set; } = new Dictionary<int, ColorPair>();
        public static Dictionary<int, ColorPair> DefaultSectionColors { get; private set; } = new Dictionary<int, ColorPair>();
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
        // Load default and custom colors from the database
        public static void LoadColors()
        {
            using (var connection = new SQLiteConnection(LoadConnectionString())) {
                connection.Open();

                string customQuery = "SELECT Number, R, G, B, ForeR, ForeG, ForeB FROM SectionColors WHERE IsDefault = 0";
                var customColors = connection.Query(customQuery);

                foreach (var color in customColors) {
                    // Explicitly cast the values to int before using them in Color.FromArgb
                    var backgroundColor = Color.FromArgb((int)color.R, (int)color.G, (int)color.B);
                    var fontColor = Color.FromArgb((int)color.ForeR, (int)color.ForeG, (int)color.ForeB);

                    var colorPair = new ColorPair(backgroundColor, fontColor);
                    SectionColors[(int)color.Number] = colorPair;
                }
                string defaultQuery = "SELECT Number, R, G, B, ForeR, ForeG, ForeB FROM SectionColors WHERE IsDefault = 1";
                var defaultColors = connection.Query(defaultQuery);

                foreach (var color in defaultColors) {
                    // Explicitly cast the values to int before using them in Color.FromArgb
                    var backgroundColor = Color.FromArgb((int)color.R, (int)color.G, (int)color.B);
                    var fontColor = Color.FromArgb((int)color.ForeR, (int)color.ForeG, (int)color.ForeB);

                    var colorPair = new ColorPair(backgroundColor, fontColor);
                    DefaultSectionColors[(int)color.Number] = colorPair;
                }
            }

            // You can also add your hardcoded defaults here, or load from a config file
            AddDefaultColors();
        }

        // Optionally add hardcoded default colors if not in the database
        private static void AddDefaultColors()
        {
            if (!SectionColors.ContainsKey(1))
                SectionColors[1] = new ColorPair(Color.FromArgb(17, 100, 184), Color.White);

            if (!SectionColors.ContainsKey(2))
                SectionColors[2] = new ColorPair(Color.FromArgb(105, 209, 0), Color.Black);

            if (!SectionColors.ContainsKey(3))
                SectionColors[3] = new ColorPair(Color.FromArgb(176, 46, 12), Color.White);

            if (!SectionColors.ContainsKey(4))
                SectionColors[4] = new ColorPair(Color.FromArgb(103, 178, 216), Color.Black);

            if (!SectionColors.ContainsKey(5))
                SectionColors[5] = new ColorPair(Color.ForestGreen, Color.White);

            if (!SectionColors.ContainsKey(6))
                SectionColors[6] = new ColorPair(Color.FromArgb(240, 246, 0), Color.Black);

            if (!SectionColors.ContainsKey(7))
                SectionColors[7] = new ColorPair(Color.FromArgb(70, 17, 122), Color.White);

            if (!SectionColors.ContainsKey(8))
                SectionColors[8] = new ColorPair(Color.FromArgb(65, 234, 212), Color.Black);

            if (!SectionColors.ContainsKey(9))
                SectionColors[9] = new ColorPair(Color.FromArgb(244, 192, 149), Color.Black);

            if (!SectionColors.ContainsKey(10))
                SectionColors[10] = new ColorPair(Color.FromArgb(130, 9, 29), Color.White);

            if (!SectionColors.ContainsKey(11))
                SectionColors[11] = new ColorPair(Color.FromArgb(194, 178, 180), Color.White);

            if (!SectionColors.ContainsKey(12))
                SectionColors[12] = new ColorPair(Color.FromArgb(7, 79, 87), Color.White);

            if (!SectionColors.ContainsKey(13))
                SectionColors[13] = new ColorPair(Color.FromArgb(250, 127, 127), Color.Black);

            if (!SectionColors.ContainsKey(14))
                SectionColors[14] = new ColorPair(Color.FromArgb(84, 92, 82), Color.White);

            if (!SectionColors.ContainsKey(15))
                SectionColors[15] = new ColorPair(Color.FromArgb(180, 134, 159), Color.Black);

            if (!SectionColors.ContainsKey(100))
                SectionColors[100] = new ColorPair(Color.LightGray, Color.Black);

            if (!SectionColors.ContainsKey(101))
                SectionColors[101] = new ColorPair(Color.Gray, Color.White);

            if (!SectionColors.ContainsKey(102))
                SectionColors[102] = new ColorPair(Color.DarkGray, Color.White);
        }


        public static ColorPair GetColorPair(int sectionNumber)
        {
            if (SectionColors.ContainsKey(sectionNumber)) {
                return SectionColors[sectionNumber];
            }

            // Return default color pair if not found
            return new ColorPair(Color.White, Color.Black);
        }
        public static ColorPair GetDefaultColorPair(int sectionNumber)
        {
            if (DefaultSectionColors.ContainsKey(sectionNumber)) {
                return DefaultSectionColors[sectionNumber];
            }

            // Return default color pair if not found
            return new ColorPair(Color.White, Color.Black);
        }
        public static void SaveDefaultColorsToDatabase()
        {
            using (var connection = new SQLiteConnection(LoadConnectionString())) {
                connection.Open();

                foreach (var sectionColor in DefaultSectionColors) {
                    int sectionNumber = sectionColor.Key;
                    ColorPair colorPair = sectionColor.Value;

                    string queryCheck = @"SELECT COUNT(*) FROM SectionColors 
                                  WHERE Number = @Number AND IsDefault = 1";

                    // Check if a record already exists
                    int recordCount = connection.ExecuteScalar<int>(queryCheck, new { Number = sectionNumber });

                    if (recordCount > 0) {
                        // If record exists, update it
                        string updateQuery = @"UPDATE SectionColors 
                                       SET R = @R, G = @G, B = @B, ForeR = @ForeR, ForeG = @ForeG, ForeB = @ForeB
                                       WHERE Number = @Number AND IsDefault = 1";

                        connection.Execute(updateQuery, new {
                            R = colorPair.BackgroundColor.R,
                            G = colorPair.BackgroundColor.G,
                            B = colorPair.BackgroundColor.B,
                            ForeR = colorPair.FontColor.R,
                            ForeG = colorPair.FontColor.G,
                            ForeB = colorPair.FontColor.B,
                            Number = sectionNumber
                        });
                    }
                    else {
                        // If no record exists, insert a new one
                        string insertQuery = @"INSERT INTO SectionColors (Number, R, G, B, ForeR, ForeG, ForeB, IsDefault)
                                       VALUES (@Number, @R, @G, @B, @ForeR, @ForeG, @ForeB, 1)";

                        connection.Execute(insertQuery, new {
                            Number = sectionNumber,
                            R = colorPair.BackgroundColor.R,
                            G = colorPair.BackgroundColor.G,
                            B = colorPair.BackgroundColor.B,
                            ForeR = colorPair.FontColor.R,
                            ForeG = colorPair.FontColor.G,
                            ForeB = colorPair.FontColor.B
                        });
                    }
                }

                connection.Close();
            }
        }
        public static void SaveCustomColorsToDatabase(Dictionary<int, ColorPair> customColors)
        {
            using (var connection = new SQLiteConnection(LoadConnectionString())) {
                connection.Open();

                foreach (var sectionColor in customColors) {
                    int sectionNumber = sectionColor.Key;
                    ColorPair colorPair = sectionColor.Value;

                    string queryCheck = @"SELECT COUNT(*) FROM SectionColors 
                                  WHERE Number = @Number AND IsDefault = 0";

                    // Check if a record already exists
                    int recordCount = connection.ExecuteScalar<int>(queryCheck, new { Number = sectionNumber });

                    if (recordCount > 0) {
                        // If record exists, update it
                        string updateQuery = @"UPDATE SectionColors 
                                       SET R = @R, G = @G, B = @B, ForeR = @ForeR, ForeG = @ForeG, ForeB = @ForeB
                                       WHERE Number = @Number AND IsDefault = 0";

                        connection.Execute(updateQuery, new {
                            R = colorPair.BackgroundColor.R,
                            G = colorPair.BackgroundColor.G,
                            B = colorPair.BackgroundColor.B,
                            ForeR = colorPair.FontColor.R,
                            ForeG = colorPair.FontColor.G,
                            ForeB = colorPair.FontColor.B,
                            Number = sectionNumber
                        });
                    }
                    else {
                        // If no record exists, insert a new one
                        string insertQuery = @"INSERT INTO SectionColors (Number, R, G, B, ForeR, ForeG, ForeB, IsDefault)
                                       VALUES (@Number, @R, @G, @B, @ForeR, @ForeG, @ForeB, 0)";

                        connection.Execute(insertQuery, new {
                            Number = sectionNumber,
                            R = colorPair.BackgroundColor.R,
                            G = colorPair.BackgroundColor.G,
                            B = colorPair.BackgroundColor.B,
                            ForeR = colorPair.FontColor.R,
                            ForeG = colorPair.FontColor.G,
                            ForeB = colorPair.FontColor.B
                        });
                    }
                }

                connection.Close();
            }
            SectionColors.Clear();
            SectionColors = customColors;
        }
        public static void SaveSectionColor(int sectionNumber, Color backColor, Color foreColor, bool isDefault)
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
    }
}
