#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Menu.Settings.Localization
{
    [CustomEditor(typeof(TextKey))]
    [CanEditMultipleObjects]
    public class TextKeyEditor : Editor
    {
        private const string KeyPropertyName = "key";

        private SerializedProperty keyProperty;
        private readonly Dictionary<string, string> translationsByLanguage = new();
        private readonly Dictionary<string, string> languageFileByCode = new();

        private string applyMessage = string.Empty;
        private MessageType applyMessageType = MessageType.None;

        private void OnEnable()
        {
            keyProperty = serializedObject.FindProperty(KeyPropertyName);
            ReloadTranslations();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawKeyField();
            DrawValidation();
            DrawTranslationsEditor();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawKeyField()
        {
            EditorGUILayout.LabelField("Localization", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(keyProperty);
            bool keyChanged = EditorGUI.EndChangeCheck();

            if (keyChanged)
            {
                serializedObject.ApplyModifiedProperties();
                ReloadTranslations();
                serializedObject.Update();
            }
        }

        private void DrawValidation()
        {
            if (string.IsNullOrWhiteSpace(keyProperty.stringValue))
            {
                EditorGUILayout.HelpBox("La key de localization est vide.", MessageType.Warning);
            }

            if (Application.isPlaying && !string.IsNullOrWhiteSpace(keyProperty.stringValue))
            {
                EditorGUILayout.Space(4);
                EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
                EditorGUILayout.HelpBox(Localization.Read(keyProperty.stringValue), MessageType.None);
            }
        }

        private void DrawTranslationsEditor()
        {
            EditorGUILayout.Space(6);
            EditorGUILayout.LabelField("Translations", EditorStyles.boldLabel);

            if (languageFileByCode.Count == 0)
            {
                EditorGUILayout.HelpBox("Aucun fichier de langue trouve dans Packages/Menu-Template/Runtime/Resources/Localization.", MessageType.Warning);
                return;
            }

            foreach (string languageCode in languageFileByCode.Keys.OrderBy(code => code, StringComparer.Ordinal))
            {
                string currentValue = translationsByLanguage.GetValueOrDefault(languageCode, string.Empty);
                translationsByLanguage[languageCode] = EditorGUILayout.TextField(languageCode.ToUpperInvariant(), currentValue);
            }

            EditorGUILayout.Space(4);
            using (new EditorGUI.DisabledScope(string.IsNullOrWhiteSpace(keyProperty.stringValue)))
            {
                if (GUILayout.Button("Apply"))
                {
                    ApplyTranslationsToJson();
                }
            }

            if (!string.IsNullOrEmpty(applyMessage))
            {
                EditorGUILayout.HelpBox(applyMessage, applyMessageType);
            }
        }

        private void ReloadTranslations()
        {
            languageFileByCode.Clear();
            translationsByLanguage.Clear();

            string key = keyProperty?.stringValue ?? string.Empty;
            applyMessage = string.Empty;
            applyMessageType = MessageType.None;

            foreach (string filePath in GetLanguageJsonPaths())
            {
                string code = Path.GetFileNameWithoutExtension(filePath);
                languageFileByCode[code] = filePath;

                Dictionary<string, string> fileData = ReadJsonDictionary(filePath);
                string value = string.Empty;
                if (!string.IsNullOrWhiteSpace(key) && fileData.TryGetValue(key, out string foundValue))
                {
                    value = foundValue;
                }

                translationsByLanguage[code] = value;
            }
        }

        private void ApplyTranslationsToJson()
        {
            string key = keyProperty.stringValue;
            if (string.IsNullOrWhiteSpace(key))
            {
                applyMessage = "Impossible d'appliquer: la key est vide.";
                applyMessageType = MessageType.Error;
                return;
            }

            int updatedCount = 0;

            try
            {
                foreach (KeyValuePair<string, string> languageEntry in languageFileByCode)
                {
                    string code = languageEntry.Key;
                    string filePath = languageEntry.Value;

                    Dictionary<string, string> fileData = ReadJsonDictionary(filePath);
                    fileData[key] = translationsByLanguage.GetValueOrDefault(code, string.Empty);

                    string json = JsonConvert.SerializeObject(fileData, Formatting.Indented);
                    File.WriteAllText(filePath, json);

                    string assetPath = ToAssetPath(filePath);
                    if (!string.IsNullOrEmpty(assetPath))
                    {
                        AssetDatabase.ImportAsset(assetPath);
                    }

                    updatedCount++;
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                applyMessage = $"Apply termine: {updatedCount} fichier(s) mis a jour.";
                applyMessageType = MessageType.Info;
            }
            catch (Exception exception)
            {
                applyMessage = $"Erreur pendant l'ecriture des JSON: {exception.Message}";
                applyMessageType = MessageType.Error;
            }
        }

        private static IEnumerable<string> GetLanguageJsonPaths()
        {
            string directory = Path.Combine(Application.dataPath, "Resources", "Localization");
            return !Directory.Exists(directory) ? Array.Empty<string>() : Directory.GetFiles(directory, "*.json", SearchOption.TopDirectoryOnly);
        }

        private static Dictionary<string, string> ReadJsonDictionary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, string>();
            }

            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new Dictionary<string, string>();
            }

            Dictionary<string, string> parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return parsed ?? new Dictionary<string, string>();
        }

        private static string ToAssetPath(string absoluteFilePath)
        {
            string normalizedDataPath = Application.dataPath.Replace("\\", "/");
            string normalizedFilePath = absoluteFilePath.Replace("\\", "/");

            if (!normalizedFilePath.StartsWith(normalizedDataPath, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return "Assets" + normalizedFilePath[normalizedDataPath.Length..];
        }
    }
}
#endif


