using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class PathRunner : MonoBehaviour
    {
        public bool IsActive = true;
        public bool FaceMovement = false;
        public bool DynamicMovement = false; 
        public float moveSpeed = 10f;
        public float WithinRangeofPoint = 1f;
        public List<Transform> pathList;

        Rigidbody rb; 
        int pathListIndex = -1;
        Transform currentPoint; 

        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>(); 
            NextPoint();
            SetVelocity(); 
        }

        public void Stop()
        {
            rb.velocity = Vector3.zero;
            IsActive = false; 
        }

        void Update()
        {
            if (!IsActive)
            {
                return; 
            }

            if (IsWithinRange())
            {
                NextPoint();
                SetVelocity();
            }

            if (DynamicMovement)
            {
                SetVelocity();
            }
        }


        void NextPoint()
        {
            pathListIndex++; 
            if (pathListIndex >= pathList.Count)
            {
                pathListIndex = 0; 
            }
            currentPoint = pathList[pathListIndex]; 
        }

        void SetVelocity()
        {
            rb.velocity = ( (currentPoint.transform.position - gameObject.transform.position).normalized * moveSpeed);
            if (FaceMovement)
            {
                gameObject.transform.LookAt(currentPoint.transform); 

            }    
        }

        bool IsWithinRange()
        {
            return ((currentPoint.transform.position - gameObject.transform.position).magnitude <= WithinRangeofPoint);
        }

    }
}
