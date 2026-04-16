using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace PTRKGames.MenuTemplate.Runtime.UI_Element
{
    [RequireComponent(typeof(Button))]
    public class ButtonBack : MonoBehaviour
    {
        protected Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        protected virtual void OnEnable()
        {
            //InputHandler.onEscape.canceled += OnBack;
        }

        protected virtual void OnDisable()
        {
            //InputHandler.onEscape.canceled -= OnBack;
        }

        protected virtual void OnBack(InputAction.CallbackContext context)
        {
            if (button != null && button.interactable)
            {
                button.onClick.Invoke();
            }
        }
        
        public virtual void OnBack()
        {
            if (button != null && button.interactable)
            {
                button.onClick.Invoke();
            }
        }
    }
}