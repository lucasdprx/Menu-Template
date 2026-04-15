using System.Linq;
using UnityEngine;
using Menu.Data;

namespace Menu.Settings.Graphics
{
    public class GraphicsSettingsManager : MonoBehaviour
    {
        public static Resolution[] resolutions;

        public static Resolution[] Resolutions
        {
            get
            {
                if (resolutions == null)
                {
                    resolutions = Screen.resolutions
                        .GroupBy(r => new { r.width, r.height })
                        .Select(g => g.First())
                        .OrderByDescending(r => r.width)
                        .ThenByDescending(r => r.height)
                        .ToArray();
                }
                return resolutions;
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
            FramerateOption option = (FramerateOption)dropdownIndex;

            switch (option)
            {
                case FramerateOption.VSync:
                    QualitySettings.vSyncCount = 1;
                    Application.targetFrameRate = -1;
                    break;
                case FramerateOption.FPS_30:
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 30;
                    break;
                case FramerateOption.FPS_60:
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 60;
                    break;
                case FramerateOption.FPS_120:
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 120;
                    break;
                case FramerateOption.FPS_240:
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = 240;
                    break;
                case FramerateOption.Unlimited:
                    QualitySettings.vSyncCount = 0;
                    Application.targetFrameRate = -1;
                    break;
                default:
                    Debug.LogWarning($"Invalid Framerate : {dropdownIndex}");
                    return;
            }

            PlayerPrefs.SetInt(SettingsKeys.FramerateIndex, dropdownIndex);
            PlayerPrefs.Save();
        }

        public static int GetSavedResolutionIndex() => PlayerPrefs.HasKey(SettingsKeys.ResolutionIndex) ? PlayerPrefs.GetInt(SettingsKeys.ResolutionIndex) : 0;
        public static bool GetSavedFullScreen() => PlayerPrefs.HasKey(SettingsKeys.FullScreen) ? PlayerPrefs.GetInt(SettingsKeys.FullScreen) == 1 : Screen.fullScreen;
        public static int GetSavedFramerateIndex() => PlayerPrefs.HasKey(SettingsKeys.FramerateIndex) ? PlayerPrefs.GetInt(SettingsKeys.FramerateIndex) : (int)FramerateOption.FPS_120; // Default 120 FPS


        public enum FramerateOption
        {
            VSync = 0,
            FPS_30 = 1,
            FPS_60 = 2,
            FPS_120 = 3,
            FPS_240 = 4,
            Unlimited = 5
        }
    }
}
