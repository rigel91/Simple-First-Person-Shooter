using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    [System.Serializable]
    public class MonthContainer
    {
        [SerializeField] public List<AssetContainer> environments;
        [SerializeField] public List<AssetContainer> characters;
        [SerializeField] public List<AssetContainer> enemies;
    }
}