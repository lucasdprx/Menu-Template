using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI_Element
{
    public class SliderPercent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textPercent;

        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();

            if (slider != null)
            {
                slider.onValueChanged.AddListener(OnValueChanged);
            }
        }

        private void Start()
        {
            if (slider != null)
            {
                OnValueChanged(slider.value);
            }
        }

        private void OnValueChanged(float value)
        {
            float minValue = slider.minValue;
            float maxValue = slider.maxValue;
            textPercent.text = $"{Mathf.RoundToInt((value - minValue) / (maxValue - minValue) * 100)}%";
        }
    }

}
