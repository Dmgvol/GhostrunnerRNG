﻿using System;

namespace GhostrunnerRNG.Game {
    class Config {
        public const string VERSION = "0.2";

        private static Config instance;

        // Settings
        public bool Gen_RngOnRestart = true;
        public bool Gen_RngOrbs = true;
        public bool Gen_RngCV = true;

        // RNG - singleton
        public Random r;
        public int Seed { get; private set; }

        // Subscriber/publisher pattern
        public delegate void NotifySeedChanged(int newSeed);
        public event NotifySeedChanged SeedChanged;

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
            NewSeed();
        }

        public void NewSeed(int? seed = null) {
            if(seed is int seedNum) {
                Seed = seedNum;
            } else {
                Seed = Guid.NewGuid().GetHashCode();
            }
            r = new Random(Seed);

            // notify if any subscribers
            if(SeedChanged != null) SeedChanged(Seed);
        }
    }
}
