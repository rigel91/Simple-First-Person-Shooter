using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Techability.Systems
{
    public class NumberBoard : MonoBehaviour
    {
        public float floatspeed = 1f;
        public float lifetime = 2.5f;
        public TextMeshProUGUI text; 

        Camera mainCam; 
        void Start()
        {
            Destroy(gameObject, lifetime);
            mainCam = Camera.main; 
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.Translate(Vector3.up * floatspeed * Time.deltaTime);
            
        }

        private void OnGUI()
        {
            if (mainCam)
            {
                gameObject.transform.LookAt(mainCam.gameObject.transform);
                gameObject.transform.rotation = Quaternion.Inverse(gameObject.transform.rotation); 
            }
        }
    }
}
