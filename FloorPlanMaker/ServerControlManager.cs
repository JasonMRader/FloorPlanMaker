using FloorplanClassLibrary;
using FloorPlanMaker;
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
        public ServerControlManager(Floorplan floorplan, FlowLayoutPanel flowLayoutPanel)
        {
            _floorplan = floorplan;
            _flowLayoutPanel = flowLayoutPanel;
            CreateServerControls();
        }

        private void CreateServerControls()
        {
            if( _floorplan == null ) { return; }
            foreach(Server server  in _floorplan.Servers)
            {
                ServerInFloorplanControl serverControl = new ServerInFloorplanControl(server, _floorplan, _flowLayoutPanel);
                serverControls.Add(serverControl);
                
                _flowLayoutPanel.Controls.Add(serverControl);
            }
        }
        public void SetNewFloorplan(Floorplan floorplan)
        {
            serverControls.Clear();
            _flowLayoutPanel.Controls.Clear();
            this._floorplan = floorplan;
            CreateServerControls();

        }
        public void UpdateServerRoster()
        {           
            for (int i = serverControls.Count - 1; i >= 0; i--)
            {
                var sc = serverControls[i];               
                if (!_floorplan.Servers.Contains(sc.Server))
                {                    
                   _flowLayoutPanel.Controls.Remove(sc);                    
                    sc.Dispose();                    
                    serverControls.RemoveAt(i);
                }
            }
            List<Server> serversWithControls = serverControls.Select(c => c.Server).ToList();
            foreach(Server server in _floorplan.Servers)
            {
                if (!serversWithControls.Contains(server))
                {
                    ServerInFloorplanControl serverControl = new ServerInFloorplanControl(server, _floorplan, _flowLayoutPanel);
                    serverControls.Add(serverControl);
                    _flowLayoutPanel.Controls.Add(serverControl);
                }
            }
        }

        public void SetToNoFloorplan()
        {
            serverControls.Clear();
            _flowLayoutPanel.Controls.Clear();
            this._floorplan = null;
        }

        private Floorplan _floorplan { get; set; }
        private List<ServerInFloorplanControl> serverControls {  get; set; } = new List<ServerInFloorplanControl>();
        private FlowLayoutPanel _flowLayoutPanel { get; set; }
    }
}
