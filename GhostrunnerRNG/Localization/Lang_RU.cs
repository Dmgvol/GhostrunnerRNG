namespace GhostrunnerRNG.Localization {
    class Lang_RU : LocalizationBase {

        public Lang_RU() : base(LocalizationManager.Language.RU) {
            LangStrings.Add(TextAlias.Mode_Title, "Режим Рандомизация");
            LangStrings.Add(TextAlias.Mode_Description, "Рандомизирует врагов и предметы сложным и неожиданным образом.");

            LangStrings.Add(TextAlias.HCMode_Title, "Жесткая Рандомизация");
            LangStrings.Add(TextAlias.HCMode_Description, "Модификации доступны с самого начала, большее количество врагов рандомизировано, чтобы бросить смертельный вызов.");

            LangStrings.Add(TextAlias.Ability_Tempest_RefundDesc_Default, "При одновременном убийстве двух и более противников с помощью шквала потраченная на приём концентрация мгновенно восполняется.");
            LangStrings.Add(TextAlias.Ability_Tempest_RefundDesc_HC, "При одновременном убийстве трех и более противников с помощью шквала потраченная на приём концентрация мгновенно восполняется.");
        }
    }
}
