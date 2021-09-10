using System.Collections.Generic;
using UnityEngine;

namespace BossPhase
{
    //Base class for the boss phases
    //Holds all the parameters

    public class BossPhaseBase : MonoBehaviour
    {
        //Boss
        [Header("Boss: ")]
        [SerializeField]
        private GameObject _boss = null;

        [SerializeField]
        private struct Layer
        {

        }



        //Layers
        [Header("Layers: ")]
        [SerializeField]
        private GameObject _layer1 = null;
        [SerializeField]
        private GameObject _layer2 = null;
        [SerializeField]
        private GameObject _layer3 = null;
        [SerializeField]
        private GameObject _layer4 = null;

        //Turrets
        [Header("Turrets: ")]
        [SerializeField]
        private List<GameObject> _turrets = new List<GameObject>();


        //Targets
        [Header("Targets: ")]
        [SerializeField]
        private List<GameObject> _targets = new List<GameObject>();

    }
}