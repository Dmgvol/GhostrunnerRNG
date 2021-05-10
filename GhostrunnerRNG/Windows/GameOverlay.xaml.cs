using GhostrunnerRNG.Game;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

namespace GhostrunnerRNG.Windows {
    /// <summary>
    /// Interaction logic for GameOverlay.xaml
    /// </summary>
    public partial class GameOverlay : Window {
       

        Timer updateTimer;
        public GameOverlay() {
            InitializeComponent();

            updateTimer = new Timer {
                Interval = (200) // 0.2sec
            };
            updateTimer.Tick += new EventHandler(Update);
            updateTimer.Start();
        }

        public bool AlignWindow() {
            // Get GR size
            Rect rect;
            GetWindowRect(GameHook.game.MainWindowHandle, out rect);
            if(rect.IsEmpty()) return false;
            // set size & pos
            Width = Math.Abs(rect.Right - rect.Left);
            Height = Math.Abs(rect.Bottom - rect.Top);
            Top = rect.Top;
            Left = rect.Left;
            return true;
        }

        private void Update(object sender, EventArgs e) {
            // no game found, hide
            if(GameHook.game == null) {
                Hide();
                return;
            }

            // Game focused?
            if(GetForegroundWindow() == GameHook.game.MainWindowHandle) {
                Show();
                AlignWindow();
            } else {
                Hide();
            }
        }

        public void UpdateUI(string text) {
            Label1.Content = text;
        }

        #region Native
        public struct Rect {
            public bool IsEmpty() => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;
            public int Left, Top, Right, Bottom;
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rect rect);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        #endregion
    }
}
