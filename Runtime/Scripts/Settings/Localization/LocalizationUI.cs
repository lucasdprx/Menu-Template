using PTRKGames.MenuTemplate.Runtime.UI_Element;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    public class LocalizationUI : MonoBehaviour
    {
        [SerializeField] protected LocalizationManager localizationManager;
        [SerializeField] protected OptionSelector selector;

        protected virtual void Start()
        {
            if (localizationManager == null || selector == null)
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
            selector.ClearOptions();
            
            List<string> options = localizationManager.AvailableLanguages
                .Select(lang => lang.displayName)
                .ToList();

            selector.AddOptions(options);
            selector.SetValue(localizationManager.GetSavedLanguageIndex());
        }

        protected virtual void SubscribeEvents()
        {
            selector?.onValueChanged.AddListener(localizationManager.ChangeLanguage);
        }

        protected virtual void UnsubscribeEvents()
        {
            selector?.onValueChanged.RemoveListener(localizationManager.ChangeLanguage);
        }
    }
}