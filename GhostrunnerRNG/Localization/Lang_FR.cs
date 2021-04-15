namespace GhostrunnerRNG.Localization {
    class Lang_FR : LocalizationBase {

        public Lang_FR() : base(LocalizationManager.Language.FR) {
            LangStrings.Add(TextAlias.Mode_Title, "Mode Aléatoire");
            LangStrings.Add(TextAlias.Mode_Description, "Rend les ennemis et les objets aléatoires pour un défi à la fois stimulant et surprenant.");
        }
    }
}
