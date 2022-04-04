using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Techability.Systems
{
    public class Weapon_Pistol : Weapon
    {
        public GameObject FirePoint;
        public float AttackValue = 10;
        public float RigidForceMultipler = 100;

        public override void OnFire()
        {
            // This is for Techability Checker for FPS Kit Week 3
            MarkStepCompete(); 

            RaycastHit hit; 
            if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out hit))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name );

                // If it has a Health, Do Damage!
                Health health = hit.collider.GetComponentInParent<Health>();
                if (health)
                {
                    Debug.Log("Damage Done");
                    health.TakeDamage(AttackValue);
                    return;
                }

                /* This is causing problems... why?!?
                // If it has a Rigid Body, Toss it around!
                Rigidbody RB = hit.collider.GetComponentInParent<Rigidbody>();
                if (RB)
                {
                    Debug.Log("Physics Object Found");
                    RB.AddForce( Quaternion.Inverse(Quaternion.Euler(hit.normal)).eulerAngles * AttackValue * RigidForceMultipler);
                    //RB.AddForceAtPosition(hit.normal * AttackValue * 10, hit.point, ForceMode.Impulse); 
                }
                */
            }  
        }


        TechAbilityStepsScriptable steps;
        string scriptableName = "Month2";
        int StepWeek = 3;
        string StepString = "UFP_W3S2";

        void MarkStepCompete()
        {
            Scene activelevel = SceneManager.GetActiveScene();
            if (activelevel.name != "Level03")
            {
                // Only check this on Level 03... 
                return; 
            }
                steps = (TechAbilityStepsScriptable)Resources.Load(scriptableName);
            if (steps.GetStep(StepWeek, StepString).completed == false)
            {
                steps.GetStep(StepWeek, StepString).completed = true;
                MonthChecker.AddPoints(steps.GetStep(StepWeek, StepString), this);
                string message = "Weapon Ready!\nStep 2: Complete!";
                HudMessageController.instance.SetMessage(message, Color.green);
            }
        }
    }
}
