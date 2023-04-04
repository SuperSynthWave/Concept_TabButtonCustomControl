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
    public class TabButton:Panel
    {
        public enum tabstate
        {
            active,
            inactive
        };

        public tabstate state = tabstate.active;
        public PictureBox tabIcon = new PictureBox();
        public PictureBox tabCloseIcon = new PictureBox();

        public TabButton()
        {
            this.tabIcon.BackColor = Color.Red;
            this.tabIcon.Visible = true;

            this.tabCloseIcon.BackColor = Color.Blue;
            this.tabCloseIcon.Visible = true;
            

            this.SuspendLayout();
            this.Controls.Add(this.tabIcon);
            this.Controls.Add(this.tabCloseIcon);
            this.tabIcon.BringToFront();
            this.tabCloseIcon.BringToFront();
            
            this.ResumeLayout();
            this.PerformLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(this.state == tabstate.inactive){

                int iconHeight = this.Height/2 > this.Width * 20 / 100 ? this.Width*20/100 : this.Height/2;

                this.tabIcon.Height = iconHeight;
                this.tabIcon.Width = iconHeight;
                this.tabIcon.SetBounds((int) this.Width * 10/100, this.Height/2 - this.tabIcon.Height/2, this.tabIcon.Width,this.tabIcon.Height);

                GraphicsPath iconPath = new GraphicsPath();
                iconPath.AddEllipse(this.tabIcon.DisplayRectangle);
                Region iconRegion = new Region(iconPath);
                this.tabIcon.Region = iconRegion;

                this.tabCloseIcon.Height = this.tabIcon.Height;
                this.tabCloseIcon.Width = this.tabIcon.Width;
                this.tabCloseIcon.SetBounds(this.Width - this.Width * 10 / 100 - this.tabCloseIcon.Width, this.Height / 2 - this.tabCloseIcon.Height / 2, this.tabCloseIcon.Width, this.tabCloseIcon.Height);

                GraphicsPath closeIconPath = new GraphicsPath();
                closeIconPath.AddEllipse(this.tabCloseIcon.DisplayRectangle);
                Region closeIconRegion = new Region(closeIconPath);
                this.tabCloseIcon.Region = closeIconRegion;

            }
            else
            {
                int offset = 18;
                int regionX = this.Width / offset;
                int arcRectwidth = regionX * (offset/(offset/2));
                int regionY = this.Height / (offset/4);

                GraphicsPath tabButtonPath = new GraphicsPath();
                Rectangle botLeftArcRect = new Rectangle(-1 * regionX, this.Height - regionY, arcRectwidth, regionY);
                Rectangle topLeftArcRect = new Rectangle(regionX, 0, arcRectwidth, regionY);
                Rectangle topRightArcRect = new Rectangle(this.Width - regionX - arcRectwidth, 0, arcRectwidth, regionY);
                Rectangle botRightArcRect = new Rectangle(this.Width - regionX, this.Height - regionY, arcRectwidth,regionY);
                tabButtonPath.StartFigure();
                tabButtonPath.AddArc(botLeftArcRect, 90,-90);
                tabButtonPath.AddArc(topLeftArcRect, 180, 90);
                tabButtonPath.AddArc(topRightArcRect, 270, 90);
                tabButtonPath.AddArc(botRightArcRect, 180, -90);
                tabButtonPath.CloseFigure();
                this.Region = new Region(tabButtonPath);

                int iconHeight = this.Height / 2 > this.Width * 20 / 100 ? this.Width * 20 / 100 : this.Height / 2;

                this.tabIcon.Height = iconHeight;
                this.tabIcon.Width = iconHeight;
                this.tabIcon.SetBounds((int)this.Width * 10 / 100, this.Height / 2 - this.tabIcon.Height / 2, this.tabIcon.Width, this.tabIcon.Height);

                GraphicsPath iconPath = new GraphicsPath();
                iconPath.AddEllipse(this.tabIcon.DisplayRectangle);
                Region iconRegion = new Region(iconPath);
                this.tabIcon.Region = iconRegion;

                this.tabCloseIcon.Height = this.tabIcon.Height;
                this.tabCloseIcon.Width = this.tabIcon.Width;
                this.tabCloseIcon.SetBounds(this.Width - this.Width * 10 / 100 - this.tabCloseIcon.Width, this.Height / 2 - this.tabCloseIcon.Height / 2, this.tabCloseIcon.Width, this.tabCloseIcon.Height);

                GraphicsPath closeIconPath = new GraphicsPath();
                closeIconPath.AddEllipse(this.tabCloseIcon.DisplayRectangle);
                Region closeIconRegion = new Region(closeIconPath);
                this.tabCloseIcon.Region = closeIconRegion;
            }
            this.BackColor = Color.Green;
            

            base.OnPaint(e);
        }
    }
}
