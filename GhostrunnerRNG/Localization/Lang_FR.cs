namespace GhostrunnerRNG.Localization {
    class Lang_FR : LocalizationBase {

        public Lang_FR() : base(LocalizationManager.Language.FR) {
            LangStrings.Add(TextAlias.Mode_Title, "Mode Aléatoire");
            LangStrings.Add(TextAlias.Mode_Description, "Rend les ennemis et les objets aléatoires pour un défi à la fois stimulant et surprenant.");

            LangStrings.Add(TextAlias.HCMode_Title, "Randomisateur Hardcore");
            LangStrings.Add(TextAlias.HCMode_Description, "Des améliorations sont disponibles dès le début, un plus grand nombre d'ennemis sont aléatoires pour poser un défi mortel.");

            LangStrings.Add(TextAlias.Ability_Tempest_RefundDesc_Default, "Éliminer au moins deux ennemis d'un coup avec Tempête restitue instantanément la concentration consommée pour son utilisation.");
            LangStrings.Add(TextAlias.Ability_Tempest_RefundDesc_HC, "Éliminer au moins trois ennemis d'un coup avec Tempête restitue instantanément la concentration consommée pour son utilisation.");
        }
    }
}
