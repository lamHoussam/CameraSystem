using UnityEngine;
using UnityEditor;

namespace NodeView
{
    [CustomEditor(typeof(CameraShake))]
    public class CameraShakeEditor : Editor
    {
        private SerializedProperty spShakeIntensity;
        private SerializedProperty spShakeTime;
        private SerializedProperty spShakeSmoothness;

        private CameraShake m_Target;

        private void OnEnable()
        {
            m_Target = target as CameraShake;

            spShakeIntensity = serializedObject.FindProperty("m_shakeIntensity");
            spShakeTime = serializedObject.FindProperty("m_shakeTime");
            spShakeSmoothness = serializedObject.FindProperty("m_shakeSmoothness");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.PropertyField(spShakeIntensity);
            EditorGUILayout.PropertyField(spShakeTime);
            EditorGUILayout.PropertyField(spShakeSmoothness);

            if(GUILayout.Button("Activate Shake")) 
                m_Target.ActivateShake();

            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }
    }
}
