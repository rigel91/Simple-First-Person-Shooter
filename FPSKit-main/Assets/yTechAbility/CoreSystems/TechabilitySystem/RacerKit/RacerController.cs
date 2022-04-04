using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class RacerController : MonoBehaviour
    {
        const float FORCEMULTI = 1000f;

        public Rigidbody rigidBody;

        [Space(15)]
        public float maxSpeed = 10f;

        [HideInInspector]
        public float MaxSpeedTurbo = 10f;
        public float forwardAccel = 8f;
        [HideInInspector]
        public float forwardAccelTurbo = 1f;
        public float reverseAccel = 1f;
        public float dragOnGround = 3f;
        public float dragInAir = .1f;
        public float turnStrenght = 180f;
        public float gravityForce = 10f;
        [HideInInspector]
        public float deadZone = 0.01f;

        [Space(15)]
        public Transform ModelRoot;
        public Transform LeftFrontWheel;
        public Transform LeftRearWheel;
        public Transform RightFrontWheel;
        public Transform RightRearWheel;
        public bool isBike = false;

        [Space(15)]
        public LayerMask GroundLayer;
        public float GoundRayLength = 1.25f;
        public Transform goundRayPoint;
        Vector3 groundNormal;

        [Space(15)]
        public GameObject ControlsPannel;

        float speedInput = 0;
        float turnInput = 0;

        Vector2 LeftThumbStick = Vector2.zero;
        Vector2 RightThumbStick = Vector2.zero;
        bool tabKeyDown = false;




        // Start is called before the first frame update
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {
            GetInput();

            // Process Input 
            turnInput = LeftThumbStick.x;
            if (Mathf.Abs(turnInput) < deadZone)
            {
                turnInput = 0;
            }

            speedInput = LeftThumbStick.y;
            if (speedInput > deadZone)
            {
                speedInput = speedInput * forwardAccel * FORCEMULTI;
            }
            else if (speedInput < -deadZone)
            {
                speedInput = speedInput * reverseAccel * FORCEMULTI;
            }

            if (tabKeyDown)
            {
                ControlsPannel.SetActive(!ControlsPannel.activeSelf);
            }
        }


        public void GetInput()
        {
            GetInput_Keyboard();
        }
        public void GetInput_Keyboard()
        {
            LeftThumbStick = Vector2.zero;
            LeftThumbStick.y = Input.GetAxis("Vertical");
            LeftThumbStick.x = Input.GetAxis("Horizontal");
            tabKeyDown = Input.GetKeyDown(KeyCode.Tab);
        }


        void FixedUpdate()
        {
            if (IsCarGrounded())
            {
                rigidBody.drag = dragOnGround;

                if (Mathf.Abs(turnInput) > 0)
                {
                    Vector3 turnrate = Vector3.zero;
                    turnrate.y = turnInput * turnStrenght * Time.fixedDeltaTime * GetSign(speedInput);
                    gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + turnrate);
                }


                if (Mathf.Abs(speedInput) > 0)
                {
                    rigidBody.AddForce(gameObject.transform.forward * speedInput);
                }

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

            ModelRoot.rotation = Quaternion.FromToRotation(gameObject.transform.up, groundNormal) * gameObject.transform.rotation;
            SetWheelRotation(LeftFrontWheel, true);
            SetWheelRotation(LeftRearWheel, false);
            if (!isBike)
            {
                SetWheelRotation(RightFrontWheel, true);
                SetWheelRotation(RightRearWheel, false);
            }
        }

        public void SetWheelRotation(Transform wheel, bool isFront)
        {
            float wheely = 0;
            if (isFront)
            {
                wheely = turnInput * 20f;
            }

            wheel.localRotation = Quaternion.Euler(wheel.rotation.x, wheely, wheel.rotation.z);
        }
    }
}