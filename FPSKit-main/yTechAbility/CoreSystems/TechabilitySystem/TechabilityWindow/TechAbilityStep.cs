using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Video;
using System;

//Example:

/*

    *H* Header

    *V*MaskedWolf

    Video Player Example

    *B*<Button|https://www.youtube.com/watch?v=MEg-oqI9qmw&ab_channel=MaskedWolf> 

    Button Example

    *I*CodeAwake

    Image Example

 */


namespace Techability.Systems
{
    public class TechAbilityStep : EditorWindow
    {

        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(TechAbilityStep));
        }

        public string instructions;
        private Vector2 scrollPosition;
        public Step step;
        private VideoPlayer vp;
        private float curTime;
        private RenderTexture render;

        /// <summary>
        /// Percentage, IE 100%
        /// </summary>
        public int videoSize;


        public void UpdateReference()
        {
           // step = a.steps.GetStep(step.name);
        }

        public void UpdatePanel()
        {
            try
            {
              //  step = a.steps.GetStep(step.name);
                instructions = step.instructions;
                Repaint();
                Debug.Log(instructions);
            }
            catch { }
        }


        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            //Initialization
            if (render == null)
            {
                render = Resources.Load<RenderTexture>("Videos/Rend");
            }


            if (vp == null)
                vp = FindObjectOfType<VideoPlayer>();

            using (StringReader reader = new StringReader(instructions))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    GUIStyle s = new GUIStyle();
                    s.wordWrap = true;
                    s.fontSize = 14;
                    s.padding.left = 10;
                    s.normal.textColor = Color.white;

                    //Header
                    if (line.Contains("*H*"))
                    {

                        line = line.Replace("*H*", "");

                        GUIStyle style = new GUIStyle();
                        style.fontSize = 22;
                        style.fontStyle = FontStyle.Bold;
                        style.normal.textColor = Color.white;
                        style.padding.left = 10;
                        GUILayout.Label(line, style);

                        GUILayout.Space(20);
                        continue;
                    }
                    if (line.Contains("*CS*"))
                    {
                        //GUILayout.Space(40);
                        GUIStyle st = new GUIStyle();
                        st.alignment = TextAnchor.UpperLeft;
                        st.padding.left = 15;
                        st.padding.top = 15;
                        line = line.Replace("*CS*", "");
                        //if (GUILayout.Button(a.steps._checkSetup, st)) //8
                        //{
                        //    TechAbility t = (TechAbility)EditorWindow.GetWindow(typeof(TechAbility));
                        //    t.CheckSetup();
                        //}
                    }
                    //Button
                    if (line.Contains("*B*"))
                    {

                        line = line.Replace("*B*", "");
                        string buttonLabel = "";
                        string link = GetInstrutions(ref line, ref buttonLabel);

                        if (GUILayout.Button(buttonLabel, GUILayout.Width(300)))
                        {
                            Application.OpenURL(link);
                        }
                    }

                    //Button
                    if (line.Contains("*M*"))
                    {

                        line = line.Replace("*M*", "");
                        string buttonLabel = "";
                        string methodName = GetInstrutions(ref line, ref buttonLabel);

                        if (GUILayout.Button(buttonLabel, GUILayout.Width(300)))
                        {

                        }
                    }

                    //Step
                    if (line.Contains("*S*"))
                    {

                        line = line.Replace("*S*", "");
                        string stepLabel = "";
                        string step = GetStep(ref line, ref stepLabel);
                        GUILayout.BeginHorizontal();
                        GUIStyle style = new GUIStyle();
                        style.fontSize = 15;
                        style.fontStyle = FontStyle.Bold;
                        style.normal.textColor = Color.white;
                        style.alignment = TextAnchor.MiddleLeft;
                        //a.steps.GetStep(a.steps.selectedWeek, step)

                        //try
                        //{
                        //    //Debug.Log("Looking for substep: " + step);
                        //    if (this.step.GetSubStep(step).completed)
                        //    {
                        //        GUILayout.Box(a.steps._checked);
                        //        GUILayout.Label(stepLabel, style, GUILayout.Height(50));
                        //    }
                        //    else
                        //    {
                        //        GUILayout.Box(a.steps._unchecked);
                        //        GUILayout.Label(stepLabel, style, GUILayout.Height(50));
                        //    }
                        //}
                        //catch
                        //{
                        //    GUILayout.Box(a.steps._unchecked);
                        //    GUILayout.Label("", style, GUILayout.Height(50));
                        //    EditorGUILayout.HelpBox("COULD NOT FIND SUBSTEP!", MessageType.Error);
                        //}
                        GUILayout.EndHorizontal();
                    }
                    if (line.Contains("*I*"))
                    {
                        line = line.Replace("*I*", "");
                        try
                        {
                            Texture2D txt = Resources.Load<Texture2D>("Textures/" + line);

                            GUILayout.Box(txt, GUILayout.Width(txt.width), GUILayout.Height(txt.height));
                            continue;
                        }
                        catch
                        {
                            Debug.Log("Could not find");
                        }
                    }
                    if (line.Contains("*V*"))
                    {
                        line = line.Replace("*V*", "");
                        try
                        {
                            //TODO: Test render texture
                            //RenderTexture render = Resources.Load<RenderTexture>("Videos/" + line);
                            /*
                            GUIStyle lBoxStyle = new GUIStyle(GUI.skin.box);
                            lBoxStyle.stretchWidth = true;
                            lBoxStyle.stretchHeight = true;
                            */
                            if (vp.isPlaying && vp.time < vp.length - .1f)
                                curTime = (float)vp.time;

                            //Set Video
                            VideoClip vc = Resources.Load<VideoClip>("Videos/" + line);

                            if (vc == null)
                            {
                                continue;
                            }
                            vp.clip = vc;

                            GUILayout.Box(render, GUILayout.Width(render.width), GUILayout.Height(render.height));


                            GUILayout.Space(10);

                            curTime = EditorGUILayout.Slider((float)curTime, 0, (float)vp.length, GUILayout.Width(render.width));

                            if (!vp.isPlaying)
                                vp.time = curTime;

                            GUILayout.BeginHorizontal();

                            if (GUILayout.Button("Stop", GUILayout.Width(render.width / 3)))
                            {
                                vp.Pause();
                            }
                            if (GUILayout.Button("Play", GUILayout.Width(render.width / 3)))
                            {
                                vp.Play();
                            }
                            if (GUILayout.Button("Rewind", GUILayout.Width(render.width / 3)))
                            {
                                vp.time = 0;
                                curTime = 0;
                            }
                            GUILayout.EndHorizontal();

                            continue;
                        }
                        catch
                        {
                            Debug.Log("Could not find");
                        }
                    }

                    //Image

                    GUILayout.Label(line, s);
                }
            }

            EditorGUILayout.EndScrollView();
        }


        void OnDestroy()
        {
            if (vp != null)
                vp.Stop();
        }
        public void Update()
        {
            if (vp != null && vp.isPlaying)
                Repaint();
        }

        public string GetMethod(ref string s, ref string buttonLabel)
        {
            string method = "";
            int a;
            int a2;
            int b;
            a = s.IndexOf('<');
            a2 = s.IndexOf('|');
            b = s.IndexOf('>');
            //Debug.Log($"{s.Substring(a,(a2-a))}, {s.Substring(a,b-a)}, {s.Substring(a2,b-a2)}");
            //Debug.Log($"a={a} a2={a2} b={b} total={s.Length} error={Mathf.Abs(a2 - b - 1)}");
            buttonLabel = s.Substring(a + 1, (a2 - a - 1));
            method = s.Substring(a2 + 1, Mathf.Abs(a2 - b + 1));
            s = s.Remove(a, Mathf.Abs(a - b - 1));
            return method;
        }

        public string GetInstrutions(ref string s, ref string buttonLabel)
        {
            string inst = "";
            int a;
            int a2;
            int b;
            a = s.IndexOf('<');
            a2 = s.IndexOf('|');
            b = s.IndexOf('>');
            //Debug.Log($"{s.Substring(a,(a2-a))}, {s.Substring(a,b-a)}, {s.Substring(a2,b-a2)}");
            //Debug.Log($"a={a} a2={a2} b={b} total={s.Length} error={Mathf.Abs(a2 - b - 1)}");
            buttonLabel = s.Substring(a + 1, (a2 - a - 1));
            inst = s.Substring(a2 + 1, Mathf.Abs(a2 - b + 1));
            s = s.Remove(a, Mathf.Abs(a - b - 1));
            return inst;
        }

        public string GetStep(ref string s, ref string stepLabel)
        {
            string step = "";
            int a;
            int a2;
            int b;
            a = s.IndexOf('<');
            a2 = s.IndexOf('|');
            b = s.IndexOf('>');
            //Debug.Log($"{s.Substring(a,(a2-a))}, {s.Substring(a,b-a)}, {s.Substring(a2,b-a2)}");
            //Debug.Log($"a={a} a2={a2} b={b} total={s.Length} error={Mathf.Abs(a2 - b - 1)}");
            stepLabel = s.Substring(a + 1, (a2 - a - 1));
            step = s.Substring(a2 + 1, Mathf.Abs(a2 - b + 1));
            //Debug.Log(step);
            s = s.Remove(a, Mathf.Abs(a - b - 1));

            return step;
        }
    }
}