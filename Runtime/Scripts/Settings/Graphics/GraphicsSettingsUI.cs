using PTRKGames.MenuTemplate.Runtime.UI_Element;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Graphics
{
    /// <summary>
    /// Handles the user interface for graphics settings, linking UI elements to the GraphicsSettingsManager.
    /// </summary>
    public class GraphicsSettingsUI : MonoBehaviour
    {
        [Tooltip("Reference to the manager handling the visual and resolution logic.")]
        [SerializeField] protected GraphicsSettingsManager graphicsSettingsManager;
        
        [Header("Graphics UI")]
        [Tooltip("Custom dropdown selector for screen resolutions.")]
        [SerializeField] protected OptionSelector selectorResolution;
        
        [Tooltip("Toggle button for Fullscreen / Windowed mode.")]
        [SerializeField] protected Toggle toggleFullScreen;
        
        [Tooltip("Custom dropdown selector for framerate limits (VSync, 60fps, etc.).")]
        [SerializeField] protected OptionSelector selectorFramerate;

        protected virtual void Start()
        {
            if (graphicsSettingsManager == null)
            {
                Debug.LogWarning("GraphicsSettingsManager reference is missing in GraphicsSettingsUI. Graphics UI will not function.");
                return;
            }
            
            InitializeUI();
            SubscribeEvents();
        }

        protected virtual void OnDestroy()
        {
            if (graphicsSettingsManager != null)
            {
                UnsubscribeEvents();
            }
        }

        /// <summary>
        /// Initializes the UI elements to reflect the currently saved graphics settings.
        /// </summary>
        protected virtual void InitializeUI()
        {
            InitResolutionDropdown();
            InitFramerateDropdown();

            if (toggleFullScreen != null)
            {
                toggleFullScreen.isOn = graphicsSettingsManager.GetSavedFullScreen();
            }
        }

        /// <summary>
        /// Populates the resolution selector with available screen formats and sets the saved index.
        /// </summary>
        protected virtual void InitResolutionDropdown()
        {
            if (selectorResolution == null) return;

            selectorResolution.ClearOptions();
            
            List<string> options = graphicsSettingsManager.Resolutions
                .Select(res => $"{res.width}x{res.height}")
                .ToList();
                
            selectorResolution.AddOptions(options);
            selectorResolution.SetValue(graphicsSettingsManager.GetSavedResolutionIndex());
        }

        /// <summary>
        /// Populates the framerate selector and sets the saved index.
        /// </summary>
        protected virtual void InitFramerateDropdown()
        {
            if (selectorFramerate == null) return;

            selectorFramerate.ClearOptions();
            selectorFramerate.AddOptions(GetFramerateStringOptions());
            selectorFramerate.SetValue(graphicsSettingsManager.GetSavedFramerateIndex());
        }

        /// <summary>
        /// Defines the display names for the framerate options.
        /// Override this method to implement localization for these strings.
        /// </summary>
        /// <returns>A list of formatted framerate strings.</returns>
        protected virtual List<string> GetFramerateStringOptions()
        {
            return new List<string> { "Vsync", "30 FPS", "60 FPS", "120 FPS", "240 FPS", "Unlimited" };
        }

        /// <summary>
        /// Subscribes UI elements to the graphics manager's update methods.
        /// </summary>
        protected virtual void SubscribeEvents()
        {
            selectorResolution?.onValueChanged.AddListener(graphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.AddListener(graphicsSettingsManager.SetFullScreen);
            selectorFramerate?.onValueChanged.AddListener(graphicsSettingsManager.SetFramerateLimit);
        }

        /// <summary>
        /// Removes the event listeners to prevent memory leaks when the UI is destroyed.
        /// </summary>
        protected virtual void UnsubscribeEvents()
        {
            selectorResolution?.onValueChanged.RemoveListener(graphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.RemoveListener(graphicsSettingsManager.SetFullScreen);
            selectorFramerate?.onValueChanged.RemoveListener(graphicsSettingsManager.SetFramerateLimit);
        }
    }
}