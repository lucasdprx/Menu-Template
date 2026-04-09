using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu.Settings.Rebind
{
    public class RebindSaveLoad : MonoBehaviour
    {
        public InputActionAsset actions;

        public void OnEnable()
        {
            string rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds))
            {
                actions.LoadBindingOverridesFromJson(rebinds);
            }
        }

        public void OnDisable()
        {
            string rebinds = actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("rebinds", rebinds);
            PlayerPrefs.Save();
        }
    }
}
