using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject followObject;
    public Vector3 localOffset = Vector3.zero;
        
    

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = followObject.transform.position + localOffset; 
    }
}
