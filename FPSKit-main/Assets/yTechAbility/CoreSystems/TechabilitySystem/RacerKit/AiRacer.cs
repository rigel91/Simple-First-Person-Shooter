using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{

    public class AiRacer : MonoBehaviour
    {
        const float FORCEMULTI = 1000f;

        public Rigidbody rigidBody;

        [Space(15)]
        public PathManager pathManager;
        public float InRangeofPointDistance = 1.0f;
        int CurrentPointIndex = -1;
        PathPoint CurrentPoint;
        bool hasPathManager = false;

        [Space(15)]
        public float maxSpeed = 30f;
        [HideInInspector]
        public float MaxSpeedTurbo = 60f;
        [HideInInspector]
        public float forwardAccel = 8f;
        [HideInInspector]
        public float forwardAccelTurbo = 12f;
        [HideInInspector]
        public float reverseAccel = 4f;
        public float dragOnGround = 3f;
        public float dragInAir = .1f;
        [HideInInspector]
        public float turnStrenght = 180f;
        [HideInInspector]
        public float gravityForce = 10f;
        [HideInInspector]
        public float deadZone = 0.01f;

        [Space(15)]
        public Transform ModelRoot;
        public Transform LeftFrontWheel;
        public Transform RightFrontWheel;
        public Transform LeftRearWheel;
        public Transform RightRearWheel;

        [Space(15)]
        public LayerMask GroundLayer;
        public float GoundRayLength = 1.25f;
        public Transform goundRayPoint;
        Vector3 groundNormal;

        // Start is called before the first frame update
        void Start()
        {
            if (pathManager)
            {
                hasPathManager = true;
            }
            else
            {
                Debug.LogError("AI Racer '" + gameObject.name + "' does not have a Path Manager Assigned to it.");
            }

            NextPathPoint();



        }


        // Update is called once per frame
        void Update()
        {
            if (!hasPathManager)
            {
                return;
            }
            if (IsClosePoint())
            {
                NextPathPoint();
            }


        }

        bool IsClosePoint()
        {
            return (GetDistanceToPathPoint() < InRangeofPointDistance);
        }

        void NextPathPoint()
        {
            if (!hasPathManager)
            {
                return;
            }
            CurrentPointIndex++;
            if (CurrentPointIndex >= pathManager.pathPoints.Count)
            {
                CurrentPointIndex = 0;
            }

            CurrentPoint = pathManager.pathPoints[CurrentPointIndex];
        }

        void FixedUpdate()
        {
            if (!hasPathManager)
            {
                return;
            }
            if (IsCarGrounded())
            {
                rigidBody.drag = dragOnGround;


                rigidBody.AddForce(GetDirectionToPathPoint() * maxSpeed * FORCEMULTI);



                if (rigidBody.velocity.magnitude > (maxSpeed * FORCEMULTI))
                {
                    rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed * FORCEMULTI;
                }
            }
            else
            {
                rigidBody.drag = dragInAir;
                rigidBody.AddForce(Vector3.down * gravityForce * 100f);
            }

        }

        Vector3 GetDirectionToPathPoint()
        {
            return (CurrentPoint.getLocation() - gameObject.transform.position).normalized;
        }

        float GetDistanceToPathPoint()
        {
            return (CurrentPoint.getLocation() - gameObject.transform.position).magnitude;
        }

        public bool IsCarGrounded()
        {
            bool result = false;
            RaycastHit hit;

            if (Physics.Raycast(goundRayPoint.position, -goundRayPoint.transform.up, out hit, GoundRayLength))
            {
                result = true;
                groundNormal = hit.normal;
            }

            return result;
        }
        public int GetSign(float value)
        {
            int result = 1;
            if (result < 0)
            {
                result = -1;
            }
            return result;
        }



        void LateUpdate()
        {

            gameObject.transform.position = rigidBody.transform.position;
            rigidBody.transform.localPosition = Vector3.zero;


            ModelRoot.LookAt(CurrentPoint.transform.position);

            //ModelRoot.rotation = Quaternion.FromToRotation(gameObject.transform.up, groundNormal) * gameObject.transform.rotation;

            // SetWheelRotation(LeftFrontWheel, true);
            // SetWheelRotation(RightFrontWheel, true);
            // SetWheelRotation(LeftRearWheel, false);
            // SetWheelRotation(RightRearWheel, false);
        }

    }
}