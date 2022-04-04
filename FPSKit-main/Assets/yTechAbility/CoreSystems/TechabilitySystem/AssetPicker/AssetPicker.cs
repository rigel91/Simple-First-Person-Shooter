using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Techability.Systems
{

    public class AssetPicker : EditorWindow
    {
        public static float SIZE_MODIFY = .95f;
        public static bool H_SCROLL_BAR = true;
        public static bool V_SCROLL_BAR = true;

        public static AssetPickerScriptable db;

        private int tab;

        [MenuItem("TechAbility/Asset Picker", priority =0)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(AssetPicker), false, "Asset Picker");
        }

        public Vector2 scrollPosition;

        public float Width()
        {
            return Screen.width * SIZE_MODIFY;
        }
        public static string cmd;
        private void OnGUI()
        {
            //Grab important scriptable objects before doing anything
            if (db == null)
            {
                db = (AssetPickerScriptable)Resources.Load("AssetPickerScriptable");
                db.Refresh();
                //If for some reason there is nothing to grab, notify user.
                if (db == null)
                {
                    Debug.LogError("Could not find AssetPickerScriptable. Please contact a teacher.");
                    return;
                }
            }

            //Setup scroll
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, H_SCROLL_BAR, V_SCROLL_BAR, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar);

            //Settings
            Color c = GUI.backgroundColor;
            GUI.backgroundColor = Color.clear;

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button(db.gearImage, GUILayout.Width(50), GUILayout.Height(50)))
            {
                db.openSettings = !db.openSettings;
            }
            GUIStyle header = new GUIStyle();
            header.fontSize = 64;
            header.fontStyle = FontStyle.Bold;
            header.normal.textColor = Color.white;
            header.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(TabName(), header);

            GUILayout.EndHorizontal();
            GUI.backgroundColor = c;
            if (db.openSettings)
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
                //database.screenScale = GUI.Slider(new Rect(new Vector2(0, 0), new Vector2(50, 50)), database.screenScale, 0, 1);
            }
            GUILayout.EndVertical();

            //CMD
            GUILayout.BeginHorizontal();
            GUILayout.Label("Command Prompt");
            cmd = GUILayout.TextField(cmd);
            if (GUILayout.Button("Run"))
            {
                RunCMD();
            }
            try
            {
                if (Event.current.keyCode == KeyCode.Return)
                {
                    Debug.Log("Enter");
                    if (cmd != "")
                    {
                        RunCMD();
                    }
                }
            }
            catch
            {

            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUIStyle s = new GUIStyle();
            s.fixedHeight = (Width() / 3) / 4;
            s.fixedWidth = Width() / 3;

            if (tab == 0)
                s.normal.background = db.selectBackground;
            else
                s.normal.background = null;
            if (GUILayout.Button(db.GetEnvironmentTexture(), s))
            {
                tab = 0;
            }
            if (tab == 1)
                s.normal.background = db.selectBackground;
            else
                s.normal.background = null;
            if (GUILayout.Button(db.GetEnemiesTexture(), s))
            {
                tab = 1;
            }
            if (tab == 2)
                s.normal.background = db.selectBackground;
            else
                s.normal.background = null;
            if (GUILayout.Button(db.GetCharactersTexture(), s))
            {
                tab = 2;
            }

            GUILayout.EndHorizontal();

            switch (tab)
            {
                case 0:
                    DrawEnvonment();
                    break;
                case 1:
                    DrawEnemies();
                    break;
                case 2:
                    DrawCharacter();
                    break;
            }


            GUILayout.EndScrollView();
        }

        public void RunCMD()
        {
            string s = cmd.ToLower();
            Debug.Log("Ran code: " + cmd);
            if(s.Equals("refresh"))
            {
                db.Refresh();
            }

            if(s.Equals("unload char"))
            {
                db.assetManager.character = false;
                Debug.Log("Unloading Char");
            }
            
            if (s.Equals("delete envo"))
            {
                db.assetManager.DeleteEnvoCMD();
                Debug.Log("Unloading envo");
            }

            try
            {
                string[] tokens = s.Split(' ');
                if (tokens[0].Equals("setdate"))
                {
                    try
                    {
                        int y = int.Parse(tokens[1]);
                        int m = int.Parse(tokens[2]);
                        int d = int.Parse(tokens[3]);
                        PlayerPrefs.SetInt("year", y);
                        PlayerPrefs.SetInt("month", m);
                        PlayerPrefs.SetInt("day", d);
                        Debug.Log($"Date set to: \nYear{y}, \nMonth: {m},\nDay: {d}");
                    }
                    catch
                    {
                        Debug.Log("Error setting offset, could not parse int.");
                    }
                }
                else
                {

                }
            }
            catch
            {

            }


            try
            {
                string[] tokens = s.Split(' ');
                if (tokens[0].Equals("date") &&
                    tokens[1].Equals("method"))
                {
                    if(tokens[2].Equals("default"))
                    {
                        PlayerPrefs.SetInt("DateO", 0);
                        Debug.Log("Date time method is now set to 'Default'");
                    }
                    else if(tokens[2].Equals("offset"))
                    {
                        PlayerPrefs.SetInt("DateO", 1);
                        Debug.Log("Date time method is now set to 'Offset'");
                    }
                    else if(tokens[2].Equals("set"))
                    {
                        PlayerPrefs.SetInt("DateO", 2);
                        Debug.Log($"Date time method is now set to 'Set' {PlayerPrefs.GetInt("DateO")}");
                    }
                    else
                    {
                        Debug.LogError("Unknown date method: " + tokens[3]);
                    }
                }
                else
                {
                }
            }
            catch(Exception E)
            {
            }

            try
            {
                string[] tokens = s.Split(' ');
                if(tokens[0].Equals("offset"))
                {
                    try
                    {
                        int amount = int.Parse(tokens[1]);
                        PlayerPrefs.SetInt("DayOffset", amount);
                        Debug.Log("Day offset set to: " + amount);
                    }catch
                    {
                        Debug.Log("Error setting offset, could not parse int.");
                    }
                }else
                {
                        
                }
            }
            catch
            {

            }

            if(s.Equals("debug date"))
            {
                Vector3Int dt = MonthChecker.GetDateTime();
                Debug.Log($"Month: {dt.x}\nWeek: {dt.y}\nDay: {dt.x}");
            }
            if (s.Equals("debug offset"))
            {
                Debug.Log("Offset to: " + PlayerPrefs.GetInt("DayOffset"));
            }
            if (s.Equals("debug set date"))
            {
                Debug.Log($"Date set to: \nYear{PlayerPrefs.GetInt("year")},\nMonth: {PlayerPrefs.GetInt("month")}\nDay: {PlayerPrefs.GetInt("day")}");
            }
            if(s.Equals("debug date method"))
            {
                if(PlayerPrefs.GetInt("DateO") == 2)
                {
                    Debug.Log("Date Method: Set");
                }
                else if (PlayerPrefs.GetInt("DateO") == 1)
                {
                    Debug.Log("Date Method: Offset");
                }
                else
                {
                    Debug.Log("Date Method: Default");
                }
            }

            cmd = "";
        }

        private string TabName()
        {
            if (tab == 0)
                return "Enviroment";
            if (tab == 1)
                return "Enemies";
            if (tab == 2)
                return "Characters";
            //If tab messed up, reset and call again.
            tab = 0;
            return TabName();
        }

        private void DrawEnvonment()
        {
            if (!db.CanPickEnvironment())
            {
                GUIStyle header = new GUIStyle();
                header.fontSize = 36;
                header.wordWrap = true;
                header.fontStyle = FontStyle.Bold;
                header.normal.textColor = Color.white;
                header.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("You have already picked an environment.\nPlease wait until next month.", header);
                return;
            }

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            //Left Image
            GUILayout.Box(db.EnvironmentOptions()[0], GUILayout.Width(Width() / 2), GUILayout.Height((Width() / 2) / 1.95f));
            if (GUILayout.Button("Pick", GUILayout.Width(100)))
            {
                PackageInstaller.InstallEnvironment(db.EnvironmentNames()[0]);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            //Right Image
            GUILayout.Box(db.EnvironmentOptions()[1], GUILayout.Width((Width() / 2)), GUILayout.Height((Width() / 2) / 1.95f));
            if (GUILayout.Button("Pick", GUILayout.Width(100)))
            {
                PackageInstaller.InstallEnvironment(db.EnvironmentNames()[0]);
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void DrawEnemies()
        {
            
            if (!db.CanPickEnemy())
            {
                GUIStyle header = new GUIStyle();
                header.fontSize = 36;
                header.wordWrap = true;
                header.fontStyle = FontStyle.Bold;
                header.normal.textColor = Color.white;
                header.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("You have already picked an enemy.\nPlease wait until next week.", header);
                return;
            }else
            {
                GUIStyle header = new GUIStyle();
                header.fontSize = 24;
                header.wordWrap = true;
                header.fontStyle = FontStyle.Bold;
                header.normal.textColor = Color.white;
                header.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("Please click on the enemy you wish to use.", header);
            }

            Texture2D[] options = db.EnemyOptions();
            string[] names = db.EnemyNames();

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button(options[0], GUILayout.Height(Width() / 3), GUILayout.Width(Width()/3)))
            {
                PackageInstaller.InstallEnemy(names[0]);
            }
            if (GUILayout.Button(options[1], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[1]);
            }
            if (GUILayout.Button(options[2], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[2]);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(options[3], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[3]);
            }
            if (GUILayout.Button(options[4], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[4]);
            }
            if (GUILayout.Button(options[5], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[5]);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(options[6], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[6]);
            }
            if (GUILayout.Button(options[7], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[7]);
            }
            if (GUILayout.Button(options[8], GUILayout.Width(Width() / 3), GUILayout.Height(Width() / 3)))
            {
                PackageInstaller.InstallEnemy(names[8]);
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

        }
        public static void Refresh()
        {
            db.Refresh
                ();
        }

        private void DrawCharacter()
        {
            if(MonthChecker.GetDateTime().x != 1)
            {
                GUIStyle header = new GUIStyle();
                header.fontSize = 36;
                header.wordWrap = true;
                header.fontStyle = FontStyle.Bold;
                header.normal.textColor = Color.white;
                header.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("You can only switch player models on player month.\n(March, July, November)", header);
                return;
            }
            if (!db.CanPickCharacter())
            {
                GUIStyle header = new GUIStyle();
                header.fontSize = 36;
                header.wordWrap = true;
                header.fontStyle = FontStyle.Bold;
                header.normal.textColor = Color.white;
                header.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("You have already picked a character.\nContact teacher if you wish to switch.", header);
                return;
            }


            GUILayout.BeginHorizontal();
            if(GUILayout.Button(db.female[0], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Female1");
            }
            if (GUILayout.Button(db.female[1], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Female2");
            }
            if (GUILayout.Button(db.female[2], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Female3");
            }
            if (GUILayout.Button(db.female[3], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Female4");
            }
            if (GUILayout.Button(db.female[4], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Female5");
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(db.male[0], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Male1");
            }
            if (GUILayout.Button(db.male[1], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Male2");
            }
            if (GUILayout.Button(db.male[2], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Male3");
            }
            if (GUILayout.Button(db.male[3], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Male4");
            }
            if (GUILayout.Button(db.male[4], GUILayout.Width(Width() / 5), GUILayout.Height(Width() / 2.5f)))
            {
                PackageInstaller.InstallCharacter("Male5");
            }
            GUILayout.EndHorizontal();
        }

    }
}