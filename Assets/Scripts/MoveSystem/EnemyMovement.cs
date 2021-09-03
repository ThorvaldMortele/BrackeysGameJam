using UnityEngine;

namespace MoveSystem
{
    public class EnemyMovement : MonoBehaviour
    {
        //WARNING: There are inconsistent line endings in the 'Assets/Scripts/MoveSystem/EnemyMovement.cs' script. Some are Mac OS X (UNIX) and some are Windows.

        private Rigidbody _rigidBody;
        public GameObject Player;

        public float walkingSpeed;

        public float flyingSpeed;
        public float groundOffset = 5f; //Offset from the ground the flying enemy should have

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();

            Player = FindObjectOfType<PlayerMovement>().gameObject;
        }

        private void Update()
        {
            LookAtPlayer();

            CalculateMoveTowardsPlayer();
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
    }
}