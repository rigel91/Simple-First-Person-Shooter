using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Techability.Systems
{
    public class Month4Check : MonthCheckerAbstract
    {

        public Month4Check()
        {
            scriptableName = "Month4";
            base.EnsureSteps();
        }

        public override void CheckWeek1()
        {
            try
            {
                


                if (steps.GetStep(1, "STEV").completed == false)
                {
                    GameObject castlep = Resources.Load<GameObject>("Environments/Island/Prefabs/Props/barrel");
                    GameObject cavep = Resources.Load<GameObject>("Environments/Desert/Prefabs/Bones/Big_skull");

                    if (castlep || cavep)
                    {
                        steps.GetStep(1, "STEV").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "STEV"), this);
                    }
                }

            }
            catch { }


            try
            {
                if (steps.GetStep(1, "SUPP").completed == false)
                {
                    if (GameObject.Find("Player") && GameObject.Find("PlayerCamera"))
                    {
                        steps.GetStep(1, "SUPP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "SUPP"), this);
                    }
                }

                
            }
            catch { }


            try
            {

                

                if (steps.GetStep(1, "SUGG").completed == false)
                {
                    GameObject gate = GameObject.Find("DungeonGate");

                    if (gate && (GetFieldValue("dungeonSceneName", "DungeonGate", gate) as string != ""))
                    {
                        steps.GetStep(1, "SUGG").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(1, "SUGG"), this);
                    }
                }
                
            }
            catch { }

            try
            {

                if (steps.GetStep(1, "DBSU").completed == false)
                {
                    if (GameObject.FindGameObjectsWithTag("Decoration").Length >= 5)
                    {
                        if (steps.GetStep(1, "DBSU").completed == false)
                        {
                            steps.GetStep(1, "DBSU").completed = true;
                            MonthChecker.AddPoints(steps.GetStep(1, "DBSU"), this);
                        }
                    }
                }
                
            }
            catch { }
        }

        public override void CheckWeek2()
        {
            try
            {
                if (steps.GetStep(2, "CVMK").completed == false)
                {
                    GameObject canvas = GameObject.Find("Player").transform.Find("MinimapCanvas").gameObject;

                    if(canvas && canvas.GetComponentInChildren<Mask>())
                    {
                        steps.GetStep(2, "CVMK").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "CVMK"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(2, "RTRI").completed == false)
                {
                    RenderTexture rt = Resources.Load<RenderTexture>("Materials/MinimapTexture");

                    if(rt != null)
                    {
                        steps.GetStep(2, "RTRI").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "RTRI"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(2, "MCOR").completed == false)
                {
                    GameObject cam = GameObject.Find("Player").transform.Find("MinimapCamera").gameObject;

                    if (cam)
                    {
                        steps.GetStep(2, "MCOR").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "MCOR"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(2, "MMSC").completed == false)
                {
                    TextAsset script = Resources.Load<TextAsset>("Scripts/Created/MinimapCamera");
                    string[] array = null;
                    Vector2Int methodRange;
                    if (script != null)
                    {
                        array = ScriptToArray(script.text);
                    }
                    methodRange = MethodRange("void Start()", array, true, false);

                    if(ContainsLine("Transform player;",script.text) &&
                        ContainsLineInRange("player = transform.parent;", array,methodRange,true) &&
                        ContainsLineInRange("transform.SetParent(null);", array, methodRange, true))
                    {
                        methodRange = MethodRange("void Update()", array, true, false);

                        if (ContainsLineInRange("Vector3 pos = player.position;", array, methodRange, true) &&
                            ContainsLineInRange("pos.y = 20;", array, methodRange, true) &&
                            ContainsLineInRange("transform.position = pos;", array, methodRange, true))
                        {
                            steps.GetStep(2, "MMSC").completed = true;
                            MonthChecker.AddPoints(steps.GetStep(2, "MMSC"), this);
                        }
                    }
                }
            }
            catch { }

        }

        public override void CheckWeek3()
        {
            try
            {
                if (steps.GetStep(3, "MNSC").completed == false)
                {
                    if (SceneManager.GetSceneAt(0).name == "MenuScene")
                    {
                        steps.GetStep(3, "MNSC").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "MNSC"), this);
                    }
                }
             }
            catch { }

            try
            {
                if (steps.GetStep(3, "BGMN").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("Background").GetComponent<Image>())
                    {
                        steps.GetStep(3, "BGMN").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "BGMN"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(3, "TLTX").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("MainMenu").transform.Find("TitleText"))
                    {
                        steps.GetStep(3, "TLTX").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "TLTX"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(3, "BTBB").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("MainMenu").GetComponentsInChildren<Button>().Length == 3)
                    {
                        steps.GetStep(3, "BTBB").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "BTBB"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(3, "BTMS").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("MainMenu").GetComponentInChildren<Button>().onClick.GetPersistentEventCount() > 0)
                    {
                        steps.GetStep(3, "BTMS").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "BTMS"), this);
                    }
                }
            }
            catch { }
        }

        public override void CheckWeek4()
        {
            try
            {
                if (steps.GetStep(4, "LSOB").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("LevelSelect"))
                    {
                        steps.GetStep(4, "LSOB").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "LSOB"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(4, "BTSI").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("LevelSelect").Find("Level1"))
                    {
                        steps.GetStep(4, "BTSI").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "BTSI"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(4, "LVLI").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("LevelSelect").Find("Level1").GetComponentInChildren<Image>())
                    {
                        steps.GetStep(4, "LVLI").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "LVLI"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (steps.GetStep(4, "MNSD").completed == false)
                {
                    if (GameObject.Find("Canvas").transform.Find("LevelSelect").Find("Level1").GetComponent<Button>().onClick.GetPersistentEventCount() > 0)
                    {
                        steps.GetStep(4, "MNSD").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "MNSD"), this);
                    }
                }
            }
            catch { }
        }
    }
}