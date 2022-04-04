using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class UFP_W1S2 : MonoBehaviour
    {

        public bool stepStatus = false;
        public TechAbilityStepsScriptable steps;
        public string scriptableName = "Month2";
        public int StepWeek = 1;
        public string StepString = "UFP_W1S2";

        void Start()
        {

            //gameObject.GetComponent<MeshRenderer>().enabled = false;


            steps = (TechAbilityStepsScriptable)Resources.Load(scriptableName);
            //steps.GetStep(StepWeek, StepString).completed = false;
            stepStatus = steps.GetStep(StepWeek, StepString).completed;

        }

        public void OnTriggerEnter(Collider other)
        {
            FPSController player = other.GetComponentInParent<FPSController>();
            if (player)
            {
                if (steps.GetStep(StepWeek, StepString).completed == false)
                {
                    if (player.JumpPower >= 75)
                    {
                        steps.GetStep(StepWeek, StepString).completed = true;
                        MonthChecker.AddPoints(steps.GetStep(StepWeek, StepString), this);
                        string message = "Step 2: Complete!";
                        HudMessageController.instance.SetMessage(message, Color.green);
                    }
                    else
                    {
                        string message = "You can't jump high Enough!\n(check the boards for hints)";
                        Debug.Log("STEP CHECKER HINT: " + message);
                        HudMessageController.instance.SetMessage(message, Color.red);
                    }
                }

            }
        }
    }
}
