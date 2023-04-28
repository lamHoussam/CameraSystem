using System.Net.Sockets;
using UnityEngine;


namespace NodeView
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float m_shakeIntensity;
        [SerializeField] private float m_shakeTime;
        [SerializeField] private float m_shakeSmoothness;

        private float m_timer;
        private bool m_isShaking;
        private Vector3 m_originalPosition;
        private Vector3 m_shakeAxisX, m_shakeAxisY;

        private void Update()
        {
            if (!m_isShaking)
                return;

            Shake();
        }

        public void ActivateShake(float intensity, float time)
        {
            m_shakeIntensity = intensity;
            m_shakeTime = time;

            m_isShaking = true;
            m_timer = 0;

            m_originalPosition = transform.position;

            m_shakeAxisX = transform.right;
            m_shakeAxisY = transform.up;
        }

        public void ActivateShake() => ActivateShake(m_shakeIntensity, m_shakeTime);

        private void Shake()
        {
            float dt = Time.deltaTime;
            m_timer += dt;
            if (m_timer >= m_shakeTime)
            {
                StopShake();
                return;
            }

            dt *= 20;
            float x = Mathf.PerlinNoise(dt, 0f) * 2f - 1f;
            float y = Mathf.PerlinNoise(0f, dt) * 2f - 1f;

            x *= m_shakeIntensity;
            y *= m_shakeIntensity;

            Vector3 targetPosition = m_originalPosition + x * m_shakeAxisX + y * m_shakeAxisY;
            transform.position = Vector3.Lerp(transform.position, targetPosition, m_shakeSmoothness);
        }

        private void StopShake()
        {
            m_isShaking = false;
        }
    }
}
