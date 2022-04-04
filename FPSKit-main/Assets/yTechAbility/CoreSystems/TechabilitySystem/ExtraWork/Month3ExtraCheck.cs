using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Month3ExtraCheck : MonthCheckerExtraAbstract
    {
        public Month3ExtraCheck()
        {
            scriptableName = "Month3Extra";
            base.EnsureSteps();
        }
        public override void CheckWeek1()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckWeek2()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckWeek3()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckWeek4()
        {
            throw new System.NotImplementedException();
        }
    }
}