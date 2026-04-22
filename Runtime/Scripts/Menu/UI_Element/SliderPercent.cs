using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{
    /// <summary>
    /// Automatically updates a TextMeshProUGUI element to display a Slider's value as a percentage.
    /// Works universally regardless of the slider's min and max values.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class SliderPercent : MonoBehaviour
    {
        [Tooltip("The text component that will display the percentage.")]
        [SerializeField] protected TextMeshProUGUI textPercent;
        
        [Header("Formatting")]
        [Tooltip("The string appended to the end of the number (e.g., '%').")]
        [SerializeField] protected string suffix = "%";

        protected Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();

            if (textPercent == null)
            {
                Debug.LogWarning($"SliderPercent on '{gameObject.name}' is missing a reference to the TextMeshProUGUI component.");
            }
        }

        protected virtual void OnEnable()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(slider.value); 
        }

        protected virtual void OnDisable()
        {
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Calculates the percentage based on the slider's normalized value and updates the text.
        /// </summary>
        /// <param name="value">The current raw value of the slider.</param>
        protected virtual void OnValueChanged(float value)
        {
            if (textPercent == null) 
                return;
            
            int percent = Mathf.RoundToInt(slider.normalizedValue * 100f);
            textPercent.text = $"{percent}{suffix}";
        }
    }
}