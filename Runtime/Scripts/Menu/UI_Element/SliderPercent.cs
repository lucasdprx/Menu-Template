using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{
    [RequireComponent(typeof(Slider))]
    public class SliderPercent : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI textPercent;
        
        [Header("Formatting")]
        [Tooltip("Le texte ajouté après la valeur numérique (ex: '%', ' dB', ' HP')")]
        [SerializeField] protected string suffix = "%";

        protected Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
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

        protected virtual void OnValueChanged(float value)
        {
            if (textPercent == null)
            {
                Debug.LogWarning("Slider Percent Text is null!");
                return;
            }
            
            int percent = Mathf.RoundToInt(slider.normalizedValue * 100f);
            textPercent.text = $"{percent}{suffix}";
        }
    }
}