using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class EnemyCountManager : MonoBehaviour
{
    public List<GameObject> EnemyList;
    public UnityEvent OnEmpty; 
   
    public void AddEnemy(GameObject enemy)
    {
        EnemyList.Add(enemy); 
    }

    public void RemoveEnemy(GameObject enemy)
    {
        EnemyList.Remove(enemy);
        if (EnemyList.Count == 0)
        {
            //Debug.Log("Empty List"); 
            OnEmpty?.Invoke(); 
        }
    }

}
