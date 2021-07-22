namespace GhostrunnerRNG.Localization {
    class Lang_EN : LocalizationBase {

        public Lang_EN() : base(LocalizationManager.Language.EN) {
            LangStrings.Add(TextAlias.Mode_Title, "Randomizer Mode");
            LangStrings.Add(TextAlias.Mode_Description, "Randomizes enemies and objects in a challenging and unexpected way.");

            LangStrings.Add(TextAlias.HCMode_Title, "Hardcore Randomizer");
            LangStrings.Add(TextAlias.HCMode_Description, "Upgrades are available from the start, greater number of enemies are randomized to pose a deathly challenge.");

        }
    }
}
