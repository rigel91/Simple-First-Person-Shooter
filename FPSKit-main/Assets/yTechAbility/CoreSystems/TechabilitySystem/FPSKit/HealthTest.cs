using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Techability.Systems
{
    public class HealthTest : MonoBehaviour
    {
        public Health test;
        public Transform locationtest;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //test.TakeDamage(10);
            }

        }

        public void Death()
        {
            Debug.Log("Death");
            // Need to spawn an effect here
            Destroy(gameObject);
        }

        public void TakenDamage()
        {
            // Use this to spawn an effect here! 
        }
    }
}
