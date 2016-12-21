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

    [ToolboxBitmap(typeof(MoUI_Switch))]
    public class MoUI_Switch : Control
    {

        #region 属性

        private Color _backColor = Color.FromArgb(153, 153, 153);
        private Color _borderColor = Color.FromArgb(17, 17, 17);
        private Color _hoverBorderColor = Color.FromArgb(204, 204, 204);
        private Color _switchBarColor = Color.FromArgb(204, 204, 204);
        private Color _switchedColor = Color.FromArgb(0, 177, 89);
        private bool IsTransing = false;
        private bool isSwitched = false;
        private Bitmap _bG;
        private bool IsMouseIn = false;
        private int CurrentTransPercent = 0;
        private int CurrentOpacityPercent = 0;
        private System.Timers.Timer TimerTrans = new System.Timers.Timer(15);
        private System.Timers.Timer TimerOpacity = new System.Timers.Timer(15);
        private double _normalOpacity = 0.5;
        private double _hoverOpacity = 0.8;

        public Color _BackColor
        {
            get
            {
                return _backColor;
            }

            set
            {
                _backColor = value;
                Invalidate();
            }
        }

        public Color _BorderColor
        {
            get
            {
                return _borderColor;
            }

            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        public Color _HoverBorderColor
        {
            get
            {
                return _hoverBorderColor;
            }

            set
            {
                _hoverBorderColor = value;
                Invalidate();
            }
        }

        public Color _SwitchBarColor
        {
            get
            {
                return _switchBarColor;
            }

            set
            {
                _switchBarColor = value;
                Invalidate();
            }
        }

        public Color _SwitchedColor
        {
            get
            {
                return _switchedColor;
            }

            set
            {
                _switchedColor = value;
                Invalidate();
            }
        }

        public bool IsSwitched
        {
            get
            {
                return isSwitched;
            }

            set
            {
                isSwitched = value;
                TimerTrans.Enabled = true;
                Invalidate();
            }
        }



        public Bitmap _BG
        {
            get
            {
                return _bG;
            }

            set
            {
                _bG = value;
                //Invalidate();
            }
        }

        public double _NormalOpacity
        {
            get
            {
                return _normalOpacity;
            }

            set
            {
                if (value > 1)
                    value = 1;
                if (value < 0)
                    value = 0;
                _normalOpacity = value;
                Invalidate();
            }
        }

        public double _HoverOpacity
        {
            get
            {
                return _hoverOpacity;
            }

            set
            {
                if (value > 1)
                    value = 1;
                if (value < 0)
                    value = 0;
                if (value < _NormalOpacity)
                    value = _NormalOpacity;
                _hoverOpacity = value;
                Invalidate();
            }
        }

        #endregion

        #region 初始化

        public MoUI_Switch()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            Width = 50;
            Height = 18;
            TimerTrans.Elapsed += new System.Timers.ElapsedEventHandler(TimerTrans_Event);
            TimerTrans.AutoReset = true;
            TimerTrans.Enabled = false;
            TimerOpacity.Elapsed += new System.Timers.ElapsedEventHandler(TimerOpacity_Event);
            TimerOpacity.AutoReset = true;
            TimerOpacity.Enabled = false;
        }

        #endregion

        #region 绘制方法

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            {
                //if (_BG != null)
                //    g.DrawImage(_BG, new Point(0, 0));
                Size Bar_Size = new Size(12, Height);
                //Draw BackGround
                {
                    g.Clear(_BackColor);
                    g.FillRectangle(
                        new SolidBrush(Color.FromArgb((int)(255 * (CurrentTransPercent / 100.0)), _SwitchedColor)),
                        new Rectangle(2, 2, (int)((Width - Bar_Size.Width) * (CurrentTransPercent / 100.0)) - 2, Height - 4));
                }
                //Draw Border
                {
                    Pen p = new Pen(_BorderColor);
                    g.DrawLine(p, new Point(2, 1), new Point(Width - 2, 1));
                    g.DrawLine(p, new Point(Width - 2, 2), new Point(Width - 2, Height - 2));
                    g.DrawLine(p, new Point(Width - 3, Height - 2), new Point(1, Height - 2));
                    g.DrawLine(p, new Point(1, Height - 3), new Point(1, 1));
                }
                //Draw HighLight Border
                if(IsMouseIn)
                {
                    Pen p = new Pen(_HoverBorderColor);
                    g.DrawLine(p, new Point(0, 0), new Point(Width, 0));
                    g.DrawLine(p, new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
                    g.DrawLine(p, new Point(Width - 1, Height - 1), new Point(0, Height - 1));
                    g.DrawLine(p, new Point(0, Height), new Point(0, 0));
                }
                //Draw SwitchBar
                {
                    Bitmap b_Bar = new Bitmap(Bar_Size.Width, Bar_Size.Height);
                    Graphics g_Bar = Graphics.FromImage(b_Bar);

                    //SwitchBar BackGround
                    g_Bar.Clear(_SwitchBarColor);
                    //SwitchBar Border
                    Pen p = new Pen(_BorderColor);
                    g_Bar.DrawLine(p, new Point(0, 0), new Point(Bar_Size.Width, 0));
                    g_Bar.DrawLine(p, new Point(Bar_Size.Width - 1, 0), new Point(Bar_Size.Width - 1, Bar_Size.Height - 1));
                    g_Bar.DrawLine(p, new Point(Bar_Size.Width - 1, Bar_Size.Height - 1), new Point(0, Bar_Size.Height - 1));
                    g_Bar.DrawLine(p, new Point(0, Bar_Size.Height), new Point(0, 0));

                    g_Bar.Save();
                    g_Bar.Dispose();
                    g.DrawImage(b_Bar, new Point((int)((Width - Bar_Size.Width) * (CurrentTransPercent / 100.0)), 0));
                    b_Bar.Dispose();
                }
            }
            g.Save();
            g.Dispose();
            if (_bG != null)
            {
                b = (Bitmap)ImageEx.ChangeImageOpacity(b, (_HoverOpacity - _NormalOpacity) * (CurrentOpacityPercent / 100.0) + _NormalOpacity);
                b = ImageEx.JoinMImage((Bitmap)_bG.Clone(), b, ClientRectangle);
            }
            //b = ImageEx.BrightnessP(b, -25);
            e.Graphics.DrawImage(b, new Point(0, 0));
            b.Dispose();
            base.OnPaint(e);
        }

        #endregion

        #region 事件

        protected override void OnMouseEnter(EventArgs e)
        {
            IsMouseIn = true;
            TimerOpacity.Enabled = true;
            if (!IsTransing)
                Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            IsMouseIn = false;
            TimerOpacity.Enabled = true;
            if (!IsTransing)
                Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            IsSwitched = !IsSwitched;
            TimerTrans.Enabled = true;
            base.OnClick(e);
        }

        private void TimerTrans_Event(object sender, ElapsedEventArgs e)
        {
            IsTransing = true;
            if (IsSwitched)
            {
                CurrentTransPercent += 10;
                if (CurrentTransPercent > 100)
                {
                    CurrentTransPercent = 100;
                    IsTransing = false;
                    TimerTrans.Enabled = false;
                }
            }
            else
            {
                CurrentTransPercent -= 10;
                if (CurrentTransPercent < 0)
                {
                    CurrentTransPercent = 0;
                    IsTransing = false;
                    TimerTrans.Enabled = false;
                }
            }
            Invalidate();
        }

        private void TimerOpacity_Event(object sender, ElapsedEventArgs e)
        {
            if (IsMouseIn)
            {
                CurrentOpacityPercent += 15;
                if (CurrentOpacityPercent > 100)
                {
                    CurrentOpacityPercent = 100;
                    TimerOpacity.Enabled = false;
                }
            }
            else
            {
                CurrentOpacityPercent -= 15;
                if (CurrentOpacityPercent < 0)
                {
                    CurrentOpacityPercent = 0;
                    TimerOpacity.Enabled = false;
                }
            }
            Invalidate();
        }

        #endregion

    }

}
