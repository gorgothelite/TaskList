using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Test
{
    /// <summary>
    /// Base form that replaces the Windows chrome with a dark custom title bar.
    /// Subclasses must call RegisterTitleBar() after InitializeComponent().
    /// </summary>
    public class DarkForm : Form
    {
        // ── Win32 ─────────────────────────────────────────────────────────────────
        [DllImport("user32.dll", SetLastError = false)]
        static extern bool ReleaseCapture();

        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        const int WM_NCLBUTTONDOWN = 0xA1;
        const int WM_NCHITTEST     = 0x84;
        const int HTCAPTION        = 2;
        const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12;
        const int HTTOPLEFT = 13, HTTOPRIGHT = 14;
        const int HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;

        // ── Colours ───────────────────────────────────────────────────────────────
        static readonly Color BtnHoverBg    = Color.FromArgb(70, 70, 78);
        static readonly Color BtnCloseHover = Color.FromArgb(196, 43, 28);

        // ── Caption button width ──────────────────────────────────────────────────
        const int BtnW = 40;

        // ── Fields ────────────────────────────────────────────────────────────────
        /// <summary>
        /// Set to true in the derived-class constructor for forms that can be resized.
        /// DarkForm will return resize hit-test values from WndProc when this is true.
        /// </summary>
        protected bool _resizable;

        private Button _btnMax;
        private Label  _lblCaption;   // auto-injected title label (null when panel has its own)

        // ── Constructor ───────────────────────────────────────────────────────────
        protected DarkForm()
        {
            FormBorderStyle = FormBorderStyle.None;
        }

        // ── Drop shadow ───────────────────────────────────────────────────────────
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle |= 0x20000; // CS_DROPSHADOW
                return cp;
            }
        }

        // ── Public API ────────────────────────────────────────────────────────────

        /// <summary>
        /// Wire <paramref name="panel"/> as the custom title bar.
        /// If the panel has no existing visible labels a title label is injected automatically.
        /// Call this after InitializeComponent().
        /// </summary>
        /// <param name="showMin">Show a minimise button.</param>
        /// <param name="showMax">Show a maximise / restore button.</param>
        protected void RegisterTitleBar(Panel panel, bool showMin, bool showMax)
        {
            int h = panel.Height;

            // ── Inject title label when the panel carries no existing label text ──
            bool hasLabel = false;
            foreach (Control c in panel.Controls)
                if (c is Label lbl && !string.IsNullOrWhiteSpace(lbl.Text)) { hasLabel = true; break; }

            if (!hasLabel)
            {
                _lblCaption = new Label
                {
                    AutoSize  = false,
                    Text      = Text,
                    Font      = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location  = new Point(12, 0),
                    Size      = new Size(0, h),          // width set after buttons are placed
                };
                panel.Controls.Add(_lblCaption);
            }

            // ── Wire drag on non-button children already in the panel ─────────────
            foreach (Control c in panel.Controls)
                if (!(c is Button)) WireDrag(c);

            // ── Build caption buttons right-to-left ───────────────────────────────
            int right = panel.Width;

            var btnClose = MakeCaptionBtn("✕", right - BtnW, 0, BtnW, h, BtnCloseHover);
            btnClose.Click += (s, e) => Close();
            panel.Controls.Add(btnClose);
            right -= BtnW;

            if (showMax)
            {
                _btnMax = MakeCaptionBtn("□", right - BtnW, 0, BtnW, h, BtnHoverBg);
                _btnMax.Click += (s, e) => ToggleMaximize();
                panel.Controls.Add(_btnMax);
                right -= BtnW;
            }

            if (showMin)
            {
                var btnMin = MakeCaptionBtn("—", right - BtnW, 0, BtnW, h, BtnHoverBg);
                btnMin.Click += (s, e) => WindowState = FormWindowState.Minimized;
                panel.Controls.Add(btnMin);
                right -= BtnW;
            }

            // ── Finalise injected label width ─────────────────────────────────────
            if (_lblCaption != null)
                _lblCaption.Size = new Size(right - 12, h);

            // ── Wire drag on the panel background itself ──────────────────────────
            WireDrag(panel);

            // ── Sync max-button glyph on window-state changes ─────────────────────
            if (showMax)
                Resize += (s, e) => UpdateMaxBtn();
        }

        // ── Text – keep injected label in sync ────────────────────────────────────
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                if (_lblCaption != null)
                    _lblCaption.Text = value;
            }
        }

        // ── Resize hit-testing ────────────────────────────────────────────────────
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST && _resizable && WindowState == FormWindowState.Normal)
            {
                int lp      = m.LParam.ToInt32();
                int screenX = (short)(lp & 0xFFFF);
                int screenY = (short)((lp >> 16) & 0xFFFF);
                var p       = PointToClient(new Point(screenX, screenY));
                const int G = 6;

                bool l = p.X < G, r = p.X >= ClientSize.Width  - G;
                bool t = p.Y < G, b = p.Y >= ClientSize.Height - G;

                if      (l && t) { m.Result = (IntPtr)HTTOPLEFT;     return; }
                else if (r && t) { m.Result = (IntPtr)HTTOPRIGHT;    return; }
                else if (l && b) { m.Result = (IntPtr)HTBOTTOMLEFT;  return; }
                else if (r && b) { m.Result = (IntPtr)HTBOTTOMRIGHT; return; }
                else if (l)      { m.Result = (IntPtr)HTLEFT;        return; }
                else if (r)      { m.Result = (IntPtr)HTRIGHT;       return; }
                else if (t)      { m.Result = (IntPtr)HTTOP;         return; }
                else if (b)      { m.Result = (IntPtr)HTBOTTOM;      return; }
            }
            base.WndProc(ref m);
        }

        // ── Private helpers ───────────────────────────────────────────────────────
        private Button MakeCaptionBtn(string symbol, int x, int y, int w, int h, Color hoverBg)
        {
            var btn = new Button
            {
                Text      = symbol,
                Font      = new Font("Segoe UI", 9f),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location  = new Point(x, y),
                Size      = new Size(w, h),
                Anchor    = AnchorStyles.Top | AnchorStyles.Right,
                TabStop   = false,
                Cursor    = Cursors.Default,
            };
            btn.FlatAppearance.BorderSize         = 0;
            btn.FlatAppearance.MouseOverBackColor = hoverBg;
            btn.FlatAppearance.MouseDownBackColor = hoverBg;
            return btn;
        }

        private void WireDrag(Control c)
        {
            c.MouseDown += (s, e) =>
            {
                if (e.Button != MouseButtons.Left) return;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
            };
            c.MouseDoubleClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left && _resizable)
                    ToggleMaximize();
            };
        }

        private void ToggleMaximize()
        {
            WindowState = WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        private void UpdateMaxBtn()
        {
            if (_btnMax == null) return;
            _btnMax.Text = WindowState == FormWindowState.Maximized ? "❐" : "□";
        }
    }
}
