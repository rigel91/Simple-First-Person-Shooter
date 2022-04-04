using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Techability.Systems
{
    public class Month4ExtraCheck : MonthCheckerExtraAbstract
    {
        public Month4ExtraCheck()
        {
            scriptableName = "Month4Extra";
            base.EnsureSteps();
        }
        //Design
        public override void CheckWeek1()
        {
            try
            {
                if (steps.GetStep("MSDG").completed == false)
                {
                    int count = 0;
                    foreach (GameObject go in FindObjectsOfType<GameObject>())
                    {
                        if (go.name.Contains("DungeonGate"))
                        {
                            count++;
                            
                        }
                    }

                    if(count > 1)
                    {
                        steps.GetStep("MSDG").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("MSDG"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep("CSBT").completed == false)
                {
                    TerrainData castlep = Resources.Load<TerrainData>("Environments/Island/Scenes/CustomBaseTerrain");
                    TerrainData cavep = Resources.Load<TerrainData>("Environments/Desert/Scenes/CustomBaseTerrain");

                    if (castlep || cavep)
                    {
                        steps.GetStep("CSBT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("CSBT"), this);
                    }
                }

            }
            catch { }

            try
            {
                
                if (steps.GetStep("MMPU").completed == false)
                {
                    TextAsset script = Resources.Load<TextAsset>("Scripts/Created/MinimapPickup");
                    string[] array = null;
                    Vector2Int methodRange;
                    if (script != null)
                    {
                        array = ScriptToArray(script.text);
                    }
                    methodRange = MethodRange("void Start()", array, true, false);

                    if(ContainsLine("GameObject minimap;",script.text) &&
                        ContainsLineInRange("minimap = GameObject.Find(" + '\u0022' + "Player" + '\u0022' + ").transform.Find(" + '\u0022' + "Minimap" + '\u0022' + ").gameObject;", array,methodRange,true) &&
                        ContainsLineInRange("minimap.SetActive(false);", array, methodRange, true))
                    {
                        methodRange = MethodRange("private void OnTriggerEnter(Collider other)", array, true, false);

                        if (ContainsLineInRange("if(other.tag == " + '\u0022' + "Player" + '\u0022' + ")", array, methodRange, true) &&
                            ContainsLineInRange("Destroy(gameObject, 0.25f);", array, methodRange, true) &&
                            ContainsLineInRange("minimap.SetActive(true);", array, methodRange, true))
                        {
                            steps.GetStep("MMPU").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("MMPU"), this);
                        }
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
                
                if (steps.GetStep("MPCM").completed == false)
                {
                    GameObject canvas = GameObject.Find("Player").transform.Find("MinimapCanvas").gameObject;

                    if (canvas && canvas.GetComponentInChildren<Text>())
                    {

                        steps.GetStep("MPCM").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("MPCM"), this);
                    }
                }

            }
            catch { }
            try
            {
                
                if (steps.GetStep("ENBP").completed == false)
                {
                    GameObject enemyPref1 = Resources.Load<GameObject>("Environments/Island/Prefabs/Enemies/Boar_Orange");
                    GameObject enemyPref2 = Resources.Load<GameObject>("Environments/Desert/Prefabs/Enemies/Ent_Burning");

                    if(enemyPref1 && enemyPref1.transform.Find("MapBlip"))
                    {
                        steps.GetStep("ENBP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("ENBP"), this);
                    }
                    else if (enemyPref2 && enemyPref2.transform.Find("MapBlip"))
                    {
                        steps.GetStep("ENBP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("ENBP"), this);
                    }
                }
                

            }
            catch { }
            try
            {
                if (steps.GetStep("TRBP").completed == false)
                {
                    GameObject trespref = Resources.Load<GameObject>("Prefabs/Environment/rust_key");

                    if (trespref && trespref.transform.Find("TreasureBlip"))
                    {
                        steps.GetStep("TRBP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("TRBP"), this);
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
                if (steps.GetStep("BTSN").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("MainMenu").Find("LevelSelect").GetComponent<AudioSource>())
                    {
                        steps.GetStep("BTSN").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("BTSN"), this);
                    }
                        
                }
                
            }
            catch { }



            try
            {
                if (steps.GetStep("MNMS").completed == false)
                {
                    if (GameObject.Find("Main Camera").GetComponent<AudioSource>())
                    {
                        steps.GetStep("MNMS").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("MNMS"), this);
                    }
                }
                
            }
            catch { }

            try
            {
                if (steps.GetStep("STGS").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("MainMenu").Find("StartGame").GetComponent<AudioSource>().clip.name == "Confirm")
                    {
                        steps.GetStep("STGS").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("STGS"), this);
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
                if (steps.GetStep("RTMU").completed == false)
                {
                    TextAsset script = Resources.Load<TextAsset>("Scripts/Created/ReturnToMenu");
                    string[] array = null;
                    Vector2Int methodRange;
                    if (script != null)
                    {
                        array = ScriptToArray(script.text);
                    }
                    methodRange = MethodRange("void Update()", array, true, false);

                    if (ContainsLineInRange("if (Input.GetKeyDown(KeyCode.O))", array, methodRange, true) &&
                        ContainsLineInRange("SceneManager.LoadScene(" + '\u0022' + "MenuScene" + '\u0022' + ");", array, methodRange, true))
                    {
                        steps.GetStep("RTMU").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("RTMU"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep("FDFB").completed == false)
                {
                    TextAsset script = Resources.Load<TextAsset>("Scripts/Created/FadeCanvas");
                    string[] array = null;
                    Vector2Int methodRange;
                    if (script != null)
                    {
                        array = ScriptToArray(script.text);
                    }
                    methodRange = MethodRange("void Start()", array, true, false);

                    if (ContainsLine("Image myImage;", script.text) &&
                        ContainsLineInRange("myImage = GetComponent<Image>();", array, methodRange, true) &&
                        ContainsLineInRange("myImage.color = Color.black;", array, methodRange, true))
                    {
                        methodRange = MethodRange("void Update()", array, true, false);

                        if (ContainsLineInRange("if(myImage.color.a > 0)", array, methodRange, true) &&
                            ContainsLineInRange("Color col = myImage.color;", array, methodRange, true) &&
                            ContainsLineInRange("myImage.color = col;", array, methodRange, true) &&
                            ContainsLineInRange("col.a -= Time.deltaTime / 3f;", array, methodRange, true))
                        {

                            steps.GetStep("FDFB").completed = true;
                            MonthChecker.AddPoints(steps.GetStep("FDFB"), this);

                        }
                    }
                }
            }
            catch { }



            try
            {
                if (steps.GetStep("CYBG").completed == false)
                {
                    TextAsset script = Resources.Load<TextAsset>("Scripts/Created/BackgroundCycle");
                    string[] array = null;
                    Vector2Int methodRange;
                    if (script != null)
                    {
                        array = ScriptToArray(script.text);
                    }
                    methodRange = MethodRange("void Start()", array, true, false);

                    if (ContainsLine("public Image backgroundImage;", script.text) &&
                        ContainsLine("public Sprite[] backgrounds;", script.text) &&
                        ContainsLine("public float switchTime = 60f;", script.text) &&
                        ContainsLine("int tracker = 0;", script.text) &&
                        ContainsLineInRange("SetBackground();", array, methodRange, true) &&
                        ContainsLineInRange("StartCoroutine(Cycle());", array, methodRange, true))
                    {

                        methodRange = MethodRange("void SetBackground()", array, true, false);

                        if (ContainsLineInRange("backgroundImage.sprite = backgrounds[tracker];", array, methodRange, true) &&
                            ContainsLineInRange("tracker++;", array, methodRange, true) &&
                            ContainsLineInRange("if (tracker >= backgrounds.Length)", array, methodRange, true) &&
                            ContainsLineInRange("tracker = 0;", array, methodRange, true))
                        {
                            methodRange = MethodRange("IEnumerator Cycle()", array, true, false);

                            if (ContainsLineInRange("yield return new WaitForSeconds(switchTime);", array, methodRange, true) &&
                                ContainsLineInRange("SetBackground();", array, methodRange, true))
                            {
                                steps.GetStep("CYBG").completed = true;
                                MonthChecker.AddPoints(steps.GetStep("CYBG"), this);
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}