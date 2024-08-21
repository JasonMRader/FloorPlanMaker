using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public interface IServerObserver
    {
        void OnServerSectionChange(Server server, Section section);
    }
}
