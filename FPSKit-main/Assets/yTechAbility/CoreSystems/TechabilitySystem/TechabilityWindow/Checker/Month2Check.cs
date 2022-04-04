using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Techability.Systems
{
    public class Month2Check : MonthCheckerAbstract
    {

        public Month2Check()
        {
            scriptableName = "Month2";
            base.EnsureSteps();
        }

        public override void CheckWeek1()
        {
            CheckWeek1Step1();
            CheckWeek1Step2();
            CheckWeek1Step3();
        }

        public void CheckWeek1Step1()
        {
            // UFP_W1S1
            // Has added the player prefab
            if (steps.GetStep(1, "UFP_W1S1").completed == true)
            {
                return;
            }

            try
            {
                if (GameObject.Find("Player"))
                {
                    //Debug.Log("Week 1 - Step 1: Found Player Racer");

                    steps.GetStep(1, "UFP_W1S1").completed = true;
                    MonthChecker.AddPoints(steps.GetStep(1, "UFP_W1S1"), this);
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: There's a player prefab for you to use! ");
                }
            }
            catch { }
        }

        public void CheckWeek1Step2()
        {
            if (steps.GetStep(1, "UFP_W1S2").completed == false)
            {
                Debug.Log("STEP CHECKER HINT: Play Level 01 to complete this step");
            }
        }

        public void CheckWeek1Step3()
        {
            if (steps.GetStep(1, "UFP_W1S3").completed == false)
            {
                Debug.Log("STEP CHECKER HINT: Play Level 01 to complete this step");
            }
        }




        public override void CheckWeek2()
        {
            CheckWeek2Step1();
            CheckWeek2Step2();
            CheckWeek2Step3();
        }

        public void CheckWeek2Step1()
        {
            if (steps.GetStep(2, "UFP_W2S1").completed == true)
            {
                return; 
            }

            GameObject door = GameObject.Find("Door Trigger"); 
            if(door)
            {
                DoorTrigger dt = door.GetComponent<DoorTrigger>(); 
                if (dt)
                {
                    if (dt.OnUnlockDoor.GetPersistentEventCount() > 0)
                    {
                        steps.GetStep(2, "UFP_W2S1").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "UFP_W2S1"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: The Door trigger needs to tell the door to open!");
                    }
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: Add the Door Trigger Script to the trigger in the level");
                }
            }


        }
        public void CheckWeek2Step2()
        {
            if (steps.GetStep(2, "UFP_W2S2").completed == true)
            {
                return; 
            }
            GameObject door = GameObject.Find("Door Trigger");
            GameObject key = GameObject.Find("Door Key");
            if (key && door)
            {
                DoorTrigger dt = door.GetComponent<DoorTrigger>();
                if (!dt)
                {
                    Debug.Log("STEP CHECKER HINT: Step 1 needs to be completed before Starting Step 2");
                    return; 
                }
                KeyItem ki = key.GetComponent<KeyItem>();
                if (!ki)
                {
                    Debug.Log("STEP CHECKER HINT: Add the Key item script to the Key object");
                    return;
                }

                if (ki.KeyName == dt.KeyName)
                {
                    steps.GetStep(2, "UFP_W2S2").completed = true;
                    MonthChecker.AddPoints(steps.GetStep(2, "UFP_W2S2"), this);
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: The Key name needs to be the same on the key and trigger. Capitalization matters!");
                    return;

                }


            }    

        }
        public void CheckWeek2Step3()
        {
            if (steps.GetStep(2, "UFP_W2S3").completed == false)
            {
                Debug.Log("STEP CHECKER HINT: Play Level 02 to complete this step");
            }
        }

        public override void CheckWeek3()
        {
            CheckWeek3Step1();
            CheckWeek3Step2();
            CheckWeek3Step3();
        }

        public void CheckWeek3Step1() 
        {
            if (steps.GetStep(3, "UFP_W3S1").completed == true)
            {
                return;
            }
            FPSController player = GameObject.FindObjectOfType<FPSController>();
            if (player)
            {
                if (player.ActiveWeapon == null)
                {
                    Debug.Log("STEP CHECKER HINT: the FPS Controller Script needs the weapon assigned to Active Weapon!");
                }
                else
                {
                    steps.GetStep(3, "UFP_W3S1").completed = true;
                    MonthChecker.AddPoints(steps.GetStep(3, "UFP_W3S1"), this);
                }
            }

        }
        public void CheckWeek3Step2()
        {
            if (steps.GetStep(3, "UFP_W3S2").completed == true)
            {
                return;
            }
            Debug.Log("STEP CHECKER HINT: Step 2 will be marked complete when you fire your weapon for the first time!");

        }
        public void CheckWeek3Step3()
        {
            if (steps.GetStep(3, "UFP_W3S3").completed == true)
            {
                return;
            }
            Debug.Log("STEP CHECKER HINT: Play Level 03 to complete this step");
        }
        public override void CheckWeek4()
        {
            CheckWeek4Step1();
            CheckWeek4Step2();
            CheckWeek4Step3();
        }

        public void CheckWeek4Step1()
        {
            if (steps.GetStep(4, "UFP_W4S1").completed == true)
            {
                return;
            }
            
            
            GameObject boss = GameObject.Find("Boss");
            GameObject gen1 = GameObject.Find("Energy Generator A");
            GameObject gen2 = GameObject.Find("Energy Generator B");
            GameObject gen3 = GameObject.Find("Energy Generator C");
            EnemyCountManager ECM = boss.GetComponent<EnemyCountManager>();
            bool hasGen1 = ECM.EnemyList.Contains(gen1);
            bool hasGen2 = ECM.EnemyList.Contains(gen2);
            bool hasGen3 = ECM.EnemyList.Contains(gen3);
            if (hasGen1 && hasGen2 && hasGen3)
            {
                steps.GetStep(4, "UFP_W4S1").completed = true;
                MonthChecker.AddPoints(steps.GetStep(4, "UFP_W4S1"), this);
            }
            else
            {
                Debug.Log("STEP CHECKER HINT: You need to assign all generators to the Boss's counter");
            }
        }
        public void CheckWeek4Step2() 
        {
            if (steps.GetStep(4, "UFP_W4S2").completed == true)
            {
                return;
            }


           
            GameObject gen1 = GameObject.Find("Energy Generator A");
            Health hgen1 = gen1.GetComponent<Health>();
            GameObject gen2 = GameObject.Find("Energy Generator B");
            Health hgen2 = gen2.GetComponent<Health>();
            GameObject gen3 = GameObject.Find("Energy Generator C");
            Health hgen3 = gen3.GetComponent<Health>();

            bool hasGen1 = (hgen1.OnDeathEvent.GetPersistentEventCount() >= 2 );
            bool hasGen2 = (hgen2.OnDeathEvent.GetPersistentEventCount() >= 2 );
            bool hasGen3 = (hgen3.OnDeathEvent.GetPersistentEventCount() >= 2 );
            if (hasGen1 && hasGen2 && hasGen3)
            {
                steps.GetStep(4, "UFP_W4S2").completed = true;
                MonthChecker.AddPoints(steps.GetStep(4, "UFP_W4S2"), this);
            }
            else
            {
                Debug.Log("STEP CHECKER HINT: You need to assign each of the generators OnDeath");
            }

        }
        public void CheckWeek4Step3()
        {
            if (steps.GetStep(4, "UFP_W4S3").completed == true)
            {
                return;
            }
            Debug.Log("STEP CHECKER HINT: Play Level 04 to complete this step");
        }



    }
}