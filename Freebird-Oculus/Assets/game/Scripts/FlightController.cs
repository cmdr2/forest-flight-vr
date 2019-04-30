using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cmdr2.ui;

namespace cmdr2 {

    public class FlightController : MonoBehaviour {
        /* extern */
        [HideInInspector]
        public static float maxSpeed = 5f;
        [HideInInspector]
        public static bool allowFullRotation = false;
        [HideInInspector]
        public static bool autoStabilize = true;
        
        /* dependencies */
        public GameObject model;
        
        /* constants */
        private const float ACCELERATION = 40;
        private const float BRAKE = 8;
        private const float MAX_PITCH = 30; // degrees
        private const float MAX_ROLL = 19; // degrees
    
        /* scratchpad */
        private float speed = 0;
    
        void Start () {
        
        }
        
        void Update () {
            var isPressed = Pointer.GetClickButton() || Pointer.GetTriggerButton();
        
            if (isPressed) {
                speed += ACCELERATION * Time.deltaTime;
            } else {
                speed -= BRAKE * Time.deltaTime;
            }
            
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            
            transform.position += transform.forward * speed * Time.deltaTime;
            
            var q = Pointer.GetOrientation();
    
            var targetR = transform.localRotation * q;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetR, Time.deltaTime * 2);
            
            model.transform.localRotation = Quaternion.Lerp(model.transform.localRotation, q, Time.deltaTime * 4);
            
            /* rotation clamps */
            if (!allowFullRotation) {
                var euler = transform.localRotation.eulerAngles;
                euler.z = (euler.z > 180 ? euler.z - 360 : euler.z);
                euler.x = (euler.x > 180 ? euler.x - 360 : euler.x);
                
                //print(euler.z + ", " + euler.x);
                euler.z = Mathf.Clamp(euler.z, -MAX_ROLL, MAX_ROLL);
                euler.x = Mathf.Clamp(euler.x, -MAX_PITCH, MAX_PITCH);
                
                transform.localRotation = Quaternion.Euler(euler);
            }
            
            /* autostabilize on touchpad release */
            if (!isPressed && autoStabilize) {
                var euler = transform.localRotation.eulerAngles;
                var destRot = Quaternion.Euler(0, euler.y, 0);
            
                transform.localRotation = Quaternion.Lerp(transform.localRotation, destRot, Time.deltaTime * 2);
            }
        }
    }

}