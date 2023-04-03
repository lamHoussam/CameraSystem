using TMPro;
using Unity.VisualScripting;
using UnityEngine;


namespace CameraSystem
{
    public class CameraSequence : MonoBehaviour
    {
        [SerializeField] private CameraController m_Camera;
        [SerializeField] private Transform[] m_CameraTargets;
        [SerializeField] private float m_transitionTime;
        [SerializeField] private bool m_loop;

        [SerializeField] private AnimationCurve m_SequenceCurve;

        private bool m_sequenceStarted;
        private int m_currentCameraTargetIndex;

        private Vector3 m_direction;

        private Vector3 m_previousPosition;

        private float t;

        private void Start()
        {
            //StartSequence();
        }

        /// <summary>
        /// Start sequence
        /// </summary>
        public void StartSequence()
        {
            m_currentCameraTargetIndex = 0;
            m_sequenceStarted = true;

            m_Camera.transform.SetPositionAndRotation(m_CameraTargets[0].position, m_CameraTargets[0].rotation);

            m_Camera.Type = CameraController.CameraType.NonControllable;

            m_currentCameraTargetIndex++;
            m_direction = (m_CameraTargets[m_currentCameraTargetIndex].position - m_Camera.transform.position).normalized;
            t = 0;
        }


        /// <summary>
        /// Stop sequence
        /// </summary>
        public void StopSequence()
        {
            m_Camera.Type = CameraController.CameraType.Controllable;
            m_sequenceStarted = false;
        }


        public void Update()
        {
            if (!m_sequenceStarted)
                return;

            Blend();
        }

        public void StartSequence(CameraSettings settings)
        {
            m_sequenceStarted = true;

            m_currentCameraTargetIndex = 0;
            m_sequenceStarted = true;

            m_Camera.transform.SetPositionAndRotation(m_CameraTargets[0].position, m_CameraTargets[0].rotation);
            m_previousPosition = m_Camera.transform.position;

            m_Camera.Type = CameraController.CameraType.NonControllable;

            m_currentCameraTargetIndex++;
            m_direction = (m_CameraTargets[m_currentCameraTargetIndex].position - m_Camera.transform.position);

            t = 0;
        }


        public void Blend()
        {
            t += Time.deltaTime;

            float val = m_SequenceCurve.Evaluate(t / m_transitionTime);

            Vector3 pos = val * m_direction + m_previousPosition;

            Quaternion targetRotation = m_CameraTargets[m_currentCameraTargetIndex].rotation;
            //Vector3 pos = m_Camera.transform.position + m_SequenceCurve.Evaluate(t) * Time.deltaTime * direction;

            m_Camera.transform.SetPositionAndRotation(
                pos,
                targetRotation
            );

            //m_offset = val * m_blendOffsetVariation + m_previousOffset;

            Debug.LogWarning("Value : " + val);

            if (t >= m_transitionTime)
                MoveToNextPoint();

        }

        private void MoveToNextPoint()
        {
            t = 0;
            m_currentCameraTargetIndex++;
            if (m_currentCameraTargetIndex >= m_CameraTargets.Length)
                StopSequence();
            else
                m_direction = (m_CameraTargets[m_currentCameraTargetIndex].position - m_Camera.transform.position);

        }
    }
}