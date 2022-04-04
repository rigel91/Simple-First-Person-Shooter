using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public bool IsActive = true;
    public float SpinRate = 90; 

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

        gameObject.transform.Rotate(Vector3.up * SpinRate * Time.deltaTime); 
        
    }
}
