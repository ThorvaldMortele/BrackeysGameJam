using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public class EnemyMovement : MonoBehaviour
    {
        private float _time;
        private Rigidbody _rigidBody;
        public GameObject Player;
        public bool Flying;

        [Header("Walking Parameters: ")]
        public float walkingSpeed;

        [Header("Flying Parameters: ")]
        public float flyingSpeed;
        public float groundOffset = 5f; //The offset from the ground the flying enemy should have
        public float period = 2f;        
        public float amplitude = 0.7f;
        public float phase = 0f;

        private float _frequency;
        private float _angularFrequency;


        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();

            Player = FindObjectOfType<PlayerMovement>().gameObject;
        }

        private void Update()
        {
            LookAtPlayer();

            _time += Time.deltaTime;

            //StartCoroutine("Movement"); //get a coroutine to get the time

            CalculateMoveTowardsPlayer();

            //MoveTowardsPlayer();
        }
        private void FixedUpdate()
        {
            MoveTowardsPlayer();
        }




        private void LookAtPlayer()
        {
            var playerTransform = Player.transform;
            transform.LookAt(playerTransform);
        }

        private float CalculateMoveTowardsPlayer() //A quick way to get the enemy to move to the player
        {
            //https://youtu.be/VAiMUZHtZyI

            var playerLocation = Player.transform.position; //Get enemy location
            var enemyLocation = transform.position; //Get player location

            enemyLocation.y = groundOffset;

            var distance = (playerLocation - enemyLocation).magnitude; //Get the distance between both

            var speed = 450f;
            var stopDistance = 5f; //Distance from the player where it needs to stop

            float clampedValue = Mathf.Clamp((distance - stopDistance)/35, 0f, 1f);

            float calculatedSpeed = clampedValue * speed;

            return calculatedSpeed;       
        }
        private void MoveTowardsPlayer()
        {
            _rigidBody.AddRelativeForce(Vector3.forward * CalculateMoveTowardsPlayer());
        }


        #region Currently not in use
        private void Movement()
        {            
            LookAtPlayer();

            if (Flying) //For flying enemies
            {
                //Update location; X = Left/Right, Y = Up/Down, Z = Forward/Backward
                var x = 0;
                var y = SmoothSineWave() + groundOffset; //Sin wave with an offset above the ground
                var z = 0;

                transform.localPosition = new Vector3(x, y, z);
            }
            else //For ground enemies
            {
                //Update location; X = Left/Right, Y = Up/Down, Z = Forward/Backward
                var x = 0;
                var y = 0;
                var z = 0;

                transform.localPosition = new Vector3(x, y, z);
            }
        }
        private void MovementZ()
        {
            var playerLocation = Player.transform.position; //Get enemy location
            var enemyLocation = transform.position; //Get player location

            var distance = (playerLocation - enemyLocation).magnitude; //Get the distance between both

            if (distance >= 10) //If distance is bigger than x amount
            {
                //Add force to the enemy to move forward
                var newPosition = transform.forward * flyingSpeed * _time;
                //newPosition.y = 5; //Keep it of the ground (since it's not using pure z axis yet

                enemyLocation += newPosition;
            }
            else
            {
                //stop the enemy, so that it keeps a little distance from the player

                //NEED TO CHANGE: when the player goes backwards after the enemy has stopped, the enemy teleports

                enemyLocation += Vector3.zero;

                //make the attack range bigger than this tho so it can still attack
            }
        }
        private float SmoothSineWave()
        {
            //https://answers.unity.com/questions/434717/how-to-make-a-sine-wave-with-a-transform.html

            // y(t) = A * sin(ωt + θ) [Basic Sine Wave Equation]
            // [A = amplitude | ω = AngularFrequency ((2*PI)f) | f = 1/T | T = [period (s)] | θ = phase | t = elapsedTime]
            // Public/Serialized Variables: amplitude, period, phase

            // If the value of period has altered last known frequency...
            if (1 / (period) != _frequency)
            {
                // Recalculate frequency & omega.
                _frequency = 1 / (period);
                _angularFrequency = (2 * Mathf.PI) *_frequency;
            }
            // Update elapsed time.
            //_time += Time.deltaTime;
            // Calculate new omega-time product.
            float omegaProduct = (_angularFrequency * _time);
            // Plug in all calculated variables into the complete Sine wave equation.
            float y = (amplitude * Mathf.Sin(omegaProduct + phase));

            return y;            
        }
        #endregion
    }
}