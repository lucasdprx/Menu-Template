using PTRKGames.MenuTemplate.Runtime.Data;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Audio
{
    /// <summary>
    /// Handles the user interface for audio settings, linking UI Sliders to the AudioSettingsManager.
    /// </summary>
    public class AudioSettingsUI : MonoBehaviour
    {
        [Tooltip("Reference to the manager handling the AudioMixer logic.")]
        [SerializeField] protected AudioSettingsManager audioManager;
        
        [Header("Audio UI")]
        [Tooltip("Slider controlling the master volume.")]
        [SerializeField] protected Slider sliderGeneral;
        
        [Tooltip("Slider controlling the music volume.")]
        [SerializeField] protected Slider sliderMusic;
        
        [Tooltip("Slider controlling the sound effects (SFX) volume.")]
        [SerializeField] protected Slider sliderSfx;

        protected virtual void Start()
        {
            if (audioManager == null) 
            {
                Debug.LogWarning("AudioSettingsManager reference is missing in AudioSettingsUI.");
                return;
            }

            InitializeUI();
            SubscribeEvents();
        }

        protected virtual void OnDestroy()
        {
            if (audioManager != null)
            {
                UnsubscribeEvents();
            }
        }

        /// <summary>
        /// Initializes all assigned sliders with their saved values.
        /// </summary>
        protected virtual void InitializeUI()
        {
            InitSlider(sliderGeneral, SettingsKeys.General);
            InitSlider(sliderMusic, SettingsKeys.Music);
            InitSlider(sliderSfx, SettingsKeys.Sfx);
        }

        /// <summary>
        /// Configures a single slider's boundaries and assigns its saved value.
        /// </summary>
        /// <param name="slider">The UI Slider component.</param>
        /// <param name="key">The PlayerPrefs save key corresponding to this volume.</param>
        protected virtual void InitSlider(Slider slider, string key)
        {
            if (slider == null)
                return;
            
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning($"key is null or empty for slider: {slider.gameObject.name}");
                return;
            }
            
            slider.minValue = 0f; 
            slider.maxValue = 1f; 
            slider.value = audioManager.GetSavedVolume(key);
        }

        /// <summary>
        /// Subscribes the sliders to the audio manager's volume update methods.
        /// </summary>
        protected virtual void SubscribeEvents()
        {
            sliderGeneral?.onValueChanged.AddListener(audioManager.SetVolumeGeneral);
            sliderMusic?.onValueChanged.AddListener(audioManager.SetVolumeMusic);
            sliderSfx?.onValueChanged.AddListener(audioManager.SetVolumeSfx);
        }

        /// <summary>
        /// Removes the event listeners to prevent memory leaks when the UI is destroyed.
        /// </summary>
        protected virtual void UnsubscribeEvents()
        {
            sliderGeneral?.onValueChanged.RemoveListener(audioManager.SetVolumeGeneral);
            sliderMusic?.onValueChanged.RemoveListener(audioManager.SetVolumeMusic);
            sliderSfx?.onValueChanged.RemoveListener(audioManager.SetVolumeSfx);
        }
    }
}
