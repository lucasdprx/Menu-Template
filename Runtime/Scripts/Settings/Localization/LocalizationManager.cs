using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    [Serializable]
    public struct LanguageOption
    {
        public string code;
        public string displayName;
    }

    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField] protected string languageFolder = "Localization";
        [SerializeField] protected List<LanguageOption> availableLanguages = new()
        {
            new LanguageOption { code = "fr", displayName = "Français" },
            new LanguageOption { code = "en", displayName = "English" },
            new LanguageOption { code = "es", displayName = "Español" },
            new LanguageOption { code = "de", displayName = "Deutsch" },
            new LanguageOption { code = "it", displayName = "Italiano" },
        };

        public static event Action OnLanguageChanged;
        protected static Dictionary<string, string> texts = new();

        public virtual List<LanguageOption> AvailableLanguages => availableLanguages;

        protected virtual void Awake()
        {
            LoadLanguage(GetSavedLanguageIndex());
        }

        public virtual void ChangeLanguage(int index)
        {
            if (index < 0 || index >= availableLanguages.Count)
            {
                Debug.LogError("Invalid language index");
                return;
            }

            LoadLanguage(index);
            SaveLanguageIndex(index);
            OnLanguageChanged?.Invoke();
        }

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
                Debug.LogError($"Localization file not found at Resources/{fullPath}.json");
                texts = new Dictionary<string, string>();
            }
        }

        public static string Read(string key)
        {
            if (texts == null) 
                return $"[MISSING:{key}]";
            return texts.TryGetValue(key, out string value) ? value : $"[MISSING:{key}]";
        }
        

        protected virtual void SaveLanguageIndex(int index)
        {
            PlayerPrefs.SetInt("LanguageIndex", index);
            PlayerPrefs.Save();
        }

        public virtual int GetSavedLanguageIndex()
        {
            return PlayerPrefs.GetInt("LanguageIndex", 0);
        }
    }
}