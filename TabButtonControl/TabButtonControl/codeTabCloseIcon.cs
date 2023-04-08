using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TabButtonControl
{
    public partial class TabButton : Panel
    {
        private PictureBox _picTabCloseIcon = new PictureBox();
        public Image tabCloseIcon
        {
            set { _picTabCloseIcon.Image = value; }
            get { return _picTabCloseIcon.Image; }
        }
        public delegate void tabCloseIconClickEventHandler(TabButton sender);
        public event tabClickEventHandler tabCloseIconClicked;
        private void _picTabCloseIcon_OnClick(object sender, System.EventArgs e)
        {
            try
            {
                if (tabCloseIconClicked == null)
                    return;
                else
                    tabCloseIconClicked(this);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}