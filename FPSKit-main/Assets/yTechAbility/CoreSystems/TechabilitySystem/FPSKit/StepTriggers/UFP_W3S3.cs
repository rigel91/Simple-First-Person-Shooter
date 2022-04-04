using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Techability.Systems
{
    public class UFP_W3S3 : MonoBehaviour
    {
        public bool stepStatus = false;
        public TechAbilityStepsScriptable steps;
        public string scriptableName = "Month2";
        public int StepWeek = 3;
        public string StepString = "UFP_W3S3";

        void Start()
        {

            //gameObject.GetComponent<MeshRenderer>().enabled = false;

            steps = (TechAbilityStepsScriptable)Resources.Load(scriptableName);
            //steps.GetStep(StepWeek, StepString).completed = false;
            stepStatus = steps.GetStep(StepWeek, StepString).completed;

        }

        public void OnComplete()
        {
            if (steps.GetStep(StepWeek, StepString).completed == false)
            {
                steps.GetStep(StepWeek, StepString).completed = true;
                MonthChecker.AddPoints(steps.GetStep(StepWeek, StepString), this);
                //Debug.Log("UFP_W3S3 : Success");
                string message = "Step 3: Complete!\nLevel 3 is DONE!";
                HudMessageController.instance.SetMessage(message, Color.green);
            } 
        }
    }
}
