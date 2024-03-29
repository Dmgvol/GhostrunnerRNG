﻿using System.Collections.Generic;
using static GhostrunnerRNG.Localization.LocalizationManager;

namespace GhostrunnerRNG.Localization {
    class LocalizationBase {

        public Language lang { get; private set; }

        public LocalizationBase(Language lang) {
            this.lang = lang;
        }

        public enum TextAlias {
            Mode_Title,
            Mode_Description,
            HCMode_Title,
            HCMode_Description,
            Ability_Tempest_RefundDesc_Default,
            Ability_Tempest_RefundDesc_HC
        };

        public Dictionary<TextAlias, string> LangStrings = new Dictionary<TextAlias, string>();
    }
}
