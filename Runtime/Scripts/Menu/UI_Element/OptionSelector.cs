using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{

    public class OptionSelector : MonoBehaviour
    {
        [SerializeField] protected Button buttonPrevious;
        [SerializeField] protected TextMeshProUGUI textValue;
        [SerializeField] protected Button buttonNext;
        [SerializeField] protected List<string> options = new() { "Option A", "Option B", "Option C" };


        /// <summary>
        /// An event triggered when the selected option changes. Returns the selected index.
        /// </summary>
        public UnityEvent<int> onValueChanged;

        protected int currentIndex = 0;

        protected void Awake()
        {
            Assert.IsNotNull(textValue, "Text Value reference is missing on " + gameObject.name);
            Assert.IsNotNull(buttonNext, "Button Next reference is missing on " + gameObject.name);
            Assert.IsNotNull(buttonPrevious, "Button Previous reference is missing on " + gameObject.name);

            buttonNext.onClick.AddListener(NextOption);
            buttonPrevious.onClick.AddListener(PreviousOption);
            UpdateUI();

            OnInit();
        }

        protected void OnDestroy()
        {
            if (buttonNext != null)
                buttonNext.onClick.RemoveListener(NextOption);
            if (buttonPrevious != null)
                buttonPrevious.onClick.RemoveListener(PreviousOption);

            OnCleanUp();
        }

        /// <summary>
        /// A method called at the end of Awake. Override this method to initialize child data.
        /// </summary>
        protected virtual void OnInit() { }

        /// <summary>
        /// Method called during OnDestroy. Override this method to clean up child events.
        /// </summary>
        protected virtual void OnCleanUp() { }

        public virtual void ClearOptions()
        {
            options.Clear();
            currentIndex = 0;
            UpdateUI();
        }

        public virtual void SetValue(int index)
        {
            if (index < 0 || index >= options.Count)
            {
                Debug.LogError($"Index invalid for OptionSelector: {index}");
                return;
            }
            currentIndex = index;
            UpdateUI();
            onValueChanged.Invoke(currentIndex);
        }

        public virtual void RemoveOption(string option)
        {
            if (options.Remove(option))
            {
                ClampIndex();
                UpdateUI();
            }
            else
            {
                Debug.LogWarning($"Option '{option}' not found in OptionSelector.");
            }
        }

        public virtual void RemoveOptionAt(int index)
        {
            if (index < 0 || index >= options.Count)
            {
                Debug.LogError($"Index invalid for OptionSelector: {index}");
                return;
            }
            options.RemoveAt(index);
            ClampIndex();
            UpdateUI();
        }

        public virtual void AddOption(string option)
        {
            options.Add(option);
            UpdateUI();
        }

        public virtual void AddOptions(List<string> newOptions)
        {
            options.AddRange(newOptions);
            UpdateUI();
        }

        protected virtual void ClampIndex()
        {
            currentIndex = Mathf.Clamp(currentIndex, 0, Mathf.Max(0, options.Count - 1));
        }

        protected virtual void UpdateUI()
        {
            if (options.Count <= 0)
            {
                textValue.text = "";
                return;
            }

            textValue.text = options[currentIndex];
        }

        public virtual void NextOption()
        {
            if (options.Count <= 0) return;

            currentIndex = (currentIndex + 1) % options.Count;
            UpdateUI();
            onValueChanged.Invoke(currentIndex);
        }

        public virtual void PreviousOption()
        {
            if (options.Count <= 0) return;

            currentIndex = (currentIndex - 1 + options.Count) % options.Count;
            UpdateUI();
            onValueChanged.Invoke(currentIndex);
        }
    }
}