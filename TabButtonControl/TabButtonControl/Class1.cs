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
    public class TabButton : Panel
    {
        private Label _lblTabLabel = new Label();
        public string caption
        {
            set {_lblTabLabel.Text = value;}
            get { return _lblTabLabel.Text; }
        }
        public delegate void tabClickEventHandler(TabButton sender);
        public event tabClickEventHandler tabClicked;
        
        private void _OnTabClick(object sender,System.EventArgs e)
        {
            try
            {
                if (tabClicked == null)
                    return;
                else
                    tabClicked(this);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        private PictureBox _picTabIcon = new PictureBox();
        public Image tabIcon
        {
            set { _picTabIcon.Image = value; }
            get { return _picTabIcon.Image; }
        }

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

        private bool _prop_active = true;
        public bool Active
        {
            set {
                _prop_active = value;
                this.OnPaint(new System.Windows.Forms.PaintEventArgs(this.CreateGraphics(),this.DisplayRectangle));
                if (value)
                    if(tabActivated != null) tabActivated(this);
                else
                    if(tabDeActivated != null) tabDeActivated(this);
            }
            get { return _prop_active; }
        }
        public delegate void tabStateChangedEventHandler(TabButton sender);
        public event tabStateChangedEventHandler tabActivated;
        public event tabStateChangedEventHandler tabDeActivated;

        protected override void OnClick(EventArgs e)
        {
            try
            {
                if (this.tabClicked == null)
                    return;
                else
                    tabClicked(this);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        public TabButton()
        {
            _lblTabLabel.AutoSize = true;
            _lblTabLabel.AutoEllipsis = true;
            _lblTabLabel.Click += _OnTabClick;

            this._picTabIcon.BackColor = Color.Red;
            this._picTabIcon.Visible = true;
            _picTabIcon.Click += _OnTabClick;
            _picTabIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            this._picTabCloseIcon.BackColor = Color.Blue;
            this._picTabCloseIcon.Visible = true;
            _picTabCloseIcon.Click += _picTabCloseIcon_OnClick;
            _picTabCloseIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            this.SuspendLayout();
            this.Controls.Add(this._picTabIcon);
            this.Controls.Add(_lblTabLabel);
            this.Controls.Add(this._picTabCloseIcon);
            this._picTabIcon.BringToFront();
            _lblTabLabel.BringToFront();
            this._picTabCloseIcon.BringToFront();
            
            this.ResumeLayout();
            this.PerformLayout();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if(!Active){
                int iconHeight = this.Height/2 > this.Width * 20 / 100 ? this.Width*20/100 : this.Height/2;

                this._picTabIcon.Height = iconHeight;
                this._picTabIcon.Width = iconHeight;
                this._picTabIcon.SetBounds((int) this.Width * 5/100, this.Height/2 - this._picTabIcon.Height/2, this._picTabIcon.Width,this._picTabIcon.Height);

                GraphicsPath iconPath = new GraphicsPath();
                iconPath.AddEllipse(this._picTabIcon.DisplayRectangle);
                Region iconRegion = new Region(iconPath);
                this._picTabIcon.Region = iconRegion;

                this._picTabCloseIcon.Height = this._picTabIcon.Height;
                this._picTabCloseIcon.Width = this._picTabIcon.Width;
                this._picTabCloseIcon.SetBounds(this.Width - this.Width * 5 / 100 - this._picTabCloseIcon.Width, this.Height / 2 - this._picTabCloseIcon.Height / 2, this._picTabCloseIcon.Width, this._picTabCloseIcon.Height);

                GraphicsPath closeIconPath = new GraphicsPath();
                closeIconPath.AddEllipse(this._picTabCloseIcon.DisplayRectangle);
                Region closeIconRegion = new Region(closeIconPath);
                this._picTabCloseIcon.Region = closeIconRegion;

                this.Region = new Region(this.ClientRectangle);
                this.BackColor = Color.Gray;
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

                this._picTabIcon.Height = iconHeight;
                this._picTabIcon.Width = iconHeight;
                this._picTabIcon.SetBounds((int)this.Width * 10 / 100, this.Height / 2 - this._picTabIcon.Height / 2, this._picTabIcon.Width, this._picTabIcon.Height);

                GraphicsPath iconPath = new GraphicsPath();
                iconPath.AddEllipse(this._picTabIcon.DisplayRectangle);
                Region iconRegion = new Region(iconPath);
                this._picTabIcon.Region = iconRegion;

                this._picTabCloseIcon.Height = this._picTabIcon.Height;
                this._picTabCloseIcon.Width = this._picTabIcon.Width;
                this._picTabCloseIcon.SetBounds(this.Width - this.Width * 10 / 100 - this._picTabCloseIcon.Width, this.Height / 2 - this._picTabCloseIcon.Height / 2, this._picTabCloseIcon.Width, this._picTabCloseIcon.Height);

                GraphicsPath closeIconPath = new GraphicsPath();
                closeIconPath.AddEllipse(this._picTabCloseIcon.DisplayRectangle);
                Region closeIconRegion = new Region(closeIconPath);
                this._picTabCloseIcon.Region = closeIconRegion;

                this.BackColor = Color.Green;
                
            }
            

            _lblTabLabel.Left = _picTabIcon.Right + this.Width * 5 / 100;
            _lblTabLabel.MaximumSize = new Size( _picTabCloseIcon.Left - _picTabIcon.Right - this.Width * 10 / 100, _lblTabLabel.Height);
            _lblTabLabel.Top = this.Height / 2 - _lblTabLabel.Height / 2;

            base.OnPaint(e);
        }
    }
}
