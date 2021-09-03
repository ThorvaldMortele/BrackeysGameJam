using GameSystem;
using System.Collections;
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

        private PlayerSounds _playerSounds;
        private PlayerStatistics _playerStats;


        private void Start()
        {
            _playerSounds = GetComponent<PlayerSounds>();
            _playerStats = GetComponent<PlayerStatistics>();
        }

        void Update()
        {
            _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDist, GroundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            if (_isGrounded)
            {
                if (_canLand == true)
                {
                    //Play sound effect landing
                    _playerSounds.PlaySoundEffect(3);
                    _canLand = false;
                }
            }


            float x;
            float z;
            if (_playerStats.IsAlive)
            {
                x = Input.GetAxis("Horizontal");
                z = Input.GetAxis("Vertical");
            }
            else
            {
                x = 0;
                z = 0;
            }


            if (x != 0 || z != 0) // if movement input exists...
            {
                if (_isGrounded)
                {
                    ChangeAnimationState(PLAYER_RUN);
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

            JumpPlayer(x, z);

            _velocity.y += Gravity * Time.deltaTime;

            Controller.Move(_velocity * Time.deltaTime);
        }

        private void JumpPlayer(float x, float z)
        {
            Vector3 move = transform.right * x + transform.forward * z;

            if (_playerStats.IsAlive)
            {
                Controller.Move(move * Speed * Time.deltaTime);

                if (Input.GetButtonDown("Jump") && _isGrounded)
                {
                    _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    StartCoroutine(SetCanLandBool());
                    _playerSounds.PlaySoundEffect(2);
                    //Play jump start sound effect
                }
            }
        }

        IEnumerator SetCanLandBool()
        {
            yield return new WaitForSeconds(0.2f);

            _canLand = true;
        }

        void ChangeAnimationState(string newState)
        {
            //Stop animation from interrupting itself
            if (_currentState == newState) return;

            //Play the animation
            _characterAnimator.Play(newState,1);

            //Reassign the current state
            _currentState = newState;
        }
    }
}