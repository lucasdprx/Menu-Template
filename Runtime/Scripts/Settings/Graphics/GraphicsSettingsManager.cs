using System.Linq;
using UnityEngine;
using Menu.Data;

namespace Menu.Settings.Graphics
{
    public class GraphicsSettingsManager : MonoBehaviour
    {
        public static Resolution[] Resolutions;
    
        private void Awake()
        {
            Resolutions = Screen.resolutions.GroupBy(r => new { r.width, r.height }).Select(g => g.First()).ToArray();

            for (int i = 0; i < Resolutions.Length / 2; i++) // Set higher resolutions first
            {
                (Resolutions[Resolutions.Length - 1 - i], Resolutions[i]) = (Resolutions[i], Resolutions[Resolutions.Length - 1 - i]);
            }
        }

        public static void SetResolution(int index)
        {
            if (index < 0 || index >= Resolutions.Length)
            {
                Debug.LogError($"Index invalid for resolution: {index}");
                return;
            }
    
            Resolution resolution = Resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
            PlayerPrefs.SetInt(SettingsKeys.ResolutionIndex, index);
            PlayerPrefs.Save();
        }
        
        public static void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
            PlayerPrefs.SetInt(SettingsKeys.FullScreen, isFullScreen ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void SetFramerateLimit(int dropdownIndex)
        {
            switch (dropdownIndex)
            {
                case 0: // VSync
                    QualitySettings.vSyncCount = 1;
                    Application.targetFrameRate = -1; 
                    break;
                case 1: // 30 FPS
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 30;
                    break;
                case 2: // 60 FPS
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 60;
                    break;
                case 3: // 120 FPS
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 120;
                    break;
                case 4: // 240 FPS
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 240;
                    break;
                case 5: // Unlimited
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = -1;
                    break;
            }
    
            PlayerPrefs.SetInt(SettingsKeys.FramerateIndex, dropdownIndex);
            PlayerPrefs.Save();
        }
    
        public static int GetSavedResolutionIndex() => PlayerPrefs.HasKey(SettingsKeys.ResolutionIndex) ? PlayerPrefs.GetInt(SettingsKeys.ResolutionIndex) : Resolutions.Length - 1;
        public static bool GetSavedFullScreen() => PlayerPrefs.HasKey(SettingsKeys.FullScreen) ? PlayerPrefs.GetInt(SettingsKeys.FullScreen) == 1 : Screen.fullScreen;
        public static int GetSavedFramerateIndex() => PlayerPrefs.HasKey(SettingsKeys.FramerateIndex) ? PlayerPrefs.GetInt(SettingsKeys.FramerateIndex) : 3; // Default 120 FPS
    }
}
