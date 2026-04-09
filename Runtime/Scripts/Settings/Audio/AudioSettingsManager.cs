using UnityEngine;
using Menu.Data;
using UnityEngine.Audio;

namespace Menu.Settings.Audio
{
    public class AudioSettingsManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        private void Start()
        {
            ApplySavedVolumes();
        }

        public void SetVolumeGeneral(float volume) => SetVolume(SettingsKeys.General, volume);
        public void SetVolumeMusic(float volume) => SetVolume(SettingsKeys.Music, volume);
        public void SetVolumeSfx(float volume) => SetVolume(SettingsKeys.Sfx, volume);

        private void SetVolume(string key, float volume)
        {
            if (audioMixer == null || string.IsNullOrEmpty(key)) 
                return;
    
            // Conversion linéaire vers logarithmique
            float dbVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
            audioMixer.SetFloat(key, dbVolume);
            
            PlayerPrefs.SetFloat(key, volume);
            PlayerPrefs.Save();
        }

        public float GetSavedVolume(string key)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : 1f;
        }

        private void ApplySavedVolumes()
        {
            SetVolume(SettingsKeys.General, GetSavedVolume(SettingsKeys.General));
            SetVolume(SettingsKeys.Music, GetSavedVolume(SettingsKeys.Music));
            SetVolume(SettingsKeys.Sfx, GetSavedVolume(SettingsKeys.Sfx));
        }
    }
}
