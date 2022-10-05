using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_distance;
    //[SerializeField] private Vector2 m_offset;

    [SerializeField] private float m_maxPitchValue = 70;
    [SerializeField] private float m_yawMaxValue = 50;


    private float m_yaw, m_pitch;

    private Vector3 m_prevMousePos;
    
    private void Start()
    {
        m_prevMousePos = Input.mousePosition;
    }

    private void LateUpdate()
    {
        transform.position = m_target.position + Vector3.forward * m_distance;
        Vector3 diff = (Input.mousePosition - m_prevMousePos) * .1f;

        m_pitch += diff.y;
        m_yaw += diff.x;

        m_pitch = ClampAngle(m_pitch, 0, m_maxPitchValue);
        m_yaw = ClampAngle(m_yaw, -m_yawMaxValue, m_yawMaxValue);

        float hor = -m_distance * Mathf.Cos(m_pitch * Mathf.Deg2Rad);

        Vector3 pos = m_target.position + new Vector3(
            hor * Mathf.Sin(m_yaw * Mathf.Deg2Rad), 
            m_distance * Mathf.Sin(m_pitch * Mathf.Deg2Rad), 
            hor * Mathf.Cos(m_yaw * Mathf.Deg2Rad)
        ) /*+ (Vector3)m_offset*/;
        Quaternion rot = Quaternion.Euler(Vector3.up * (m_yaw) + Vector3.right * m_pitch);

        transform.SetPositionAndRotation(pos, rot);
        
        //transform.LookAt(m_target);
        m_prevMousePos = Input.mousePosition;
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
