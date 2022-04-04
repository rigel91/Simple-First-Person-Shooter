using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Techability.Systems
{
    public class HudMessageController : MonoBehaviour
    {
        public static HudMessageController instance;

        public TextMeshProUGUI TextColor;
        public TextMeshProUGUI TextBlack;
        public float showTime = 2.5f;
        public float fadeTime = .5f;

        [Header("Interals")]
        public bool MessageShown = false;
        public float timer = 0;
        public float fadePercent = 0;
        void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Second Player Prefab found!");
                return;
            }
            instance = this;
        }

        public void SetMessage(string message, Color color)
        {
            TextColor.text = message;
            TextBlack.text = message;
            TextColor.color = color;
            TextBlack.color = Color.black;
            MessageShown = true;
            timer = 0;
        }

        void Update()
        {
            if (MessageShown)
            {
                timer += Time.deltaTime;

                if (timer >= (showTime + fadeTime))
                {
                    // We're done showing the message
                    // Percent would have been a Neg number anyways... 
                    MessageShown = false;
                }

                if (timer <= showTime)
                {
                    // Too Early to start the fade
                    return;
                }

                // Fade the text now... 
                fadePercent = 1 - ((timer - showTime) / fadeTime);

                Color nextColor = TextColor.color;
                nextColor.a = fadePercent;
                TextColor.color = nextColor;

                nextColor = TextBlack.color;
                nextColor.a = fadePercent;
                TextBlack.color = nextColor;
            }
        }
    }
}