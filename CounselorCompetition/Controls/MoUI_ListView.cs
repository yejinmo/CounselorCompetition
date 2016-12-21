using CounselorCompetition.ImageAlgorithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CounselorCompetition.Controls
{
    public class MoUI_ListView : ListView
    {

        #region 属性

        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MouseStateEnum MouseState { get; set; }

        [Browsable(false)]
        public Point MouseLocation { get; set; }

        private const int ITEM_PADDING = 18;

        private Bitmap _bG;

        public Bitmap _BG
        {
            get
            {
                return _bG;
            }

            set
            {
                _bG = value;
                if (value == null)
                    return;
                Invalidate();
            }
        }

        public enum MouseStateEnum
        {
            HOVER,
            DOWN,
            OUT
        }

        #endregion

        #region 初始化

        public MoUI_ListView()
        {
            GridLines = false;
            FullRowSelect = true;
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            View = View.Details;
            OwnerDraw = true;
            ResizeRedraw = true;
            BorderStyle = BorderStyle.None;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

            //Fix for hovers, by default it doesn't redraw
            //TODO: should only redraw when the hovered line changed, this to reduce unnecessary redraws
            HScroll += new EventHandler(OnHScroll);
            VScroll += new EventHandler(OnVScroll);
            MouseLocation = new Point(-1, -1);
            MouseState = MouseStateEnum.OUT;
            MouseEnter += delegate
            {
                MouseState = MouseStateEnum.HOVER;
            };
            MouseLeave += delegate
            {
                MouseState = MouseStateEnum.OUT;
                MouseLocation = new Point(-1, -1);
                Invalidate();
            };
            MouseDown += delegate { MouseState = MouseStateEnum.DOWN; };
            MouseUp += delegate { MouseState = MouseStateEnum.HOVER; };
            MouseMove += delegate (object sender, MouseEventArgs args)
            {
                MouseLocation = args.Location;
                Invalidate();
            };
        }

        #endregion

        #region 绘制

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            if (_BG != null)
                e.Graphics.DrawImage(_BG, e.Bounds, e.Bounds, GraphicsUnit.Pixel);
            e.Graphics.DrawString(e.Header.Text,
                e.Font,
                new SolidBrush(ForeColor),
                e.Bounds
                );
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            //We draw the current line of items (= item with subitems) on a temp bitmap, then draw the bitmap at once. This is to reduce flickering.
            Bitmap b = new Bitmap(e.Item.Bounds.Width, e.Item.Bounds.Height);
            Graphics g = Graphics.FromImage(b);
            //always draw default background
            if (_BG != null)
                g.DrawImage(_BG, 0, 0, e.Bounds, GraphicsUnit.Pixel);

            #region 

            //if (e.State.HasFlag(ListViewItemStates.Selected))
            //{
            //    //selected background
            //    {
            //        Bitmap tb = new Bitmap(e.Item.Bounds.Width, e.Item.Bounds.Height);
            //        Graphics tg = Graphics.FromImage(tb);
            //        tg.Clear(Color.White);
            //        ImageEx.ChangeImageOpacity(tb, 0.2);
            //        var t = ImageEx.JoinImage((Bitmap)b.Clone(), tb, new Rectangle(0, 0, b.Width, b.Height));
            //        tg.Save();
            //        tg.Dispose();
            //        tb.Dispose();
            //        g.DrawImage(t, 0, 0, e.Bounds, GraphicsUnit.Pixel);
            //        t.Dispose();
            //    }
            //}
            //else if (e.Bounds.Contains(MouseLocation) && MouseState == MouseStateEnum.HOVER)
            //{
            //    //hover background
            //    {
            //        Bitmap tb = new Bitmap(e.Item.Bounds.Width, e.Item.Bounds.Height);
            //        Graphics tg = Graphics.FromImage(tb);
            //        tg.Clear(Color.White);
            //        tg.Save();
            //        tb = (Bitmap)ImageEx.ChangeImageOpacity((Bitmap)tb.Clone(), 0.1);
            //        var t = ImageEx.JoinMImage((Bitmap)tb.Clone(), (Bitmap)b.Clone(), new Rectangle(0, 0, b.Width, b.Height));
            //        tb.Save(e.Item.SubItems[1].Text + ".jpg");
            //        g.DrawImage(t, 0, 0, e.Bounds, GraphicsUnit.Pixel);
            //        tg.Dispose();
            //        tb.Dispose();
            //        t.Dispose();
            //    }
            //}

            #endregion

            //Draw separator
            g.DrawLine(new Pen(Color.FromArgb(125, ForeColor)), e.Bounds.Left, 0, e.Bounds.Right, 0);

            foreach (ListViewItem.ListViewSubItem subItem in e.Item.SubItems)
            {
                //Draw text
                g.DrawString(subItem.Text, Font, new SolidBrush(Color.FromArgb(225, ForeColor)),
                 subItem.Bounds.Left , 0);

                //e.Graphics.DrawString(subItem.Text, Font, new SolidBrush(Color.White), subItem.Bounds);
            }
            g.Save();
            //b.Save(e.Item.SubItems[1].Text + ".jpg");
            e.Graphics.DrawImage(b, e.Item.Bounds.Location);
            g.Dispose();
            b.Dispose();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        #endregion

        #region 事件

        protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = Columns[e.ColumnIndex].Width;
        }

        public event EventHandler HScroll;

        public event EventHandler VScroll;

        /// <summary>
        /// 垂直滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void OnHScroll(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// 水平滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void OnVScroll(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void OnMouseWhell(object sender, EventArgs e)
        {
            Invalidate();
        }

        const int WM_HSCROLL = 0x0114;
        const int WM_VSCROLL = 0x0115;
        const int WM_MOUSEWHEEL = 0x020A;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HSCROLL)
            {
                OnHScroll(this, new EventArgs());
            }
            else if (m.Msg == WM_VSCROLL)
            {
                OnVScroll(this, new EventArgs());
            }
            else if (m.Msg == WM_MOUSEWHEEL)
            {
                OnMouseWhell(this, new EventArgs());
            }

            base.WndProc(ref m);
        }

        #endregion

    }
}
