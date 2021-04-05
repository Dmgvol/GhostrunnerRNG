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
            checkbox_RngCybervoid.IsChecked = Config.GetInstance().Gen_RngCV;
            checkbox_SlideForceTrigger.IsChecked = Config.GetInstance().Setting_RemoveForceSlideTrigger;
        }

        // OK Button
        private void ApplyButton_Click(object sender, RoutedEventArgs e) {
            // save changes
            Config.GetInstance().Gen_RngCV = checkbox_RngCybervoid.IsChecked == true;
            Config.GetInstance().Gen_RngOnRestart = checkbox_RngOnRestart.IsChecked == true;
            Config.GetInstance().Setting_RemoveForceSlideTrigger = checkbox_SlideForceTrigger.IsChecked == true;
            // and close
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
