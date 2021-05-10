using GhostrunnerRNG.Localization;
using System;
using static GhostrunnerRNG.Localization.LocalizationBase;
using static GhostrunnerRNG.Localization.LocalizationManager;

namespace GhostrunnerRNG.Game {
    class Config {
        public const string VERSION = "0.3.04";

        private static Config instance;

        // Settings
        public bool Gen_RngOnRestart = true;
        public bool Gen_RngOrbs = true;
        public bool Gen_RngCV = true;
        public bool Gen_RngTargets = true;

        public bool Settings_RemoveForceSlideTrigger = true;
        public bool Settings_ForcedRestart = true;
        public bool Settings_EnableOverlay = false;

        public readonly bool Settings_DisableUplinksOnEasy = true;

        public enum Difficulty { Easy, Normal, Nightmare}
        public Difficulty Setting_Difficulty = Difficulty.Normal;

        // RNG - singleton
        public Random r;
        public int Seed { get; private set; }
        public int? ForceSeed;

        // Subscriber/publisher pattern
        public delegate void NotifySeedChanged(int newSeed);
        public event NotifySeedChanged SeedChanged;

        // localization
        public LocalizationManager localizationManager;

        // Singelton Config
        public static Config GetInstance() {
            if(instance == null)
                instance = new Config();
            return instance;
        }

        public Config() {
            NewSeed();
            localizationManager = new LocalizationManager();
        }

        public void NewSeed(int? seed = null) {
            if(seed is int seedNum) {
                Seed = seedNum;
            } else {
                Seed = Guid.NewGuid().GetHashCode();
            }

            // has forced seed number?
            if(ForceSeed != null && ForceSeed is int newSeed)
                Seed = newSeed;

            // generate new rng with seed
            r = new Random(Seed);

            // notify if any subscribers
            if(SeedChanged != null) SeedChanged(Seed);
        }


        // Localization
        public void SetLanguage(Language lang) {
            localizationManager.SetLanguage(lang);
        }

        public string GetString(TextAlias alias) =>  localizationManager.GetLocalization(alias);
    }
}
