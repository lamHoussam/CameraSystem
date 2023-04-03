    using UnityEngine;

namespace CameraSystem
{

    public class Player : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        [SerializeField] private float RotationSmoothTime;

        private bool m_crouched;

        private CameraLogicGraph m_CameraLogic;
        private CameraController m_CameraController;

        private float m_targetRotation;
        private bool m_rightShoulder;

        private CharacterController m_CharacterController;

        private void Awake()
        {
            m_crouched = false;
            m_rightShoulder = false;


            m_CameraLogic = GetComponent<CameraLogicGraph>();
            m_CameraController = Camera.main.GetComponent<CameraController>();
            m_CharacterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            m_CameraController.TakePitchYawInput();

            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (Input.GetKeyDown(KeyCode.R))
                LockOn();

            if (direction != Vector3.zero)
            {
                float rotationVelocity = 0;
                m_targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                  m_CameraController.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, m_targetRotation, ref rotationVelocity,
                    RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

                Vector3 targetDirection = Quaternion.Euler(0.0f, m_targetRotation, 0.0f) * Vector3.forward;
                m_CharacterController.Move(m_speed * Time.deltaTime * targetDirection.normalized);
                //transform.position += m_speed * Time.deltaTime * targetDirection.normalized;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                m_crouched = !m_crouched;
                Debug.Log(m_crouched);
                m_CameraLogic.SetBool("crouch", m_crouched);
            }

            if (Input.GetKeyDown(KeyCode.Q))
                SwitchShoulder();
        }
        public void SwitchShoulder()
        {
            Debug.Log("Switch");
            m_rightShoulder = !m_rightShoulder;
            m_CameraLogic.SetBool("rightShoulder", m_rightShoulder);
        }

        public void LockOn()
        {
            Debug.Log("Lock");
            if (m_CameraController.IsLockedOnTarget)
            {
                m_CameraController.DeactivateLockOn();
                return;
            }

            Vector2 centerPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = m_CameraController.GetComponent<Camera>().ScreenPointToRay(centerPoint);

            Debug.DrawRay(ray.origin, ray.direction * 25, Color.blue, 30);

            if (Physics.Raycast(ray, out RaycastHit hit, 25))
                m_CameraController.ActivateLockOn(hit.transform);
        }
    }
}