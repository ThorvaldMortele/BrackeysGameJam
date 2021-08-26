using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController Controller;

        //public Vector3 PlayerPosition? -> enemy's destination

        public float Speed = 10f;
        public float Gravity = -9.81f;
        public float JumpHeight = 3;

        public Transform GroundCheck;
        public float GroundDist = 0.4f;
        public LayerMask GroundMask;

        private Vector3 _velocity;
        private bool _isGrounded;

        // Update is called once per frame
        void Update()
        {
            _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDist, GroundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            Controller.Move(move * Speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            _velocity.y += Gravity * Time.deltaTime;

            Controller.Move(_velocity * Time.deltaTime);
        }
    }
}