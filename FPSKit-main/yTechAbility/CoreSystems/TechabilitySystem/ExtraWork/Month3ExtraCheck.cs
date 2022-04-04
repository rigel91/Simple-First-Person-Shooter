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

        //Design
        public override void CheckWeek1()
        {
            try
            {
                if (getClassOnPrefab("DrawBridge, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep("DRBD").completed == false)
                    {
                        steps.GetStep("DRBD").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("DRBD"), this);
                    }
                }

            }
            catch { }

            try
            {
                if (getClassOnPrefab("PressurePlate, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep("PRPL").completed == false)
                    {
                        steps.GetStep("PRPL").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("PRPL"), this);
                    }
                }

            }
            catch { }

            try
            {
                if (getClassOnPrefab("DartTrap, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep("DTTP").completed == false)
                    {
                        steps.GetStep("DTTP").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("DTTP"), this);
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
                if (FindGameObjectWithClass("Timer, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep("LVTM").completed == false)
                    {
                        steps.GetStep("LVTM").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("LVTM"), this);
                    }
                }

            }
            catch { }
            try
            {
                if (FindGameObjectWithClass("PauseGame, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep("PSBT").completed == false)
                    {
                        steps.GetStep("PSBT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("PSBT"), this);
                    }
                }

            }
            catch { }
            try
            {
                if (FindGameObjectWithClass("RangeIndicator, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep("RNID").completed == false)
                    {
                        steps.GetStep("RNID").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("RNID"), this);
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


                GameObject objl = GameObject.Find("ObjectiveLights");

                if (objl)
                {
                    if (steps.GetStep("OBLT").completed == false)
                    {
                        steps.GetStep("OBLT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("OBLT"), this);
                    }
                }
            }
            catch { }

           

            try
            {


                GameObject pathl = GameObject.Find("PathLights");

                if (pathl)
                {
                    if (steps.GetStep("PTLT").completed == false)
                    {
                        steps.GetStep("PTLT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("PTLT"), this);
                    }
                }
            }
            catch { }

            try
            {
                if (FindGameObjectWithClass("SunCycle, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"))
                {
                    if (steps.GetStep("DYNT").completed == false)
                    {
                        steps.GetStep("DYNT").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("DYNT"), this);
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
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/Lever");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("void Start()", array, true, false);

                if (ContainsLine("public UnityEvent OnActivate;", script.text) &&
                    ContainsLine("Animator anim;", script.text) &&
                    ContainsLineInRange("anim = GetComponentInChildren<Animator>();", array, methodRange, true))
                {
                    methodRange = MethodRange("private void OnTriggerStay(Collider other)", array, true, false);

                    if (ContainsLineInRange("if(other.tag == " + '\u0022' + "Player" + '\u0022' + ")", array, methodRange, true) &&
                        ContainsLineInRange("if (Input.GetKeyDown(KeyCode.E))", array, methodRange, true) &&
                        ContainsLineInRange("OnActivate.Invoke();", array, methodRange, true) &&
                        ContainsLineInRange("anim.SetBool(" + '\u0022' + "LeverUp" + '\u0022' + ", true);", array, methodRange, true))
                    {
                        methodRange = MethodRange("private void OnTriggerExit(Collider other)", array, true, false);

                        if (ContainsLineInRange("if (other.tag == " + '\u0022' + "Player" + '\u0022' + ")", array, methodRange, true) &&
                            ContainsLineInRange("anim.SetBool(" + '\u0022' + "LeverUp" + '\u0022' + ", false);", array, methodRange, true))
                        {
                            if (steps.GetStep("LEVR").completed == false)
                            {
                                steps.GetStep("LEVR").completed = true;
                                MonthChecker.AddPoints(steps.GetStep("LEVR"), this);
                            }
                        }

                    }
                }
            }
            catch { }

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/RotatingDoor");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("IEnumerator MoveDoor(bool open)", array, true, false);

                if (ContainsLine("public Transform doorTransform;", script.text) &&
                    ContainsLine("bool isOpen = false;", script.text) &&
                    ContainsLineInRange("float desiredAngle = open ? 90 : -90;", array, methodRange, true) &&
                    ContainsLineInRange("float stopTime = Time.time + 1f;", array, methodRange, true) &&
                    ContainsLineInRange("while(Time.time < stopTime)", array, methodRange, true) &&
                    ContainsLineInRange("doorTransform.Rotate(Vector3.up, desiredAngle * Time.deltaTime, Space.World);", array, methodRange, true) &&
                    ContainsLineInRange("yield return new WaitForEndOfFrame();", array, methodRange, true))
                {
                    methodRange = MethodRange("public void OpenDoor()", array, true, false);

                    if (ContainsLineInRange("if (!isOpen)", array, methodRange, true) &&
                        ContainsLineInRange("StartCoroutine(MoveDoor(true));", array, methodRange, true) &&
                        ContainsLineInRange("isOpen = true;", array, methodRange, true))
                    {
                        methodRange = MethodRange("public void CloseDoor()", array, true, false);

                        if (ContainsLineInRange("if (isOpen)", array, methodRange, true) &&
                        ContainsLineInRange("StartCoroutine(MoveDoor(false));", array, methodRange, true) &&
                        ContainsLineInRange("isOpen = false;", array, methodRange, true))
                        {
                            methodRange = MethodRange("private void OnTriggerEnter(Collider other)", array, true, false);

                            if (ContainsLineInRange("if(other.tag == " + '\u0022' + "Player" + '\u0022' + ")", array, methodRange, true) &&
                            ContainsLineInRange("OpenDoor();", array, methodRange, true))
                            {
                                methodRange = MethodRange("private void OnTriggerExit(Collider other)", array, true, false);

                                if (ContainsLineInRange("if(other.tag == " + '\u0022' + "Player" + '\u0022' + ")", array, methodRange, true) &&
                                ContainsLineInRange("CloseDoor();", array, methodRange, true))
                                {
                                    if (steps.GetStep("RTDR").completed == false)
                                    {
                                        steps.GetStep("RTDR").completed = true;
                                        MonthChecker.AddPoints(steps.GetStep("RTDR"), this);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            

            try
            {
                TextAsset script = Resources.Load<TextAsset>("Scripts/Created/CameraUpgrader");
                string[] array = null;
                Vector2Int methodRange;
                if (script != null)
                {
                    array = ScriptToArray(script.text);
                }
                methodRange = MethodRange("private void FixedUpdate()", array, true, false);

                if (ContainsLine("bool objectHit = false;", script.text) &&
                    ContainsLine("public LayerMask LM;", script.text) &&
                    ContainsLine("public PlayerCamera playerCam;", script.text) &&
                    (ContainsLineInRange("if(Physics.Raycast(transform.position,Camera.main.transform.position - transform.position,", array, methodRange, true) &&
                    ContainsLineInRange("out hit,", array, methodRange, true) &&
                    ContainsLineInRange("Vector3.Distance(transform.position,transform.position + playerCam.defaultCamPos),", array, methodRange, true) &&
                    ContainsLineInRange("LM))", array, methodRange, true) ||
                    ContainsLineInRange("if(Physics.Raycast(transform.position,Camera.main.transform.position - transform.position, out hit, Vector3.Distance(transform.position, transform.position + playerCam.defaultCamPos), LM))", array, methodRange, true)) &&
                    ContainsLineInRange("objectHit = true;", array, methodRange, true) &&
                    ContainsLineInRange("playerCam.SetCameraPosition(hit.point);", array, methodRange, true) &&
                    ContainsLineInRange("else if (objectHit)", array, methodRange, true) &&
                    ContainsLineInRange("objectHit = false;", array, methodRange, true) &&
                    ContainsLineInRange("playerCam.ResetCameraPosition();", array, methodRange, true))
                {
                    if (steps.GetStep("IPCM").completed == false)
                    {
                        steps.GetStep("IPCM").completed = true;
                        MonthChecker.AddPoints(steps.GetStep("IPCM"), this);
                    }
                }
            }
            catch { }
        }
    }
}