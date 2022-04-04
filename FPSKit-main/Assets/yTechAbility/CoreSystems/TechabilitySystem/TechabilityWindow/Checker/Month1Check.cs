using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Techability.Systems
{
    public class Month1Check : MonthCheckerAbstract
    {

        public Month1Check()
        {
            scriptableName = "Month1";
            base.EnsureSteps();
        }

        public override void CheckWeek1()
        {
            CheckStepW1S1();
            CheckStepW1S2();
            CheckStepW1S3();

        }

        public void CheckStepW1S1()
        {
            // RKW1_01
            // Has Added their Racer Prefab
            if (steps.GetStep(1, "RKW1_01").completed == true)
            {
                return; 
            }

            try
            {
                if (GameObject.Find("Player Racer"))
                {
                    //Debug.Log("Week 1 - Step 1: Found Player Racer");

                    steps.GetStep(1, "RKW1_01").completed = true;
                    MonthChecker.AddPoints(steps.GetStep(1, "RKW1_01"), this);
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: There's a Racer prefab for you to use! ");
                }
            }
            catch { }

        }

        public void CheckStepW1S2()
        {
            // RKW1_01
            // Increased the speed of the racer 
            if (steps.GetStep(1, "RKW1_02").completed == false)
            {
                try
                {
                    GameObject GO = GameObject.Find("Player Racer");
                    if (GO)
                    {
                        
                        RacerController RC = GO.GetComponentInParent<RacerController>();
                        if (RC)
                        {
                            bool check1 = false;
                            bool check2 = false;
                            bool check3 = false; 

                            if ( RC.maxSpeed >= 40.0f ) 
                            { 
                                check1 = true;  
                            }
                            else
                            {
                                Debug.Log("STEP CHECKER HINT: Try increasing maxSpeed to 40 or more"); 
                            }

                            if ( RC.forwardAccel >= 15.0f ) 
                            { 
                                check2 = true;  
                            }
                            else
                            {
                                Debug.Log("STEP CHECKER HINT: Try increasing forwardAccel to 15 or more");
                            }

                            if (RC.reverseAccel >= 8.0f) 
                            { 
                                check3 = true; 
                            }
                            else
                            {
                                Debug.Log("STEP CHECKER HINT: Try increasing reverseAccel to 15 or more");
                            }

                            if ( check1 && check2 && check3)
                            {
                                Debug.Log("Step2 complete"); 
                                steps.GetStep(1, "RKW1_02").completed = true;
                                MonthChecker.AddPoints(steps.GetStep(1, "RKW1_02"), this);
                            }
                            else
                            {
                              
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("error: " + e.ToString());
                }
            }
           
        }

        public void CheckStepW1S3()
        {
            // RKW1_03
            // Step 3 is completed via playing the game. 
            // See Lap Counter in RacerKit for more info. 
            if (steps.GetStep(1, "RKW1_03").completed == true)
            {
                return;
            }
            else
            {
                Debug.Log("STEP CHECKER HINT: You need to play the game to complete Step 3");
            }

        }

        public override void CheckWeek2()
        {
            CheckStepW2S1();
            CheckStepW2S2();
            CheckStepW2S3();
        }
        public void CheckStepW2S1()
        {
            // RKW2_01
            // Created Their Track Scene
            try
            {

                if (steps.GetStep(2, "RKW2_01").completed == false)
                {
                    //Debug.Log("Check");
                    Scene activelevel = SceneManager.GetActiveScene();
                    //Debug.Log("Check2");
                    if (activelevel.name == "Track")
                    {
                        steps.GetStep(2, "RKW2_01").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "RKW2_01"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: Is your Track Scene open? ");
                    }
                }
                
            }
            catch { }
        }

        public void CheckStepW2S2()
        {
            // RKW2_02
            if (steps.GetStep(2, "RKW2_02").completed == true)
            {
                return; 
            }
            // Tagged the ground plane to the ground layer
            try
            {
                GameObject GO = GameObject.Find("Ground");
               
                if (GO)
                {
                    if ( GO.layer == 6 )
                    {
                        // Debug.Log("YES");
                        
                        steps.GetStep(2, "RKW2_02").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "RKW2_02"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: The Ground needs to be assigned to the layer mask 'ground' #6");
                    }

                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: You need an object named 'Ground' assigned to the ground layer mask (#6)");

                }

            }
            catch
            {

            }
        }

        public void CheckStepW2S3()
        {
            // RKW2_03
            // Has 15 or more Track Pieces in Track
            if (steps.GetStep(2, "RKW2_03").completed == true)
            {
                return;
            }

            //Debug.Log("Test");
            // Tagged the ground plane to the ground layer
            try
            {
                GameObject GO = GameObject.Find("Track");
                //Debug.Log("GO");
                if (GO)
                {
                    //Debug.Log("Test3");
                    Transform[] golist = GO.GetComponentsInChildren<Transform>();
                    if (golist.Length >= 15)
                    {
                        //Debug.Log("YES");

                        steps.GetStep(2, "RKW2_03").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(2, "RKW2_03"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: You need 15 or more track pieces under 'Track'");
                    }

                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: Create an Empty Object name 'Track' to hold your track pieces");

                }

            }
            catch
            {

            }
        }

        public override void CheckWeek3()
        {
            CheckStepW3S1();
            CheckStepW3S2();
            CheckStepW3S3();
            CheckStepW3S4();
        }

        public void CheckStepW3S1()
        {
            // RKW3_01
            // Add the Path Manager
            if (steps.GetStep(3, "RKW3_01").completed == true)
            {
                return;
            }

            try
            {
                if (GameObject.Find("PathManager"))
                {
                    //Debug.Log("Week 3 - Step 1: PathManager");

                    steps.GetStep(3, "RKW3_01").completed = true;
                    MonthChecker.AddPoints(steps.GetStep(3, "RKW3_01"), this);
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: There's a Prefab created for the Path Manager");
                }
            }
            catch { }

        }

        public void CheckStepW3S2()
        {
            // RKW3_02
            // Added Path Points to the Scene
            if (steps.GetStep(3, "RKW3_02").completed == true)
            {
                return;
            }

            //Debug.Log("Test");
            // Tagged the ground plane to the ground layer
            try
            {
                PathPoint[] pointlist = GameObject.FindObjectsOfType<PathPoint>();
                
                if (pointlist.Length >= 5)
                {
                    Debug.Log("YES");

                    steps.GetStep(3, "RKW3_02").completed = true;
                    MonthChecker.AddPoints(steps.GetStep(3, "RKW3_02"), this);
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: You need 15 or more track pieces under 'Track'");
                }


            }
            catch
            {

            }
        }

        public void CheckStepW3S3()
        {
            // RKW3_03
            // Added the Path Points to the Manager
            if (steps.GetStep(3, "RKW3_03").completed == true)
            {
                return;
            }

            try
            {
                PathManager PM = GameObject.FindObjectOfType<PathManager>();
                if (PM)
                {
                    if (PM.pathPoints.Count >= 5)
                    {
                        //Debug.Log("Week 3 - Step 3: PathManager");

                        steps.GetStep(3, "RKW3_03").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "RKW3_03"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: You need to assign your path points to the 'pathPoints' list in the Path Manager");
                    }

                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: There's a Prefab created for the Path Manager");
                }
            }
            catch { }


        }

        public void CheckStepW3S4()
        {
            // RKW3_04
            // Has 15 or more Pieces of decorations
            if (steps.GetStep(3, "RKW3_04").completed == true)
            {
                return;
            }

            //Debug.Log("Test");
            // Tagged the ground plane to the ground layer
            try
            {
                GameObject GO = GameObject.Find("OffTrackDecorations");
                //Debug.Log("GO");
                if (GO)
                {
                    //Debug.Log("Test3");
                    Transform[] golist = GO.GetComponentsInChildren<Transform>();
                    if (golist.Length >= 15)
                    {
                        //Debug.Log("YES");

                        steps.GetStep(3, "RKW3_04").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(3, "RKW3_04"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: You need 15 or more pieces under 'OffTrackDecorations'");
                    }

                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: Create an Empty Object name 'OffTrackDecorations' to hold your track pieces");
                }

            }
            catch
            {

            }
        }

        public override void CheckWeek4()
        {
            CheckStepW4S1();
            CheckStepW4S2();
            CheckStepW4S3();
        }

        public void CheckStepW4S1()
        {
            // RKW4_01
            // Add Ai Racer
            if (steps.GetStep(4, "RKW4_01").completed == true)
            {
                return;
            }

            try
            {
                if (GameObject.Find("AI Racer"))
                {
                    //Debug.Log("Week 3 - Step 1: PathManager");

                    steps.GetStep(4, "RKW4_01").completed = true;
                    MonthChecker.AddPoints(steps.GetStep(4, "RKW4_01"), this);
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: There's a Prefab created for the AI Racer");
                }
            }
            catch { }



        }

        public void CheckStepW4S2()
        {
            // RKW4_02
            // Add Ai Racer
            if (steps.GetStep(4, "RKW4_02").completed == true)
            {
                return;
            }

            try
            {
                AiRacer ai = GameObject.FindObjectOfType<AiRacer>(); 
                if (ai)
                {
                    if (ai.pathManager != null)
                    {
                        steps.GetStep(4, "RKW4_02").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "RKW4_02"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: You Need to assign a path manager to your AI Racer");
                    }
                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: There's a Prefab created for the AI Racer");
                }
            }
            catch { }
        }

        public void CheckStepW4S3()
        {
            // Has 15 or more Pieces of Physics
            if (steps.GetStep(4, "RKW4_03").completed == true)
            {
                return;
            }

            //Debug.Log("Test");
            // Tagged the ground plane to the ground layer
            try
            {
                GameObject GO = GameObject.Find("Physics");
                //Debug.Log("GO");
                if (GO)
                {
                    //Debug.Log("Test3");
                    Rigidbody[] golist = GO.GetComponentsInChildren<Rigidbody>();
                    if (golist.Length >= 7)
                    {
                        //Debug.Log("YES");

                        steps.GetStep(4, "RKW4_03").completed = true;
                        MonthChecker.AddPoints(steps.GetStep(4, "RKW4_03"), this);
                    }
                    else
                    {
                        Debug.Log("STEP CHECKER HINT: You need 7 or more objects with RigidBodies pieces under 'Physics'");
                    }

                }
                else
                {
                    Debug.Log("STEP CHECKER HINT: Create an Empty Object name 'Physics' to hold your track pieces with rigidbodies");

                }

            }
            catch
            {

            }
        }
    }
}