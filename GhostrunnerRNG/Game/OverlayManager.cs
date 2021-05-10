using GhostrunnerRNG.Windows;

namespace GhostrunnerRNG.Game {
    class OverlayManager {

        GameOverlay overlayWindow = null;

        public OverlayManager() {}

        public void CloseOverlay() {
            overlayWindow?.Close();
        }

        public void CheckOverlay() {
            if(overlayWindow == null && Config.GetInstance().Settings_EnableOverlay) {
                // turn on
                overlayWindow = new GameOverlay();
                overlayWindow.Show();
            }else if(overlayWindow != null && !Config.GetInstance().Settings_EnableOverlay) {
                // turn off
                overlayWindow.Close();
                overlayWindow = null;
            }
        }

        public void UpdateOverlay_SimpleText(string text) {
            overlayWindow?.UpdateUI(text);
        }
    }
}
