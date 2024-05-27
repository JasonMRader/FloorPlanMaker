using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class DiningAreaManager
    {
        public DiningAreaManager() 
        {
            
            DiningAreas = SqliteDataAccess.LoadDiningAreas();
        }
        public DiningArea? DiningAreaSelected { get; set; }
        public List<DiningArea> DiningAreas { get; set; }
        public Table SelectedTable { get; set; }
        public List<Table> SelectedTables = new List<Table>();
        public void SaveSelectedTable()
        {

        }
        
        
    }
}
