using UnityEditor;
using UnityEngine;

namespace Menu
{
    public static class MenuTemplateSetup
    {
        private const string PrefabPath = "Packages/Menu-Template/Runtime/Prefabs/Menu/MenuTemplate.prefab";

        [MenuItem("Menu Template/Create Menu Template", false, 10)]
        public static void CreateMenuInScene()
        {
            GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);

            if (prefabAsset == null)
            {
                prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Packages/Menu-Template/Runtime/Prefabs/Menu/MenuTemplate.prefab");
                Debug.Log("[Menu Template] Chemin alternatif tenté : Assets/Packages/Menu-Template/Runtime/Prefabs/Menu/MenuTemplate.prefab : " + (prefabAsset != null ? "Reussi" : "Echec"));
            }

            if (prefabAsset == null)
            {
                Debug.LogError($"[Menu Template] Impossible de charger le Prefab. Fichier introuvable à : {PrefabPath}");
                return;
            }

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
            instance.transform.SetParent(null);
            Undo.RegisterCreatedObjectUndo(instance, "Create Menu Template");
            Selection.activeObject = instance;
        }
    }
}