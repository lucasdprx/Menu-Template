using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{
    [System.Serializable]
    public struct SectionMapping
    {
        public Button button;
        public GameObject sectionObject;
        
        [HideInInspector] public Image cachedImage;
    }

    public class ButtonsSection : MonoBehaviour
    {
        [SerializeField] protected List<SectionMapping> sections = new();
        [SerializeField] protected Button firstButton;

        [Header("Image")]
        [SerializeField] protected bool enableColor = true;
        [SerializeField] protected Color selectedColor = Color.white;
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
                    btn.onClick.AddListener(() => OnButtonClicked(btn));
                    
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
            foreach (var mapping in sections)
            {
                if (mapping.button != null)
                {
                    mapping.button.onClick.RemoveAllListeners();
                }
            }
        }

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

        public virtual void OnButtonSelected(Button button)
        {
            OnButtonClicked(button);
        }
    }
}