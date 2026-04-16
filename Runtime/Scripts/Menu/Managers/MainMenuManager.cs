using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PTRKGames.MenuTemplate.Runtime.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button firstButton;
        
        private InputDevice currentDevice;
        private Action<InputDevice> onDeviceChange;
        private Button currentSelectable;
        
        private void Awake()
        {
            InputSystem.onActionChange += OnActionChange;
            onDeviceChange += OnDeviceChange;
        }

        private void Start()
        {
            currentSelectable  = firstButton;
        }

        private void OnActionChange(object action, InputActionChange change)
        {
            if (change != InputActionChange.ActionPerformed)
                return;
            
            InputDevice device = ((InputAction)action)?.activeControl.device;
            if (device == null)
                return;

            if (currentDevice != device)
            {
                currentDevice = device;
                onDeviceChange?.Invoke(device);
            }
        }

        private void OnDeviceChange(InputDevice device)
        {
            if (device is Gamepad)
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

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
        public void QuitGame()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

        public void SelectElement(Selectable selectable)
        {
            SetCurrentSelectable(selectable);
            if (currentDevice is Gamepad)
            {
                selectable.Select();
            }
        }

        public void SetCurrentSelectable(Selectable selectable)
        {
            if (selectable is Button btn)
                currentSelectable = btn;
        }
    }
}

