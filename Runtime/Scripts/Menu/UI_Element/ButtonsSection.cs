using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Menu.UI_Element
{
    public class ButtonsSection : MonoBehaviour
    {
        [SerializeField] private List<SectionMapping> sections = new();
        [SerializeField] private Button firstButton;

        [Header("Image")]
        [SerializeField] private bool enableColor = true;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color deselectedColor;

        private void Awake()
        {
            foreach (Button btn in sections.Select(value => value.button))
            {
                btn.onClick.AddListener(() => OnButtonClicked(btn));
            }
            firstButton.onClick.Invoke();
        }

        private void OnButtonClicked(Button button)
        {
            foreach (SectionMapping value in sections)
            {
                if (enableColor)
                    value.button.GetComponent<Image>().color = deselectedColor;
                value.gameObject.SetActive(value.button == button);
            }

            if (enableColor)
                button.GetComponent<Image>().color = selectedColor;
        }

        public void OnButtonSelected(Button button)
        {
            OnButtonClicked(button);
        }
    }

    [System.Serializable]
    public struct SectionMapping
    {
        public Button button;
        public GameObject gameObject;
    }
}
