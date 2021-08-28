﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController Controller;

        [SerializeField]
        private Animator _characterAnimator;
        private string _currentState;

        const string PLAYER_IDLE = "Take_Idle";
        const string PLAYER_RUN = "Take_Run";



        //public Vector3 PlayerPosition? -> enemy's destination

        public float Speed = 10f;
        public float Gravity = -9.81f;
        public float JumpHeight = 3;

        public Transform GroundCheck;
        public float GroundDist = 0.4f;
        public LayerMask GroundMask;

        private Vector3 _velocity;
        private bool _isGrounded;
        private bool _canLand;

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

            if (x != 0 || z != 0) // if movement input exists...
            {
                if (_isGrounded)
                {
                    ChangeAnimationState(PLAYER_RUN);
                    if (_canLand == true)
                    {
                        // play sound effect landing
                        _canLand = false;
                    }
                }
                else
                {
                    ChangeAnimationState(PLAYER_IDLE);
                }
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }


            Vector3 move = transform.right * x + transform.forward * z;

            Controller.Move(move * Speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                _canLand = true;
                // play jump start sound effect
            }

            _velocity.y += Gravity * Time.deltaTime;

            Controller.Move(_velocity * Time.deltaTime);
        }



        void ChangeAnimationState(string newState)
        {
            // stop animation from interrupting itself
            if (_currentState == newState) return;

            // play the animation
            _characterAnimator.Play(newState,1);

            // reassign the current state
            _currentState = newState;
        }
    }
}