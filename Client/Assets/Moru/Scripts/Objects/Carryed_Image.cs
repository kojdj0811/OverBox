using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Moru
{
    public class Carryed_Image : Alarm
    {
        public override void Alarming(Sprite sprite, Transform targetTransform)
        {
            base.Alarming(sprite, targetTransform);
        }
        private void Update()
        {
            LookAtCam(); 
        }
    }
}