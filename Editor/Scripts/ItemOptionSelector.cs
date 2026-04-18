using UnityEditor;
using UnityEngine;

namespace PTRKGames.MenuTemplate.Editor
{
    public static class ItemOptionSelector
    {
        private const string SelectorPrefabGUID = "a6bc1126721c0df4ab705e9bdddf56b5";

        [MenuItem("GameObject/UI (Canvas)/Option Selector", false, 10)]
        public static void CreateOptionSelector(MenuCommand menuCommand)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(SelectorPrefabGUID);
            GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefabAsset != null)
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
                PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                instance.transform.SetParent(Selection.activeTransform, false);
                instance.transform.localPosition = Vector3.zero;
                Undo.RegisterCreatedObjectUndo(instance, "Create Option Selector");
                Selection.activeObject = instance;
            }
            else
            {
                Debug.LogError("Option Selector prefab not found at path: " + prefabPath);
            }
        }
    }
}