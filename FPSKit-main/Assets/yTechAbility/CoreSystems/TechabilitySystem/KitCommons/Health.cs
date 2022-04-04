using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Techability.Systems
{
    public class Health : MonoBehaviour
    {
        public bool SpawnNumberBoards = true;
        public GameObject NumberBoardSpawnOverride;
        [Space(10)]
        public bool isAlive = true;
        public float currentHealth = 100;
        public float MaxHealth = 100;
        [Space(10)]
        public UnityEvent OnDeathEvent;
        public UnityEvent TakenDamageEvent;
        public UnityEvent AddedHealthEvent;

        [HideInInspector]
        public EnemyCountManager enemyCountManager;
        [HideInInspector]
        public GameObject RemoveObject; 


        // Start is called before the first frame update
        void Start()
        {

        }

        public void RemovefromCountManager()
        {
            //Debug.Log("Remove Call"); 
            if (enemyCountManager != null)
            {
                enemyCountManager.RemoveEnemy(RemoveObject); 
            }
        }

       public void TakeDamage(float value)
        {
            TakeDamage(value, NumberBoardSpawnOverride.transform.position); 
        }

        public void TakeDamage(float value, Vector3? location = null)
        {
            Debug.Log("TakeDamage - Start");
            if (!isAlive)
            {
                return;
            }
            currentHealth -= value;

            SpawnNumberBoard(value, location); 

            if (currentHealth <= 0)
            {
                isAlive = false;
                if (OnDeathEvent != null)
                {
                    OnDeathEvent.Invoke();
                }
                return;
            }

         

            if (TakenDamageEvent != null)
            {
                TakenDamageEvent.Invoke();
            }

        }

        void SpawnNumberBoard(float value, Vector3? location = null)
        {
            if (SpawnNumberBoards && location.HasValue && NumberBoardManager.instance)
            {
                NumberBoardManager.instance.SpawnBoard(location.Value, value + "!", Color.red);
            }
        }

        void AddHealth(float value)
        {
            if (!isAlive)
            {
                return;
            }
            
            currentHealth += value;
            
            if (currentHealth > MaxHealth)
            {
                currentHealth = MaxHealth; 
            }

            if (AddedHealthEvent != null)
            {
                AddedHealthEvent.Invoke();
            }
        }         
    }
}
