using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Techability.Systems
{
    [CanEditMultipleObjects]
    [System.Serializable]
    [CreateAssetMenu(menuName = "Techability System/Extra Steps")]
    public class TechAbilityExtraSteps : ScriptableObject
    {
        [Header("UI references")]
        public Texture2D designS;
        public Texture2D uiS;
        public Texture2D uxS;
        public Texture2D codingS;
        [Space(10)]
        public Texture2D designSelected;
        public Texture2D uiSelected;
        public Texture2D uxSelected;
        public Texture2D codingSelected;
        [Space(10)]
        public Texture2D _checkSetup;
        public Texture2D _checked;
        public Texture2D _unchecked;

        public Texture2D _incompleted;
        public Texture2D _completed;

        public Texture2D gearImage;
        [Space(10)]
        [Header("Settings")]
        [SerializeField] public bool openSettings;
        [SerializeField] public float screenScale;

        [Header("Weeks")]
        [Space(10)]
        [SerializeField] public int selectedWeek;

        [HideInInspector] [SerializeField] public bool drawDefault;

        [SerializeField] public StepCategory design = new StepCategory();
        [SerializeField] public StepCategory UI = new StepCategory();
        [SerializeField] public StepCategory visual = new StepCategory();
        [SerializeField] public StepCategory coding = new StepCategory();

        public StepCategory GetDesign() => design;

        public StepCategory GetUI() => UI;

        public StepCategory GetVisual() => visual;

        public StepCategory GetCoding() => coding;

        public Step GetStep(string name)
        {
            foreach (Step step in design.Steps())
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }
            foreach (Step step in UI.Steps())
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }
            foreach (Step step in visual.Steps())
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }
            foreach (Step step in coding.Steps())
            {
                if (name.Equals(step.name))
                {
                    return step;
                }
            }

            return null;
        }
        public void SetStep(Step s)
        {
            foreach (Step step in design.Steps())
            {
                if (name.Equals(step.name))
                {
                    step.Set(s.completed, s.count);
                }
            }
            foreach (Step step in UI.Steps())
            {
                if (name.Equals(step.name))
                {
                    step.Set(s.completed, s.count);
                }
            }
            foreach (Step step in visual.Steps())
            {
                if (name.Equals(step.name))
                {
                    step.Set(s.completed, s.count);
                }
            }
            foreach (Step step in coding.Steps())
            {
                if (name.Equals(step.name))
                {
                    step.Set(s.completed, s.count);
                }
            }
        }

    }
    [System.Serializable]
    public class StepCategory
    {
        public StepCategory()
        {
            easy = new List<Step>();
            intermediate = new List<Step>();
            hard = new List<Step>();
        }
        [SerializeField] public List<Step> easy;
        [SerializeField] public List<Step> intermediate;
        [SerializeField] public List<Step> hard;

        public List<Step> Steps()
        {
            List<Step> steps = new List<Step>();
            steps.AddRange(easy);
            steps.AddRange(intermediate);
            steps.AddRange(hard);
            return steps;
        }
        public void Remove(Step r)
        {
            foreach(Step s in easy)
            {
                if(s.name.Equals(r.name))
                {
                    easy.Remove(s);
                }
            }
            foreach (Step s in intermediate)
            {
                if (s.name.Equals(r.name))
                {
                    intermediate.Remove(s);
                }
            }
            foreach (Step s in hard)
            {
                if (s.name.Equals(r.name))
                {
                    hard.Remove(s);
                }
            }
        }
    }

}