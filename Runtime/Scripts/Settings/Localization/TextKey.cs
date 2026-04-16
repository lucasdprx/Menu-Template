using TMPro;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Runtime.Settings.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextKey : MonoBehaviour
    {
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
    }
}