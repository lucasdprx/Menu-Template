using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{
    /// <summary>
    /// A custom UI component that cycles through a list of string options using Next and Previous buttons.
    /// Acts as a cleaner alternative to standard Dropdowns for settings menus.
    /// </summary>
    public class OptionSelector : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("Button used to cycle to the previous option.")]
        [SerializeField] protected Button buttonPrevious;
        
        [Tooltip("Text component displaying the currently selected option.")]
        [SerializeField] protected TextMeshProUGUI textValue;
        
        [Tooltip("Button used to cycle to the next option.")]
        [SerializeField] protected Button buttonNext;
        
        [Header("Data")]
        [Tooltip("The list of text options to display.")]
        [SerializeField] protected List<string> options = new() { "Option A", "Option B", "Option C" };

        /// <summary>
        /// An event triggered when the selected option changes via user interaction. Returns the selected index.
        /// </summary>
        public UnityEvent<int> onValueChanged;

        protected int currentIndex;

        protected virtual void Awake()
        {
            if (textValue == null || buttonNext == null || buttonPrevious == null)
            {
                Debug.LogWarning($"OptionSelector on '{gameObject.name}' is missing UI references");
                return;
            }

            buttonNext.onClick.AddListener(NextOption);
            buttonPrevious.onClick.AddListener(PreviousOption);
            
            UpdateUI();
            OnInit();
        }

        protected virtual void OnDestroy()
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

        /// <summary>
        /// Clears all available options and resets the index to 0.
        /// </summary>
        public virtual void ClearOptions()
        {
            options.Clear();
            currentIndex = 0;
            UpdateUI();
        }

        /// <summary>
        /// Sets the current index and triggers the onValueChanged event.
        /// </summary>
        /// <param name="index">The index to select.</param>
        public virtual void SetValue(int index)
        {
            SetValueWithoutNotify(index);
            onValueChanged?.Invoke(currentIndex);
        }

        /// <summary>
        /// Sets the current index and updates the UI WITHOUT triggering the onValueChanged event.
        /// Use this when initializing UI from saved data to avoid feedback loops.
        /// </summary>
        /// <param name="index">The index to select.</param>
        public virtual void SetValueWithoutNotify(int index)
        {
            if (index < 0 || index >= options.Count)
            {
                Debug.LogWarning($"Invalid index '{index}' for OptionSelector on {gameObject.name}.");
                return;
            }
            currentIndex = index;
            UpdateUI();
        }

        /// <summary>
        /// Removes a specific string from the options list.
        /// </summary>
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

        /// <summary>
        /// Removes the option at the specified index.
        /// </summary>
        public virtual void RemoveOptionAt(int index)
        {
            if (index < 0 || index >= options.Count) return;
            
            options.RemoveAt(index);
            ClampIndex();
            UpdateUI();
        }

        /// <summary>
        /// Adds a single option to the end of the list.
        /// </summary>
        public virtual void AddOption(string option)
        {
            options.Add(option);
            UpdateUI();
        }

        /// <summary>
        /// Adds a list of options to the end of the current list.
        /// </summary>
        public virtual void AddOptions(List<string> newOptions)
        {
            if (newOptions == null) return;
            options.AddRange(newOptions);
            UpdateUI();
        }

        /// <summary>
        /// Ensures the current index remains within the valid bounds of the list.
        /// </summary>
        protected virtual void ClampIndex()
        {
            currentIndex = Mathf.Clamp(currentIndex, 0, Mathf.Max(0, options.Count - 1));
        }

        /// <summary>
        /// Refreshes the text display based on the current index.
        /// </summary>
        protected virtual void UpdateUI()
        {
            if (options.Count <= 0 || textValue == null)
            {
                if (textValue != null) textValue.text = "";
                return;
            }

            textValue.text = options[currentIndex];
        }

        /// <summary>
        /// Selects the next option in the list. Wraps around to the beginning if at the end.
        /// </summary>
        public virtual void NextOption()
        {
            if (options.Count <= 0) return;

            currentIndex = (currentIndex + 1) % options.Count;
            UpdateUI();
            onValueChanged?.Invoke(currentIndex);
        }

        /// <summary>
        /// Selects the previous option in the list. Wraps around to the end if at the beginning.
        /// </summary>
        public virtual void PreviousOption()
        {
            if (options.Count <= 0) return;

            currentIndex = (currentIndex - 1 + options.Count) % options.Count;
            UpdateUI();
            onValueChanged?.Invoke(currentIndex);
        }
    }
}