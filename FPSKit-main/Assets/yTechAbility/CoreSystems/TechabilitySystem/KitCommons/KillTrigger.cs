using System.Collections;
using System;
using UnityEngine;

namespace Techability.Systems
{
   

    public class KillTrigger : MonoBehaviour
    {
        [Header("This kills only the gameobject the collider is on...")]
        public bool maketheheaderwork = true; 

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject); 
        }
    }
}
