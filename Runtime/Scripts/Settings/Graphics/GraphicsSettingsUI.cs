using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Graphics
{
    public class GraphicsSettingsUI : MonoBehaviour
    {
        [SerializeField] protected GraphicsSettingsManager graphicsSettingsManager;
        
        [Header("Graphics UI")]
        [SerializeField] protected TMP_Dropdown dropdownResolution;
        [SerializeField] protected Toggle toggleFullScreen;
        [SerializeField] protected TMP_Dropdown dropdownFramerate;

        protected virtual void Start()
        {
            if (graphicsSettingsManager == null)
            {
                Debug.LogError("GraphicsSettingsManager reference is missing on " + gameObject.name);
                return;
            }
            
            SubscribeEvents();
            InitializeUI();
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
                toggleFullScreen.isOn = graphicsSettingsManager.GetSavedFullScreen();
        }

        protected virtual void InitResolutionDropdown()
        {
            if (dropdownResolution == null) 
                return;

            dropdownResolution.ClearOptions();
            List<string> options = graphicsSettingsManager.Resolutions.Select(res => $"{res.width}x{res.height}").ToList();
            dropdownResolution.AddOptions(options);
            dropdownResolution.value = graphicsSettingsManager.GetSavedResolutionIndex();
            dropdownResolution.RefreshShownValue();
        }

        protected virtual void InitFramerateDropdown()
        {
            if (dropdownFramerate == null) 
                return;

            dropdownFramerate.ClearOptions();
            dropdownFramerate.AddOptions(GetFramerateStringOptions());
            dropdownFramerate.value = graphicsSettingsManager.GetSavedFramerateIndex();
            dropdownFramerate.RefreshShownValue();
        }

        protected virtual List<string> GetFramerateStringOptions()
        {
            return new List<string> { "Vsync", "30 FPS", "60 FPS", "120 FPS", "240 FPS", "Unlimited" };
        }

        protected virtual void SubscribeEvents()
        {
            dropdownResolution?.onValueChanged.AddListener(graphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.AddListener(graphicsSettingsManager.SetFullScreen);
            dropdownFramerate?.onValueChanged.AddListener(graphicsSettingsManager.SetFramerateLimit);
        }

        protected virtual void UnsubscribeEvents()
        {
            dropdownResolution?.onValueChanged.RemoveListener(graphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.RemoveListener(graphicsSettingsManager.SetFullScreen);
            dropdownFramerate?.onValueChanged.RemoveListener(graphicsSettingsManager.SetFramerateLimit);
        }
    }
}