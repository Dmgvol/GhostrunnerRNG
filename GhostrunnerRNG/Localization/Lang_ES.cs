namespace GhostrunnerRNG.Localization {
    class Lang_ES : LocalizationBase {

        public Lang_ES() : base(LocalizationManager.Language.ES) {
            LangStrings.Add(TextAlias.Mode_Title, "Modo Randomizador");
            LangStrings.Add(TextAlias.Mode_Description, "Randomiza los enemigos y objetos de una manera desafiante e inesperada.");

            LangStrings.Add(TextAlias.HCMode_Title, "Extremo Randomizador");
            LangStrings.Add(TextAlias.HCMode_Description, "Las mejoras están disponibles desde el principio, un mayor número de enemigos son aleatorios para plantear un desafío mortal.");
        }
    }
}
