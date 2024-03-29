﻿using System.Windows;
using System.Diagnostics;
using System.Windows.Forms;
using GhostrunnerRNG.Game;
using GhostrunnerRNG.Windows;
using System.ComponentModel;

namespace GhostrunnerRNG {
    public partial class MainWindow : Window {

        public readonly bool DEBUG_MODE = Debugger.IsAttached;

        //// Hooks ////
        globalKeyboardHook kbHook = new globalKeyboardHook();
        private GameHook game; // Main game hook and logic

        /// <summary>Log outside MainWindow/GameHook</summary>
        public static string GlobalLog = "";

        private void SeedChanged(int seed) {
            LabelSeed.Content = $"RNG Seed: {seed}";
        }

        public MainWindow() {
            InitializeComponent();
            label_Version.Content = $" v{Config.VERSION}";

            // Config - singleton
            Config.GetInstance().SeedChanged += SeedChanged;

            game = new GameHook(this);

            // UI
            ToggleButton(ButtonNewRng, false);
            ButtonDev.Visibility = DEBUG_MODE ? Visibility.Visible : Visibility.Collapsed;
            ButtonDev.IsEnabled = DEBUG_MODE;

            //// HotKeys
            kbHook.KeyDown += InputKeyDown;
            kbHook.HookedKeys.Add(Keys.F7);

            LogStatus("Idle");
        }

        protected override void OnClosing(CancelEventArgs e) {
            GameHook.overlayM?.CloseOverlay();
            base.OnClosing(e);
        }

        //// HotKeys ////
        private void InputKeyDown(object sender, KeyEventArgs e) {
            switch(e.KeyCode) {
                case Keys.F7:
                    game.NewRNG(true);
                    break;
                default:
                    break;
            }
            e.Handled = false;
        }

        //// Log & GlobalLog ////
        public void LogStatus(string str) {
            str = str.Replace("\n", "\n ");
            label_RNGStatus.Text = $"RNG Status:\n {str}";
        }

        private string globalLogTemp = GlobalLog;
        public void CheckOutsideLog() {
            if(!globalLogTemp.Equals(GlobalLog)) {
                globalLogTemp = GlobalLog;
                label_GlobalLog.Text = GlobalLog;
            }
        }

        ////// UI //////
        public void ToggleRngControls(bool flag) {
            ToggleButton(ButtonNewRng, flag);
        }

        public void ClearTexts() {
            LabelSeed.Content = "";
            label_GlobalLog.Text = "";
            label_levelName.Text = "Current Level: ";
            label_RNGStatus.Text = "RNG Status: waiting...";
        }

        private void ButtonNewRng_Click(object sender, RoutedEventArgs e) {
            if(ButtonNewRng.Visibility == Visibility.Visible)
                game.NewRNG(true);
        }

        public void ToggleButton(System.Windows.Controls.Control control, bool flag) {
            control.Visibility = flag ? Visibility.Visible : Visibility.Hidden;
            control.IsEnabled = flag;
        }

        private void ButtonDev_Click(object sender, RoutedEventArgs e) {
            Window devWindow = new DevWindow();
            devWindow.ShowDialog();
        }

        private void ButtonAbout_Click(object sender, RoutedEventArgs e) {
            Window aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e) {
            Window settingsWindow = new Settings();
            settingsWindow.ShowDialog();
        }

        private void LabelSeed_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            System.Windows.Clipboard.SetText($"RNG Seed: {Config.GetInstance().Seed}");
        }
    }
}
