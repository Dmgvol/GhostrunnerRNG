using GhostrunnerRNG.Game;
using System.Windows;

namespace GhostrunnerRNG.Windows {
    
    public partial class About : Window {
        public About() {
            InitializeComponent();
            versionTitle.Content = "v" + Config.VERSION;
        }
        public void Hyperlink_RequestNavigate(object sender,  System.Windows.Navigation.RequestNavigateEventArgs e) {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
    }
}
