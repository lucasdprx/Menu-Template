namespace PTRKGames.MenuTemplate.Runtime.Data
{
    /// <summary>
    /// Contains all the unique keys used to save and load the user's settings via PlayerPrefs.
    /// The prefix ensures these keys do not collide with the main game's own save data.
    /// </summary>
    public static class SettingsKeys
    {
        private const string Prefix = "PtrkMenu_";

        /// <summary>Key for the Master/General audio volume (Float).</summary>
        public const string General = Prefix + "General";
        
        /// <summary>Key for the Music audio volume (Float).</summary>
        public const string Music = Prefix + "Music";
        
        /// <summary>Key for the Sound Effects (SFX) audio volume (Float).</summary>
        public const string Sfx = Prefix + "SFX";
        
        /// <summary>Key for the saved screen resolution index (Int).</summary>
        public const string ResolutionIndex = Prefix + "ResolutionIndex";
        
        /// <summary>Key for the fullscreen toggle state (Int: 0 = Windowed, 1 = Fullscreen).</summary>
        public const string FullScreen = Prefix + "FullScreen";
        
        /// <summary>Key for the saved framerate limit index (Int matching FramerateOption enum).</summary>
        public const string FramerateIndex = Prefix + "FramerateIndex";
        
        /// <summary>
        /// Key for the saved language index (Int matching the available languages list in the LocalizationManager).
        /// </summary>
        public const string LanguageIndex = Prefix + "LanguageIndex";
    }
}