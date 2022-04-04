using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Techability System/Asset Picker")]
    public class AssetPickerScriptable : ScriptableObject
    {

        [SerializeField] public AssetManager assetManager;

        [HideInInspector] public bool openSettings;

        //===============================================//
        //               Private Vars                    //
        //===============================================//

        public bool CanPickCharacter() => assetManager.CharacterAvailable();
        public bool CanPickEnvironment() => assetManager.EnvironmentAvailable();
        public bool CanPickEnemy() => assetManager.EnemyAvailable();

        public void Refresh()
        {
            assetManager.Refresh();
        }

        //===============================================//
        //                  Textures                     //
        //===============================================//


        [Header("Picker Textures")]
        public Texture2D gearImage;
        [Space(10)]
        [SerializeField] private Texture2D characters;
        [SerializeField] private Texture2D charactersAlert;
        [SerializeField] private Texture2D environment;
        [SerializeField] private Texture2D environmentAlert;
        [SerializeField] private Texture2D enemies;
        [SerializeField] private Texture2D enemiesAlert;
        [SerializeField] public Texture2D selectBackground;

        [Space(10)]
        public Texture2D[] male;
        public Texture2D[] female;

        public Texture2D[] EnvironmentOptions()
        {
            return assetManager.EnvironmentOptions();
        }

        public Texture2D[] EnemyOptions()
        {
            return assetManager.EnemyOptions();
        }

        public string[] EnvironmentNames()
        {
            return assetManager.EnvironmentNames();
        }

        public Texture2D GetCharactersTexture()
        {
            if (CanPickCharacter())
                return charactersAlert;
            else
                return characters;
        }
        public Texture2D GetEnemiesTexture()
        {
            if (CanPickEnemy())
                return enemiesAlert;
            else
                return enemies;
        }
        public Texture2D GetEnvironmentTexture()
        {
            if (CanPickEnvironment())
                return environmentAlert;
            else
                return environment;
        }

        public string[] EnemyNames()
        {
            return assetManager.EnemyNames();
        }
    }
}