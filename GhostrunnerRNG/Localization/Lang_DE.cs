namespace GhostrunnerRNG.Localization {
    class Lang_DE : LocalizationBase {

        public Lang_DE() : base(LocalizationManager.Language.DE){
            LangStrings.Add(TextAlias.Mode_Title, "Zufalls Modus");
            LangStrings.Add(TextAlias.Mode_Description, "Platziert Gegenstände und Gegner in unerwarteten und herausfordernden orten.");
        }
    }
}
