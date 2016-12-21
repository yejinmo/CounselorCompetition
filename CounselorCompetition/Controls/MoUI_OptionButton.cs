using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Drawing.Drawing2D;
using CounselorCompetition.ImageAlgorithm;
using System.Data.SQLite;
using CounselorCompetition.Struct;

namespace CounselorCompetition.Controls
{

    [ToolboxBitmap(typeof(MoUI_Switch))]
    public class MoUI_OptionBUtton : Control
    {

        #region 属性

        private string _text = "内容";
        private string _textB = "内容";
        private Bitmap _bG;
        private double _normalOpacity = 0.5;
        private double _hoverOpacity = 0.8;
        private int CurrentOpacityPercent = 0;
        private System.Timers.Timer TimerOpacity = new System.Timers.Timer(15);
        private bool IsTransing = false;
        private bool IsMouseIn = false;
        private Color _backColor = Color.FromArgb(153, 153, 153);
        private Color _borderColor = Color.FromArgb(255, 255, 255);
        private Color _hoverBorderColor = Color.FromArgb(204, 204, 204);
        private Font _font = new Font("微软雅黑", 20, FontStyle.Regular, GraphicsUnit.Pixel);
        private Color _normalFontColor = Color.White;
        private bool _autoSize = true;
        private bool _IsMouseDown = false;
        private bool _isSelected = false;
        public TeacherInfoStruct teacherInfoStruct;

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

        public string _TextB
        {
            get
            {
                return _textB;
            }

            set
            {
                _textB = value;
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

        public Color _NormalFontColor
        {
            get
            {
                return _normalFontColor;
            }

            set
            {
                _normalFontColor = value;
                Invalidate();
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

        public bool _IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                Invalidate();
            }
        }

        private bool _bGColorStyle = false;

        public bool _BGColorStyle
        {
            get
            {
                return _bGColorStyle;
            }
            set
            {
                _bGColorStyle = value;
                if (value)
                    Invalidate();
            }
        }

        #endregion

        #region 初始化 

        public MoUI_OptionBUtton()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            TimerOpacity.Elapsed += new ElapsedEventHandler(TimerOpacity_Event);
            TimerOpacity.AutoReset = true;
            TimerOpacity.Enabled = false;
        }

        #endregion

        #region 绘制

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            {
                //try
                //{
                //    if(NeedRefresh)
                //        if (Parent is MoUI_Panel)
                //        {
                //            if (((MoUI_Panel)Parent)._BG != null)
                //            {
                //                if (_BG == null)
                //                    _BG = new Bitmap(Width, Height);
                //                _BG = ((MoUI_Panel)Parent)._BG.Clone(Bounds, ((MoUI_Panel)Parent)._BG.PixelFormat);
                //                NeedRefresh = false;
                //            }
                //        }
                //}
                //catch { }
               //Draw Background
                {
                    g.Clear(_BackColor);
                    if (_IsSelected)
                    {
                        if (_BG != null && !_BGColorStyle)
                            g.DrawImage(_BG, new Point(0, 0));
                        Bitmap bb = new Bitmap(Width, Height);
                        Graphics gb = Graphics.FromImage(bb);
                        gb.FillRectangle(new SolidBrush(Color.FromArgb(20, 0, 0, 255)), new Rectangle(0, 0, Width, Height));
                        gb.Save();
                        gb.Dispose();
                        bb = (Bitmap)ImageEx.ChangeImageOpacity(bb, 0.5);
                        g.DrawImage(bb, new Point(0, 0));
                        bb.Dispose();
                    }
                    else
                    {
                        if (_BG != null && !_BGColorStyle)
                            g.DrawImage(_BG, new Point(0, 0));
                        Bitmap bb = new Bitmap(Width, Height);
                        Graphics gb = Graphics.FromImage(bb);
                        gb.FillRectangle(new SolidBrush(Color.FromArgb(20, 255, 255, 255)), new Rectangle(0, 0, Width, Height));
                        gb.Save();
                        gb.Dispose();
                        bb = (Bitmap)ImageEx.ChangeImageOpacity(bb, (CurrentOpacityPercent / 100.0) * 0.5);
                        g.DrawImage(bb, new Point(0, 0));
                        bb.Dispose();
                    }
                }
                //Draw Border
                {
                    if (_IsSelected)
                    {
                        Pen p = new Pen(Color.FromArgb(28, 100, 160));
                        g.DrawLine(p, new Point(2, 1), new Point(Width - 2, 1));
                        g.DrawLine(p, new Point(Width - 2, 2), new Point(Width - 2, Height - 2));
                        g.DrawLine(p, new Point(Width - 3, Height - 2), new Point(1, Height - 2));
                        g.DrawLine(p, new Point(1, Height - 3), new Point(1, 1));
                    }
                    else if (IsMouseIn)
                    {
                        Pen p = new Pen(Color.FromArgb(125, 255, 255, 255));
                        g.DrawLine(p, new Point(2, 1), new Point(Width - 2, 1));
                        g.DrawLine(p, new Point(Width - 2, 2), new Point(Width - 2, Height - 2));
                        g.DrawLine(p, new Point(Width - 3, Height - 2), new Point(1, Height - 2));
                        g.DrawLine(p, new Point(1, Height - 3), new Point(1, 1));
                    }
                    
                }
                //Draw Text
                {
                    //Draw Text
                    SizeF size_of_Text;
                    {
                        Graphics gs = this.CreateGraphics();
                        size_of_Text = gs.MeasureString(_Text, _Font);
                        gs.Dispose();
                    }
                    g.DrawString(_Text, _Font, new SolidBrush(_NormalFontColor),
                        new Point((Width - (int)size_of_Text.Width) / 2 + (_IsMouseDown ? 1 : 0),
                        (Height - (int)size_of_Text.Height) / 2 + (_IsMouseDown ? 1 : 0) - (Height / 4)));
                    //Draw Text
                    SizeF size_of_TextB;
                    {
                        Graphics gs = this.CreateGraphics();
                        size_of_TextB = gs.MeasureString(_TextB, _Font);
                        gs.Dispose();
                    }
                    g.DrawString(_TextB, _Font, new SolidBrush(_NormalFontColor),
                        new Point((Width - (int)size_of_TextB.Width) / 2 + (_IsMouseDown ? 1 : 0),
                        (Height - (int)size_of_TextB.Height) / 2 + (_IsMouseDown ? 1 : 0) + (Height / 4)));

                }
            }
            g.Save();
            g.Dispose();
            if (_BG != null && !_BGColorStyle)
            {
                if (_IsSelected)
                {

                }
                else
                {
                    b = (Bitmap)ImageEx.ChangeImageOpacity(b, (_HoverOpacity - _NormalOpacity) * (CurrentOpacityPercent / 100.0) + _NormalOpacity);
                    b = ImageEx.JoinMImage((Bitmap)_bG.Clone(), b, ClientRectangle);
                }
            }
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _IsMouseDown = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _IsMouseDown = false;
            Invalidate();
            base.OnMouseUp(e);
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
