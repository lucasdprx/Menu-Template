using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Menu
{
    public static class MenuTemplateSetup
    {
        private const string PrefabPath = "Packages/com.ptrkgames.menu-template/Runtime/Prefabs/Menu/MenuTemplate.prefab";
        
        private const string SourceFolder = "Packages/com.ptrkgames.menu-template/Runtime/DefaultTranslations";
        private const string SourceFolderAssets = "Assets/Packages/Menu-Template/Runtime/DefaultTranslations";
        private const string TargetFolder = "Assets/Resources/Localization";

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

            InstallDefaultTranslations();
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
            instance.transform.SetParent(null);
            Undo.RegisterCreatedObjectUndo(instance, "Create Menu Template");
            Selection.activeObject = instance;
            
            if (Object.FindFirstObjectByType<EventSystem>() == null)
            {
                GameObject eventSystemGO = new("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
                Undo.RegisterCreatedObjectUndo(eventSystemGO, "Create Event System");
            }
        }

        private static void InstallDefaultTranslations()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
            
            if (!AssetDatabase.IsValidFolder(TargetFolder))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Localization");
            }

            string[] guids = AssetDatabase.FindAssets("t:TextAsset", new[] { SourceFolder });
            if (guids.Length == 0)
            {
                guids = AssetDatabase.FindAssets("t:TextAsset", new[] { SourceFolderAssets });
            }

            foreach (string guid in guids)
            {
                string sourcePath = AssetDatabase.GUIDToAssetPath(guid);
                
                if (sourcePath.EndsWith(".json"))
                {
                    string fileName = Path.GetFileName(sourcePath);
                    string targetPath = $"{TargetFolder}/{fileName}";

                    if (AssetDatabase.LoadAssetAtPath<TextAsset>(targetPath) == null)
                    {
                        AssetDatabase.CopyAsset(sourcePath, targetPath);
                    }
                }
            }

            AssetDatabase.Refresh();
        }
    }
}