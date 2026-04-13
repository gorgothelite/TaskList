using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    /// <summary>
    /// Manages dark / light theme switching.
    /// Call <see cref="Apply"/> on a form after InitializeComponent to remap all
    /// hardcoded dark-palette colors to the appropriate light equivalents (or back).
    /// Owner-drawn controls (ListView, DarkComboBox) should read the color
    /// properties directly instead of relying on Apply.
    /// </summary>
    public static class ThemeManager
    {
        // ── Theme state ───────────────────────────────────────────────────────
        public static bool IsDark { get; private set; } = true;

        public static event EventHandler ThemeChanged;

        /// <summary>Sets the theme flag without firing ThemeChanged (use on startup).</summary>
        public static void LoadTheme(bool isDark) => IsDark = isDark;

        /// <summary>Toggles the theme and notifies all subscribers.</summary>
        public static void Toggle()
        {
            IsDark = !IsDark;
            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        // ── Palette maps ──────────────────────────────────────────────────────
        // Keys = dark ARGB  /  Values = light ARGB

        // Background colors
        static readonly Dictionary<int, int> BackMap = new Dictionary<int, int>
        {
            [Argb(28,  28,  30)]  = Argb(248, 248, 250),  // main bg / list bg
            [Argb(33,  33,  37)]  = Argb(242, 242, 246),  // alternate row (owner-draw ref)
            [Argb(36,  36,  40)]  = Argb(238, 238, 244),  // detail panel
            [Argb(37,  37,  38)]  = Argb(245, 245, 248),  // dialog form bg
            [Argb(44,  44,  46)]  = Argb(224, 224, 229),  // title bar / toolbar
            [Argb(45,  45,  48)]  = Argb(250, 250, 252),  // tray popup
            [Argb(50,  50,  56)]  = Argb(255, 255, 255),  // notes text area
            [Argb(55,  55,  60)]  = Argb(255, 255, 255),  // input fields / combo boxes
            [Argb(60,  60,  68)]  = Argb(208, 208, 218),  // divider panels
            [Argb(60,  60,  70)]  = Argb(202, 202, 214),  // neutral buttons (variant)
            [Argb(65,  65,  78)]  = Argb(210, 210, 222),  // neutral buttons (AlertDialog)
            [Argb(70,  70,  78)]  = Argb(215, 215, 226),  // neutral buttons / caption hover
        };

        // Foreground / text colors
        static readonly Dictionary<int, int> ForeMap = new Dictionary<int, int>
        {
            [Color.White.ToArgb()]       = Argb(20,  20,  25),   // primary text
            [Color.WhiteSmoke.ToArgb()]  = Argb(30,  30,  35),   // near-white text
            [Color.Silver.ToArgb()]      = Argb(75,  75,  85),   // toolbar label Silver
            [Color.LightGray.ToArgb()]   = Argb(80,  80,  90),   // light-gray text
            [Argb(130, 130, 140)]        = Argb(100, 100, 110),  // secondary text
            [Argb(175, 175, 185)]        = Argb(90,  90,  100),  // caption / tertiary text
            [Argb(180, 180, 190)]        = Argb(100, 100, 110),  // muted text
            [Argb(210, 210, 220)]        = Argb(80,  80,  90),   // very muted text
        };

        // Reverse maps (light → dark) built automatically
        static readonly Dictionary<int, int> BackMapReverse = new Dictionary<int, int>();
        static readonly Dictionary<int, int> ForeMapReverse = new Dictionary<int, int>();

        static ThemeManager()
        {
            foreach (var kv in BackMap) BackMapReverse[kv.Value] = kv.Key;
            foreach (var kv in ForeMap) ForeMapReverse[kv.Value] = kv.Key;
        }

        // ── Apply ─────────────────────────────────────────────────────────────
        public static void Apply(Control root) => ApplyRecursive(root, IsDark);

        static void ApplyRecursive(Control c, bool toDark)
        {
            // BackColor — skip Transparent
            if (c.BackColor != Color.Transparent)
            {
                int mapped = RemapArgb(c.BackColor.ToArgb(), BackMap, BackMapReverse, toDark);
                if (mapped != c.BackColor.ToArgb())
                    c.BackColor = Color.FromArgb(mapped);
            }

            // ForeColor — for Buttons only remap when background is a neutral palette color
            bool remapFore = !(c is Button) || IsNeutralBack(c.BackColor.ToArgb(), toDark);
            if (remapFore)
            {
                int mapped = RemapArgb(c.ForeColor.ToArgb(), ForeMap, ForeMapReverse, toDark);
                if (mapped != c.ForeColor.ToArgb())
                    c.ForeColor = Color.FromArgb(mapped);
            }

            foreach (Control child in c.Controls)
                ApplyRecursive(child, toDark);
        }

        static bool IsNeutralBack(int argb, bool toDark)
            => toDark ? BackMapReverse.ContainsKey(argb) : BackMap.ContainsKey(argb);

        static int RemapArgb(int argb,
            Dictionary<int, int> darkToLight,
            Dictionary<int, int> lightToDark,
            bool toDark)
        {
            if (toDark)  { if (lightToDark.TryGetValue(argb, out int d)) return d; }
            else         { if (darkToLight.TryGetValue(argb, out int l)) return l; }
            return argb;
        }

        static int Argb(int r, int g, int b) => Color.FromArgb(r, g, b).ToArgb();

        // ── Owner-draw color properties ───────────────────────────────────────

        // ListView
        public static Color ListBg          => IsDark ? Color.FromArgb(28,  28,  30)  : Color.FromArgb(248, 248, 250);
        public static Color ListBgAlt       => IsDark ? Color.FromArgb(33,  33,  37)  : Color.FromArgb(242, 242, 246);
        public static Color ListSelected    => Color.FromArgb(0, 84, 158);
        public static Color ListHeaderBg    => IsDark ? Color.FromArgb(44,  44,  46)  : Color.FromArgb(224, 224, 229);
        public static Color ListHeaderLine  => IsDark ? Color.FromArgb(60,  60,  65)  : Color.FromArgb(195, 195, 208);
        public static Color ListHeaderFg    => IsDark ? Color.FromArgb(160, 160, 170) : Color.FromArgb(105, 105, 118);
        public static Color ListHeaderFgSorted => IsDark ? Color.FromArgb(220, 220, 230) : Color.FromArgb(25,  25,  38);
        public static Color ListTextNormal  => IsDark ? Color.FromArgb(218, 218, 225) : Color.FromArgb(35,  35,  48);
        public static Color ListTextDone    => IsDark ? Color.FromArgb(100, 100, 108) : Color.FromArgb(155, 155, 165);
        public static Color ListTextOnHold  => IsDark ? Color.FromArgb(220, 160,   0) : Color.FromArgb(175, 118,   0);
        public static Color ListTextOverdue => IsDark ? Color.FromArgb(255, 108, 108) : Color.FromArgb(195,  50,  50);
        public static Color ListArrowColor  => IsDark ? Color.FromArgb(150, 150, 165) : Color.FromArgb(118, 118, 135);
        public static Color ListTextSelected => Color.White;

        // DarkComboBox
        public static Color ComboBoxBg     => IsDark ? Color.FromArgb(55, 55, 60)  : Color.White;
        public static Color ComboBoxSel    => Color.FromArgb(0, 84, 158);
        public static Color ComboBoxBorder => IsDark ? Color.FromArgb(80, 80, 88)  : Color.FromArgb(178, 178, 188);
        public static Color ComboBoxArrow  => IsDark ? Color.FromArgb(170, 170, 180): Color.FromArgb(80, 80, 92);
        public static Color ComboBoxText   => IsDark ? Color.White                 : Color.FromArgb(20, 20, 25);

        // TrayPopup
        public static Color TrayPopupBg     => IsDark ? Color.FromArgb(45, 45, 48)  : Color.FromArgb(250, 250, 252);
        public static Color TrayPopupBorder => IsDark ? Color.FromArgb(80, 80, 85)  : Color.FromArgb(195, 195, 208);
        public static Color TrayPopupText   => IsDark ? Color.White                 : Color.FromArgb(20, 20, 25);

        // Caption button hover (neutral — not close)
        public static Color CaptionBtnHover => IsDark ? Color.FromArgb(70, 70, 78)  : Color.FromArgb(215, 215, 226);
    }
}
