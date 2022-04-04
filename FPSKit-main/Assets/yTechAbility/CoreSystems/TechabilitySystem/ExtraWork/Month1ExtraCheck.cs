using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Month1ExtraCheck : MonthCheckerExtraAbstract
    {
        public Month1ExtraCheck()
        {
            scriptableName = "Month1Extra";
            base.EnsureSteps();
        }

        //Design
        public override void CheckWeek1()
        {
            try
            {
                GameObject GO = GameObject.Find("Player");
                UnityEditor.Animations.AnimatorController RC = GO.GetComponentInChildren<Animator>().runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
                UnityEditor.Animations.ChildAnimatorState[] AST = RC.layers[0].stateMachine.states;
                UnityEditor.Animations.AnimatorState AS = RC.layers[0].stateMachine.defaultState;
                UnityEditor.Animations.AnimatorStateTransition[] ASTs = RC.layers[0].stateMachine.anyStateTransitions;

                foreach (var ast0 in ASTs)
                {
                    if (ast0.conditions[0].parameter == "Hit")
                    {
                        if (steps.GetStep("FLCH").completed == false)
                        {
                            steps.GetStep("FLCH").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("FLCH"), this);
                        }
                    }
                }
            }
            catch { }

            try
            {
                Material customMat = Resources.Load<Material>("Materials/CustomHairMaterial");

                if (customMat != null)
                {
                    if (steps.GetStep("CRMT").completed == false)
                    {
                        steps.GetStep("CRMT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("CRMT"), this);
                    }
                }
            }
            catch { }

            try
            {
                GameObject torch = GameObject.Find("Torch");

                if (torch && torch.GetComponentInChildren<ParticleSystem>())
                {
                    if (steps.GetStep("TRCH").completed == false)
                    {
                        steps.GetStep("TRCH").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("TRCH"), this);
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
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/CurrentText");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public Text currentStamina;", script.text) &&
                    ContainsLine("public Text currentHealth;", script.text) &&
                    ContainsLine("PlayerStats stats;", script.text) &&
                    ContainsLineInRange("stats = GetComponentInParent<PlayerStats>();", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("currentStamina.text = stats.CurStamina.ToString(" + '\u0022' + "F0" + '\u0022' + ");", array, methodRange, true) &&
                        ContainsLineInRange("currentHealth.text = stats.CurHealth.ToString(" + '\u0022' + "F0" + '\u0022' + ");", array, methodRange, true))
                    {
                        if (GameObject.Find("Player").transform.Find("PlayerCanvas").Find("CurrentHealth") &&
                            GameObject.Find("Player").transform.Find("PlayerCanvas").Find("CurrentStamina"))
                        {
                            if (steps.GetStep("STXT").completed == false)
                            {
                                steps.GetStep("STXT").completed = true;
                                MonthChecker.AddPoints(steps.GetStep("STXT"), this);
                            }
                        }
                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/RegenText");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public Text staminaRegen;", script.text) &&
                    ContainsLine("public Text healthRegen;", script.text) &&
                    ContainsLine("PlayerStats stats;", script.text) &&
                    ContainsLineInRange("stats = GetComponentInParent<PlayerStats>();", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);
                    if (ContainsLineInRange("staminaRegen.text = (stats.MaxStamina / 25f).ToString(" + '\u0022' + "F1" + '\u0022' + ");", array, methodRange, true) &&
                        ContainsLineInRange("healthRegen.text = (stats.MaxHealth / 40f).ToString(" + '\u0022' + "F1" + '\u0022' + ");", array, methodRange, true))
                    {
                        if (GameObject.Find("Player").transform.Find("PlayerCanvas").Find("PlayerHealthSlider").Find("Regen") &&
                            GameObject.Find("Player").transform.Find("PlayerCanvas").Find("PlayerStaminaSlider").Find("Regen"))
                        {
                            if (steps.GetStep("RTXT").completed == false)
                            {
                                steps.GetStep("RTXT").completed = true;
                                MonthChecker.AddPoints(steps.GetStep("RTXT"), this);

                            }
                        }
                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/BonusHealth");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public GameObject bonusHealthObject;", script.text) &&
                    ContainsLine("public Text bonusHealthText;", script.text) &&
                    ContainsLine("PlayerStats stats;", script.text) &&
                    ContainsLineInRange("stats = GetComponentInParent<PlayerStats>();", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if(stats.BonusHealth > 0)", array, methodRange, true) &&
                        ContainsLineInRange("bonusHealthText.text = stats.BonusHealth.ToString(" + '\u0022' + "F0" + '\u0022' + ");", array, methodRange, true) &&
                        ContainsLineInRange("if (!bonusHealthObject.activeSelf)", array, methodRange, true) &&
                        ContainsLineInRange("bonusHealthObject.SetActive(true);", array, methodRange, true) &&
                        ContainsLineInRange("if (bonusHealthObject.activeSelf)", array, methodRange, true) &&
                        ContainsLineInRange("bonusHealthObject.SetActive(false);", array, methodRange, true))
                    {

                        if (GameObject.Find("Player").transform.Find("PlayerCanvas").Find("BonusHealth"))
                        {
                            if (steps.GetStep("BTXT").completed == false)
                            {
                                steps.GetStep("BTXT").completed = true;
                                MonthChecker.AddPoints(steps.GetStep("BTXT"), this);
                            }
                        }
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
                ParticleSystem[] parts = GameObject.Find("Player").GetComponentsInChildren<ParticleSystem>();

                foreach (var part in parts)
                {
                    if (part.gameObject.name.Contains("CharacterCircle"))
                    {
                        if (steps.GetStep("RING").completed == false)
                        {
                            steps.GetStep("RING").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("RING"), this);

                        }
                    }
                }
            }
            catch { }

            try
            {


                ParticleSystem[] parts = GameObject.Find("Player").GetComponentsInChildren<ParticleSystem>();

                foreach (var part in parts)
                {
                    if (part.gameObject.name.Contains("Shockwave"))
                    {
                        TextAsset script = Resources.Load<TextAsset>("Scripts/Created/JumpParticle");
                        string[] array = null;
                        Vector2Int methodRange;
                        if (script != null)
                        {
                            array = ScriptToArray(script.text);
                        }
                        methodRange = MethodRange("void Start()", array, true, false);

                        if (ContainsLine("public ParticleSystem jumpParticles;", script.text) &&
                            ContainsLine("CapsuleCollider col;", script.text) &&
                            ContainsLineInRange("col = GetComponentInChildren<CapsuleCollider>();", array, methodRange, true))
                        {
                            methodRange = MethodRange("void Update()", array, true, false);

                            if (ContainsLineInRange("Vector3 center = transform.position;", array, methodRange, true) &&
                                ContainsLineInRange("center.y += col.height / 2f;", array, methodRange, true) &&
                                ContainsLineInRange("if (Physics.Raycast(center, Vector3.down, col.height / 2f + .1f))", array, methodRange, true) &&
                                ContainsLineInRange("if (Input.GetKeyDown(KeyCode.Space))", array, methodRange, true) &&
                                ContainsLineInRange("jumpParticles.Play();", array, methodRange, true))
                            {
                                if (steps.GetStep("JPPT").completed == false)
                                {
                                    steps.GetStep("JPPT").completed = true;
                                    MonthChecker.AddPoints(steps.GetStep("JPPT"), this);
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            try
            {
                if (FindObjectOfType<UnityEngine.AI.NavMeshAgent>().GetComponentInChildren<ParticleSystem>())
                {
                    if (steps.GetStep("ENDT").completed == false)
                    {
                        steps.GetStep("ENDT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("ENDT"), this);

                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/FootstepSounds");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public float stepDistance = 3f;", script.text) &&
                    ContainsLine("public AudioClip[] footstepSounds;", script.text) &&
                    ContainsLine("AudioSource AS;", script.text) &&
                    ContainsLine("Vector3 previousPos;", script.text) &&
                    ContainsLineInRange("AS = GetComponent<AudioSource>();", array, methodRange, true) &&
                    ContainsLineInRange("previousPos = transform.position;", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if(Vector3.Distance(transform.position,previousPos) > stepDistance)", array, methodRange, true) &&
                        ContainsLineInRange("previousPos = transform.position;", array, methodRange, true) &&
                    ContainsLineInRange("AS.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);", array, methodRange, true))
                    {
                        if (steps.GetStep("STEP").completed == false)
                        {
                            steps.GetStep("STEP").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("STEP"), this);

                        }
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
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/Sneak");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("Animator anim;", script.text) &&
                    ContainsLine("PlayerController PC;", script.text) &&
                    ContainsLineInRange("anim = GetComponentInChildren<Animator>();", array, methodRange, true) &&
                    ContainsLineInRange("PC = GetComponent<PlayerController>();", array, methodRange, true))
                {
                    Debug.Log("1");

                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if (Input.GetKeyDown(KeyCode.LeftControl))", array, methodRange, true) &&
                        ContainsLineInRange("PC.SetMoveSpeed(2f);", array, methodRange, true) &&
                        ContainsLineInRange("if(anim != null)", array, methodRange, true) &&
                        ContainsLineInRange("anim.speed = .5f;", array, methodRange, true) &&
                        ContainsLineInRange("if (Input.GetKeyUp(KeyCode.LeftControl))", array, methodRange, true) &&
                        ContainsLineInRange("PC.SetMoveSpeed(5f);", array, methodRange, true) &&
                        ContainsLineInRange("anim.speed = 1f;", array, methodRange, true))
                    {
                        if (steps.GetStep("SNEK").completed == false)
                        {
                            steps.GetStep("SNEK").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("SNEK"), this);
                        }
                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/Hover");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("PlayerController PC;", script.text) &&
                    ContainsLine("PlayerStats stats;", script.text) &&
                    ContainsLineInRange("PC = GetComponent<PlayerController>();", array, methodRange, true) &&
                    ContainsLineInRange("stats = GetComponent<PlayerStats>();", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if (Input.GetKeyDown(KeyCode.Space) && stats.CurStamina > 1)", array, methodRange, true) &&
                        ContainsLineInRange("PC.fall = false;", array, methodRange, true) &&
                        ContainsLineInRange("if (!PC.fall)", array, methodRange, true) &&
                        ContainsLineInRange("stats.UseStamina(stats.MaxStamina / 2f * Time.deltaTime);", array, methodRange, true) &&
                        ContainsLineInRange("if (Input.GetKeyUp(KeyCode.Space) || stats.CurStamina < 1)", array, methodRange, true) &&
                        ContainsLineInRange("PC.fall = true;", array, methodRange, true))
                    {
                        if (steps.GetStep("HOVR").completed == false)
                        {
                            steps.GetStep("HOVR").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("HOVR"), this);
                        }
                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/CriticalAttack");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public int criticalChance = 10;", script.text) &&
                    ContainsLine("public float criticalMultiplier = 2.5f;", script.text) &&
                    ContainsLine("PlayerController PC;", script.text) &&
                    ContainsLineInRange("PC = GetComponent<PlayerController>();", array, methodRange, true))
                {
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if (Input.GetMouseButtonDown(0))", array, methodRange, true) &&
                        ContainsLineInRange("if (Random.Range(0, criticalChance) == 1)", array, methodRange, true) &&
                        ContainsLineInRange("PC.UseCritAttack(criticalMultiplier);", array, methodRange, true))
                    {
                        if (steps.GetStep("CRIT").completed == false)
                        {
                            steps.GetStep("CRIT").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("CRIT"), this);
                        }
                    }
                }
            }
            catch { }
        }
    }
}