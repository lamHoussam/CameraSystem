    using UnityEngine;
using UnityEngine.Windows;

namespace NodeView
{

    public class Player : MonoBehaviour
    {
        private CameraController _cameraController;

        private CameraLogicGraph _logicGraph;
        private bool _rightShoulder;
        private bool _isAiming;

        private StarterAssets.StarterAssetsInputs _input;

        private void Awake()
        {
            _cameraController = Camera.main.GetComponent<CameraController>();
            _logicGraph = GetComponent<CameraLogicGraph>();
            _rightShoulder = true;

            _input = GetComponent<StarterAssets.StarterAssetsInputs>();
        }

        private void Update()
        {
            if (_input.switchShoulder)
            {
                SwitchShoulder();
                _input.switchShoulder = false;
            }

            if (_input.aim)
            {
                AimLogic();
                _input.aim = false;
            }

            if (_input.lockOn)
            {
                LockOn();
                _input.lockOn = false;
            }
        }

        private void LateUpdate()
        {
            _cameraController.TakePitchYawInput(_input.look);
        }

        public void SwitchShoulder()
        {
            _rightShoulder = !_rightShoulder;
            _logicGraph.SetBool("rightShoulder", _rightShoulder);
        }

        public void AimLogic()
        {
            _isAiming = !_isAiming;
            _logicGraph.SetBool("aim", _isAiming);
        }

        public void LockOn()
        {
            if (_cameraController.IsLockedOnTarget)
            {
                _cameraController.DeactivateLockOn();
                return;
            }

            Vector2 center = new Vector2(Screen.width, Screen.height) / 2;
            Ray ray = _cameraController.GetComponent<Camera>().ScreenPointToRay(center);

            Debug.DrawRay(ray.origin, ray.direction * 25);

            if (Physics.Raycast(ray, out RaycastHit hit, 25, _cameraController.LockOnTargetCollisionLayer))
                _cameraController.ActivateLockOn(hit.transform);
        }

    }
}