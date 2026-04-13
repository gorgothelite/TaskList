using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    /// <summary>
    /// Owner-drawn ComboBox that follows the application's active theme.
    /// </summary>
    class DarkComboBox : ComboBox
    {
        public DarkComboBox()
        {
            DrawMode  = DrawMode.OwnerDrawFixed;
            FlatStyle = FlatStyle.Flat;
            SyncColors();
            ThemeManager.ThemeChanged += (s, e) => { SyncColors(); Invalidate(); };
        }

        void SyncColors()
        {
            BackColor = ThemeManager.ComboBoxBg;
            ForeColor = ThemeManager.ComboBoxText;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                using (var br = new SolidBrush(ThemeManager.ComboBoxBg))
                    e.Graphics.FillRectangle(br, e.Bounds);
                return;
            }

            bool isFace     = (e.State & DrawItemState.ComboBoxEdit) != 0;
            bool isSelected = (e.State & DrawItemState.Selected) != 0;

            Color bg = (isSelected && !isFace) ? ThemeManager.ComboBoxSel : ThemeManager.ComboBoxBg;

            using (var br = new SolidBrush(bg))
                e.Graphics.FillRectangle(br, e.Bounds);

            string text = Items[e.Index].ToString();
            int ty = e.Bounds.Y + (e.Bounds.Height - Font.Height) / 2;
            using (var br = new SolidBrush(ThemeManager.ComboBoxText))
                e.Graphics.DrawString(text, Font, br, e.Bounds.X + 3, ty);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != 0x000F) return; // WM_PAINT

            using (var g = Graphics.FromHwnd(Handle))
            {
                using (var pen = new Pen(ThemeManager.ComboBoxBorder))
                    g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

                const int BtnW = 17;
                var btnR = new System.Drawing.Rectangle(Width - BtnW - 1, 1, BtnW, Height - 2);
                using (var br = new SolidBrush(ThemeManager.ComboBoxBg))
                    g.FillRectangle(br, btnR);

                int cx = btnR.Left + btnR.Width  / 2;
                int cy = btnR.Top  + btnR.Height / 2 - 1;
                var pts = new[]
                {
                    new System.Drawing.Point(cx - 4, cy),
                    new System.Drawing.Point(cx + 4, cy),
                    new System.Drawing.Point(cx,     cy + 4),
                };
                using (var br = new SolidBrush(ThemeManager.ComboBoxArrow))
                    g.FillPolygon(br, pts);
            }
        }
    }
}
