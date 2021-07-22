namespace GhostrunnerRNG.Localization {
    class Lang_PL : LocalizationBase {

        public Lang_PL() : base(LocalizationManager.Language.PL) {
            LangStrings.Add(TextAlias.Mode_Title, "Tryb losowy");
            LangStrings.Add(TextAlias.Mode_Description, "Losuje przeciwników oraz obiekty w wymagającym i nieprzewidujący sposób.");

            LangStrings.Add(TextAlias.HCMode_Title, "Hardkorowy Randomizator");
            LangStrings.Add(TextAlias.HCMode_Description, "Ulepszenia są dostępne od samego początku, większa liczba wrogów jest losowana tak, aby stanowili śmiertelne wyzwanie.");

            LangStrings.Add(TextAlias.Ability_Tempest_RefundDesc_Default, "Zabicie Burza co najmniej dwóch przeciwników naraz zapewnia natychmiastowe odzyskanie wykorzystanych na nia punktów Skupienia.");
            LangStrings.Add(TextAlias.Ability_Tempest_RefundDesc_HC, "Zabicie Burza co najmniej trzech przeciwników naraz zapewnia natychmiastowe odzyskanie wykorzystanych na nia punktów Skupienia.");

        }
    }
}
