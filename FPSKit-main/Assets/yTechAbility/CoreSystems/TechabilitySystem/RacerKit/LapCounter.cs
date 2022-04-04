using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Techability.Systems;
using Techability;
using UnityEditor;
using System.Reflection;

namespace Techability.Systems
{
    public class LapCounter : MonoBehaviour
    {
        public int laps = 0;
        public bool stepStatus = false;

        public TechAbilityStepsScriptable steps;
        public string scriptableName = "Month1";
        public int StepWeek = 1;
        public string StepString = "RKW1_03";

        void Start()
        {

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            laps = 0;

            steps = (TechAbilityStepsScriptable)Resources.Load(scriptableName);
            //steps.GetStep(StepWeek, StepString).completed = false;
            stepStatus = steps.GetStep(StepWeek, StepString).completed;

        }

        public void OnTriggerEnter(Collider other)
        {
            RacerController RC = other.GetComponentInParent<RacerController>();
            if (RC)
            {
                Debug.Log("LapCount");
                laps++;

                if (laps >= 2)
                {
                    if (steps.GetStep(StepWeek, StepString).completed == false)
                    {
                        steps.GetStep(StepWeek, StepString).completed = true;
                        MonthChecker.AddPoints(steps.GetStep(StepWeek, StepString), this);
                    }
                }
            }
        }

    }
}