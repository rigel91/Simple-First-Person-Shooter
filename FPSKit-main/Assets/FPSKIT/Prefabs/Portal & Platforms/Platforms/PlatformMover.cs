using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Techability.Systems;

public class PlatformMover : MonoBehaviour
{
    public Transform thePlatform;
    public Transform[] waypoints;
    public bool startEnabled = true;
    public bool leftRight = false;
    public float platformSpeed = 2f;

    List<Transform> moveableTrans = new List<Transform>();
    int waypointIndex;
    bool moving = false;
    AudioSource AS;

    private GameObject target = null;
    private Vector3 offset;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        target = null;
        if (startEnabled)
        {
            EnablePlatform();
        }
    }

    void Update()
    {
        if (moving)
        {           
            Vector3 prevPos = thePlatform.position;
                    
            foreach (var trans in moveableTrans)
            {
                trans.position += thePlatform.position - prevPos;
            }

            if (leftRight)
            {
                Vector3 sideways = new Vector3(thePlatform.position.x, thePlatform.position.y, waypoints[waypointIndex].position.z);
                
                if(Vector3.Distance(thePlatform.position, sideways) > .05f)
                {
                    thePlatform.position = Vector3.MoveTowards(thePlatform.position, sideways, platformSpeed * Time.deltaTime);
                }
                else
                {
                    newWaypoint();
                }
            }
            else if (!leftRight)
            {
                if(Vector3.Distance(thePlatform.position, waypoints[waypointIndex].position) > .05)
                {
                    thePlatform.position = Vector3.MoveTowards(thePlatform.position, waypoints[waypointIndex].position, platformSpeed * Time.deltaTime);  
                }
                else
                {
                    newWaypoint();
                }
            }
        }
        
    }

    public void EnablePlatform()
    {
        moving = true;
        AS.Play();
    }

    public void DisablePlatform()
    {
        moving = false;
        AS.Stop();
    }

    void OnTriggerStay(Collider col)
    {
        target = col.gameObject;
        offset = target.transform.position - thePlatform.position;
    }
    void OnTriggerExit(Collider col)
    {
        target = null;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = thePlatform.position + offset;
        }
    }

    private void newWaypoint()
    {
            waypointIndex++;

        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}




//transform.localScale = new Vector3(Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, platformSpeed * Time.deltaTime).x, transform.localScale.y, transform.localScale.z);
