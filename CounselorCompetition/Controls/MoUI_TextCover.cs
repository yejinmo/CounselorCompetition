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
    public class MoUI_TextCover : Control
    {

        #region 属性

        private string _hintText = "显示";
        private string _text = "内容";
        private bool _isShow = false;
        private Bitmap _bG;
        private double _normalOpacity = 0.5;
        private double _hoverOpacity = 0.8;
        private int CurrentOpacityPercent = 0;
        private System.Timers.Timer TimerOpacity = new System.Timers.Timer(15);
        private bool IsTransing = false;
        private bool IsMouseIn = false;
        private Color _backColor = Color.FromArgb(153, 153, 153);
        private Color _borderColor = Color.FromArgb(17, 17, 17);
        private Color _hoverBorderColor = Color.FromArgb(204, 204, 204);
        private Font _font = new Font("微软雅黑", 20, FontStyle.Regular, GraphicsUnit.Pixel);
        private Color _hintFontColor = Color.Black;
        private Color _normalFontColor = Color.White;
        private bool _autoSize = true;
        private bool _IsMouseDown = false;
        private MoUI_Switch _moUI_Switch = null;

        public string _HintText
        {
            get
            {
                return _hintText;
            }

            set
            {
                _hintText = value;
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

        public bool _IsShow
        {
            get
            {
                return _isShow;
            }

            set
            {
                _isShow = value;
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

        public Color _HintFontColor
        {
            get
            {
                return _hintFontColor;
            }

            set
            {
                _hintFontColor = value;
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

        public MoUI_Switch _MoUI_Switch
        {
            get
            {
                return _moUI_Switch;
            }

            set
            {
                if (value != null)
                    value.Visible = _IsShow;
                _moUI_Switch = value;
            }
        }

        #endregion

        #region 初始化 

        public MoUI_TextCover()
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
            g.SmoothingMode = SmoothingMode.AntiAlias;
            {
                //Draw Background
                {
                    if(!_IsShow)
                        g.Clear(_BackColor);
                }
                //Draw Border
                {
                    Pen p = new Pen(_BorderColor);
                    g.DrawLine(p, new Point(2, 1), new Point(Width - 2, 1));
                    g.DrawLine(p, new Point(Width - 2, 2), new Point(Width - 2, Height - 2));
                    g.DrawLine(p, new Point(Width - 3, Height - 2), new Point(1, Height - 2));
                    g.DrawLine(p, new Point(1, Height - 3), new Point(1, 1));
                    if (_IsShow)
                    {
                        Pen p_outer = new Pen(_BackColor);
                        g.DrawLine(p_outer, new Point(0, 0), new Point(Width, 0));
                        g.DrawLine(p_outer, new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
                        g.DrawLine(p_outer, new Point(Width - 1, Height - 1), new Point(0, Height - 1));
                        g.DrawLine(p_outer, new Point(0, Height), new Point(0, 0));
                    }
                }
                //Draw Text
                {
                    //Draw HintText
                    if (!_IsShow)
                    {
                        SizeF size_of_HintText;
                        {
                            Graphics gs = this.CreateGraphics();
                            size_of_HintText = gs.MeasureString(_HintText, _Font);
                            gs.Dispose();
                        }
                        g.DrawString(_HintText, _Font, new SolidBrush(_HintFontColor),
                            new Point((Width - (int)size_of_HintText.Width) / 2 + (_IsMouseDown ? 1 : 0), 
                            (Height - (int)size_of_HintText.Height) / 2 + (_IsMouseDown ? 1 : 0)));
                    }
                    //Draw Text
                    else
                    {
                        SizeF size_of_NormalText;
                        {
                            Graphics gs = this.CreateGraphics();
                            size_of_NormalText = gs.MeasureString(_Text, _Font);
                            gs.Dispose();
                        }
                        g.DrawString(_Text, _Font, new SolidBrush(_NormalFontColor),
                            new Point((Width - (int)size_of_NormalText.Width) / 2 + (_IsMouseDown ? 1 : 0),
                            (Height - (int)size_of_NormalText.Height) / 2 + (_IsMouseDown ? 1 : 0)));
                        //Draw TextCover
                        {

                        }
                    }
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
            _IsShow = !_IsShow;
            if (_MoUI_Switch != null)
                _MoUI_Switch.Visible = _IsShow;
            Invalidate();
            base.OnClick(e);
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
