using UnityEngine;

namespace MoveSystem
{
    public class MouseLook : MonoBehaviour
    {
        public float MouseSensitivity = 100;
        public Transform PlayerBody;
        private float _xRotation = 0f;


        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90);

            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
