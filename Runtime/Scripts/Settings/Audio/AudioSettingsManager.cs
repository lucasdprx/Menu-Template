using UnityEngine;
using PTRKGames.MenuTemplate.Runtime.Data;
using UnityEngine.Audio;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Audio
{
    public class AudioSettingsManager : MonoBehaviour
    {
        [SerializeField] protected AudioMixer audioMixer;

        private void Start()
        {
            ApplySavedVolumes();
        }

        public virtual void SetVolumeGeneral(float volume) => SetVolume(SettingsKeys.General, volume);
        public virtual void SetVolumeMusic(float volume) => SetVolume(SettingsKeys.Music, volume);
        public virtual void SetVolumeSfx(float volume) => SetVolume(SettingsKeys.Sfx, volume);

        protected virtual void SetVolume(string key, float volume)
        {
            if (audioMixer == null || string.IsNullOrEmpty(key))
            {
                Debug.LogError($"AudioMixer reference is missing or key is null for {key}");
                return;
            }

            // Conversion from linear to logarithmic
            float dbVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
            audioMixer.SetFloat(key, dbVolume);
            
            SaveVolume(key, volume);
        }
        
        protected virtual void SaveVolume(string key, float volume)
        {
            PlayerPrefs.SetFloat(key, volume);
            PlayerPrefs.Save();
        }

        public virtual float GetSavedVolume(string key)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : 1f;
        }

        protected virtual void ApplySavedVolumes()
        {
            SetVolume(SettingsKeys.General, GetSavedVolume(SettingsKeys.General));
            SetVolume(SettingsKeys.Music, GetSavedVolume(SettingsKeys.Music));
            SetVolume(SettingsKeys.Sfx, GetSavedVolume(SettingsKeys.Sfx));
        }
    }
}
