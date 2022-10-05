using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_distance;

    private float m_yaw, m_pitch;
    private float m_angleAround;
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
        float hor = m_distance * Mathf.Cos(m_pitch * Mathf.Deg2Rad);
        transform.position = m_target.position + new Vector3(hor * Mathf.Sin(m_yaw * Mathf.Deg2Rad), m_distance * Mathf.Sin(m_pitch * Mathf.Deg2Rad), hor * Mathf.Cos(m_yaw * Mathf.Deg2Rad));
        transform.rotation = Quaternion.Euler(Vector3.up * (m_yaw) + Vector3.left * m_pitch);
        
        //transform.LookAt(m_target);
        m_prevMousePos = Input.mousePosition;
    }
}
