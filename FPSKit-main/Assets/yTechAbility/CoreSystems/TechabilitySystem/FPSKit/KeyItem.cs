using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class KeyItem : MonoBehaviour
    {
        public string KeyName = "Key Item";

        [Header("Sounds")]
        public bool HasCollectSound = false; 
        public AudioClip collectSound;
        public AudioSource audioSource; 
        private void OnTriggerEnter(Collider other)
        {
            FPSController player = other.GetComponentInParent<FPSController>(); 
            if (player)
            {
                player.AddItem(KeyName);
                if (HasCollectSound)
                { 
                    audioSource.PlayOneShot(collectSound);
                }
                Destroy(gameObject);
                
            }
        }
    }
}
