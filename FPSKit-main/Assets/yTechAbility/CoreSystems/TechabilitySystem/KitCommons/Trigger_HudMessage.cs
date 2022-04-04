using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class Trigger_HudMessage : MonoBehaviour
    {
        public bool ShowOnlyOnce = true;
        bool ShowMessage = true;

        public Color color = Color.cyan;

        [TextArea]
        public string message = "Hello there, Stay a while and listen.";
        
        
        private void OnTriggerEnter(Collider other)
        {   
            if (!ShowMessage)
            {
                return; 
            }

            StudentPlayer player = other.GetComponentInParent<StudentPlayer>();
            if (player)
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
