using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace PTRKGames.MenuTemplate.Editor
{
    public static class MenuTemplateSetup
    {
        private const string MenuPrefabGUID = "1b4d50fac1d0ef540a2dc0139b18b474";
        private const string TargetFolder = "Assets/Resources/Localization";

        [MenuItem("Menu Template/Create Menu Template", false, 10)]
        public static void CreateMenuInScene()
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(MenuPrefabGUID);
            GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefabAsset == null)
            {
                Debug.LogError($"[Menu Template] Impossible de trouver le Prefab avec le GUID : {MenuPrefabGUID}. Vérifiez votre fichier .meta.");
                return;
            }

            InstallDefaultTranslations();
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabAsset);
            instance.transform.SetParent(null);

            Undo.RegisterCreatedObjectUndo(instance, "Create Menu Template");
            Selection.activeObject = instance;

            if (Object.FindAnyObjectByType<EventSystem>() == null)
            {
                GameObject eventSystemGO = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
                Undo.RegisterCreatedObjectUndo(eventSystemGO, "Create Event System");
            }
        }

        private static void InstallDefaultTranslations()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");

            if (!AssetDatabase.IsValidFolder(TargetFolder))
                AssetDatabase.CreateFolder("Assets/Resources", "Localization");

            string[] folderGuids = AssetDatabase.FindAssets("DefaultTranslations t:Folder");
            if (folderGuids.Length == 0) 
                return;

            string sourceFolderPath = AssetDatabase.GUIDToAssetPath(folderGuids[0]);
            string[] jsonGuids = AssetDatabase.FindAssets("t:TextAsset", new[] { sourceFolderPath });

            foreach (string guid in jsonGuids)
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