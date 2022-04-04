using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Techability System/StepSequence")]
    public class StepSequenceScriptable : ScriptableObject
    {
        [SerializeField] public StepSequenceObject[] sequence;

        public void AddNew(string code)
        {
            for(int i = 0; i < sequence.Length; i++)
            {
                if (sequence[i].GetCode().Equals(code))
                {
                    return;
                }
                if(sequence[i].GetCode().Equals(""))
                {
                    sequence[i].Set(code);
                    return;
                }
            }
        }

        [ContextMenu("Reset Items")]
        public void Reset()
        {
            foreach(StepSequenceObject s in sequence)
            {
                s.Reset();
            }
        }

        public int GetSequenceCount()
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                if (sequence[i].GetCode().Equals(""))
                {
                    return i;
                }
            }
            return 0;
        }
    }
    [System.Serializable]
    public class StepSequenceObject
    {
        [SerializeField] private bool _locked = true;
        [SerializeField] private string _code = "";

        public StepSequenceObject()
        {
            _locked = true;
            _code = "";
        }

        public void Reset()
        {
            _locked = true;
            _code = "";
        }

        public string GetCode() => _code;

        public bool GetLocked() => _locked;

        public void Set(string code)
        {
            _locked = false;
            _code = code;
        }
    }
}