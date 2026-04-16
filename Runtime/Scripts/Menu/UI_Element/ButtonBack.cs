using UnityEngine;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{
    [RequireComponent(typeof(Button))]
    public class ButtonBack : MonoBehaviour
    {
        private Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            //InputHandler.onEscape.canceled += OnBack;
        }

        private void OnDisable()
        {
            //InputHandler.onEscape.canceled -= OnBack;
        }

        private void OnBack()
        {
            if (button.interactable)
                button.onClick.Invoke();
        }
    }
}
