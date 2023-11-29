using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary{
    public class UpdateEventArgs : EventArgs
    {
        public ControlType ControlType { get; private set; }
        public UpdateType UpdateType { get; private set; }
        public object UpdateData { get; private set; }

        public UpdateEventArgs(ControlType controlType,UpdateType updateType, object updateData)
        {
            ControlType = controlType;
            UpdateType = updateType;
            UpdateData = updateData;
        }
    }
    public enum ControlType
    {
        SectionLabel,
        ServerControl,
        SectionPanel,
        TableControl,
        // Add more as needed
    }
    public enum UpdateType
    {
        Remove,
        Add,
        Refresh,
        Assign
        
        
        // Add more as needed
    }


}
