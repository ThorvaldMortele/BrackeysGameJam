using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveSystem
{
    public class EnemyMovement : MonoBehaviour
    {
        private float _time;
        private Rigidbody _rigidBody;
        public Transform Player;
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
        }

        private void Update()
        {
            _time += Time.deltaTime;

            //Movement();

            MovementZ();
        }

        private void Movement()
        {            
            LookAtPlayer();

            if (Flying)
            {
                //Update location; X = Left/Right, Y = Up/Down, Z = Forward/Backward
                var x = 0;
                var y = SmoothSineWave() + groundOffset;
                var z = 0;

                transform.localPosition = new Vector3(x, y, z);
            }
            else
            {
                //Update location; X = Left/Right, Y = Up/Down, Z = Forward/Backward
                var x = 0;
                var y = 0;
                var z = 0;

                transform.localPosition = new Vector3(x, y, z);
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

        private void LookAtPlayer()
        {
            transform.LookAt(Player);
        }



        

        private void MovementZ()
        {
            //var zPosition = Player.position.z - transform.forward.z; //target lcatoin          
            //zPosition += flyingSpeed * _time;

            //LookAtPlayer();
            //var targetlocation = Player.position - transform.position;
            //float distance = targetlocation.magnitude;


            //rb.AddRelativeForce(Vector3.forward * Mathf.Clamp((distance /*- 10*/) / 50, 0f, 1f) * 1000f);


            LookAtPlayer();
            var targetLocation = Player.position - transform.position;
            float distanceFromTarget = targetLocation.magnitude;

            



            var test = transform.forward * Mathf.Clamp(distanceFromTarget / 5, 0f, 1f) * 10f;


            var movement = transform.position + test;
        }
    }
}