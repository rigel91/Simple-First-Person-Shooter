using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Trigger_Check4Weapon : MonoBehaviour
    {
        public bool ShowOnlyOnce = true;
        bool ShowMessage = true;

        public Color color = Color.red;

        
        string message = "Oh No!\nYou have no weapon!";


        private void OnTriggerEnter(Collider other)
        {
            if (!ShowMessage)
            {
                return;
            }

            FPSController player = other.GetComponentInParent<FPSController>();
            if (player)
            {
                if (player.ActiveWeapon == null)
                {
                    HudMessageController.instance.SetMessage(message, color);
                    if (ShowOnlyOnce)
                    {
                        ShowMessage = false;
                    }
                }
            }
        }
    }
}
