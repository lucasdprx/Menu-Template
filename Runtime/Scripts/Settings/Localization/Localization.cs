using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    public class Localization : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private string languageFolder = "Localization";

        public static event Action OnLanguageChanged;

        private static Dictionary<string, string> texts = new();
        private readonly Dictionary<string, string> languageNames = new()
        {
            {"fr", "Français"},
            {"en", "English"},
            {"es", "Español"},
            {"de", "Deutsch"},
            {"it", "Italiano"}
        };

        private void Awake()
        {
            LoadLanguage(PlayerPrefs.GetInt("Language", 0));

            if (dropdown)
            {
                dropdown.ClearOptions();
                List<string> options = languageNames.Select(code => code.Value).ToList();
                dropdown.AddOptions(options);
                dropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("Language", 0));
                dropdown.onValueChanged.AddListener(ChangeLanguage);
            }
        }

        private void ChangeLanguage(int index)
        {
            if (index < 0 || index >= languageNames.Count)
            {
                Debug.LogError("Invalid language index");
                return;
            }
        
            LoadLanguage(index);
    
            PlayerPrefs.SetInt("Language", index);
            OnLanguageChanged?.Invoke();
        }

        private void LoadLanguage(int index)
        {
            string languageCode = languageNames.ElementAt(index).Key;
            string fullPath = $"{languageFolder}/{languageCode}";
        
            TextAsset textAsset = Resources.Load<TextAsset>(fullPath);
            
            string json = textAsset != null ? textAsset.text : File.ReadAllText(fullPath);
            
            texts = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            Resources.UnloadAsset(textAsset);
        }
    
        public static string Read(string key)
        {
            return texts.TryGetValue(key, out string value) ? value : $"[MISSING:{key}]";
        }
    }
}
