using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Month4ExtraCheck : MonthCheckerExtraAbstract
    {
        public Month4ExtraCheck()
        {
            scriptableName = "Month4Extra";
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