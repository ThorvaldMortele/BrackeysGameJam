using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossSystem
{
    public class CannonDownwards : CannonBase
    {
        // cannon that aims at the player and shoot straight
        //gets locked in rotation right before shooting

        public override void Start()
        {
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                transform.LookAt(other.transform);
            }
        }
    }
}