using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PTRKGames.MenuTemplate.Runtime.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] protected Button firstButton;
        
        protected InputDevice currentDevice;
        protected Button currentSelectable;

        public static event Action<InputDevice> OnDeviceChanged;
        
        protected virtual void Awake()
        {
            InputSystem.onActionChange += OnActionChange;
        }

        protected virtual void Start()
        {
            currentSelectable = firstButton;
        }

        protected virtual void OnDestroy()
        {
            InputSystem.onActionChange -= OnActionChange;
        }

        protected virtual void OnActionChange(object action, InputActionChange change)
        {
            if (change != InputActionChange.ActionPerformed)
                return;
            
            InputDevice device = ((InputAction)action)?.activeControl.device;
            if (device == null)
                return;

            if (currentDevice != device)
            {
                currentDevice = device;
                HandleDeviceChange(device);
            }
        }

        protected virtual void HandleDeviceChange(InputDevice device)
        {
            OnDeviceChanged?.Invoke(device);

            if (device is Gamepad or Keyboard)
            {
                if (currentSelectable != null)
                {
                    currentSelectable.Select();
                }
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        public virtual void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
        
        public virtual void QuitGame()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        public virtual void SelectElement(Selectable selectable)
        {
            SetCurrentSelectable(selectable);
            if (currentDevice is Gamepad or Keyboard)
            {
                selectable.Select();
            }
        }

        public virtual void SetCurrentSelectable(Selectable selectable)
        {
            if (selectable is Button btn)
                currentSelectable = btn;
        }
    }
}