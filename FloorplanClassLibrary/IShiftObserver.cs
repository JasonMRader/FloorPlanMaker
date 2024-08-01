using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public interface IShiftObserver
    {
        void UpdateShift(Shift shift);
    }
}
