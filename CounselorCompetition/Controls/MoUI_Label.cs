using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Drawing.Drawing2D;
using CounselorCompetition.ImageAlgorithm;

namespace CounselorCompetition.Controls
{

    [ToolboxBitmap(typeof(MoUI_Label))]
    public class MoUI_Label : Control
    {
        public bool drag = false;
        public bool enab = false;
        private bool _autoSize = true;
        private int m_opacity = 200;
        private int hoverOpacity = 255;
        private string _text;
        private System.Timers.Timer TimerTrans = new System.Timers.Timer(40);
        private bool IsMouseIn = false;
        private bool IsMouseDown = false;
        private bool isSelected = true;
        private int current_opacity;
        private Bitmap bG;
        private Color fontColor = Color.Black;
        private Font _font = new Font("微软雅黑", 20, FontStyle.Regular, GraphicsUnit.Pixel);

        public MoUI_Label()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            TimerTrans.Elapsed += new System.Timers.ElapsedEventHandler(TimerTrans_Event);
            TimerTrans.AutoReset = true;
            TimerTrans.Enabled = true;
        }

        private void TimerTrans_Event(object sender, ElapsedEventArgs e)
        {
            if (IsMouseIn)
            {
                current_opacity += 30;
                if (current_opacity > hoverOpacity)
                {
                    current_opacity = hoverOpacity;
                    TimerTrans.Enabled = false;
                }
            }
            else
            {
                current_opacity -= 30;
                if (current_opacity < Opacity)
                {
                    current_opacity = Opacity;
                    TimerTrans.Enabled = false;
                }
            }
            Invalidate();
        }

        public int Opacity
        {
            get
            {
                if (m_opacity > 255)
                {
                    m_opacity = 255;
                }
                else if (m_opacity < 1)
                {
                    m_opacity = 1;
                }
                return this.m_opacity;
            }
            set
            {
                this.m_opacity = value;
                current_opacity = value;
                if (this.Parent != null)
                {
                    Parent.Invalidate(this.Bounds, true);
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        public bool _AutoSize
        {
            get
            {
                return _autoSize;
            }

            set
            {
                _autoSize = value;
                Invalidate();
            }
        }

        public string _Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                Invalidate();
            }
        }

        public Bitmap BG
        {
            get
            {
                return bG;
            }

            set
            {
                bG = value;
            }
        }

        public Color FontColor
        {
            get
            {
                return fontColor;
            }

            set
            {
                fontColor = value;
                Invalidate();
            }
        }

        public int HoverOpacity
        {
            get
            {
                return hoverOpacity;
            }

            set
            {
                if (value > 255)
                    value = 255;
                if (value < Opacity)
                    value = Opacity;
                hoverOpacity = value;
            }
        }

        public Font _Font
        {
            get
            {
                return _font;
            }

            set
            {
                _font = value;
                Invalidate();
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            {
                //Draw Background
                {
                    if (BG != null)
                        g.DrawImage(BG, new Point(0, 0));
                }
                //Draw Text
                {
                    SizeF sf;
                    if (_AutoSize)
                    {
                        Graphics gs = this.CreateGraphics();
                        sf = gs.MeasureString(_Text, _Font);
                        Size = new Size((int)sf.Width, (int)sf.Height);
                        gs.Dispose();
                    }
                    Point point;
                    if (IsMouseDown)
                        point = new Point(1, 1);
                    else
                        point = new Point(0, 0);
                    if (IsSelected)
                        g.DrawString(_Text, _Font, new SolidBrush(Color.FromArgb(HoverOpacity, FontColor)), point);
                    else
                        g.DrawString(_Text, _Font, new SolidBrush(Color.FromArgb(current_opacity, FontColor)), point);
                }
            }
            g.Save();
            g.Dispose();
            e.Graphics.DrawImage(b, new Point(0, 0));
            b.Dispose();
            base.OnPaint(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (this.Parent != null)
            {
                Parent.Invalidate(this.Bounds, true);
            }
            base.OnBackColorChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnParentBackColorChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!IsMouseDown)
            {
                IsMouseIn = true;
                TimerTrans.Enabled = true;
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!IsMouseDown)
            {

                IsMouseIn = false;
                TimerTrans.Enabled = true;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            IsMouseDown = true;
            IsMouseIn = false;
            TimerTrans.Enabled = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            IsMouseDown = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!IsMouseDown && !IsMouseIn)
            {
                IsMouseIn = true;
                TimerTrans.Enabled = true;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

    }
    
}
