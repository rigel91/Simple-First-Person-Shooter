using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Techability.Systems; 

public class TeleportPortal : MonoBehaviour
{
    public Transform TeleportPoint;
    bool hasPorted = false;


    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // FPSController is the primary script that defines our player object
        FPSController player = other.GetComponentInParent<FPSController>();
        
        // if (player != null)
        if (player)
        {

            Transform tr = player.GetComponent<Transform>();
            tr.position = TeleportPoint.position;
            tr.rotation = TeleportPoint.rotation;


            //StartCoroutine(Teleport(player));           
        }
    }

    /*IEnumerator Teleport(FPSController player)
    {
        if (!player.teleported)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Teleport: " + gameObject.name);
            player.teleported = true;
            Transform tr = player.GetComponent<Transform>();
            tr.position = TeleportPoint.position;
            tr.rotation = Quaternion.Euler(tr.rotation.x, 90, tr.rotation.z);
        }
        
        // May need to change velocity to new facing

        yield return new WaitForSeconds(10f);
        player.teleported = false;
    }*/
}
