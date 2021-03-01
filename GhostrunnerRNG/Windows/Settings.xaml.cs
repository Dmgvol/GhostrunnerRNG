using GhostrunnerRNG.Game;
using System.Windows;

namespace GhostrunnerRNG.Windows {
    /// <summary>
    /// Super simple settings window, in-memory settings (no local save)
    /// </summary>
    public partial class Settings : Window {
        public Settings() {
            InitializeComponent();
            // load cfg to controls
            checkbox_RngOnRestart.IsChecked = Config.GetInstance().Gen_RngOnRestart;
            checkbox_RngOrbs.IsChecked = Config.GetInstance().Gen_RngOrbs;
            checkbox_RngCybervoid.IsChecked = Config.GetInstance().Gen_RngCV;
        }

        // OK Button
        private void ApplyButton_Click(object sender, RoutedEventArgs e) {
            // save changes
            Config cfg = new Config();
            cfg.Gen_RngOnRestart = checkbox_RngOnRestart.IsChecked == true;
            cfg.Gen_RngOrbs = checkbox_RngOrbs.IsChecked == true;
            cfg.Gen_RngCV = checkbox_RngCybervoid.IsChecked == true;
            Config.GetInstance().SetInstance(cfg);

            // and close
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
