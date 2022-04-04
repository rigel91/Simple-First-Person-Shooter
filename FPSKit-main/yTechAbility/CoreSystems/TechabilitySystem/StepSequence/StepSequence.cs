using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public static class StepSequence
    {
        public static StepSequenceScriptable steps;

        public static void EnsureLoad()
        {
            if (steps == null)
            {
                steps = (StepSequenceScriptable) Resources.Load("StepSequence");
                if(steps == null)
                {
                    Debug.LogError("Error grabbing 'StepSequence' scriptable object. Please contact a teacher.");
                }
            }
        }

        public static void SetStep(string code)
        {
            EnsureLoad();
            steps.AddNew(code);
        }

        public static int StepCount()
        {
            EnsureLoad();
            return steps.GetSequenceCount();
        }

    }
}