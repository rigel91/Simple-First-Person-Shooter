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
            try
            {
                GameObject castlep = Resources.Load<GameObject>("Environments/Snow/Prefabs/Props/big_wall");
                GameObject cavep = Resources.Load<GameObject>("Environments/Forest/Prefabs/mushroom");

                if (castlep || cavep)
                {
                    if (steps.GetStep(1, "STRT").completed == false)
                    {
                        steps.GetStep(1, "STRT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "STRT"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (GameObject.FindGameObjectsWithTag("Decoration").Length >= 5)
                {
                    if (steps.GetStep(1, "PRPB").completed == false)
                    {
                        steps.GetStep(1, "PRPB").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "PRPB"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (GameObject.Find("Player") && GameObject.Find("PlayerCamera"))
                {
                    if (steps.GetStep(1, "PRPB").completed == false)
                    {
                        steps.GetStep(1, "PRPB").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "PRPB"), this);
                    }
                }
            }
            catch { }

            try
            {
                GameObject gate = GameObject.Find("DungeonGate");

                if (gate && (GetFieldValue("dungeonSceneName", "DungeonGate", gate) as string != ""))
                {
                    if (steps.GetStep(1, "DGGT").completed == false)
                    {
                        steps.GetStep(1, "DGGT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "DGGT"), this);
                    }
                }
            }
            catch { }
        }

        

        public override void CheckWeek2()
        {
            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolSimple");
                if (script != null)
                {
                    if (steps.GetStep(2, "PSSC").completed == false)
                    {
                        steps.GetStep(2, "PSSC").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "PSSC"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolSimple");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public Transform pointA;", script.text) &&
                    ContainsLine("public Transform pointB;", script.text) &&
                    ContainsLine("NavMeshAgent agent;", script.text) &&
                    ContainsLine("Animator anim;", script.text))
                {
                    if (steps.GetStep(2, "PSVR").completed == false)
                    {
                        steps.GetStep(2, "PSVR").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "PSVR"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolSimple");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLineInRange("agent = GetComponent<NavMeshAgent>();", array, methodRange, true) &&
                    ContainsLineInRange("anim = GetComponent<Animator>();", array, methodRange, true) &&
                    ContainsLineInRange("agent.SetDestination(pointA.position);", array, methodRange, true) &&
                    ContainsLineInRange("anim.SetBool(" + '\u0022' + "Running" + '\u0022' + ", true);", array, methodRange, true))
                {
                    if (steps.GetStep(2, "PSSM").completed == false)
                    {
                        steps.GetStep(2, "PSSM").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "PSSM"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolSimple");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Update()", array, true, false);

                if (ContainsLineInRange("if(Vector3.Distance(agent.destination,transform.position) < .5f)", array, methodRange, true) &&
                    ContainsLineInRange("if(agent.destination.x == pointA.position.x && agent.destination.z == pointA.position.z)", array, methodRange, true) &&
                    ContainsLineInRange("agent.SetDestination(pointB.position);", array, methodRange, true) &&
                    ContainsLineInRange("agent.SetDestination(pointA.position);", array, methodRange, true))
                {
                    if (steps.GetStep(2, "PSUP").completed == false)
                    {
                        steps.GetStep(2, "PSUP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "PSUP"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (GameObject.Find("PointA") && GameObject.Find("PointB"))
                {
                    if (steps.GetStep(2, "PSWP").completed == false)
                    {
                        steps.GetStep(2, "PSWP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "PSWP"), this);
                    }
                }
            }
            catch
            {

            }
        }
        public override void CheckWeek3()
        {
            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolPath");
                if (script != null)
                {
                    if (steps.GetStep(3, "PPSC").completed == false)
                    {
                        steps.GetStep(3, "PPSC").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "PPSC"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolPath");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public Transform[] path;", script.text) &&
                    ContainsLine("int pathTracker = 0;", script.text) &&
                    ContainsLine("NavMeshAgent agent;", script.text) &&
                    ContainsLine("Animator anim;", script.text))
                {
                    if (steps.GetStep(3, "PPVR").completed == false)
                    {
                        steps.GetStep(3, "PPVR").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "PPVR"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolPath");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLineInRange("agent = GetComponent<NavMeshAgent>();", array, methodRange, true) &&
                    ContainsLineInRange("anim = GetComponent<Animator>();", array, methodRange, true) &&
                    ContainsLineInRange("agent.SetDestination(path[pathTracker].position);", array, methodRange, true) &&
                    ContainsLineInRange("anim.SetBool(" + '\u0022' + "Running" + '\u0022' + ", true);", array, methodRange, true))
                {
                    if (steps.GetStep(3, "PPSM").completed == false)
                    {
                        steps.GetStep(3, "PPSM").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "PPSM"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/PatrolPath");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Update()", array, true, false);

                if (ContainsLineInRange("if(Vector3.Distance(agent.destination,transform.position) < .5f)", array, methodRange, true) &&
                    ContainsLineInRange("pathTracker++;", array, methodRange, true) &&
                    ContainsLineInRange("if (pathTracker >= path.Length)", array, methodRange, true) &&
                    ContainsLineInRange("pathTracker = 0;", array, methodRange, true) &&
                    ContainsLineInRange("agent.SetDestination(path[pathTracker].position);", array, methodRange, true))
                {
                    if (steps.GetStep(3, "PPUP").completed == false)
                    {
                        steps.GetStep(3, "PPUP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "PPUP"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (GameObject.Find("PointA") && GameObject.Find("PointB") && GameObject.Find("PointC"))
                {
                    if (steps.GetStep(3, "PPWP").completed == false)
                    {
                        steps.GetStep(3, "PPWP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "PPWP"), this);
                    }
                }
            }
            catch
            {

            }
        }
        public override void CheckWeek4()
        {
            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/RangedAttack");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    if (steps.GetStep(4, "RASC").completed == false)
                    {
                        steps.GetStep(4, "RASC").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "RASC"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/RangedAttack");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }

                if (ContainsLine("public GameObject projectilePrefab;", script.text) &&
                    ContainsLine("public Transform firePoint;", script.text) &&
                    ContainsLine("public float projectileSpeed;", script.text))
                {
                    if (steps.GetStep(4, "RAVR").completed == false)
                    {
                        steps.GetStep(4, "RAVR").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "RAVR"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/RangedAttack");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("public void FireProjectile()", array, true, false);

                if (ContainsLineInRange("GameObject projectile = Instantiate<GameObject>(projectilePrefab);", array, methodRange, true) &&
                    ContainsLineInRange("projectile.transform.position = firePoint.position;", array, methodRange, true) &&
                    ContainsLineInRange("Vector3 playerPosition = GameObject.Find(" + '\u0022' + "Player" + '\u0022' + ").transform.position;", array, methodRange, true) &&
                    ContainsLineInRange("playerPosition.y += 1;", array, methodRange, true) &&
                    ContainsLineInRange("projectile.GetComponent<Rigidbody>().velocity = (playerPosition - firePoint.position).normalized * projectileSpeed;", array, methodRange, true))
                {
                    if (steps.GetStep(4, "RAFP").completed == false)
                    {
                        steps.GetStep(4, "RAFP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "RAFP"), this);
                    }
                }
            }
            catch
            {

            }

            try
            {
                if (FindGameObjectWithClass("RangedAttack").transform.Find("FirePoint"))
                {
                    if (steps.GetStep(4, "FPPP").completed == false)
                    {
                        steps.GetStep(4, "FPPP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "FPPP"), this);
                    }
                }
            }
            catch { }

            try
            {
                if(FieldHasNonDefaultValue("attackEvent","MeleeEnemy",typeof(UnityEvent),null, FindGameObjectWithClass("RangedAttack")))
                {
                    if (steps.GetStep(4, "AEPB").completed == false)
                    {
                        steps.GetStep(4, "AEPB").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "AEPB"), this);
                    }
                }
            }
            catch { }
        }
    }
}