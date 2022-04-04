using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    [System.Serializable]
    public class AssetContainer
    {
        [SerializeField] public string name;

        [SerializeField] public Texture2D sample;

        [SerializeField] public Type type;

        [SerializeField] public bool loaded;

        public enum Type
        {
            Environment,
            Character,
            Enemy
        }
    }
}