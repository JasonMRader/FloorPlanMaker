using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class ClickOutsideDialogFilter : IMessageFilter
    {
        private readonly Form _form;

        public ClickOutsideDialogFilter(Form form)
        {
            _form = form;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x201) // WM_LBUTTONDOWN message
            {
                Point cursorPosition = Control.MousePosition;
                if (!_form.Bounds.Contains(cursorPosition))
                {
                    _form.DialogResult = DialogResult.Cancel;
                    _form.Close();
                    return true;
                }
            }
            return false;
        }
    }

}
