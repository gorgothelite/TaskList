using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    /// <summary>
    /// Owner-drawn ComboBox styled to match the application's dark theme.
    /// Paints a dark border and custom dropdown arrow over the default Windows rendering.
    /// </summary>
    class DarkComboBox : ComboBox
    {
        static readonly Color BgColor     = Color.FromArgb(55, 55, 60);
        static readonly Color SelColor    = Color.FromArgb(0, 84, 158);
        static readonly Color BorderColor = Color.FromArgb(80, 80, 88);
        static readonly Color ArrowColor  = Color.FromArgb(170, 170, 180);

        public DarkComboBox()
        {
            DrawMode  = DrawMode.OwnerDrawFixed;
            FlatStyle = FlatStyle.Flat;
            BackColor = BgColor;
            ForeColor = Color.White;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                using (var br = new SolidBrush(BgColor))
                    e.Graphics.FillRectangle(br, e.Bounds);
                return;
            }

            // DrawItemState.ComboBoxEdit means we're painting the face (not the dropdown list)
            bool isFace    = (e.State & DrawItemState.ComboBoxEdit) != 0;
            bool isSelected = (e.State & DrawItemState.Selected) != 0;

            Color bg = (isSelected && !isFace) ? SelColor : BgColor;

            using (var br = new SolidBrush(bg))
                e.Graphics.FillRectangle(br, e.Bounds);

            string text = Items[e.Index].ToString();
            int ty = e.Bounds.Y + (e.Bounds.Height - Font.Height) / 2;
            using (var br = new SolidBrush(Color.White))
                e.Graphics.DrawString(text, Font, br, e.Bounds.X + 3, ty);
        }

        // Repaint border and arrow after the default Windows rendering
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != 0x000F) return; // WM_PAINT

            using (var g = Graphics.FromHwnd(Handle))
            {
                // Dark border
                using (var pen = new Pen(BorderColor))
                    g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

                // Arrow button background
                const int BtnW = 17;
                var btnR = new Rectangle(Width - BtnW - 1, 1, BtnW, Height - 2);
                using (var br = new SolidBrush(BgColor))
                    g.FillRectangle(br, btnR);

                // Downward triangle arrow
                int cx = btnR.Left + btnR.Width  / 2;
                int cy = btnR.Top  + btnR.Height / 2 - 1;
                var pts = new[]
                {
                    new System.Drawing.Point(cx - 4, cy),
                    new System.Drawing.Point(cx + 4, cy),
                    new System.Drawing.Point(cx,     cy + 4),
                };
                using (var br = new SolidBrush(ArrowColor))
                    g.FillPolygon(br, pts);
            }
        }
    }
}
