namespace GhostrunnerRNG.Localization {
    class Lang_RU: LocalizationBase {

        public Lang_RU() : base(LocalizationManager.Language.RU) {
            LangStrings.Add(TextAlias.Mode_Title, "Режим Рандомизация");
            LangStrings.Add(TextAlias.Mode_Description, "Рандомизирует врагов и предметы сложным и неожиданным образом.");
        }
    }
}
