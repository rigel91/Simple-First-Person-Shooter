using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Techability.Systems
{
    public class Month3Check : MonthCheckerAbstract
    {

        public Month3Check()
        {
            scriptableName = "Month3";
            base.EnsureSteps();
        }

        public override void CheckWeek1()
        {

            try
            {
                GameObject castlep = Resources.Load<GameObject>("Environments/Temple/Prefabs/Props/flag_A");
                GameObject cavep = Resources.Load<GameObject>("Environments/Alien/Prefabs/Effects/Water");

                if (castlep || cavep)
                {
                    if (steps.GetStep(1, "ENVR").completed == false)
                    {
                        steps.GetStep(1, "ENVR").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "ENVR"), this);
                    }
                }
            }
            catch { }


            try
            {
                if (GameObject.Find("Player") && GameObject.Find("PlayerCamera"))
                {
                    if (steps.GetStep(1, "PLPF").completed == false)
                    {
                        steps.GetStep(1, "PLPF").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "PLPF"), this);
                    }
                }
            }
            catch { }


            try
            {
                GameObject gate = GameObject.Find("DungeonGate");

                if (gate && (GetFieldValue("dungeonSceneName", "DungeonGate", gate) as string != ""))
                {
                    if (steps.GetStep(1, "DNGT").completed == false)
                    {
                        steps.GetStep(1, "DNGT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "DNGT"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (GameObject.FindGameObjectsWithTag("Decoration").Length >= 5)
                {
                    if (steps.GetStep(1, "DCBS").completed == false)
                    {
                        steps.GetStep(1, "DCBS").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "DCBS"), this);
                    }
                }
            }
            catch { }
        }

        public override void CheckWeek2()
        {
            try
            {
                GameObject env = GameObject.Find("EnvironmentHazard");

                if (env)
                {
                    if (steps.GetStep(2, "HZEN").completed == false)
                    {
                        steps.GetStep(2, "HZEN").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "HZEN"), this);
                    }
                }
            }
            catch { }

            try
            {

                GameObject env = GameObject.Find("EnvironmentHazard");

                if (env.tag == "Hazard")
                {
                    if (steps.GetStep(2, "HZSC").completed == false)
                    {
                        steps.GetStep(2, "HZSC").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "HZSC"), this);
                    }
                }
            }
            catch { }

            try
            {
                GameObject env = GameObject.Find("EnvironmentHazard");
                if (env.GetComponent<BoxCollider>())
                {
                    if (steps.GetStep(2, "HZBC").completed == false)
                    {
                        steps.GetStep(2, "HZBC").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "HZBC"), this);
                    }
                }
            }
            catch { }

            try
            {
                GameObject env = GameObject.Find("EnvironmentHazard");
                if (env.tag == "Hazard")
                {
                    if (steps.GetStep(2, "HZTG").completed == false)
                    {
                        steps.GetStep(2, "HZTG").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "HZTG"), this);
                    }
                }
            }
            catch { }
        }

        public override void CheckWeek3()
        {
            try
            {
                GameObject door = Resources.Load<GameObject>("Prefabs/Environment/LockedDoor");

                if (door)
                {
                    if (steps.GetStep(3, "DRPB").completed == false)
                    {
                        steps.GetStep(3, "DRPB").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "DRPB"), this);
                    }
                }
            }
            catch { }

            try
            {
                GameObject door = GameObject.Find("LockedDoor");
                if (door)
                {
                    if (steps.GetStep(3, "DOOR").completed == false)
                    {
                        steps.GetStep(3, "DOOR").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "DOOR"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (getClassOnPrefab("Key, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep(3, "KYPB").completed == false)
                    {
                        steps.GetStep(3, "KYPB").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "KYPB"), this);
                    }
                }

            }
            catch { }

            try
            {
                GameObject door = GameObject.Find("LockedDoor");
                GameObject key = GameObject.Find("rust_key");
                if (door && key)
                {
                    if (steps.GetStep(3, "KEYS").completed == false)
                    {
                        steps.GetStep(3, "KEYS").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "KEYS"), this);
                    }
                }
            }
            catch { }

            try
            {
                GameObject key = GameObject.Find("KeyCanvas");
                if (key)
                {
                    if (steps.GetStep(3, "KYUI").completed == false)
                    {
                        steps.GetStep(3, "KYUI").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "KYUI"), this);
                    }
                }
            }
            catch { }
        }

        public override void CheckWeek4()
        {
            try
            {
                GameObject spikes = GameObject.Find("SpikeTrap");
                if (spikes && spikes.transform.Find("SpikeRow").Find("TrapSpikes"))
                {
                    if (steps.GetStep(4, "SPKE").completed == false)
                    {
                        steps.GetStep(4, "SPKE").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "SPKE"), this);
                    }
                }

            }
            catch { }

            try
            {
                List<GameObject> spikes = FindGameObjectsWithClass("EnvironmentHazard, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

                foreach(var spike in spikes)
                {
                    if (spike.name == "SpikeRow")
                    {
                        if (steps.GetStep(4, "SPTG").completed == false)
                        {
                            steps.GetStep(4, "SPTG").completed = true;
                            MonthChecker.AddPoints(steps.GetStep(4, "SPTG"), this);
                        }
                    }
                }

                

            }
            catch { }

            try
            {

                if (FindGameObjectWithClass("SpikeTrap, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep(4, "TRCP").completed == false)
                    {
                        steps.GetStep(4, "TRCP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "TRCP"), this);
                    }
                }



            }
            catch { }

            try
            {
                if (FindGameObjectWithClass("SpikeTrap, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null").name == "SpikeTrap")
                {
                    if (steps.GetStep(4, "SPLV").completed == false)
                    {
                        steps.GetStep(4, "SPLV").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "SPLV"), this);
                    }
                }
            }
            catch { }
        }
    }
}