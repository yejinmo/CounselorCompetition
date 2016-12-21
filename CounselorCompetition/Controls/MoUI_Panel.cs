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
    public class MoUI_Panel : Panel
    {

        private Bitmap _bG = null;
        public bool ControlNeedRefresh = false;
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

        public Bitmap _BG
        {
            get
            {
                return _bG;
            }

            set
            {
                _bG = value;
            }
        }

        public MoUI_Panel()
        {

        }

        private bool _DrawLineEnabled = false;
        private int _DrawLineLeft = 0;
        private Color _DrawLineColor = Color.White;
        public void DrawLine(bool Enabled, int Left, Color color)
        {
            _DrawLineEnabled = Enabled;
            _DrawLineLeft = Left;
            _DrawLineColor = color;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            if (_BG != null && !_BGColorStyle)
                e.Graphics.DrawImage(_BG, new Point(0, 0));
            if (_DrawLineEnabled)
            {
                e.Graphics.DrawLine(new Pen(_DrawLineColor), new Point(_DrawLineLeft, 0), new Point(_DrawLineLeft, Height));
            }
            if (ControlNeedRefresh && (_BG != null))
            {
                ControlNeedRefresh = false;
                MoUI_Controler.RefreshMoUIControlBackgroundImage(Controls, _BG, ForeColor);
                base.OnPaint(e);
            }
        }

    }
}