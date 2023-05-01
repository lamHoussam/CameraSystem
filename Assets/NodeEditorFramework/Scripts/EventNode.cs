using System.Buffers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


namespace NodeEditorFramework
{
    public class EventNode : Node
    {
        #region Members
        [SerializeField] private UnityAction m_Action;
        #endregion Members
	
        public void Execute() => m_Action?.Invoke();

#if UNITY_EDITOR
        public override bool Removable => base.Removable;
        public static EventNode Create(Rect rect)
        {
            EventNode node = CreateInstance<EventNode>();

            node.m_Rect = rect;
            node.m_InitialRect = rect;

            node.Init();

            return node;
        
        }

        public override void Draw(float scale = 1)
        {
            //UnityEngine.UI.Button
            base.Draw(scale);

            GUILayout.BeginArea(m_Rect, m_isEvaluationResult ? NodeEditor.Instance.m_EvaluatedNodeResult : NodeEditor.Instance.m_NodeBox);

            // Serialize Action Here

            //EditorGUILayout.ObjectField("Action", m_Action, typeof(UnityAction), true);

            GUILayout.EndArea();
        }
    
        public override void OnRemove()
        {
        
            base.OnRemove();
        }
    #endif
    }
}
