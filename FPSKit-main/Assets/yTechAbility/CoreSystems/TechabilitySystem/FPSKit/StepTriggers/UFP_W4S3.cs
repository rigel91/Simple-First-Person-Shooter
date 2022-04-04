using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class UFP_W4S3 : MonoBehaviour
    {
        public bool stepStatus = false;
        public TechAbilityStepsScriptable steps;
        public string scriptableName = "Month2";
        public int StepWeek = 4;
        public string StepString = "UFP_W4S3";

        void Start()
        {

            //gameObject.GetComponent<MeshRenderer>().enabled = false;

            steps = (TechAbilityStepsScriptable)Resources.Load(scriptableName);
            //steps.GetStep(StepWeek, StepString).completed = false;
            stepStatus = steps.GetStep(StepWeek, StepString).completed;

        }

        public void OnComplete()
        {
            // Step 1 and 2 need to be done before Step 3 can be done.
            bool Step1 = steps.GetStep(StepWeek, "UFP_W4S1").completed;
            bool Step2 = steps.GetStep(StepWeek, "UFP_W4S2").completed;
            if (!(Step1 && Step2))
            {
                return; 
            }

            if (steps.GetStep(StepWeek, StepString).completed == false)
            {
                steps.GetStep(StepWeek, StepString).completed = true;
                MonthChecker.AddPoints(steps.GetStep(StepWeek, StepString), this);
                //Debug.Log("UFP_W4S3 : Success");
                string message = "Step 3: Complete!\nLevel 4 is DONE!";
                HudMessageController.instance.SetMessage(message, Color.green);
            }
        }
    }
}
