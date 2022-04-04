using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Techability.Systems
{
    public class DoorTrigger : MonoBehaviour
    {
        public bool IsActive = true; 
        public string KeyName = "Key Trigger";
        public string KeyDisplayName = "Display Name";

        public UnityEvent OnUnlockDoor;
        


        private void OnTriggerEnter(Collider other)
        {
            if (!IsActive)
            {
                return; 
            }

            FPSController player = other.GetComponentInParent<FPSController>();
            if (player)
            {
                if (player.HasItem(KeyName))
                {
                    Debug.Log("Success");
                    OnUnlockDoor?.Invoke();
                    IsActive = false; 
                } else
                {
                    string message = "Missing Key: " + KeyDisplayName; 
                    Debug.Log(message);
                    HudMessageController.instance.SetMessage(message, Color.cyan); 
                }


            }
        }

    }
}
