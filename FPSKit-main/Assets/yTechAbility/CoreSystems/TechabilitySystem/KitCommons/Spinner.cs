using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public bool IsActive = true;
    public float SpinRate = 90;

    private GameObject player;
    public float distance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Stop()
    {
       
        IsActive = false;
    }


    // Update is called once per frame
    void Update()
    {
        float step = 5*Time.deltaTime;

        float val = Vector3.Distance(transform.position, player.transform.position);
        if (val < distance && val > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }


        if (!IsActive)
        {
            return; 
        }

        gameObject.transform.Rotate(Vector3.up * SpinRate * Time.deltaTime); 
        
    }
}
