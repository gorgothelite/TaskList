using System;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    /// <summary>
    /// A small dark toast that appears near the system tray and auto-dismisses.
    /// </summary>
    internal sealed class TrayPopup : Form
    {
        private static TrayPopup _current;

        public static void Show(string message, int durationMs = 3000)
        {
            // Dismiss any existing popup first
            _current?.Close();

            var popup = new TrayPopup(message, durationMs);
            _current = popup;
            popup.Show();
        }

        private TrayPopup(string message, int durationMs)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition   = FormStartPosition.Manual;
            ShowInTaskbar   = false;
            TopMost         = true;
            BackColor       = ThemeManager.TrayPopupBg;
            Padding         = new Padding(12, 10, 12, 10);
            AutoSize        = false;

            // Drop shadow
            // (CS_DROPSHADOW via CreateParams not needed — kept simple)

            var icon = new Label
            {
                Text      = "ℹ",
                Font      = new Font("Segoe UI", 13f),
                ForeColor = Color.FromArgb(0, 122, 255),
                AutoSize  = false,
                Size      = new Size(24, 40),
                Location  = new Point(12, 10),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            var lbl = new Label
            {
                Text      = message,
                Font      = new Font("Segoe UI", 9f),
                ForeColor = ThemeManager.TrayPopupText,
                AutoSize  = false,
                Size      = new Size(220, 40),
                Location  = new Point(42, 10),
                TextAlign = ContentAlignment.MiddleLeft,
            };

            Size = new Size(278, 60);
            Controls.Add(icon);
            Controls.Add(lbl);

            // Border
            Paint += (s, e) =>
            {
                using (var pen = new Pen(ThemeManager.TrayPopupBorder, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            };

            // Position: bottom-right, just above the taskbar
            var screen = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(screen.Right - Width - 12, screen.Bottom - Height - 8);

            // Click to dismiss
            foreach (Control c in Controls)
                c.Click += (s, e) => Close();
            Click += (s, e) => Close();

            // Auto-dismiss timer
            var timer = new Timer { Interval = durationMs };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();
                if (!IsDisposed) Close();
            };
            timer.Start();

            FormClosed += (s, e) =>
            {
                if (_current == this) _current = null;
            };
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle |= 0x20000; // CS_DROPSHADOW
                return cp;
            }
        }
    }
}
