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
        private bool _prop_active = true;
        public bool Active
        {
            set {
                _prop_active = value;
                this.Invalidate();
                if (value == true)
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
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private readonly Size _minSize = new Size(20, 20);
        public override Size MinimumSize {
            get => base.MinimumSize;
            set{
                if (value.Height < _minSize.Height || value.Width < _minSize.Width)
                    base.MinimumSize = _minSize;
                else
                    base.MinimumSize = value;
            }
        }

        private Color _prop_ActiveBackgroundColor = Color.SkyBlue;
        public Color ActiveBackgroundColor
        {
            set { _prop_ActiveBackgroundColor = value; }
            get { return _prop_ActiveBackgroundColor; }
        }
        private Color _prop_InActiveBackgroundColor = Color.Gray;
        public Color InActiveBackgroundColor
        {
            set { _prop_InActiveBackgroundColor = value; }
            get { return _prop_InActiveBackgroundColor; }
        }

        public TabButton()
        {
            _lblTabLabel.AutoSize = true;
            _lblTabLabel.AutoEllipsis = true;
            _lblTabLabel.Click += _OnTabClick;

            this._picTabIcon.BackColor = Color.Red;
            _picTabIcon.Visible = true;
            _picTabIcon.Click += _OnTabClick;
            _picTabIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            this._picTabCloseIcon.BackColor = Color.Blue;
            _picTabCloseIcon.Visible = true;
            _picTabCloseIcon.Click += _picTabCloseIcon_OnClick;
            _picTabCloseIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            this.SuspendLayout();
            this.Controls.Add(_picTabIcon);
            this.Controls.Add(_lblTabLabel);
            this.Controls.Add(_picTabCloseIcon);
            _picTabIcon.BringToFront();
            _lblTabLabel.BringToFront();
            _picTabCloseIcon.BringToFront();
            
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
                this.BackColor = InActiveBackgroundColor;
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

                this.BackColor = ActiveBackgroundColor;
                
            }
            

            _lblTabLabel.Left = _picTabIcon.Right + this.Width * 5 / 100;
            _lblTabLabel.MaximumSize = new Size( _picTabCloseIcon.Left - _picTabIcon.Right - this.Width * 10 / 100, _lblTabLabel.Height);
            _lblTabLabel.Top = this.Height / 2 - _lblTabLabel.Height / 2;

            base.OnPaint(e);
        }
    }
}
