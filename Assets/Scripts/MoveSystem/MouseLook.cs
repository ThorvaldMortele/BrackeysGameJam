using UnityEngine;

namespace MoveSystem
{
    public class MouseLook : MonoBehaviour
    {
        public float MouseSensitivity; //100
        public Transform PlayerBody;
        private float _xRotation = 0f;


        void Start()
        {
            //Only needed in unity editor, when starting from the gamescene
            Cursor.lockState = CursorLockMode.Locked; //Lock MouseCursor so it doesn't show up on top of the game
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

        public void UpdateSensititvity(float amount) //Called by the MouseSensitivity slider
        {
            MouseSensitivity = amount;
        }
    }
}
