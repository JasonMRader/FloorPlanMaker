using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public static class TableEditorFactory
    {
        public static TableEditorControl CreateEditor(TableControl tableControl)
        {
            return new TableEditorControl(tableControl)
            {

            };
        }
    }
}
