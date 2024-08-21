using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class ServerControlManager
    {
        public ServerControlManager(Floorplan floorplan)
        {

        }
        private Floorplan _floorplan { get; set; }
        private List<ServerInFloorplanControl> serverControls {  get; set; }
    }
}
