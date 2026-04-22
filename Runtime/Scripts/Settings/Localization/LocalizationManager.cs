using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PTRKGames.MenuTemplate.Runtime.Data;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    [Serializable]
    public struct LanguageOption
    {
        [Tooltip("The language code matching the JSON file name (e.g., 'en', 'fr').")]
        public string code;
        
        [Tooltip("The name displayed in the UI dropdown (e.g., 'English', 'Français').")]
        public string displayName;
    }

    /// <summary>
    /// Core manager handling the loading and reading of localized JSON text files.
    /// Exposes a static dictionary for fast O(1) text lookups across the game.
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        [Tooltip("The folder inside a 'Resources' directory where JSON files are stored.")]
        [SerializeField] protected string languageFolder = "Localization";
        
        [Tooltip("List of all available languages supported by the game.")]
        [SerializeField] protected List<LanguageOption> availableLanguages = new()
        {
            new LanguageOption { code = "fr", displayName = "Français" },
            new LanguageOption { code = "en", displayName = "English" },
            new LanguageOption { code = "es", displayName = "Español" },
            new LanguageOption { code = "de", displayName = "Deutsch" },
            new LanguageOption { code = "it", displayName = "Italiano" }
        };

        /// <summary>
        /// Event triggered globally whenever the user changes the active language.
        /// </summary>
        public static event Action OnLanguageChanged;
        
        protected static Dictionary<string, string> texts = new();

        public virtual List<LanguageOption> AvailableLanguages => availableLanguages;

        protected virtual void Awake()
        {
            LoadLanguage(GetSavedLanguageIndex());
        }

        /// <summary>
        /// Changes the active language, loads the new dictionary, and triggers UI updates.
        /// </summary>
        /// <param name="index">The index of the language in the AvailableLanguages list.</param>
        public virtual void ChangeLanguage(int index)
        {
            if (index < 0 || index >= availableLanguages.Count)
            {
                Debug.LogWarning($"Invalid language index: {index}. Cannot change language.");
                return;
            }

            LoadLanguage(index);
            SaveLanguageIndex(index);
            OnLanguageChanged?.Invoke();
        }

        /// <summary>
        /// Loads and deserializes the JSON file from the Resources folder into memory.
        /// </summary>
        protected virtual void LoadLanguage(int index)
        {
            if (index < 0 || index >= availableLanguages.Count) 
                index = 0;

            string languageCode = availableLanguages[index].code;
            string fullPath = $"{languageFolder}/{languageCode}";

            TextAsset textAsset = Resources.Load<TextAsset>(fullPath);
            
            if (textAsset != null)
            {
                texts = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text) ?? new Dictionary<string, string>();
                Resources.UnloadAsset(textAsset);
            }
            else
            {
                Debug.LogWarning($"[Localization] File not found at Resources/{fullPath}.json. Text keys will be displayed instead.");
                texts.Clear();
            }
        }

        /// <summary>
        /// Retrieves the translated text for a given key. 
        /// Called statically by all TextKey components.
        /// </summary>
        public static string Read(string key)
        {
            if (!texts.TryGetValue(key, out string value))
            {
                Debug.LogWarning($"Localization key '{key}' not found in current language.");
                return $"[MISSING:{key}]";
            }

            return value;
        }

        /// <summary>
        /// Saves the selected language index to RAM.
        /// </summary>
        protected virtual void SaveLanguageIndex(int index)
        {
            PlayerPrefs.SetInt(SettingsKeys.LanguageIndex, index);
        }

        /// <summary>
        /// Retrieves the saved language index. Defaults to 0 if no save exists.
        /// </summary>
        public virtual int GetSavedLanguageIndex()
        {
            return PlayerPrefs.GetInt(SettingsKeys.LanguageIndex, 0);
        }

        /// <summary>
        /// Commits the language preference to the hard drive when the manager is disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            PlayerPrefs.Save();
        }
    }
}