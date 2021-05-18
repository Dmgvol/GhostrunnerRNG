namespace GhostrunnerRNG.Maps {
    public interface IModes {
        void Gen_Easy(); // Easy layouts, no uplinks
        void Gen_Normal(); // Normal, the usual rng
        void Gen_Hardcore(); // CLASSIC LEVELS - Ingame Hardcore mode selected
        void Gen_Nightmare(); // Make them suffer, crazy layouts!
    }

    public interface IModesMidCV {
        void Gen_Easy_BeforeCV();
        void Gen_Easy_AfterCV();

        void Gen_Normal_BeforeCV();
        void Gen_Normal_AfterCV();


        void Gen_Nightmare_BeforeCV();
        void Gen_Nightmare_AfterCV();
        void Gen_Hardcore();
    }
}
