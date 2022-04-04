using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Varibles")]
        public float Lifespan = 5f;
        public float Speed = 30f; 
        public bool UseGraity = false;
        public float Damage = 25;

        [Header("Ingore Owner? -- Must assign owner at spawn")]
        public bool ignoreOwner = false;
        public GameObject Owner;

        Rigidbody RB;

        void Start()
        {
            Destroy(gameObject, Lifespan);
            RB = GetComponent<Rigidbody>();
            RB.useGravity = UseGraity;
            RB.velocity = gameObject.transform.forward * Speed;

        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponentInParent<Health>(); 

            if (health)
            {
                if (ignoreOwner && (health.gameObject == Owner))
                {
                    // Hit the owner, and ignore is on. 
                    return;
                }

                health.TakeDamage(Damage); 
            }

            Destroy(gameObject); 
        }

    }
}
