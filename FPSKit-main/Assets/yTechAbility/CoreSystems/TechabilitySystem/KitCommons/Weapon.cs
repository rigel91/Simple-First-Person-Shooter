using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Weapon : MonoBehaviour
    {
        public float RefireDelay = .7f; // in seconds
        public float RefireDelayALT = .7f; // in seconds
        
        public bool onCoolDown = false;
        public bool onCoolDownALT = false;

        float refireCounter = 0;
        float refireCounterALT = 0;
        public void Update()
        {
            if (onCoolDown)
            {
                refireCounter -= Time.deltaTime;
                if (RefireDelay <= 0)
                {
                    onCoolDown = false;
                }
            }

            if (onCoolDown)
            {
                refireCounter -= Time.deltaTime;
                if (RefireDelay <= 0)
                {
                    onCoolDown = false;
                }
            }

        }

        public void Fire()
        {
            refireCounter = RefireDelay;
            onCoolDown = true;
            OnFire(); 
        }

        public virtual void FireAlt()
        {
            refireCounterALT = RefireDelayALT;
            onCoolDownALT = true;
            OnFireAlt();
        }

        public virtual void OnFire()
        {
            // Implement this in children
        }
         
        public virtual void OnFireAlt()
        {
            // Implement this in children
        }

    }
}
