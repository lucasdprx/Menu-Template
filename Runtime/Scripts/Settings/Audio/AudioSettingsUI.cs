using UnityEngine;
using Menu.Data;
using UnityEngine.UI;

namespace Menu.Settings.Audio
{
    public class AudioSettingsUI : MonoBehaviour
    {
        [SerializeField] private AudioSettingsManager audioManager;
        
        [Header("Audio UI")]
        [SerializeField] private Slider sliderGeneral;
        [SerializeField] private Slider sliderMusic;
        [SerializeField] private Slider sliderSfx;

        private void Start()
        {
            if (audioManager == null) return;

            InitializeUI();
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void InitializeUI()
        {
            InitSlider(sliderGeneral, SettingsKeys.General);
            InitSlider(sliderMusic, SettingsKeys.Music);
            InitSlider(sliderSfx, SettingsKeys.Sfx);
        }

        private void InitSlider(Slider slider, string key)
        {
            if (slider == null || string.IsNullOrEmpty(key)) 
                return;
            
            slider.minValue = 0.0001f; 
            slider.maxValue = 1f; 
            slider.value = audioManager.GetSavedVolume(key);
        }

        private void SubscribeEvents()
        {
            sliderGeneral?.onValueChanged.AddListener(audioManager.SetVolumeGeneral);
            sliderMusic?.onValueChanged.AddListener(audioManager.SetVolumeMusic);
            sliderSfx?.onValueChanged.AddListener(audioManager.SetVolumeSfx);
        }

        private void UnsubscribeEvents()
        {
            sliderGeneral?.onValueChanged.RemoveListener(audioManager.SetVolumeGeneral);
            sliderMusic?.onValueChanged.RemoveListener(audioManager.SetVolumeMusic);
            sliderSfx?.onValueChanged.RemoveListener(audioManager.SetVolumeSfx);
        }
    }
}
