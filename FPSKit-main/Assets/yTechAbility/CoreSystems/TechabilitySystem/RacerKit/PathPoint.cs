using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class PathPoint : MonoBehaviour
    {
        void Start()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        public Vector3 getLocation()
        {
            return gameObject.transform.position;
        }


    }
}
