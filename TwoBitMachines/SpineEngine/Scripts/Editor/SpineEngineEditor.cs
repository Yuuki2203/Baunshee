using UnityEngine;
using UnityEditor;

namespace TwoBitMachines.SpineEngine.Editor
{
    [CustomEditor(typeof(SpineEngine))]
    public class SpineEngineEditor : UnityEditor.Editor
    {
        private SpineEngine spineEngine;
        private bool showAnimations = true;
        private bool showTree = true;
        private bool showSettings = true;

        private void OnEnable()
        {
            spineEngine = (SpineEngine)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Spine Engine", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 基本設定
            showSettings = EditorGUILayout.Foldout(showSettings, "Settings", true);
            if (showSettings)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("pause"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("setToFirst"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableFlip"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            // アニメーション設定
            showAnimations = EditorGUILayout.Foldout(showAnimations, "Animations", true);
            if (showAnimations)
            {
                EditorGUI.indentLevel++;
                SerializedProperty animationsProperty = serializedObject.FindProperty("animations");
                EditorGUILayout.PropertyField(animationsProperty, true);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            // ツリー設定
            showTree = EditorGUILayout.Foldout(showTree, "Animation Tree", true);
            if (showTree)
            {
                EditorGUI.indentLevel++;
                SerializedProperty treeProperty = serializedObject.FindProperty("tree");
                EditorGUILayout.PropertyField(treeProperty, true);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            // スキンスワップ設定
            SerializedProperty skinSwapProperty = serializedObject.FindProperty("skinSwap");
            EditorGUILayout.PropertyField(skinSwapProperty);

            EditorGUILayout.Space();

            // ボタン
            if (GUILayout.Button("Refresh Animations"))
            {
                RefreshAnimations();
            }

            if (GUILayout.Button("Clear All"))
            {
                ClearAll();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void RefreshAnimations()
        {
            // アニメーションの更新処理
            Debug.Log("Spine Engine: Refreshing animations...");
        }

        private void ClearAll()
        {
            // 全データのクリア処理
            if (EditorUtility.DisplayDialog("Clear All", "Are you sure you want to clear all data?", "Yes", "No"))
            {
                spineEngine.animations.Clear();
                spineEngine.tree.states.Clear();
                EditorUtility.SetDirty(spineEngine);
                Debug.Log("Spine Engine: All data cleared.");
            }
        }
    }

    [CustomEditor(typeof(SpineAnimationPacket))]
    public class SpineAnimationPacketEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Spine Animation Packet", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 基本設定
            EditorGUILayout.PropertyField(serializedObject.FindProperty("name"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("animationName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));

            EditorGUILayout.Space();

            // ランダム設定
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isRandom"));
            if (serializedObject.FindProperty("isRandom").boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("randomAnimations"), true);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            // トランジション設定
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useTransition"));
            if (serializedObject.FindProperty("useTransition").boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionAnimation"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            // 同期設定
            EditorGUILayout.PropertyField(serializedObject.FindProperty("canSync"));
            if (serializedObject.FindProperty("canSync").boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("syncID"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            // フリップ設定
            EditorGUILayout.PropertyField(serializedObject.FindProperty("flipX"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("flipY"));

            EditorGUILayout.Space();

            // エクストラプロパティ
            EditorGUILayout.PropertyField(serializedObject.FindProperty("extraProperties"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
} 