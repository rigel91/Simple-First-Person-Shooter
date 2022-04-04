using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Techability.Systems
{
    public class AdditionalFeatures : EditorWindow
    {
        //Keep to 3 letters
        public static string PROJECT_NAME = "GLF";
        public static int DESCRIPTION_CHAR_LIMIT = 35;
        public static float SIZE_MODIFY = .95f;
        public static bool H_SCROLL_BAR = true;
        public static bool V_SCROLL_BAR = true;

        public static AdditionalFeatures Instance;

        List<string> userMessages = new List<string>();

        [SerializeField]
        public TechAbilityExtraSteps database;
        [SerializeField]
        private string path;

        [MenuItem("TechAbility/Additional Features", priority = 2)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(AdditionalFeatures), false, "Additional Features");
        }
        public Vector2 scrollPosition;

        public void OnProjectChange()
        {
            UpdateStepsReference();
        }

        public float Width()
        {
            return Screen.width * SIZE_MODIFY;
        }


        public void AddPoints(Step s, string codeAddition = "")
        {
            EditorCoroutineUtility.StartCoroutine(_AddPoints(s, codeAddition), this);
        }

        private IEnumerator _AddPoints(Step s, string codeAddition)
        {
            int index = 0;
            foreach (Points p in s.points)
            {
                int cat1 = p.GetSubCategory();
                int points1 = p.subCategoryPoints;
                int coins = s.coins;
                string disc = s.description;

                string activityCode = TechAbility.PROJECT_NAME + "_" + s.name + codeAddition;
                if (activityCode.Length > 16)
                {
                    activityCode = activityCode.Substring(0, 16);
                    activityCode += index.ToString();
                }


                string l = $"https://techability.education/webservice/log_Activity.php?" +
                    $"Student_Id={PlayerPrefs.GetString("studentID")}&" +
                    $"Unity_Activity_Code={activityCode}" +
                    $"&sub_category_ID1={cat1}&" +
                    $"sub_category_ID2={0}&" +
                    $"sub_category_ID3={0}&" +
                    $"points1={points1}&" +
                    $"points2={0}&" +
                    $"points3={0}&" +
                    $"Coins={coins}&" +
                    $"Description={disc}";

                //   Debug.Log($"SubCat1={cat1}, SubCat2={0},SubCat3={0},Points1={points1},Points2=0,Points3=0,coints={coins},desc={disc}");

                // Debug.Log(l);

                UnityWebRequest web = UnityWebRequest.Head(l);

                yield return web.SendWebRequest();
                index++;
            }

        }

        public void UpdateStepsReference()
        {
            database = MonthChecker.GetExtraSteps();
        }

        #region GUI

        void OnGUI()
        {
            if (Instance == null || Instance != this)
            {
                Instance = this;
            }

            if (database == null)
            {
                UpdateStepsReference();
            }


            scrollPosition = GUILayout.BeginScrollView(scrollPosition, H_SCROLL_BAR, V_SCROLL_BAR, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar);

            Color c = GUI.backgroundColor;
            GUI.backgroundColor = Color.clear;

            GUILayout.BeginVertical();

            if (GUILayout.Button(database.gearImage, GUILayout.Width(50), GUILayout.Height(50)))
            {
                database.openSettings = !database.openSettings;
            }
            GUI.backgroundColor = c;
            if (database.openSettings)
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 18;
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.white;

                GUILayout.Label("Screen Scale:      " + (int)(SIZE_MODIFY * 100) + "%", style);
                SIZE_MODIFY = GUILayout.HorizontalSlider(SIZE_MODIFY, .6f, 2, GUILayout.Width(100));
                GUILayout.Space(15);
                H_SCROLL_BAR = EditorGUILayout.Toggle("Horizontal Scroll: ", H_SCROLL_BAR);
                V_SCROLL_BAR = EditorGUILayout.Toggle("Vertical Scroll: ", V_SCROLL_BAR);
                //database.screenScale = GUI.Slider(new Rect(new Vector2(0, 0), new Vector2(50, 50)), database.screenScale, 0, 1);
            }

            // Set to true to bypass login.  Information will not be transferred to/from TechAbility website.
            GUILayout.EndVertical();


            //Setup the style
            /*
            GUIStyle style = new GUIStyle();
            style.fontSize = 22;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.Label("Cosmic Crusaders", style);
            */
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUIStyle s = new GUIStyle();

            if (GUILayout.Button(database.selectedWeek == 1 ? database.designSelected : database.designS, s, GUILayout.Width(Width() / 4), GUILayout.Height(80)))
            { database.selectedWeek = 1; }
            if (GUILayout.Button(database.selectedWeek == 2 ? database.uiSelected : database.uiS, s, GUILayout.Width(Width() / 4), GUILayout.Height(80)))
            { database.selectedWeek = 2; }
            if (GUILayout.Button(database.selectedWeek == 3 ? database.uxSelected : database.uxS, s, GUILayout.Width(Width() / 4), GUILayout.Height(80)))
            { database.selectedWeek = 3; }
            if (GUILayout.Button(database.selectedWeek == 4 ? database.codingSelected : database.codingS, s, GUILayout.Width(Width() / 4), GUILayout.Height(80)))
            { database.selectedWeek = 4; }

            GUILayout.EndHorizontal();

            //GUILayout.Space(40);
            s = new GUIStyle();
            s.alignment = TextAnchor.MiddleCenter;
            //s.padding.left = 50;
            if (GUILayout.Button(database._checkSetup, s, GUILayout.Width(Width()))) //8
            {

                CheckSetup();
                if (EditorWindow.HasOpenInstances<TechAbilityStep>())
                {
                    EditorWindow.GetWindow<TechAbilityStep>().UpdateReference();
                    EditorWindow.GetWindow(typeof(TechAbilityStep)).Repaint();
                }
            }
            GUILayout.Space(10);

            try
            {
                if (database.selectedWeek == 1)
                    DrawWeekOne();
                else if (database.selectedWeek == 2)
                    DrawWeekTwo();
                else if (database.selectedWeek == 3)
                    DrawWeekThree();
                else if (database.selectedWeek == 4)
                    DrawWeekFour();
            }
            catch { }
            //  if (GUILayout.Button("Reset steps"))
            //     cc.ResetSteps();


            EditorGUILayout.EndScrollView();
        }

        private int width = 160, height = 50;

        private void DrawStep(Step s)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 12;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            style.wordWrap = true;
            style.padding.left = 20;
            style.padding.right = 20;
            GUI.backgroundColor = Color.clear;

            GUILayout.BeginHorizontal();
            if (s.completed)
            {
                if (GUILayout.Button(database._completed, GUILayout.Width(width), GUILayout.Height(height))) //8
                {
                    if (!s.customInstructions)
                        Application.OpenURL(s.url);
                    else
                    {
                        //reference.instrution = s.instructions;
                        //reference.selectedStep = s;

                        EditorWindow.GetWindow(typeof(TechAbilityStep));
                        TechAbilityStep t = (TechAbilityStep)EditorWindow.GetWindow(typeof(TechAbilityStep));

                        t.step = s;
                        t.instructions = s.instructions;

                        //t.UpdateReference();
                        //t.UpdatePanel();
                    }
                }
                GUILayout.Label(s.completedText, style, GUILayout.Height(50), GUILayout.Width(Width() - width));
            }
            else
            {
                if (GUILayout.Button(database._incompleted, GUILayout.Width(width), GUILayout.Height(height))) //8
                {
                    if (!s.customInstructions)
                        Application.OpenURL(s.url);
                    else
                    {
                        //reference.instrution = s.instructions;
                        //reference.selectedStep = s;

                        EditorWindow.GetWindow(typeof(TechAbilityStep));
                        TechAbilityStep t = (TechAbilityStep)EditorWindow.GetWindow(typeof(TechAbilityStep));
                        t.step = s;
                        t.instructions = s.instructions;
                        //t.UpdateReference();
                        //t.UpdatePanel();
                    }
                }
                GUILayout.Label(s.incompletedText, style, GUILayout.Height(50), GUILayout.Width(Width() - width));
            }
            GUILayout.EndHorizontal();

            if (s.feedback != null && !s.feedback.Equals("") && s.completed == false)
            {
                GUILayout.Label(s.feedback, style, GUILayout.Height(50));
            }
        }

        private void DrawWeekOne()
        {
            //Setup style
            GUIStyle style = new GUIStyle();
            style.fontSize = 22;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.Space(15);
            if(database.GetDesign().easy.Count > 0)
                GUILayout.Label("---- Easy ----", style);
            //Draw Easy
            foreach (Step s in database.GetDesign().easy)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetDesign().intermediate.Count > 0)
                GUILayout.Label("---- Intermediate ----", style);
            foreach (Step s in database.GetDesign().intermediate)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetDesign().hard.Count > 0)
                GUILayout.Label("---- Hard ----", style);
            foreach (Step s in database.GetDesign().hard)
            {
                DrawStep(s);
            }
        }

        private void DrawWeekTwo()
        {
            //Setup style
            GUIStyle style = new GUIStyle();
            style.fontSize = 22;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.Space(15);
            if (database.GetUI().easy.Count > 0)
                GUILayout.Label("---- Easy ----", style);
            //Draw Easy
            foreach (Step s in database.GetUI().easy)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetUI().intermediate.Count > 0)
                GUILayout.Label("---- Intermediate ----", style);
            foreach (Step s in database.GetUI().intermediate)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetUI().hard.Count > 0)
                GUILayout.Label("---- Hard ----", style);
            foreach (Step s in database.GetUI().hard)
            {
                DrawStep(s);
            }
        }
        private void DrawWeekThree()
        {
            //Setup style
            GUIStyle style = new GUIStyle();
            style.fontSize = 22;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.Space(15);
            if (database.GetVisual().easy.Count > 0)
                GUILayout.Label("---- Easy ----", style);
            //Draw Easy
            foreach (Step s in database.GetVisual().easy)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetVisual().intermediate.Count > 0)
                GUILayout.Label("---- Intermediate ----", style);
            foreach (Step s in database.GetVisual().intermediate)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetVisual().hard.Count > 0)
                GUILayout.Label("---- Hard ----", style);
            foreach (Step s in database.GetVisual().hard)
            {
                DrawStep(s);
            }
        }
        private void DrawWeekFour()
        {
            //Setup style
            GUIStyle style = new GUIStyle();
            style.fontSize = 22;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.Space(15);
            if (database.GetCoding().easy.Count > 0)
                GUILayout.Label("---- Easy ----", style);
            //Draw Easy
            foreach (Step s in database.GetCoding().easy)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetCoding().intermediate.Count > 0)
                GUILayout.Label("---- Intermediate ----", style);
            foreach (Step s in database.GetCoding().intermediate)
            {
                DrawStep(s);
            }
            GUILayout.Space(15);
            if (database.GetCoding().hard.Count > 0)
                GUILayout.Label("---- Hard ----", style);
            foreach (Step s in database.GetCoding().hard)
            {
                DrawStep(s);
            }
        }

        public void CheckSetup()
        {
            if (database.selectedWeek == 1)
                CheckWeek1();
            if (database.selectedWeek == 2)
                CheckWeek2();
            if (database.selectedWeek == 3)
                CheckWeek3();
            if (database.selectedWeek == 4)
                CheckWeek4();
        }

        #endregion

        public void CheckWeek1()
        {
            int i = MonthChecker.GetDateTime().x;
            if (i == 1)
                MonthChecker.month1Extra.CheckWeek1();
            if (i == 2)
                MonthChecker.month2Extra.CheckWeek1();
            if (i == 3)
                MonthChecker.month3Extra.CheckWeek1();
            if (i == 4)
                MonthChecker.month4Extra.CheckWeek1();
        }
        public void CheckWeek2()
        {
            int i = MonthChecker.GetDateTime().x;
            if (i == 1)
                MonthChecker.month1Extra.CheckWeek2();
            if (i == 2)
                MonthChecker.month2Extra.CheckWeek2();
            if (i == 3)
                MonthChecker.month3Extra.CheckWeek2();
            if (i == 4)
                MonthChecker.month4Extra.CheckWeek2();
        }

        public void CheckWeek3()
        {
            int i = MonthChecker.GetDateTime().x;
            if (i == 1)
                MonthChecker.month1Extra.CheckWeek3();
            if (i == 2)
                MonthChecker.month2Extra.CheckWeek3();
            if (i == 3)
                MonthChecker.month3Extra.CheckWeek3();
            if (i == 4)
                MonthChecker.month4Extra.CheckWeek3();
        }

        public void CheckWeek4()
        {
            int i = MonthChecker.GetDateTime().x;
            if (i == 1)
                MonthChecker.month1Extra.CheckWeek4();
            if (i == 2)
                MonthChecker.month2Extra.CheckWeek4();
            if (i == 3)
                MonthChecker.month3Extra.CheckWeek4();
            if (i == 4)
                MonthChecker.month4Extra.CheckWeek4();
        }


        public Step CreateNewStep(string _name = "", string url = "", string incompleteText = "", string completeText = "", List<SubStep> subs = null, List<Points> points = null, int coins = 0)
        {
            Step s = new Step("");
            s.name = _name;
            s.customInstructions = false;
            s.url = url;
            s.incompletedText = incompleteText;
            s.completedText = completeText;
            s.coins = coins;
            return s;
        }

        public Points NewPoints(Points.MainCategory mainCat, Points.SubCategoryDesign design = Points.SubCategoryDesign.Components, Points.SubCategoryFx fx = Points.SubCategoryFx.Animation, Points.SubCategoryScripting script = Points.SubCategoryScripting.ConditionalStatements, Points.SubCategoryUIUX ui = Points.SubCategoryUIUX.Images, int points = 0)
        {
            Points p = new Points();
            p.mainCategory = (Points.MainCategory)mainCat;
            p.subCategoryDesign = design;
            p.subCategoryFx = fx;
            p.subCategoryScript = script;
            p.subCategoryUIUX = ui;
            p.subCategoryPoints = points;
            return p;
        }

    }
}