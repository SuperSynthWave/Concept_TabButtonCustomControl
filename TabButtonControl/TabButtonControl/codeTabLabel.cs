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
        private Label _lblTabLabel = new Label();
        public string caption
        {
            set { _lblTabLabel.Text = value; }
            get { return _lblTabLabel.Text; }
        }
        public delegate void tabClickEventHandler(TabButton sender);
        public event tabClickEventHandler tabClicked;

        private void _OnTabClick(object sender, System.EventArgs e)
        {
            try
            {
                if (Active)
                {
                    if (this.tabClicked == null)
                        return;
                    else
                        tabClicked(this);
                }
                else
                {
                    Active = true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}