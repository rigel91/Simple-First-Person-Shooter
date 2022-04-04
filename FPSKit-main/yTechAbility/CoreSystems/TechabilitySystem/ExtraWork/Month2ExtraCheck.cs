using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Month2ExtraCheck : MonthCheckerExtraAbstract
    {
        public Month2ExtraCheck()
        {
            scriptableName = "Month2Extra";
            base.EnsureSteps();
        }
        //Design
        public override void CheckWeek1()
        {
            

            try
            {
                Material customMat = Resources.Load<Material>("Materials/ProjectileMat");

                if (customMat != null)
                {
                    if (steps.GetStep("CPRM").completed == false)
                    {
                        steps.GetStep("CPRM").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("CPRM"), this);
                    }
                }
            }
            catch { }
            try
            {
                Material customMat = Resources.Load<Material>("Materials/CustomEnemyMaterial");

                if (customMat != null)
                {
                    if (steps.GetStep("ENCS").completed == false)
                    {
                        steps.GetStep("ENCS").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("ENCS"), this);
                    }
                }
            }
            catch { }

            try
            {
                GameObject customMat = Resources.Load<GameObject>("Prefabs/Environment/GoldCoins");

                if (customMat.GetComponent<BoxCollider>())
                {
                    if (steps.GetStep("GDPU").completed == false)
                    {
                        steps.GetStep("GDPU").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("GDPU"), this);
                    }
                }
            }
            catch { }
        }

        //UI
        public override void CheckWeek2()
        {
            try
            {
                if (FieldHasNonDefaultValue("textObj", "PlayerStats", typeof(GameObject), null, FindGameObjectWithClass("PlayerStats")))
                {
                    if (steps.GetStep("DMID").completed == false)
                    {
                        steps.GetStep("DMID").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("DMID"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (FieldHasNonDefaultValue("textObj", "MeleeEnemy", typeof(GameObject), null, FindGameObjectWithClass("MeleeEnemy")))
                {
                    if (steps.GetStep("EDMI").completed == false)
                    {
                        steps.GetStep("EDMI").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("EDMI"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (GameObject.Find("Player").transform.Find("PlayerCanvas").Find("KillTracker"))
                {
                    if (steps.GetStep("EKCT").completed == false)
                    {
                        steps.GetStep("EKCT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("EKCT"), this);
                    }
                }
            }
            catch { }

        }

        //visual
        public override void CheckWeek3()
        {
            try
            {

                GameObject projectile = Resources.Load<GameObject>("Prefabs/Weapons/Spikeball_Small");

                if (FieldHasNonDefaultValue("particlePrefab", "Projectile", typeof(GameObject), null, projectile))
                {
                    if (steps.GetStep("PRHE").completed == false)
                    {
                        steps.GetStep("PRHE").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("PRHE"), this);
                    }
                }
            }
            catch { }

            try
            {


                GameObject projectile = Resources.Load<GameObject>("Prefabs/Weapons/Spikeball_Small");

                if (projectile.transform.Find("RocketTrailEffect"))
                {
                    if (steps.GetStep("PRFP").completed == false)
                    {
                        steps.GetStep("PRFP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("PRFP"), this);
                    }
                }
            }
            catch { }

            try
            {

                GameObject projectile = Resources.Load<GameObject>("Prefabs/Weapons/Spikeball_Small");

                if (FieldHasNonDefaultValue("soundPrefab", "Projectile", typeof(GameObject), null, projectile))
                {
                    if (steps.GetStep("PRHS").completed == false)
                    {
                        steps.GetStep("PRHS").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("PRHS"), this);
                    }
                }
            }
            catch { }
        }

        //scripting
        public override void CheckWeek4()
        {
            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/ExplosiveChest");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("public void Explode()", array, true, false);

                if (ContainsLine("public GameObject explosionPrefab;", script.text) &&
                    ContainsLine("public float explosionDamage;", script.text) &&
                    ContainsLine("public float explosionRange;", script.text) &&
                    ContainsLineInRange("GameObject explosion = Instantiate<GameObject>(explosionPrefab);", array, methodRange, true) &&
                    ContainsLineInRange("explosion.transform.position = transform.position;", array, methodRange, true) &&
                    ContainsLineInRange("explosion.transform.SetParent(null);", array, methodRange, true) &&
                    ContainsLineInRange("GameObject player = GameObject.Find(" + '\u0022' + "Player" + '\u0022' + ");", array, methodRange, true) &&
                    ContainsLineInRange("if(Vector3.Distance(transform.position,player.transform.position) < explosionRange)", array, methodRange, true) &&
                    ContainsLineInRange("player.GetComponent<PlayerStats>().TakeDamage(explosionDamage);", array, methodRange, true) &&
                    ContainsLineInRange("Destroy(gameObject, .25f);", array, methodRange, true))
                {
                    if (steps.GetStep("FKCH").completed == false)
                    {
                        steps.GetStep("FKCH").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("FKCH"), this);
                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/DefendObjective");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public Transform objectToDefend;", script.text) &&
                    ContainsLine("public float maxDistance;", script.text) &&
                    ContainsLine("MeleeEnemy melee;", script.text) &&
                    ContainsLine("NavMeshAgent agent;", script.text) &&
                    ContainsLine("Animator anim;", script.text) &&
                    ContainsLineInRange("agent = GetComponent<NavMeshAgent>();", array, methodRange, true) &&
                    ContainsLineInRange("anim = GetComponent<Animator>();", array, methodRange, true) &&
                    ContainsLineInRange("melee = GetComponent<MeleeEnemy>();", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if(Vector3.Distance(transform.position,objectToDefend.position) > maxDistance)", array, methodRange, true) &&
                        ContainsLineInRange("melee.enabled = false;", array, methodRange, true) &&
                        ContainsLineInRange("agent.SetDestination(objectToDefend.position);", array, methodRange, true) &&
                        ContainsLineInRange("anim.SetBool(" + '\u0022' + "Running" + '\u0022' + ", true);", array, methodRange, true) &&
                        ContainsLineInRange("else if (Vector3.Distance(transform.position,objectToDefend.position) < maxDistance / 3)", array, methodRange, true) &&
                        ContainsLineInRange("if (!melee.enabled)", array, methodRange, true) &&
                        ContainsLineInRange("melee.enabled = true;", array, methodRange, true) &&
                        ContainsLineInRange("agent.SetDestination(transform.position);", array, methodRange, true) &&
                        ContainsLineInRange("anim.SetBool(" + '\u0022' + "Running" + '\u0022' + ", false);", array, methodRange, true))
                    {
                        if (steps.GetStep("EDOB").completed == false)
                        {
                            steps.GetStep("EDOB").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("EDOB"), this);
                        }
                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/EnemySpawner");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public GameObject[] enemyPrefabs;", script.text) &&
                    ContainsLine("public float spawnTime = 10f;", script.text) &&
                    ContainsLine("public float spawnRange = 10f;", script.text) &&
                    ContainsLine("bool spawning = false;", script.text) &&
                    ContainsLine("Transform player;", script.text) &&
                    ContainsLineInRange("player = GameObject.Find(" + '\u0022' + "Player" + '\u0022' + ").transform;", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if(Vector3.Distance(transform.position,player.position) < spawnRange)", array, methodRange, true) &&
                        ContainsLineInRange("if (!spawning)", array, methodRange, true) &&
                        ContainsLineInRange("spawning = true;", array, methodRange, true) &&
                        ContainsLineInRange("StartCoroutine(SpawnEnemy());", array, methodRange, true) &&
                        ContainsLineInRange("if (spawning)", array, methodRange, true) &&
                        ContainsLineInRange("spawning = false;", array, methodRange, true) &&
                        ContainsLineInRange("StopAllCoroutines();", array, methodRange, true))
                    {
                        methodRange = MethodRange("IEnumerator SpawnEnemy()", array, true, false);

                        if (ContainsLineInRange("GameObject newEnemy = Instantiate<GameObject>(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);", array, methodRange, true) &&
                            ContainsLineInRange("newEnemy.transform.position = transform.position;", array, methodRange, true) &&
                            ContainsLineInRange("yield return new WaitForSeconds(spawnTime);", array, methodRange, true) &&
                            ContainsLineInRange("StartCoroutine(SpawnEnemy());", array, methodRange, true))
                        {
                            if (steps.GetStep("ENSP").completed == false)
                            {
                                steps.GetStep("ENSP").completed = true;
                                MonthChecker.AddPoints(steps.GetStep("ENSP"), this);
                            }
                        }
                        
                    }
                }
            }
            catch { }
        }
    }
}