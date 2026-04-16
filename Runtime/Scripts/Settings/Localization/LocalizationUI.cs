using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    public class LocalizationUI : MonoBehaviour
    {
        [SerializeField] protected LocalizationManager localizationManager;
        [SerializeField] protected TMP_Dropdown dropdown;

        protected virtual void Start()
        {
            if (localizationManager == null || dropdown == null)
            {
                Debug.LogError("Missing references in LocalizationUI");
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
            dropdown.ClearOptions();
            
            List<string> options = localizationManager.AvailableLanguages
                .Select(lang => lang.displayName)
                .ToList();
                
            dropdown.AddOptions(options);
            dropdown.SetValueWithoutNotify(localizationManager.GetSavedLanguageIndex());
            dropdown.RefreshShownValue();
        }

        protected virtual void SubscribeEvents()
        {
            dropdown.onValueChanged.AddListener(localizationManager.ChangeLanguage);
        }

        protected virtual void UnsubscribeEvents()
        {
            dropdown.onValueChanged.RemoveListener(localizationManager.ChangeLanguage);
        }
    }
}