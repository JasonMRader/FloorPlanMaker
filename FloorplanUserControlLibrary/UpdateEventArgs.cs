using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
    public class UpdateEventArgs : EventArgs
    {
        public UpdateType UpdateType { get; private set; }
        public object UpdateData { get; private set; }

        public UpdateEventArgs(UpdateType updateType, object updateData)
        {
            UpdateType = updateType;
            UpdateData = updateData;
        }
    }
    public enum UpdateType
    {
        SectionLabel,
        ServerControl,
        SectionControl,
        TableControl,
        // Add more as needed
    }


}
