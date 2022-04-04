using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLarge : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public Vector3 LeftDoorOpenLocation;
    public Vector3 RightDoorOpenLocation;
    public bool ClosedAtStart = true; 
    public Vector3 LeftDoorCloseLocation;
    public Vector3 RightDoorCloseLocation;

    public void Start()
    {
        if (ClosedAtStart)
        {
            LeftDoorCloseLocation = LeftDoor.transform.localPosition;
            RightDoorCloseLocation = RightDoor.transform.localPosition;
        }
    }

    public void OpenDoor()
    {
        LeftDoor.transform.localPosition = LeftDoorOpenLocation;
        RightDoor.transform.localPosition = RightDoorOpenLocation;
    }

    public void CloseDoor()
    {
        LeftDoor.transform.localPosition = LeftDoorCloseLocation;
        RightDoor.transform.localPosition = RightDoorCloseLocation;
    }


}
