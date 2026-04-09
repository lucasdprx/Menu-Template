using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings.Graphics
{
    public class GraphicsSettingsUI : MonoBehaviour
    {
        [Header("Graphics UI")]
        [SerializeField] private TMP_Dropdown dropdownResolution;
        [SerializeField] private Toggle toggleFullScreen;
        [SerializeField] private TMP_Dropdown dropdownFramerate;

        private void Start()
        {
            InitializeUI();
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void InitializeUI()
        {
            if (dropdownResolution != null)
            {
                dropdownResolution.ClearOptions();
                List<string> options = GraphicsSettingsManager.Resolutions.Select(res => $"{res.width}x{res.height}").ToList();
                dropdownResolution.AddOptions(options);
                dropdownResolution.value = GraphicsSettingsManager.GetSavedResolutionIndex();
            }
            if (dropdownFramerate != null)
            {
                dropdownFramerate.ClearOptions();
                List<string> framerateOptions = new() { "Vsync", "30 FPS", "60 FPS", "120 FPS", "240 FPS", "Unlimited" };
                dropdownFramerate.AddOptions(framerateOptions);
                dropdownFramerate.value = GraphicsSettingsManager.GetSavedFramerateIndex();
            }

            if (toggleFullScreen != null) 
                toggleFullScreen.isOn = GraphicsSettingsManager.GetSavedFullScreen();
        }

        private void SubscribeEvents()
        {
            dropdownResolution?.onValueChanged.AddListener(GraphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.AddListener(GraphicsSettingsManager.SetFullScreen);
            dropdownFramerate?.onValueChanged.AddListener(GraphicsSettingsManager.SetFramerateLimit);
        }

        private void UnsubscribeEvents()
        {
            dropdownResolution?.onValueChanged.RemoveListener(GraphicsSettingsManager.SetResolution);
            toggleFullScreen?.onValueChanged.RemoveListener(GraphicsSettingsManager.SetFullScreen);
            dropdownFramerate?.onValueChanged.RemoveListener(GraphicsSettingsManager.SetFramerateLimit);
        }
    }
}