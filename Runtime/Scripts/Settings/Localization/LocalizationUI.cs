using PTRKGames.MenuTemplate.Runtime.UI_Element;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    /// <summary>
    /// Handles the user interface for language selection, linking an OptionSelector to the LocalizationManager.
    /// </summary>
    public class LocalizationUI : MonoBehaviour
    {
        [Tooltip("Reference to the core manager handling language data.")]
        [SerializeField] protected LocalizationManager localizationManager;
        
        [Tooltip("The UI selector component used to cycle through available languages.")]
        [SerializeField] protected OptionSelector selector;

        protected virtual void Start()
        {
            if (localizationManager == null || selector == null)
            {
                Debug.LogWarning($"Missing references in LocalizationUI on '{gameObject.name}'");
                return;
            }

            InitializeUI();
            SubscribeEvents();
        }

        protected virtual void OnDestroy()
        {
            if (localizationManager != null && selector != null)
            {
                UnsubscribeEvents();
            }
        }

        /// <summary>
        /// Populates the UI selector with available languages and sets the initial visual state.
        /// </summary>
        protected virtual void InitializeUI()
        {
            selector.ClearOptions();
            
            List<string> options = localizationManager.AvailableLanguages
                .Select(lang => lang.displayName)
                .ToList();

            selector.AddOptions(options);
            selector.SetValueWithoutNotify(localizationManager.GetSavedLanguageIndex());
        }

        /// <summary>
        /// Subscribes the UI selector to the manager's language change logic.
        /// </summary>
        protected virtual void SubscribeEvents()
        {
            selector?.onValueChanged.AddListener(localizationManager.ChangeLanguage);
        }

        /// <summary>
        /// Removes the event listener to prevent memory leaks when the UI is destroyed.
        /// </summary>
        protected virtual void UnsubscribeEvents()
        {
            selector?.onValueChanged.RemoveListener(localizationManager.ChangeLanguage);
        }
    }
}