using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace CounselorCompetition.Controls
{
    public class MoUI_Controler
    {

        public static void RefreshMoUIControlBackgroundImage(ControlCollection CC, Bitmap _BG, Color TextColor)
        {
            try
            {
                foreach (var ctl in CC)
                {
                    CloneBitmap(ctl, _BG, TextColor);
                }
            }
            catch
            {
            }
        }

        private static void CloneBitmap(object ctl, Bitmap _BG, Color TextColor)
        {
            try
            {
                if (ctl is MoUI_Label)
                {
                    MoUI_Label c = (MoUI_Label)ctl;
                    c.BG = _BG.Clone(new Rectangle(c.Left, c.Top, c.Width, c.Height), _BG.PixelFormat);
                    c.Invalidate();
                }
                if (ctl is MoUI_Switch)
                {
                    MoUI_Switch c = (MoUI_Switch)ctl;
                    c._BG = _BG.Clone(new Rectangle(c.Left, c.Top, c.Width, c.Height), _BG.PixelFormat);
                    c.Invalidate();
                }
                if (ctl is MoUI_TextCover)
                {
                    MoUI_TextCover c = (MoUI_TextCover)ctl;
                    c._BG = _BG.Clone(new Rectangle(c.Left, c.Top, c.Width, c.Height), _BG.PixelFormat);
                    c.Invalidate();
                }
                if (ctl is MoUI_Button)
                {
                    MoUI_Button c = (MoUI_Button)ctl;
                    c._BG = _BG.Clone(new Rectangle(c.Left, c.Top, c.Width, c.Height), _BG.PixelFormat);
                    c.Invalidate();
                }
                if (ctl is MoUI_Panel)
                {
                    MoUI_Panel c = (MoUI_Panel)ctl;
                    c.ControlNeedRefresh = true;
                    c._BG = _BG.Clone(new Rectangle(c.Left, c.Top, c.Width, c.Height), _BG.PixelFormat);
                    c.Invalidate();
                }
                if (ctl is MoUI_PictureBox)
                {
                    MoUI_PictureBox c = (MoUI_PictureBox)ctl;
                    c._BG = _BG.Clone(new Rectangle(c.Left, c.Top, c.Width, c.Height), _BG.PixelFormat);
                    c.Invalidate();
                }
                //if (ctl is MoUI_OptionBUtton)
                //{
                //    MoUI_OptionBUtton c = (MoUI_OptionBUtton)ctl;
                //    c.Invalidate();
                //}
                if (ctl is MoUI_ListView)
                {
                    MoUI_ListView c = (MoUI_ListView)ctl;
                    c._BG = _BG.Clone(new Rectangle(c.Left, c.Top, c.Width, c.Height), _BG.PixelFormat);
                    c.Invalidate();
                }
            }
            catch
            {
            }
        }

        public static void SetTextColor(ControlCollection CC, Color TextColor)
        {
            try
            {
                foreach (var ctl in CC)
                {
                    if (ctl is MoUI_Label)
                    {
                        MoUI_Label c = (MoUI_Label)ctl;
                        if (c.FontColor != TextColor)
                            c.FontColor = TextColor;
                        c.Invalidate();
                    }
                    if (ctl is MoUI_TextCover)
                    {
                        MoUI_TextCover c = (MoUI_TextCover)ctl;
                        if (c._NormalFontColor != TextColor)
                            c._NormalFontColor = TextColor;
                        c.Invalidate();
                    }
                    if (ctl is MoUI_ListView)
                    {
                        MoUI_ListView c = (MoUI_ListView)ctl;
                        if (c.ForeColor != TextColor)
                            c.ForeColor = TextColor;
                        c.Invalidate();
                    }
                    if (ctl is MoUI_OptionBUtton)
                    {
                        MoUI_OptionBUtton c = (MoUI_OptionBUtton)ctl;
                        if (c._NormalFontColor != TextColor)
                            c._NormalFontColor = TextColor;
                        c.Invalidate();
                    }

                }
            }
            catch
            {
            }
        }

    }
}
