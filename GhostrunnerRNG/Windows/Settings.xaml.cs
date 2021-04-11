using GhostrunnerRNG.Game;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GhostrunnerRNG.Windows {
    /// <summary>
    /// Super simple settings window, in-memory settings (no local save)
    /// </summary>
    public partial class Settings : Window {

        private List<CheckBox> difBoxes;
        private Config.Difficulty selectedDiff;

        public Settings() {
            InitializeComponent();
            // load cfg to controls
            checkbox_RngOnRestart.IsChecked = Config.GetInstance().Gen_RngOnRestart;
            checkbox_RngCybervoid.IsChecked = Config.GetInstance().Gen_RngCV;
            checkbox_RngTargets.IsChecked = Config.GetInstance().Gen_RngTargets;
            checkbox_SlideForceTrigger.IsChecked = Config.GetInstance().Setting_RemoveForceSlideTrigger;

            // diff
            difBoxes = new List<CheckBox>() {Dif_easy, Dif_Normal, Dif_SR, Dif_NM };
            selectedDiff = Config.GetInstance().Setting_Difficulty;

            switch(selectedDiff) {
                case Config.Difficulty.Easy:
                    Dif_easy.IsChecked = true;
                    break;
                case Config.Difficulty.Normal:
                    Dif_Normal.IsChecked = true;
                    break;
                case Config.Difficulty.SR:
                    Dif_SR.IsChecked = true;
                    break;
                case Config.Difficulty.Nightmare:
                    Dif_NM.IsChecked = true;
                    break;
            }

            // debug? enable all difficulties for testing/debugging only
            if(Debugger.IsAttached) {
                difBoxes.ForEach(x => x.IsHitTestVisible = true);
            }
        }

        // OK Button
        private void ApplyButton_Click(object sender, RoutedEventArgs e) {
            // save changes
            Config.GetInstance().Gen_RngCV = checkbox_RngCybervoid.IsChecked == true;
            Config.GetInstance().Gen_RngOnRestart = checkbox_RngOnRestart.IsChecked == true;
            Config.GetInstance().Gen_RngTargets = checkbox_RngTargets.IsChecked == true;
            Config.GetInstance().Setting_RemoveForceSlideTrigger = checkbox_SlideForceTrigger.IsChecked == true;
            Config.GetInstance().Setting_Difficulty = selectedDiff;
            // and close
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Dif_Checked(object sender, RoutedEventArgs e) {
            if(DifDesc is null) return;
            if(sender is CheckBox box) {
                // dif select
                if(box.Name.Equals(nameof(Dif_easy))) {
                    selectedDiff = Config.Difficulty.Easy;
                    DifDesc.Text =
                        " \u2022 Reduced hard to reach spots.\n" +
                        " \u2022 No RNG for: Uplinks, Amida fans, billboards.\n" +
                        " \u2022 No rare spawns (unique hard-to-reach).";
                } else if(box.Name.Equals(nameof(Dif_Normal))) {
                    selectedDiff = Config.Difficulty.Normal;
                    DifDesc.Text =
                       " \u2022 Balanced as originally intended,\n    be ready for quite a challenge.";
                } else if(box.Name.Equals(nameof(Dif_SR))) {
                    selectedDiff = Config.Difficulty.SR;
                    DifDesc.Text =
                        " \u2022 Enemies placed in speedrunning routes.\n" +
                        " \u2022 Heavily rely on DSJ and SDSJ.\n" +
                        " \u2022 Hard-to-reach spots rarity boost.\n" +
                        " \u2022 You agree to suffer";
                } else if(box.Name.Equals(nameof(Dif_NM))) {
                    selectedDiff = Config.Difficulty.Nightmare;
                    DifDesc.Text =
                        " \u2022 Super hard enemy placements.\n" +
                        " \u2022 You agree to suffer.\n" +
                        " \u2022 You agree to become soil.";
                }
                DisableDifBoxes(box);
            }
        }

        private void DisableDifBoxes(CheckBox besidesThis) {
            for(int i = 0; i < difBoxes.Count; i++) {
                if(!difBoxes[i].Equals(besidesThis))
                    difBoxes[i].IsChecked = false;
            }


            if(difBoxes.Where(x => x.IsChecked == true).Count() == 0)
                besidesThis.IsChecked = true;
        }
    }
}
