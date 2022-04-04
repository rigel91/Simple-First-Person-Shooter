using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Techability.Systems
{
    [CustomEditor(typeof(TechAbilityExtraSteps))]
    public class TechAbilityExtraStepsEditor : Editor
    {
        public TechAbilityExtraSteps ta;
        public int a;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (ta == null)
                ta = (TechAbilityExtraSteps)target;

            EditorUtility.SetDirty(ta);

            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Draw default?", style);
            ta.drawDefault = EditorGUILayout.Toggle(ta.drawDefault);

            GUILayout.EndHorizontal();

            if (ta.drawDefault)
            {
                base.OnInspectorGUI();
                return;
            }

            style.fontSize = 12;

            GUILayout.Space(10);

            string[] texts = { "Design", "UI", "VX", "Coding" };
            a = GUILayout.Toolbar(a, texts);
            Draw();
            serializedObject.ApplyModifiedProperties();
        }

        public void Draw()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            style.fontSize = 25;
            style.alignment = TextAnchor.MiddleCenter;

            GUILayout.Label("Easy Steps", style);

            //Easy
            GUILayout.Space(10);
            if (GUILayout.Button("Add New Easy Step"))
            {
                GetWeekSteps().easy.Add(new Step("New Step"));
            }
            GUILayout.Space(10);

            foreach (Step s in GetWeekSteps().easy)
            {
                DrawStep(s);
            }

            GUILayout.Label("Intermediate Steps", style);

            //Intermediate
            GUILayout.Space(10);
            if (GUILayout.Button("Add New Intermediate Step"))
            {
                GetWeekSteps().intermediate.Add(new Step("New Step"));
            }
            GUILayout.Space(10);

            foreach (Step s in GetWeekSteps().intermediate)
            {
                DrawStep(s);
            }

            GUILayout.Label("Hard Steps", style);
            //Hard
            GUILayout.Space(10);
            if (GUILayout.Button("Add New Hard Step"))
            {
                GetWeekSteps().hard.Add(new Step("New Step"));
            }
            GUILayout.Space(10);

            foreach (Step s in GetWeekSteps().hard)
            {
                DrawStep(s);
            }

        }

        public StepCategory GetWeekSteps()
        {
            switch (a)
            {
                case 0:
                    return ta.design;
                case 1:
                    return ta.UI;
                case 2:
                    return ta.visual;
                case 3:
                    return ta.coding;
            }
            return null;
        }

        public void DrawStep(Step s)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.BeginHorizontal();
            s.name = EditorGUILayout.TextField("Name: ", s.name);
            if (GUILayout.Button("Delete Step"))
            {
                GetWeekSteps().Remove(s);
                return;
            }
            if (GUILayout.Button("Collapse Step"))
            {
                s.collapse = !s.collapse;
            }
            GUILayout.EndHorizontal();

            if (s.collapse)
                return;


            GUILayout.BeginHorizontal();
            GUILayout.Label("Description: ");
            if (s.description != null && s.description.Length > TechAbility.DESCRIPTION_CHAR_LIMIT)
                s.description = GUILayout.TextArea(s.description).Substring(0, TechAbility.DESCRIPTION_CHAR_LIMIT);
            else
                s.description = GUILayout.TextArea(s.description, GUILayout.Width(400));
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
            s.count = EditorGUILayout.IntField("Count: ", s.count);
            GUILayout.Space(10);
            s.customInstructions = EditorGUILayout.Toggle("Custom Instructions", s.customInstructions);
            if (s.customInstructions == true)
            {
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
            }
            else
            {
                s.url = EditorGUILayout.TextField("URL: ", s.url);
            }
            GUILayout.Space(10);
            s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
            s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Sub Step"))
            {
                s.subSteps.Add(new SubStep());
            }
            GUILayout.Label("Sub Steps: ", style);
            foreach (SubStep ss in s.subSteps)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Delete Sub Step"))
                {
                    s.subSteps.Remove(ss);
                    break;
                }

                GUILayout.Space(10);
            }

            GUILayout.Space(10);
            s.coins = EditorGUILayout.IntField("Coins for Step Complete: ", s.coins);
            GUILayout.Space(10);
            //THIS THIS
            GUILayout.Label("Points for step", style);
            if (GUILayout.Button("Add more points"))
            {
                s.points.Add(new Points());
            }

            foreach (Points p in s.points)
            {
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Main Category: ");
                p.mainCategory = (Points.MainCategory)EditorGUILayout.EnumPopup(p.mainCategory, GUILayout.Width(Screen.width / 2));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Sub Category: ");
                switch (p.mainCategory)
                {
                    case Points.MainCategory.Scripting:
                        p.subCategoryScript = (Points.SubCategoryScripting)EditorGUILayout.EnumPopup(p.subCategoryScript, GUILayout.Width(Screen.width / 2));
                        break;
                    case Points.MainCategory.UIUX:
                        p.subCategoryUIUX = (Points.SubCategoryUIUX)EditorGUILayout.EnumPopup(p.subCategoryUIUX, GUILayout.Width(Screen.width / 2));
                        break;
                    case Points.MainCategory.Design:
                        p.subCategoryDesign = (Points.SubCategoryDesign)EditorGUILayout.EnumPopup(p.subCategoryDesign, GUILayout.Width(Screen.width / 2));
                        break;
                    case Points.MainCategory.Fx:
                        p.subCategoryFx = (Points.SubCategoryFx)EditorGUILayout.EnumPopup(p.subCategoryFx, GUILayout.Width(Screen.width / 2));
                        break;
                }
                p.subCategoryPoints = EditorGUILayout.IntField(p.subCategoryPoints);
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Delete points"))
                {
                    s.points.Remove(p);
                }

            }

            GUILayout.Space(30);
        }
        /*
        public void DrawWeek1()
        {

            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek1().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            foreach(Step s in ta.GetWeek1())
            {
                GUILayout.BeginHorizontal();
                s.name = EditorGUILayout.TextField("Name: ", s.name);
                if(GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek1().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ",s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if(GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                //THIS THIS
                GUILayout.Label("Points for step", style);
                if(GUILayout.Button("Add more points"))
                {
                    s.points.Add(new Points());
                }

                foreach (Points p in s.points)
                {
                    GUILayout.Space(10);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Main Category: ");
                    p.mainCategory = (Points.MainCategory)EditorGUILayout.EnumPopup(p.mainCategory, GUILayout.Width(Screen.width / 2));
                    p.mainCategoryPoints = EditorGUILayout.IntField(p.mainCategoryPoints);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Sub Category: ");
                    switch (p.mainCategory)
                    {
                        case Points.MainCategory.Scripting:
                            p.subCategoryScript = (Points.SubCategoryScripting)EditorGUILayout.EnumPopup(p.subCategoryScript, GUILayout.Width(Screen.width / 2));
                            break;
                        case Points.MainCategory.UIUX:
                            p.subCategoryUIUX = (Points.SubCategoryUIUX)EditorGUILayout.EnumPopup(p.subCategoryUIUX, GUILayout.Width(Screen.width / 2));
                            break;
                        case Points.MainCategory.Design:
                            p.subCategoryDesign = (Points.SubCategoryDesign)EditorGUILayout.EnumPopup(p.subCategoryDesign, GUILayout.Width(Screen.width / 2));
                            break;
                        case Points.MainCategory.Fx:
                            p.subCategoryFx = (Points.SubCategoryFx)EditorGUILayout.EnumPopup(p.subCategoryFx, GUILayout.Width(Screen.width / 2));
                            break;
                    }
                    p.subCategoryPoints = EditorGUILayout.IntField(p.subCategoryPoints);
                    GUILayout.EndHorizontal();

                    if(GUILayout.Button("Delete points"))
                    {
                        s.points.Remove(p);
                    }

                }

                GUILayout.Space(30);
            }


            GUILayout.Label("--------- EXTRA STEPS ---------", style);

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek1Extra().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            //-------------------------------------------- Extra Steps
            foreach (Step s in ta.GetWeek1Extra())
            {
                GUILayout.BeginHorizontal();
                s.name = EditorGUILayout.TextField("Name: ", s.name);
                if (GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek1Extra().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                GUILayout.Space(30);
            }
        }
        public void DrawWeek2()
        {

            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek2().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            foreach (Step s in ta.GetWeek2())
            {
                GUILayout.BeginHorizontal();
                s.name = GUILayout.TextField("Name: ", s.name);
                if (GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek2().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                GUILayout.Space(30);
            }
            GUILayout.Label("--------- EXTRA STEPS ---------", style);

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek2Extra().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            //-------------------------------------------- Extra Steps
            foreach (Step s in ta.GetWeek2Extra())
            {
                GUILayout.BeginHorizontal();
                s.name = EditorGUILayout.TextField("Name: ", s.name);
                if (GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek2Extra().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                GUILayout.Space(30);
            }
        }
        public void DrawWeek3()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek3().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            foreach (Step s in ta.GetWeek3())
            {
                GUILayout.BeginHorizontal();
                s.name = EditorGUILayout.TextField("Name: ", s.name);
                if (GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek3().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                GUILayout.Space(30);
            }
            GUILayout.Label("--------- EXTRA STEPS ---------", style);

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek3Extra().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            //-------------------------------------------- Extra Steps
            foreach (Step s in ta.GetWeek3Extra())
            {
                GUILayout.BeginHorizontal();
                s.name = EditorGUILayout.TextField("Name: ", s.name);
                if (GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek3Extra().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                GUILayout.Space(30);
            }
        }
        public void DrawWeek4()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 15;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek4().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            foreach (Step s in ta.GetWeek4())
            {
                GUILayout.BeginHorizontal();
                s.name = EditorGUILayout.TextField("Name: ", s.name);
                if (GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek4().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = EditorGUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                GUILayout.Space(30);
            }

            GUILayout.Label("--------- EXTRA STEPS ---------", style);
            GUILayout.Space(10);
            if (GUILayout.Button("Add New Step"))
            {
                ta.GetWeek4Extra().Add(new Step("New Step"));
            }
            GUILayout.Space(10);
            //-------------------------------------------- Extra Steps
            foreach (Step s in ta.GetWeek4Extra())
            {
                GUILayout.BeginHorizontal();
                s.name = EditorGUILayout.TextField("Name: ", s.name);
                if (GUILayout.Button("Delete Step"))
                {
                    ta.GetWeek4Extra().Remove(s);
                    break;
                }
                if (GUILayout.Button("Collapse Step"))
                {
                    s.collapse = !s.collapse;
                }
                GUILayout.EndHorizontal();

                if (s.collapse)
                    continue;

                GUILayout.Space(5);
                s.completed = EditorGUILayout.Toggle("Completed: ", s.completed);
                s.count = EditorGUILayout.IntField("Count: ", s.count);
                GUILayout.Space(10);
                GUILayout.Label("Instructions: ");
                GUILayout.Space(10);
                s.instructions = GUILayout.TextArea(s.instructions);
                s.url = EditorGUILayout.TextField("URL: ", s.url);
                GUILayout.Space(10);
                s.incompletedText = EditorGUILayout.TextField("Incomplete Text: ", s.incompletedText);
                s.completedText = EditorGUILayout.TextField("Complete Text: ", s.completedText);

                GUILayout.Space(10);
                if (GUILayout.Button("Add New Sub Step"))
                {
                    s.subSteps.Add(new SubStep());
                }
                GUILayout.Label("Sub Steps: ", style);
                foreach (SubStep ss in s.subSteps)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.name = EditorGUILayout.TextField("Name: ", ss.name);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    ss.completed = EditorGUILayout.Toggle("Completed: ", ss.completed);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Delete Sub Step"))
                    {
                        s.subSteps.Remove(ss);
                        break;
                    }

                    GUILayout.Space(10);
                }
                GUILayout.Space(30);
            }
        }

        */
    }
}