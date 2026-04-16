using PTRKGames.MenuTemplate.Runtime.Data;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Audio
{
    public class AudioSettingsUI : MonoBehaviour
    {
        [SerializeField] protected AudioSettingsManager audioManager;
        
        [Header("Audio UI")]
        [SerializeField] protected Slider sliderGeneral;
        [SerializeField] protected Slider sliderMusic;
        [SerializeField] protected Slider sliderSfx;

        protected virtual void Start()
        {
            if (audioManager == null) 
            {
                Debug.LogError("AudioSettingsManager reference is missing in AudioSettingsUI.");
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
            InitSlider(sliderGeneral, SettingsKeys.General);
            InitSlider(sliderMusic, SettingsKeys.Music);
            InitSlider(sliderSfx, SettingsKeys.Sfx);
        }

        protected virtual void InitSlider(Slider slider, string key)
        {
            if (slider == null || string.IsNullOrEmpty(key))
            {
                Debug.LogError($"Slider or key is null for {key}");
                return;
            }
            
            slider.minValue = 0.0001f; 
            slider.maxValue = 1f; 
            slider.value = audioManager.GetSavedVolume(key);
        }

        protected virtual void SubscribeEvents()
        {
            sliderGeneral?.onValueChanged.AddListener(audioManager.SetVolumeGeneral);
            sliderMusic?.onValueChanged.AddListener(audioManager.SetVolumeMusic);
            sliderSfx?.onValueChanged.AddListener(audioManager.SetVolumeSfx);
        }

        protected virtual void UnsubscribeEvents()
        {
            sliderGeneral?.onValueChanged.RemoveListener(audioManager.SetVolumeGeneral);
            sliderMusic?.onValueChanged.RemoveListener(audioManager.SetVolumeMusic);
            sliderSfx?.onValueChanged.RemoveListener(audioManager.SetVolumeSfx);
        }
    }
}
