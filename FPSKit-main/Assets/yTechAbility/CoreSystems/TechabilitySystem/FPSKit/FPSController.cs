using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class FPSController : StudentPlayer
    {
        Rigidbody RB;
        PlayerHacks hacks;
        public GameObject CameraRoot;
        int InvertMouseInt = -1;
        [HideInInspector]
        public bool isMouseLocked = true;
        [HideInInspector]
        public float GroundDistance = .1f;
        [HideInInspector]
        public float CameraAngle = 0;

        [HideInInspector]
        public bool InvertMouse = true;
        [HideInInspector]
        public float MinCameraAngle = -45;
        [HideInInspector]
        public float MaxCameraAngle = 75;
        [HideInInspector]
        public float MouseSensitivity = 3; 

        [HideInInspector]
        public float MoveSpeed = 10;
        [HideInInspector]
        public float RotationSpeed = 90;
        [HideInInspector]
        public float JumpPower = 100;
        [HideInInspector]
        public float GravityPower = 10;

        public Weapon ActiveWeapon;

        [HideInInspector]
        public InputData CurrentInputData;
        [HideInInspector]
        public InputData PreviousInputData;

        Dictionary<string, bool> InventoryList; 

        void Start()
        {
            InventoryList = new Dictionary<string, bool>(); 
            RB = gameObject.GetComponent<Rigidbody>();
            hacks = gameObject.GetComponent<PlayerHacks>();
            CurrentInputData = InputData.GetCleanInput(); 
            PreviousInputData = InputData.GetCleanInput();
            MouseLock();
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
            ProcessInput();
        }

      

        void ProcessInput()
        {

            Vector3 vel = Vector3.zero; 
            vel += CurrentInputData.ThumbstickLeft.y * MoveSpeed * gameObject.transform.forward;
            vel += CurrentInputData.ThumbstickLeft.x * MoveSpeed * gameObject.transform.right;
            RB.velocity = vel;

            gameObject.transform.Rotate(CurrentInputData.ThumbstickRight.x * Vector3.up * RotationSpeed * MouseSensitivity * Time.deltaTime);

            CameraAngle += (CurrentInputData.ThumbstickRight.y * Time.deltaTime * MouseSensitivity * RotationSpeed * GetInvertMouseInt());
            CameraAngle = Mathf.Clamp(CameraAngle, MinCameraAngle, MaxCameraAngle);
            CameraRoot.transform.localRotation = Quaternion.Euler(CameraAngle, 0, 0);

           

            if (IsGrounded())
            {
                if(CurrentInputData.ButtonSouth)
                {
                    RB.AddForce(Vector3.up * JumpPower * 1000);
                }
            }
            else
            {
                RB.AddForce(Vector3.down * GravityPower);
            }
        }

        bool IsGrounded()
        {
            return Physics.Raycast(gameObject.transform.position, Vector3.down, GroundDistance); 
        }

        int GetInvertMouseInt()
        {
            if (InvertMouse) return -1;
            else return 1; 
        }

        void GetInput()
        {
            PreviousInputData = CurrentInputData; 
            CurrentInputData = InputData.GetCleanInput();

            if (Input.GetKey(KeyCode.W))
            {
                CurrentInputData.ThumbstickLeft.y += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                CurrentInputData.ThumbstickLeft.y += -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                CurrentInputData.ThumbstickLeft.x += -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                CurrentInputData.ThumbstickLeft.x += 1;
            }

            CurrentInputData.ThumbstickRight.x = Input.GetAxis("Mouse X");
            CurrentInputData.ThumbstickRight.y = Input.GetAxis("Mouse Y");

            if (Input.GetMouseButtonDown(0))
            {
                CurrentInputData.ButtonShoulderLeft = true; 
            }
            if (Input.GetMouseButtonDown(1))
            {
                CurrentInputData.ButtonShoulderRight = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentInputData.ButtonSouth = true; 
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isMouseLocked = !isMouseLocked;
                MouseLock(); 
            }

        }

        public void AddItem(string item)
        {
            InventoryList.Add(item, true); 
        }
        public void RemoveItem(string item)
        {
            InventoryList.Remove(item);
        }
        public bool HasItem(string item)
        {
            if ( InventoryList.ContainsKey(item) )
            {
                return InventoryList[item]; 
            }
            return false; 
        }

        void MouseLock()
        {
            if (isMouseLocked)
            {
                Debug.Log("Lock Mouse");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Debug.Log("UnLock Mouse");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

    }
}
