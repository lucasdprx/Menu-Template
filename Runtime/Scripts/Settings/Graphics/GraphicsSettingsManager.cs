using System.Linq;
using UnityEngine;
using PTRKGames.MenuTemplate.Runtime.Data;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Graphics
{
    /// <summary>
    /// Manages the game's graphics settings including resolution, fullscreen mode, and framerate limits.
    /// </summary>
    public class GraphicsSettingsManager : MonoBehaviour
    {
        protected Resolution[] resolutions;

        /// <summary>
        /// Gets the list of available screen resolutions, filtered to unique width-height pairs to avoid duplicates from different refresh rates.
        /// </summary>
        public virtual Resolution[] Resolutions
        {
            get
            {
                if (resolutions == null)
                {
                    resolutions = Screen.resolutions
                        .GroupBy(r => new { r.width, r.height })
                        .Select(g => g.First()) 
                        .ToArray();
                }
                return resolutions;
            }
        }

        protected virtual void Awake()
        {
            ApplySavedGraphics();
        }

        /// <summary>
        /// Applies all saved graphics settings (Resolution, FullScreen, Framerate) on game boot.
        /// </summary>
        protected virtual void ApplySavedGraphics()
        {
            int resIndex = GetSavedResolutionIndex();
            if (resIndex >= 0 && resIndex < Resolutions.Length)
            {
                Resolution res = Resolutions[resIndex];
                Screen.SetResolution(res.width, res.height, GetSavedFullScreen());
            }

            SetFramerateLimit(GetSavedFramerateIndex());
        }

        /// <summary>
        /// Sets the screen resolution based on the filtered resolutions array.
        /// </summary>
        /// <param name="index">The array index of the desired resolution.</param>
        public virtual void SetResolution(int index)
        {
            if (index < 0 || index >= Resolutions.Length)
            {
                Debug.LogWarning($"Invalid resolution index: {index}");
                return;
            }
    
            Resolution resolution = Resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
            SaveInt(SettingsKeys.ResolutionIndex, index);
        }
        
        /// <summary>
        /// Toggles fullscreen mode.
        /// </summary>
        /// <param name="isFullScreen">True for fullscreen, false for windowed.</param>
        public virtual void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
            SaveInt(SettingsKeys.FullScreen, isFullScreen ? 1 : 0);
        }

        /// <summary>
        /// Sets the target framerate or enables VSync.
        /// </summary>
        /// <param name="dropdownIndex">The index matching the FramerateOption enum.</param>
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
                    Application.targetFrameRate = -1; // -1 = Unlimited in Unity
                    break;
                default:
                    Debug.LogWarning($"Invalid Framerate option: {dropdownIndex}");
                    return;
            }

            SaveInt(SettingsKeys.FramerateIndex, dropdownIndex);
        }

        /// <summary>
        /// Saves an integer value to RAM. Data is physically written to disk on disable.
        /// </summary>
        protected virtual void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        /// <summary>
        /// Retrieves the saved resolution index.
        /// </summary>
        public virtual int GetSavedResolutionIndex() => PlayerPrefs.GetInt(SettingsKeys.ResolutionIndex, Resolutions.Length - 1);
        
        /// <summary>
        /// Retrieves the saved fullscreen state.
        /// </summary>
        public virtual bool GetSavedFullScreen() => PlayerPrefs.GetInt(SettingsKeys.FullScreen, Screen.fullScreen ? 1 : 0) == 1;
        
        /// <summary>
        /// Retrieves the saved framerate limit index.
        /// </summary>
        public virtual int GetSavedFramerateIndex() => PlayerPrefs.GetInt(SettingsKeys.FramerateIndex, (int)FramerateOption.FPS_120);

        /// <summary>
        /// Commits all graphics changes to the hard drive when the manager is disabled (e.g., closing the menu).
        /// </summary>
        protected virtual void OnDisable()
        {
            PlayerPrefs.Save();
        }

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