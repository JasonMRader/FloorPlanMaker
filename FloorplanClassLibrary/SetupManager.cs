using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SetupManager
    {
        public void InitialSetup()
        {
            // Use FolderBrowserDialog to let the user choose a directory
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string userSelectedPath = folderBrowserDialog.SelectedPath;
                    CreateApplicationFolders(userSelectedPath);
                }
                else
                {
                    // Handle cases where the user does not select a folder
                }
            }
        }

        private void CreateApplicationFolders(string basePath)
        {
            try
            {
                // Create a folder for the database file
                string dbFolderPath = Path.Combine(basePath, "Database");
                Directory.CreateDirectory(dbFolderPath);

                // Create a folder for backup files
                string backupFolderPath = Path.Combine(basePath, "Backups");
                Directory.CreateDirectory(backupFolderPath);

                // You can also copy the .exe or .db files if needed
                // Example: File.Copy(sourcePath, Path.Combine(dbFolderPath, "yourdatabase.db"));
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., permission issues)
            }
        }
    }
}
