using UnityEngine;
using System;
using System.Collections.Generic;

namespace Techability.Systems
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Techability System/TechAbility Steps")]
    public class TechAbilityStepsScriptable : ScriptableObject
    {
        [Header("UI references")]
        public Texture2D week1;
        public Texture2D week2;
        public Texture2D week3;
        public Texture2D week4;

        public Texture2D week1Selected;
        public Texture2D week2Selected;
        public Texture2D week3Selected;
        public Texture2D week4Selected;

        public Texture2D _checkSetup;
        public Texture2D _checked;
        public Texture2D _unchecked;

        public Texture2D _incompleted;
        public Texture2D _completed;
        public Texture2D headerImage;
        public Texture2D gearImage;
        [Space(10)]
        [Header("Settings")]
        [SerializeField] public bool openSettings;
        [SerializeField] public float screenScale;

        [Space(10)]
        [Header("Log In Info")]
        [SerializeField] public string email;
        [SerializeField] public string password;
        [SerializeField] public string studentID;
        [SerializeField] public int classesPerWeek;
        //Year,Month,Day
        [SerializeField] public string[] startDate;
        [SerializeField] public bool loggedIn;
        [SerializeField] public bool isTeacher;

        [Header("Weeks")]
        [Space(10)]
        [SerializeField] public int selectedWeek;

        [HideInInspector] [SerializeField] public bool drawDefault;

        [SerializeField] public List<Step> steps1 = new List<Step>();
        [SerializeField] public List<Step> steps2 = new List<Step>();
        [SerializeField] public List<Step> steps3 = new List<Step>();
        [SerializeField] public List<Step> steps4 = new List<Step>();

        public List<Step> GetWeek1() => steps1;

        public List<Step> GetWeek2() => steps2;

        public List<Step> GetWeek3() => steps3;

        public List<Step> GetWeek4() => steps4;

        public Step GetStep(int week, string name)
        {
            if (week == 1)
            {
                foreach (Step step in steps1)
                {
                    if (name.Equals(step.name))
                    {
                        return step;
                    }
                }
            }
            if (week == 2)
            {
                foreach (Step step in steps2)
                {
                    if (name.Equals(step.name))
                    {
                        return step;
                    }
                }
            }
            if (week == 3)
            {
                foreach (Step step in steps3)
                {
                    if (name.Equals(step.name))
                    {
                        return step;
                    }
                }
            }
            if (week == 4)
            {
                foreach (Step step in steps4)
                {
                    if (name.Equals(step.name))
                    {
                        return step;
                    }
                }
            }
            return null;
        }

        public Step GetStep(string name)
        {

            foreach (Step step in steps1)
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }
            foreach (Step step in steps2)
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }

            foreach (Step step in steps3)
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }

            foreach (Step step in steps4)
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }

            return null;
        }
        public void SetStep(int week, Step s)
        {
            if (week == 1)
            {
                foreach (Step step in steps1)
                {
                    if (s.name.Equals(s.name))
                    {
                        step.Set(s.completed, s.count);
                    }
                }
            }
            if (week == 2)
            {
                foreach (Step step in steps2)
                {
                    if (s.name.Equals(s.name))
                    {
                        step.Set(s.completed, s.count);
                    }
                }
            }
            if (week == 3)
            {
                foreach (Step step in steps3)
                {
                    if (s.name.Equals(s.name))
                    {
                        step.Set(s.completed, s.count);
                    }
                }
            }
            if (week == 4)
            {
                foreach (Step step in steps4)
                {
                    if (s.name.Equals(s.name))
                    {
                        step.Set(s.completed, s.count);
                    }
                }
            }
        }

    }

    [System.Serializable]
    public class Step
    {
        [SerializeField] public string name;
        [SerializeField] public bool completed;
        [SerializeField] public int count;
        [SerializeField] public bool customInstructions;
        [SerializeField] public string instructions;
        [SerializeField] public string url;

        [SerializeField] public bool collapse;

        [SerializeField] public string incompletedText;
        [SerializeField] public string completedText;

        [SerializeField] public string feedback;

        [SerializeField] public List<SubStep> subSteps = new List<SubStep>();

        [SerializeField] public List<Points> points = new List<Points>();

        [SerializeField] public int coins;

        [SerializeField] public string description;

        //TODO: Finish implementing this
        [SerializeField] public string prerequisite;

        public Step(string name)
        {
            completed = false;
            count = 0;
        }

        public void GetPoints(ref int subCat1, ref int subCat2, ref int subCat3, ref int points1, ref int points2, ref int points3, ref int Coins, ref string disc)
        {
            int i = 0;
            foreach (Points p in points)
            {
                if (i == 0)
                {
                    switch (p.mainCategory)
                    {
                        case Points.MainCategory.Scripting:
                            subCat1 = (int)p.subCategoryScript;
                            break;
                        case Points.MainCategory.Design:
                            subCat1 = (int)p.subCategoryDesign;
                            break;
                        case Points.MainCategory.UIUX:
                            subCat1 = (int)p.subCategoryUIUX;
                            break;
                        case Points.MainCategory.Fx:
                            subCat1 = (int)p.subCategoryFx;
                            break;
                    }
                    points1 = p.subCategoryPoints;
                }
                if (i == 1)
                {
                    switch (p.mainCategory)
                    {
                        case Points.MainCategory.Scripting:
                            subCat2 = (int)p.subCategoryScript;
                            break;
                        case Points.MainCategory.Design:
                            subCat2 = (int)p.subCategoryDesign;
                            break;
                        case Points.MainCategory.UIUX:
                            subCat2 = (int)p.subCategoryUIUX;
                            break;
                        case Points.MainCategory.Fx:
                            subCat2 = (int)p.subCategoryFx;
                            break;
                    }
                    points2 = p.subCategoryPoints;
                }
                if (i == 2)
                {
                    switch (p.mainCategory)
                    {
                        case Points.MainCategory.Scripting:
                            subCat3 = (int)p.subCategoryScript;
                            break;
                        case Points.MainCategory.Design:
                            subCat3 = (int)p.subCategoryDesign;
                            break;
                        case Points.MainCategory.UIUX:
                            subCat3 = (int)p.subCategoryUIUX;
                            break;
                        case Points.MainCategory.Fx:
                            subCat3 = (int)p.subCategoryFx;
                            break;
                    }
                    points3 = p.subCategoryPoints;
                }
                i++;
            }

            Coins = coins;
            disc = completedText;
        }



        public SubStep GetSubStep(string name)
        {
            foreach (SubStep ss in subSteps)
            {
                if (ss.name.Equals(name))
                    return ss;
            }
            return null;
        }

        public void Set(bool completed, int count)
        {
            this.completed = completed;
            this.count = count;
        }

        /// <summary>
        /// Scans through all of the sub steps to see if they are complete, if they are then this step will be switched to completed
        /// </summary>
        public bool CheckComplete()
        {
            foreach (SubStep s in subSteps)
            {
                if (s.completed == false)
                {
                    completed = false;
                    return (false);
                }
            }
            completed = true;
            return (true);
        }
    }

    [System.Serializable]
    public class Points
    {
        [SerializeField]
        public enum MainCategory
        {
            Scripting = 1,
            UIUX = 2,
            Design = 3,
            Fx = 4,
        }

        [SerializeField]
        public enum SubCategoryScripting
        {
            Scripts = 1,
            Variables = 2,
            Methods = 3,
            Encapsulation = 12,
            Instatiate = 19,
            Enumerators = 20,
            ConditionalStatements = 21,
            Loops = 22,
            DataTypes = 23
        }
        [SerializeField]
        public enum SubCategoryUIUX
        {
            Images = 13,
            Text = 4,
            Numbers = 5,
            Sliders = 6
        }
        [SerializeField]
        public enum SubCategoryDesign
        {
            Transforms = 7,
            GameObjects = 8,
            Components = 9,
            Prefabs = 14,
            LevelDesign = 15,
        }
        [SerializeField]
        public enum SubCategoryFx
        {
            Animation = 16,
            ParticleEffects = 17,
            Projectiles = 18,
            StateMachines = 10,
            Parameters = 10
        }

        [SerializeField] public MainCategory mainCategory;

        [SerializeField] public SubCategoryScripting subCategoryScript;
        [SerializeField] public SubCategoryUIUX subCategoryUIUX;
        [SerializeField] public SubCategoryDesign subCategoryDesign;
        [SerializeField] public SubCategoryFx subCategoryFx;

        [SerializeField] public int subCategoryPoints;

        public int GetSubCategory()
        {
            switch (mainCategory)
            {
                case Points.MainCategory.Scripting:
                    return (int)subCategoryScript;
                case Points.MainCategory.Design:
                    return (int)subCategoryDesign;
                case Points.MainCategory.UIUX:
                    return (int)subCategoryUIUX;
                case Points.MainCategory.Fx:
                    return (int)subCategoryFx;
            }
            return -1;
        }


    }

    [System.Serializable]
    public class SubStep
    {
        public SubStep()
        {
            name = "";
            completed = false;
        }
        public SubStep(string name)
        {
            this.name = name;
        }
        [SerializeField] public string name;
        [SerializeField] public bool completed;
    }
}