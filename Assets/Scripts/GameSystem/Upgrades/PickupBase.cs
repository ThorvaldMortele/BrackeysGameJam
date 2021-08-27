﻿using ShootSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Upgrades
{ 
    public class PickupBase : MonoBehaviour
    {
        public ArmShoot ArmShoot;

        public virtual void OnTriggerEnter(Collider other) { }

    }
}
