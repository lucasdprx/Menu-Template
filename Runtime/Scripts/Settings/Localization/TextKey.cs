using TMPro;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    /// <summary>
    /// Automatically translates a TextMeshProUGUI component based on a localization key.
    /// Listens to global language changes to update dynamically.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextKey : MonoBehaviour
    {
        [Tooltip("The unique string key matching the JSON localization file.")]
        [SerializeField] protected string key;
        
        protected TextMeshProUGUI text;

        protected virtual void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        protected virtual void Start()
        {
            LocalizationManager.OnLanguageChanged += UpdateText;
            UpdateText();
        }

        protected virtual void OnDestroy()
        {
            LocalizationManager.OnLanguageChanged -= UpdateText;
        }

        /// <summary>
        /// Fetches the translated text from the LocalizationManager and applies it.
        /// </summary>
        protected virtual void UpdateText()
        {
            if (text == null) 
                return;

            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning($"Localization key is missing on GameObject: {gameObject.name}");
                return;
            }
        
            text.text = LocalizationManager.Read(key);
        }

        /// <summary>
        /// Dynamically changes the localization key at runtime and refreshes the text.
        /// </summary>
        /// <param name="newKey">The new localization key to apply.</param>
        public virtual void SetKey(string newKey)
        {
            key = newKey;
            UpdateText();
        }
    }
}