using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class NumberBoardManager : MonoBehaviour
    {
        public static NumberBoardManager instance;
        public GameObject NumberBoardPrefab;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Found Second NumberBoardManager: " + gameObject.name + ". Removing it"); 
                Destroy(gameObject);
            }
            instance = this; 
        }

        public NumberBoard SpawnBoard(Vector3 location, string number, Color color)
        {
            GameObject go = Instantiate(NumberBoardPrefab, location, Quaternion.identity);
            NumberBoard NB = go.GetComponent<NumberBoard>();
            NB.text.text = number; 
            NB.text.color = color; 
            return NB; 
        }
    }
}
