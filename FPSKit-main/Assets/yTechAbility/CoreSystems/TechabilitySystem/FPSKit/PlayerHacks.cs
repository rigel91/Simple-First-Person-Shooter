using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Techability.Systems
{
    public class PlayerHacks : MonoBehaviour
    {
        FPSController Player;

        void Start()
        {
            Player = gameObject.GetComponent<FPSController>();            

            //these are the default values, change them and play around with them
            //Player.MoveSpeed: movespeed is at 10
            //Player.RotationSpeed: rotation speed is at 90
            //jumpPower is at 100
            //Player.MouseSensitivity: mouse sensitivity is at 3
            Player.JumpPower = 80;
            Player.MoveSpeed = 8;
            Player.RotationSpeed = 75;
            Player.MouseSensitivity = 2.2f;
            //Player.GravityPower = 400;
        }

        void Update()
        {


        }

    }
}
