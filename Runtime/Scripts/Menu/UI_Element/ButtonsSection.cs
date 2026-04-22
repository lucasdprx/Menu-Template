using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{
    /// <summary>
    /// Links a UI Button to a specific GameObject, acting as a tab system.
    /// </summary>
    [System.Serializable]
    public struct SectionMapping
    {
        [Tooltip("The button the user must click to open this section.")]
        public Button button;
        
        [Tooltip("The GameObject (panel) to activate when this button is clicked.")]
        public GameObject sectionObject;
        
        // Données mises en cache pour les performances et la gestion de la mémoire
        [HideInInspector] public Image cachedImage;
        public UnityAction clickAction; 
    }

    /// <summary>
    /// Manages a group of sections (tabs), ensuring only one is active at a time 
    /// and updating the visual states of the corresponding buttons.
    /// </summary>
    public class ButtonsSection : MonoBehaviour
    {
        [Tooltip("List of all buttons and their associated section panels.")]
        [SerializeField] protected List<SectionMapping> sections = new List<SectionMapping>();
        
        [Tooltip("The button that should be automatically selected when the menu first loads.")]
        [SerializeField] protected Button firstButton;

        [Header("Image")]
        [Tooltip("If true, the script will automatically change the color of the selected button's Image.")]
        [SerializeField] protected bool enableColor = true;
        
        [Tooltip("Color applied to the active section's button.")]
        [SerializeField] protected Color selectedColor = Color.white;
        
        [Tooltip("Color applied to all inactive section buttons.")]
        [SerializeField] protected Color deselectedColor = Color.gray;

        protected virtual void Awake()
        {
            for (int i = 0; i < sections.Count; i++)
            {
                SectionMapping mapping = sections[i];
                if (mapping.button != null)
                {
                    mapping.cachedImage = mapping.button.GetComponent<Image>();
                    
                    Button btn = mapping.button; 
                    
                    mapping.clickAction = () => OnButtonClicked(btn);
                    btn.onClick.AddListener(mapping.clickAction);
                    
                    sections[i] = mapping;
                }
            }
        }

        protected virtual void Start()
        {
            if (firstButton != null)
            {
                firstButton.onClick.Invoke();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (SectionMapping mapping in sections)
            {
                if (mapping.button != null && mapping.clickAction != null)
                {
                    mapping.button.onClick.RemoveListener(mapping.clickAction);
                }
            }
        }

        /// <summary>
        /// Activates the section linked to the clicked button and deactivates all others.
        /// Updates button colors if enableColor is true.
        /// </summary>
        /// <param name="clickedButton">The button that triggered the event.</param>
        protected virtual void OnButtonClicked(Button clickedButton)
        {
            foreach (SectionMapping mapping in sections)
            {
                if (mapping.button == null || mapping.sectionObject == null) continue;

                bool isSelected = (mapping.button == clickedButton);
                
                mapping.sectionObject.SetActive(isSelected);

                if (enableColor && mapping.cachedImage != null)
                {
                    mapping.cachedImage.color = isSelected ? selectedColor : deselectedColor;
                }
            }
        }

        /// <summary>
        /// Public method allowing other scripts or UI events to manually trigger a tab switch.
        /// </summary>
        /// <param name="button">The button corresponding to the section to open.</param>
        public virtual void OnButtonSelected(Button button)
        {
            OnButtonClicked(button);
        }
    }
}