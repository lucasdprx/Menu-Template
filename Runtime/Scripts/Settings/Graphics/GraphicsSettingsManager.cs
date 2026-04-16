using System.Linq;
using UnityEngine;
using PTRKGames.MenuTemplate.Runtime.Data;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Graphics
{
    public class GraphicsSettingsManager : MonoBehaviour
    {
        private Resolution[] resolutions;

        public virtual Resolution[] Resolutions
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

        public virtual void SetResolution(int index)
        {
            if (index < 0 || index >= Resolutions.Length)
            {
                Debug.LogError($"Index invalid for resolution: {index}");
                return;
            }
    
            Resolution resolution = Resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
            SaveInt(SettingsKeys.ResolutionIndex, index);
        }
        
        public virtual void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
            SaveInt(SettingsKeys.FullScreen, isFullScreen ? 1 : 0);
        }

        public virtual void SetFramerateLimit(int dropdownIndex)
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

            SaveInt(SettingsKeys.FramerateIndex, dropdownIndex);
        }

        protected virtual void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public virtual int GetSavedResolutionIndex() => PlayerPrefs.HasKey(SettingsKeys.ResolutionIndex) ? PlayerPrefs.GetInt(SettingsKeys.ResolutionIndex) : 0;
        
        public virtual bool GetSavedFullScreen() => PlayerPrefs.HasKey(SettingsKeys.FullScreen) ? PlayerPrefs.GetInt(SettingsKeys.FullScreen) == 1 : Screen.fullScreen;
        
        public virtual int GetSavedFramerateIndex() => PlayerPrefs.HasKey(SettingsKeys.FramerateIndex) ? PlayerPrefs.GetInt(SettingsKeys.FramerateIndex) : (int)FramerateOption.FPS_120;

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