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
    /// <summary>
    /// Core manager for the Main Menu. Handles scene transitions, quitting, 
    /// and smart UI focus switching between Mouse, Keyboard, and Gamepad.
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        [Tooltip("The default button that should be highlighted when the menu opens.")]
        [SerializeField] protected Button firstButton;
        
        protected InputDevice currentDevice;
        protected Button currentSelectable;

        /// <summary>
        /// Triggered whenever the player switches input devices (e.g., from Mouse to Gamepad).
        /// </summary>
        public static event Action<InputDevice> OnDeviceChanged;
        
        protected virtual void Awake()
        {
            InputSystem.onActionChange += OnActionChange;
            currentSelectable = firstButton;
        }

        protected virtual void OnDestroy()
        {
            InputSystem.onActionChange -= OnActionChange;
        }

        /// <summary>
        /// Intercepts all input actions to detect the current active device.
        /// </summary>
        protected virtual void OnActionChange(object action, InputActionChange change)
        {
            if (change != InputActionChange.ActionPerformed) return;
            
            InputDevice device = ((InputAction)action)?.activeControl.device;
            if (device == null) return;

            if (currentDevice != device)
            {
                currentDevice = device;
                HandleDeviceChange(device);
            }
        }

        /// <summary>
        /// Updates the UI focus state based on the newly active device.
        /// </summary>
        /// <param name="device">The device currently being used.</param>
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
                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
        }

        /// <summary>
        /// Loads a new scene by its Build Index.
        /// </summary>
        public virtual void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
        
        /// <summary>
        /// Quits the application natively, or stops play mode if running in the Unity Editor.
        /// </summary>
        public virtual void QuitGame()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        /// <summary>
        /// Updates the current selection memory. Automatically called when hovering buttons.
        /// </summary>
        public virtual void SelectElement(Selectable selectable)
        {
            SetCurrentSelectable(selectable);
            
            if (currentDevice is Gamepad or Keyboard)
            {
                selectable.Select();
            }
        }

        /// <summary>
        /// Remembers the last selected button so the focus can be restored when switching back from the mouse.
        /// </summary>
        public virtual void SetCurrentSelectable(Selectable selectable)
        {
            if (selectable is Button btn)
            {
                currentSelectable = btn;
            }
        }
    }
}