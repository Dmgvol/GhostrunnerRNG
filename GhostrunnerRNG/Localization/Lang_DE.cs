namespace GhostrunnerRNG.Localization {
    class Lang_DE : LocalizationBase {

        public Lang_DE() : base(LocalizationManager.Language.DE){
            LangStrings.Add(TextAlias.Mode_Title, "Zufalls Modus");
            LangStrings.Add(TextAlias.Mode_Description, "Platziert Gegenstände und Gegner in unerwarteten und herausfordernden orten.");

            LangStrings.Add(TextAlias.HCMode_Title, "Hardcore-Zufallsgenerator");
            LangStrings.Add(TextAlias.HCMode_Description, "Upgrades sind von Anfang an verfügbar, eine größere Anzahl von Gegnern wird zufällig ausgewählt und stellt eine tödliche Herausforderung dar.");
        }
    }
}
