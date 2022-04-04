using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace Techability.Systems
{
    public struct InputData
    {
        public Vector2 ThumbstickLeft;
        public Vector2 ThumbstickRight;
        public float TriggerLeft;
        public float TriggerRight;
        public bool ButtonNorth;
        public bool ButtonSouth;
        public bool ButtonEast;
        public bool ButtonWest;
        public bool ButtonShoulderLeft;
        public bool ButtonShoulderRight;
        public bool ButtonThumbstickLeft;
        public bool ButtonThumbstickRight;
        public bool dPadUP;
        public bool dPadDown;
        public bool dPadLeft;
        public bool dPadRight;
        public bool ButtonSelect;
        public bool ButtonStart; 

        public static InputData GetCleanInput()
        {
            InputData data = new InputData();
            data.ThumbstickLeft = Vector2.zero;
            data.ThumbstickRight = Vector2.zero;
            data.TriggerLeft = 0.0f;
            data.TriggerRight = 0.0f;
            data.ButtonNorth = false;
            data.ButtonSouth = false;
            data.ButtonEast = false;
            data.ButtonWest = false;
            data.ButtonShoulderLeft = false;
            data.ButtonThumbstickLeft = false;
            data.ButtonThumbstickRight = false;
            data.dPadUP = false;
            data.dPadDown = false;
            data.dPadLeft = false;
            data.dPadRight = false;
            data.ButtonSelect = false;
            data.ButtonStart = false;

            return data;
        }
    }
}
