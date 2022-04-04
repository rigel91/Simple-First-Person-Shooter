using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class UFP_W2S3 : MonoBehaviour
    {
        public bool stepStatus = false;
        public TechAbilityStepsScriptable steps;
        public string scriptableName = "Month2";
        public int StepWeek = 2;
        public string StepString = "UFP_W2S3";

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
                    steps.GetStep(StepWeek, StepString).completed = true;
                    MonthChecker.AddPoints(steps.GetStep(StepWeek, StepString), this);
                    string message = "Step 3: Complete!\nLevel 2 is DONE!";
                    HudMessageController.instance.SetMessage(message, Color.green);
                }
            }
        }
    }
}
