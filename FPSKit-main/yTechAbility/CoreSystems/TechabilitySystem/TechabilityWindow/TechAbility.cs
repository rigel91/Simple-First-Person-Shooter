using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.AI;
using System.Reflection;
using UnityEditor.Animations;
using UnityEngine.SceneManagement;
using Unity.EditorCoroutines;
using Unity.EditorCoroutines.Editor;

namespace Techability.Systems
{
    public class TechAbility : EditorWindow
    {
        bool bypassLogin;

        private Vector2Int ifRange;
        private MonoBehaviour[] gunComps;
        //Keep to 3 letters
        public static string PROJECT_NAME = "GLF";
        public static int DESCRIPTION_CHAR_LIMIT = 35;
        public static float SIZE_MODIFY = .95f;
        public static bool H_SCROLL_BAR = true;
        public static bool V_SCROLL_BAR = true;

        public static TechAbility Instance;

        public string codeEnter;


        List<string> userMessages = new List<string>();

        [SerializeField]
        public TechAbilityStepsScriptable database;
        [SerializeField]
        private string path;

        [MenuItem("TechAbility/Tech Ability", priority =2)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(TechAbility), false, "Techability");
        }
        public Vector2 scrollPosition;

        [MenuItem("MyMenu/SelectMissing")]
        static void SelectMissing(MenuCommand command)
        {
            Transform[] ts = FindObjectsOfType<Transform>();
            List<GameObject> selection = new List<GameObject>();
            foreach (Transform t in ts)
            {
                Component[] cs = t.gameObject.GetComponents<Component>();
                foreach (Component c in cs)
                {
                    if (c == null)
                    {
                        DestroyImmediate(c);
                        selection.Add(t.gameObject);
                    }
                }
            }
            Selection.objects = selection.ToArray();
        }

        public void OnProjectChange()
        {
            UpdateStepsReference();
        }

        public float Width()
        {
            return Screen.width * SIZE_MODIFY;
        }


        #region Techability.Reference
        public void Login()
        {
            EditorCoroutineUtility.StartCoroutine(login(), this);
        }

        private IEnumerator login()
        {
            if (database.email.Equals("Teacher") && database.password.Equals("Password"))
            {
                database.loggedIn = true;
                database.isTeacher = true;
            }
            else
            {
                database.isTeacher = false;
                WWWForm form = new WWWForm();

                UnityWebRequest web = UnityWebRequest.Get($"https://techability.education/webservice/login_unity.php?email={database.email}&password={database.password}");

                yield return web.SendWebRequest();

                string results = web.downloadHandler.text;

                Debug.Log(results);

                if (results.Trim().Equals("\"ERROR\"") || results.Equals("0"))
                {
                    Debug.Log("Incorrect password entered");
                }
                else
                {
                    try
                    {
                        // ["301804040404",1,26]
                        //ID
                        //Days per week
                        //Days since start
                        results = results.Replace("\"", "");
                        results = results.Replace("]", "");
                        results = results.Replace("[", "");
                        string[] tokens = results.Split(',');
                        results = results.Trim();
                        Debug.Log(results);
                        DateTime d = DateTime.Now.AddDays(-int.Parse(tokens[2]));

                        database.studentID = tokens[0];
                        database.classesPerWeek = int.Parse(tokens[1]);
                        database.startDate = new string[] { d.Year.ToString(), d.Month.ToString(), d.Day.ToString() };
                        PlayerPrefs.SetString("studentID", tokens[0].Trim());
                        database.loggedIn = true;
                    }
                    catch
                    {
                        database.loggedIn = false;
                    }

                }
                //Get first class date

                //Every seven days after, unlock next week
            }

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
                    $"Student_Id={database.studentID.Trim()}&" +
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

        #endregion

        public void UpdateStepsReference()
        {
            database = MonthChecker.GetSteps();
        }

        #region GUI

        void OnGUI()
        {
            if(Instance == null || Instance != this)
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

                GUILayout.Space(15);
                if (GUILayout.Button("Logout", GUILayout.Width(Width() / 2)))
                {
                    database.email = "";
                    database.password = "";
                    database.loggedIn = false;
                }
                //database.screenScale = GUI.Slider(new Rect(new Vector2(0, 0), new Vector2(50, 50)), database.screenScale, 0, 1);
            }

            // Set to true to bypass login.  Information will not be transferred to/from TechAbility website.
            bypassLogin = false;
            GUILayout.EndVertical();


            //Setup the style
            /*
            GUIStyle style = new GUIStyle();
            style.fontSize = 22;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.Label("Cosmic Crusaders", style);
            */

            if(database.isTeacher && GUILayout.Button("Open Teacher Doc", GUILayout.Width(250)))
            {
                Application.OpenURL("https://techability.online/Information/Gear%201.pdf");
            }

            GUILayout.Box(database.headerImage, GUILayout.Width(Width()));


            GUILayout.Space(20);

            if (bypassLogin)
            {
                database.loggedIn = false;
                GUILayout.Label("TechAbility Offline Version");
            }
            else
            {
                if (!database.loggedIn)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Email : ");
                    database.email = GUILayout.TextField(database.email);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Password : ");
                    database.password = EditorGUILayout.PasswordField(database.password);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Login"))
                    {
                        Login();
                        //database.loggedIn = true;
                        //string id = "Dungeon Escape48";
                    }
                    EditorGUILayout.EndScrollView();
                    return;
                }
            }

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUIStyle s = new GUIStyle();

            if (GUILayout.Button(database.selectedWeek == 1 ? database.week1Selected : database.week1, s, GUILayout.Width(Width() / 4), GUILayout.Height(60)))
            { database.selectedWeek = 1; }
            if (GUILayout.Button(database.selectedWeek == 2 ? database.week2Selected : database.week2, s, GUILayout.Width(Width() / 4), GUILayout.Height(60)))
            { database.selectedWeek = 2; }
            if (GUILayout.Button(database.selectedWeek == 3 ? database.week3Selected : database.week3, s, GUILayout.Width(Width() / 4), GUILayout.Height(60)))
            { database.selectedWeek = 3; }
            if (GUILayout.Button(database.selectedWeek == 4 ? database.week4Selected : database.week4, s, GUILayout.Width(Width() / 4), GUILayout.Height(60)))
            { database.selectedWeek = 4; }

            GUILayout.EndHorizontal();

            //GUILayout.Space(40);
            s = new GUIStyle();
            s.alignment = TextAnchor.MiddleCenter;
            //s.padding.left = 50;
            if ((WeekUnlocked(database.selectedWeek)) && GUILayout.Button(database._checkSetup, s, GUILayout.Width(Width()))) //8
            {
                AddWeek1();
                AddWeek2();
                AddWeek3();
                AddWeek4();

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
                if (database.selectedWeek == 1 && WeekUnlocked(1))
                    DrawWeekOne();
                else if (database.selectedWeek == 2 && (WeekUnlocked(2)))
                    DrawWeekTwo();
                else if (database.selectedWeek == 3 && (WeekUnlocked(3)))
                    DrawWeekThree();
                else if (database.selectedWeek == 4 && (WeekUnlocked(4)))
                    DrawWeekFour();
                else
                {
                    DrawLocked();
                }
            }
            catch { }
            //  if (GUILayout.Button("Reset steps"))
            //     cc.ResetSteps();


            EditorGUILayout.EndScrollView();
        }

        public bool WeekUnlocked(int week)
        {
            Vector3Int time = MonthChecker.GetDateTime();
            if (time.y >= week)
            {
                return true;
            }
            return false;
        }

        private void DrawWeekOne()
        {
            foreach (Step s in database.GetWeek1())
            {
                DrawStep(s);
            }
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

        private void DrawWeekTwo()
        {
            foreach (Step s in database.GetWeek2())
            {
                DrawStep(s);
            }
        }
        private void DrawWeekThree()
        {
            foreach (Step s in database.GetWeek3())
            {
                DrawStep(s);
            }
        }
        private void DrawWeekFour()
        {
            foreach (Step s in database.GetWeek4())
            {
                DrawStep(s);
            }
        }

        public void DrawLocked()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.Label($"Week {database.selectedWeek} is locked, it will unlock automatically.", style);
            style.fontSize = 12;
            GUILayout.Space(15);
            
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
                MonthChecker.month1.CheckWeek1();
            if (i == 2)
                MonthChecker.month2.CheckWeek1();
            if (i == 3)
                MonthChecker.month3.CheckWeek1();
            if (i == 4)
                MonthChecker.month4.CheckWeek1();
        }
        public void CheckWeek2()
        {
            int i = MonthChecker.GetDateTime().x;
            if (i == 1)
                MonthChecker.month1.CheckWeek2();
            if (i == 2)
                MonthChecker.month2.CheckWeek2();
            if (i == 3)
                MonthChecker.month3.CheckWeek2();
            if (i == 4)
                MonthChecker.month4.CheckWeek2();
        }

        public void CheckWeek3()
        {
            int i = MonthChecker.GetDateTime().x;
            if (i == 1)
                MonthChecker.month1.CheckWeek3();
            if (i == 2)
                MonthChecker.month2.CheckWeek3();
            if (i == 3)
                MonthChecker.month3.CheckWeek3();
            if (i == 4)
                MonthChecker.month4.CheckWeek3();
        }

        public void CheckWeek4()
        {
            int i = MonthChecker.GetDateTime().x;
            if (i == 1)
                MonthChecker.month1.CheckWeek4();
            if (i == 2)
                MonthChecker.month2.CheckWeek4();
            if (i == 3)
                MonthChecker.month3.CheckWeek4();
            if (i == 4)
                MonthChecker.month4.CheckWeek4();
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


        //-------------------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------------------------------------//
        //                                          CHECK STEPS BELOW THIS POINT                                                   //
        //-------------------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------------------------------------//
        public void AddWeek1()
        {


        }

        public void AddWeek2()
        {

        }

        public void AddWeek3()
        {

        }

        public void AddWeek4()
        {

        }
    }
}