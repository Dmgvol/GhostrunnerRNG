using System.Collections.Generic;
using static GhostrunnerRNG.Localization.LocalizationBase;

namespace GhostrunnerRNG.Localization {
    class LocalizationManager {

        private static LocalizationManager Instance;

        public static LocalizationManager GetInstance() {
            if(Instance == null) Instance = new LocalizationManager();
            return Instance;
        }

        private Dictionary<Language, LocalizationBase> LocalizationList = new Dictionary<Language, LocalizationBase>();
        public enum Language { EN, RU, DE, PL, ES, FR, OTHER };
        public Language CurrentLang { get; private set; } = Language.EN;

        public void SetLanguage(Language lang) => CurrentLang = lang;

        public LocalizationManager() {
            LocalizationList.Add(Language.EN, new Lang_EN());
            LocalizationList.Add(Language.RU, new Lang_RU());
            LocalizationList.Add(Language.DE, new Lang_DE());
            LocalizationList.Add(Language.PL, new Lang_PL());
            LocalizationList.Add(Language.ES, new Lang_ES());
            LocalizationList.Add(Language.FR, new Lang_FR());
        }

        public string GetLocalization(TextAlias alias) {
            // has current lang?
            if(LocalizationList.ContainsKey(CurrentLang)){
                // has alias in correct lang?
                if(LocalizationList[CurrentLang].LangStrings.ContainsKey(alias)) {
                    return LocalizationList[CurrentLang].LangStrings[alias];
                }
            }
            // return default text in EN
            return LocalizationList[Language.EN].LangStrings[alias];
        }
    }
}
