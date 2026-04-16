using PTRKGames.MenuTemplate.Runtime.FeedBack;
using UnityEditor;


namespace PTRKGames.MenuTemplate.Editor.FeedBack
{
    #if DOTWEEN
    // Custom inspector pour le script UIButtonFeedback
    // Ce script a été généré par une IA
    [CustomEditor(typeof(UIButtonFeedback))]
    public class UIButtonFeedbackEditor : UnityEditor.Editor
    {
        // État d'ouverture des menus déroulants
        private bool showTransform;
        private bool showImage;
        private bool showText;
        private bool showPunch;

        // Transform
        private SerializedProperty enableScaleAnimation, scaleMultiplier, scaleDuration, scaleEase;
        // Image
        private SerializedProperty enableImageAnimation, highlightedImageColor, imageColorDuration, imageColorEase;
        // Text
        private SerializedProperty enableTextAnimation, highlightedTextColor, textColorEase, textColorDuration, fontSizeMultiplier, fontSizeEase;
        // Punch
        private SerializedProperty enablePunchAnimation, punchStrength, punchDuration, punchVibrato, punchElasticity, punchEase;

        private void OnEnable()
        {
            // Initialisation des propriétés Transform
            enableScaleAnimation = serializedObject.FindProperty("enableScaleAnimation");
            scaleMultiplier = serializedObject.FindProperty("scaleMultiplier");
            scaleDuration = serializedObject.FindProperty("scaleDuration");
            scaleEase = serializedObject.FindProperty("scaleEase");

            // Initialisation des propriétés Image
            enableImageAnimation = serializedObject.FindProperty("enableImageAnimation");
            highlightedImageColor = serializedObject.FindProperty("highlightedImageColor");
            imageColorDuration = serializedObject.FindProperty("imageColorDuration");
            imageColorEase = serializedObject.FindProperty("imageColorEase");

            // Initialisation des propriétés Text
            enableTextAnimation = serializedObject.FindProperty("enableTextAnimation");
            highlightedTextColor = serializedObject.FindProperty("highlightedTextColor");
            textColorEase = serializedObject.FindProperty("textColorEase");
            textColorDuration = serializedObject.FindProperty("textColorDuration");
            fontSizeMultiplier = serializedObject.FindProperty("fontSizeMultiplier");
            fontSizeEase = serializedObject.FindProperty("fontSizeEase");

            // Initialisation des propriétés Punch
            enablePunchAnimation = serializedObject.FindProperty("enablePunchAnimation");
            punchStrength = serializedObject.FindProperty("punchStrength");
            punchDuration = serializedObject.FindProperty("punchDuration");
            punchVibrato = serializedObject.FindProperty("punchVibrato");
            punchElasticity = serializedObject.FindProperty("punchElasticity");
            punchEase = serializedObject.FindProperty("punchEase");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            showTransform = EditorGUILayout.BeginFoldoutHeaderGroup(showTransform, "Transform Settings");
            if (showTransform)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(enableScaleAnimation);
                if (enableScaleAnimation.boolValue)
                {
                    EditorGUILayout.PropertyField(scaleMultiplier);
                    EditorGUILayout.PropertyField(scaleDuration);
                    EditorGUILayout.PropertyField(scaleEase);
                }

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showImage = EditorGUILayout.BeginFoldoutHeaderGroup(showImage, "Image Settings");
            if (showImage)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(enableImageAnimation);
                if (enableImageAnimation.boolValue)
                {
                    EditorGUILayout.PropertyField(highlightedImageColor);
                    EditorGUILayout.PropertyField(imageColorDuration);
                    EditorGUILayout.PropertyField(imageColorEase);
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showText = EditorGUILayout.BeginFoldoutHeaderGroup(showText, "Text Settings");
            if (showText)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(enableTextAnimation);
                if (enableTextAnimation.boolValue)
                {
                    EditorGUILayout.PropertyField(highlightedTextColor);
                    EditorGUILayout.PropertyField(textColorEase);
                    EditorGUILayout.PropertyField(textColorDuration);
                    EditorGUILayout.PropertyField(fontSizeMultiplier);
                    EditorGUILayout.PropertyField(fontSizeEase);
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            showPunch = EditorGUILayout.BeginFoldoutHeaderGroup(showPunch, "Punch Settings");
            if (showPunch)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(enablePunchAnimation);
                if (enablePunchAnimation.boolValue)
                {
                    EditorGUILayout.PropertyField(punchStrength);
                    EditorGUILayout.PropertyField(punchDuration);
                    EditorGUILayout.PropertyField(punchVibrato);
                    EditorGUILayout.PropertyField(punchElasticity);
                    EditorGUILayout.PropertyField(punchEase);
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
    #else
    [CustomEditor(typeof(UIButtonFeedback))]
    public class UIButtonFeedbackEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("L'animation des boutons nécessite le package DOTween.\n\nInstallez DOTween pour activer le feedback visuel'.", MessageType.Warning);
            DrawDefaultInspector();
        }
    }
    #endif
}
