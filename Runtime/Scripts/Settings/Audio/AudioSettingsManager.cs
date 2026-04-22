using PTRKGames.MenuTemplate.Runtime.Data;
using UnityEngine;
using UnityEngine.Audio;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Audio
{
    /// <summary>
    /// Manages the game's audio volumes using an AudioMixer and saves user preferences.
    /// </summary>
    public class AudioSettingsManager : MonoBehaviour
    {
        [Tooltip("The main AudioMixer controlling the game's audio groups (General, Music, SFX).")]
        [SerializeField] protected AudioMixer audioMixer;

        protected virtual void Start()
        {
            ApplySavedVolumes();
        }
        
        /// <summary>
        /// Retrieves saved volume values from memory and applies them to the AudioMixer.
        /// </summary>
        protected virtual void ApplySavedVolumes()
        {
            SetVolume(SettingsKeys.General, GetSavedVolume(SettingsKeys.General));
            SetVolume(SettingsKeys.Music, GetSavedVolume(SettingsKeys.Music));
            SetVolume(SettingsKeys.Sfx, GetSavedVolume(SettingsKeys.Sfx));
        }

        /// <summary>
        /// Sets the master volume for the entire game.
        /// </summary>
        /// <param name="volume">A linear volume value between 0.0 and 1.0.</param>
        public virtual void SetVolumeGeneral(float volume) => SetVolume(SettingsKeys.General, volume);
        
        /// <summary>
        /// Sets the music volume.
        /// </summary>
        /// <param name="volume">A linear volume value between 0.0 and 1.0.</param>
        public virtual void SetVolumeMusic(float volume) => SetVolume(SettingsKeys.Music, volume);
        
        /// <summary>
        /// Sets the sound effects (SFX) volume.
        /// </summary>
        /// <param name="volume">A linear volume value between 0.0 and 1.0.</param>
        public virtual void SetVolumeSfx(float volume) => SetVolume(SettingsKeys.Sfx, volume);

        /// <summary>
        /// Converts a linear volume to a logarithmic decibel scale and applies it to the AudioMixer.
        /// </summary>
        /// <param name="key">The exposed parameter name in the AudioMixer.</param>
        /// <param name="volume">The linear volume (0.0 to 1.0).</param>
        protected virtual void SetVolume(string key, float volume)
        {
            if (audioMixer == null)
            {
                Debug.LogWarning($"AudioMixer reference is missing");
                return;
            }
            
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning($"key is null or empty for {key}");
                return;
            }

            // Conversion from linear to logarithmic
            float dbVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
            audioMixer.SetFloat(key, dbVolume);
            
            SaveVolume(key, volume);
        }
        
        /// <summary>
        /// Saves the linear volume to RAM. Data is physically written to disk on disable.
        /// </summary>
        /// <param name="key">The PlayerPrefs save key.</param>
        /// <param name="volume">The linear volume to save.</param>
        protected virtual void SaveVolume(string key, float volume)
        {
            PlayerPrefs.SetFloat(key, volume);
        }

        /// <summary>
        /// Retrieves the saved linear volume.
        /// </summary>
        /// <param name="key">The PlayerPrefs save key.</param>
        /// <returns>The saved volume, or 1f (100%) if no save exists.</returns>
        public virtual float GetSavedVolume(string key)
        {
            return PlayerPrefs.GetFloat(key, 1f);
        }

        /// <summary>
        /// Commits all volume changes to the hard drive when the manager is disabled (e.g., closing the menu).
        /// </summary>
        protected virtual void OnDisable()
        {
            PlayerPrefs.Save();
        }
    }
}
