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
    public  partial class TabButton : Panel
    {
        private PictureBox _picTabIcon = new PictureBox();
        public Image tabIcon
        {
            set { _picTabIcon.Image = value; }
            get { return _picTabIcon.Image; }
        }
    }
}