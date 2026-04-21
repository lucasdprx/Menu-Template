using PTRKGames.MenuTemplate.Runtime.UI_Element;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Graphics
{
    public class GraphicsSettingsUI : MonoBehaviour
    {
        [SerializeField] protected GraphicsSettingsManager graphicsSettingsManager;
        
        [Header("Graphics UI")]
        [SerializeField] protected OptionSelector selectorResolution;
        [SerializeField] protected Toggle toggleFullScreen;
        [SerializeField] protected OptionSelector selectorFramerate;

        protected virtual void Start()
        {
            if (graphicsSettingsManager == null)
            {
                Debug.LogError("GraphicsSettingsManager reference is missing on " + gameObject.name);
                return;
            }
            
            InitializeUI();
            SubscribeEvents();
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeEvents();
        }

        protected virtual void InitializeUI()
        {
            InitResolutionDropdown();
            InitFramerateDropdown();

            if (toggleFullScreen != null)
            {
                toggleFullScreen.isOn = graphicsSettingsManager.GetSavedFullScreen();
                graphicsSettingsManager.SetFullScreen(toggleFullScreen.isOn);
            }
        }

        protected virtual void InitResolutionDropdown()
        {
            if (selectorResolution == null) 
                return;

            selectorResolution.ClearOptions();
            List<string> options = graphicsSettingsManager.Resolutions.Select(res => $"{res.width}x{res.height}").ToList();
            selectorResolution.AddOptions(options);
            selectorResolution.SetValue(graphicsSettingsManager.GetSavedResolutionIndex());
            graphicsSettingsManager.SetResolution(graphicsSettingsManager.GetSavedResolutionIndex());
        }

        protected virtual void InitFramerateDropdown()
        {
            if (selectorFramerate == null) 
                return;

            selectorFramerate.ClearOptions();
            selectorFramerate.AddOptions(GetFramerateStringOptions());
            selectorFramerate.SetValue(graphicsSettingsManager.GetSavedFramerateIndex());
            graphicsSettingsManager.SetFramerateLimit(graphicsSettingsManager.GetSavedFramerateIndex());
        }

        protected virtual List<string> GetFramerateStringOptions()
        {
            return new List<string> { "Vsync", "30 FPS", "60 FPS", "120 FPS", "240 FPS", "Unlimited" };
        }

        protected virtual void SubscribeEvents()
        {
            selectorResolution?.onValueChanged.AddListener(graphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.AddListener(graphicsSettingsManager.SetFullScreen);
            selectorFramerate?.onValueChanged.AddListener(graphicsSettingsManager.SetFramerateLimit);
        }

        protected virtual void UnsubscribeEvents()
        {
            selectorResolution?.onValueChanged.RemoveListener(graphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.RemoveListener(graphicsSettingsManager.SetFullScreen);
            selectorFramerate?.onValueChanged.RemoveListener(graphicsSettingsManager.SetFramerateLimit);
        }
    }
}