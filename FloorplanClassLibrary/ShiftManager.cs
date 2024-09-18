using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftManager
    {
        public ShiftManager() { }
        public DateOnly DateOnly => _selectedShift.DateOnly;
       
        public bool IsAM => _selectedShift.IsAM;
        public Shift? SelectedShift 
        { get { return _selectedShift; } 
            set { _selectedShift = value; } }
        private Shift? _selectedShift {  get; set; }
       
        public Shift? NewShift
        { get { return _newShift; } 
            set { _newShift = value; }}
        private Shift? _newShift {  get; set; }
       
        public void CreateNewShift(DateOnly dateOnly, bool isAM)
        {
            _newShift = new Shift(dateOnly, isAM);
        }
        public void CreateNewShift(DateOnly dateOnly, bool isAM, List<Floorplan> floorplans)
        {
            _newShift = new Shift(dateOnly, isAM, floorplans);
            
        }
        public void SetNewShiftToSelectedShift() { _newShift = this._selectedShift;}

        public void SetSelectedShift(DateOnly dateOnly, bool isAM)
        {
            if(this._newShift != null && dateOnly == _newShift.DateOnly && isAM == _newShift.IsAM)
            {
                _selectedShift = _newShift;
            }
            else
            {
                //List<Floorplan> floorplans = SqliteDataAccess.LoadFloorplansByDateAndShift(dateOnly, isAM);
                _selectedShift = SqliteDataAccess.LoadShift(dateOnly, isAM);//new Shift(dateOnly, isAM, floorplans);
                _selectedShift.PickupSectionUpdate();
            }
        }
    }
}
