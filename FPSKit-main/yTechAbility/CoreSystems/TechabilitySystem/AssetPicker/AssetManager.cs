using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Techability.Systems
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Techability System/Asset Manager")]
    public class AssetManager : ScriptableObject
    {
        [Header("Asset Setup")]
        [SerializeField] public MonthContainer month1;
        [SerializeField] public MonthContainer month2;
        [SerializeField] public MonthContainer month3;
        [SerializeField] public MonthContainer month4;

        //If these are loaded, you cannot pick
        [Header("Loaded options")]
        [SerializeField] public bool _envo1Loaded;
        [SerializeField] public bool _envo2Loaded;
        [SerializeField] public bool _envo3Loaded;
        [SerializeField] public bool _envo4Loaded;

        [SerializeField] public bool enemy;

        [SerializeField] public bool character;

        public void Refresh()
        {
            LoadedAssets();
        }

        [ContextMenu("Test Loaded")]
        public void LoadedAssets()
        {
            //Check characters
            string path1 = Application.dataPath + "\\Character";
            string[] files1 = System.IO.Directory.GetFiles(path1);
            if (files1.Length != 0)
            {
                character = true;
            }
            else
            {
                character = false;
            }
            enemy = true;
            //Check envo
            string path = Application.dataPath;
            path += "\\yTechAbility\\CoreSystems\\TechabilitySystem\\AssetPicker\\Loaded";
            string[] files = System.IO.Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (!file.Contains("meta"))
                {
                    CheckLoaded(file);
                }
            }
        }

        public Texture2D[] EnemyOptions()
        {
            Vector3Int date = MonthChecker.GetDateTime();
            Texture2D[] options = new Texture2D[9];

            if (date.x == 1)
            {
                for(int i = 0; i < month1.enemies.Count; i ++)
                {
                    options[i] = month1.enemies[i].sample;
                }
                return options;
            }
            if (date.x == 2)
            {
                for (int i = 0; i < month2.enemies.Count; i++)
                {
                    options[i] = month2.enemies[i].sample;
                }
                return options;
            }
            if (date.x == 3)
            {
                for (int i = 0; i < month3.enemies.Count; i++)
                {
                    options[i] = month3.enemies[i].sample;
                }
                return options;
            }
            if (date.x == 4)
            {
                for (int i = 0; i < month4.enemies.Count; i++)
                {
                    options[i] = month4.enemies[i].sample;
                }
                return options;
            }
            return options;
        }

        public string[] EnemyNames()
        {
            Vector3Int date = MonthChecker.GetDateTime();
            string[] options = new string[9];

            if (date.x == 1)
            {
                for (int i = 0; i < month1.enemies.Count; i++)
                {
                    options[i] = month1.enemies[i].name;
                }
                return options;
            }
            if (date.x == 2)
            {
                for (int i = 0; i < month2.enemies.Count; i++)
                {
                    options[i] = month2.enemies[i].name;
                }
                return options;
            }
            if (date.x == 3)
            {
                for (int i = 0; i < month3.enemies.Count; i++)
                {
                    options[i] = month3.enemies[i].name;
                }
                return options;
            }
            if (date.x == 4)
            {
                for (int i = 0; i < month4.enemies.Count; i++)
                {
                    options[i] = month4.enemies[i].name;
                }
                return options;
            }
            return options;
        }

        public string[] EnvironmentNames()
        {
            Vector3Int date = MonthChecker.GetDateTime();
            string[] options = new string[2];
            if (date.x == 1)
            {
                options[0] = month1.environments[0].name;
                options[1] = month1.environments[1].name;
                return options;
            }
            if (date.x == 2)
            {
                options[0] = month2.environments[0].name;
                options[1] = month2.environments[1].name;
                return options;
            }
            if (date.x == 3)
            {
                options[0] = month3.environments[0].name;
                options[1] = month3.environments[1].name;
                return options;
            }
            if (date.x == 4)
            {
                options[0] = month4.environments[0].name;
                options[1] = month4.environments[1].name;
                return options;
            }
            return options;
        }

        

        public void UnlockEnvironment(int x)
        {
            if (x == 1)
                _envo1Loaded = false;
            if (x == 2)
                _envo2Loaded = false;
            if (x == 3)
                _envo3Loaded = false;
            if (x == 4)
                _envo4Loaded = false;
        }

        public void CheckLoaded(string file)
        {
            if (file.Contains("Environment1"))
                _envo1Loaded = true;

            if (file.Contains("Environment2"))
                _envo2Loaded = true;

            if (file.Contains("Environment3"))
                _envo3Loaded = true;

            if (file.Contains("Environment4"))
            {
                _envo4Loaded = true;
            }
            Vector3Int dt = MonthChecker.GetDateTime();

            if(file.Contains($"Enemy{dt.x}x{dt.y}"))
            {
                enemy = false;
            }
        }

        public bool CharacterAvailable()
        {
            return !character;
        }
        public bool EnvironmentAvailable()
        {
            Vector3Int dt = MonthChecker.GetDateTime();

            if (dt.x == 1)
                return !_envo1Loaded;
            if (dt.x == 2)
                return !_envo2Loaded;
            if (dt.x == 3)
                return !_envo3Loaded;
            if (dt.x == 4)
                return !_envo4Loaded;
            return false;
        }
        public bool EnemyAvailable()
        {
            return enemy;
        }

        public Texture2D[] EnvironmentOptions()
        {
            Vector3Int date = MonthChecker.GetDateTime();
            Texture2D[] options = new Texture2D[2];
            if (date.x == 1)
            {
                options[0] = month1.environments[0].sample;
                options[1] = month1.environments[1].sample;
                return options;
            }
            if (date.x == 2)
            {
                options[0] = month2.environments[0].sample;
                options[1] = month2.environments[1].sample;
                return options;
            }
            if (date.x == 3)
            {
                options[0] = month3.environments[0].sample;
                options[1] = month3.environments[1].sample;
                return options;
            }
            if (date.x == 4)
            {
                options[0] = month4.environments[0].sample;
                options[1] = month4.environments[1].sample;
                return options;
            }
            return options;
        }

        public void DeleteEnvoCMD()
        {
            Vector3Int dt = MonthChecker.GetDateTime();
            if(dt.x == 1)
            {
                _envo1Loaded = false;
            }
            if (dt.x == 2)
            {
                _envo2Loaded = false;
            }
            if (dt.x == 3)
            {
                _envo3Loaded = false;
            }
            if (dt.x == 4)
            {
                _envo4Loaded = false;
            }
        }
    }
}