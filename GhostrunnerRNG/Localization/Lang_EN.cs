namespace GhostrunnerRNG.Localization {
    class Lang_EN : LocalizationBase {

        public Lang_EN() : base(LocalizationManager.Language.EN) {
            LangStrings.Add(TextAlias.Mode_Title, "Randomizer Mode");
            LangStrings.Add(TextAlias.Mode_Description, "Randomizes enemies and objects in a challenging and unexpected way.");
        }
    }
}
