using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Techability.Systems
{
    public class EnemyGenerator : MonoBehaviour
    {
        public bool IsActive = true; 
        [Range(.1f, 100f)]
        public float spawnTime = 3;
        public bool spawnInOrder = true;
        public EnemyCountManager enemyCountManager; 

        [Space(10)]
        public List<GameObject> spawnList;
        
        [Space(10)]
        public List<Transform> spawnLocationList;
    

        [Space(10)]
        [Header("public void MethodName(GameObject object)")]
        [Header("OnSpawn Events must be of:")]
        
        public UnityEvent<GameObject> OnSpawn;

        float timerCounter = 0;
        int spawnIndex = 0;
        int spawnLocationIndex = 0;
        bool IsSetupCorrect = false;
        void Start()
        {
            if (spawnLocationList.Count == 0)
            {
                Debug.LogWarning("Generator " + gameObject.name + " has no spawn locations in spawnLocationList ");
                return;
            }
            if (spawnList.Count == 0)
            {
                Debug.LogWarning("Generator " + gameObject.name + " has no enemies in spawn list ");
                return;
            }
            IsSetupCorrect = true;
            timerCounter = spawnTime;

        }

        public void Stop()
        {
            IsActive = false; 
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsActive)
            {
                return; 
            }

            if (!IsSetupCorrect)
            {
                return;
            }

            timerCounter -= Time.deltaTime;
            
            if (timerCounter <= 0)
            {
                timerCounter = spawnTime;
                Spawn();
            }
        }

        void Spawn()
        {
            GameObject enemy = NextObject();
            Transform location = NextLocation();
            GameObject spawn = Instantiate(enemy, location.position, location.rotation);
            if (enemyCountManager)
            {
                Health h = spawn.GetComponentInChildren<Health>(); 
                if (h)
                {
                    h.enemyCountManager = enemyCountManager;
                    h.RemoveObject = spawn; 
                    h.OnDeathEvent.AddListener(h.RemovefromCountManager); 
                    enemyCountManager.AddEnemy(spawn);
                }
            }
            OnSpawn?.Invoke(spawn);
        }

        Transform NextLocation()
        {
            Transform result = spawnLocationList[spawnLocationIndex];

            spawnLocationIndex++;
            if (spawnLocationIndex >= spawnLocationList.Count)
            {
                spawnLocationIndex = 0;
            }

            return result;
        }

        GameObject NextObject()
        {
            if (spawnInOrder)
            {
                return NextObjectInOrder();
            }
            else
            {
                return NextObjectRandom();
            }
        }

        GameObject NextObjectInOrder()
        {
            GameObject result = spawnList[spawnIndex];

            spawnIndex++;
            if (spawnIndex >= spawnList.Count)
            {
                spawnIndex = 0;
            }

            return result;
        }

        GameObject NextObjectRandom()
        {
            if (spawnList.Count == 1)
            {
                return spawnList[0];
            }
            spawnIndex = Random.Range(0, spawnList.Count);
            return spawnList[spawnIndex];
        }
    }
}
