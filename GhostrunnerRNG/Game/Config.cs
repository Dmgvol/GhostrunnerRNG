namespace GhostrunnerRNG.Game {
    class Config {
        private static Config instance;

        // Settings
        public bool Gen_RngOnRestart = true;
        public bool Gen_RngOrbs = true;
        public bool Gen_RngCV = true;

        // Singelton Config
        public static Config GetInstance() {
            if(instance == null)
                instance = new Config();
            return instance;
        }

        public void SetInstance(Config cfg) {
            instance = cfg;
        }

        public Config() {

        }
    }
}
