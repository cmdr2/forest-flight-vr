using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cmdr2.ui {

    // it's nice to have an abstraction, helps in porting to different platforms
    public class Pointer : MonoBehaviour {
        /* extern */
        public static bool GetClickButton() {
            return (_instance != null ? _instance.GetClickButton() : false);
        }
        
        public static bool GetClickButtonDown() {
            return (_instance != null ? _instance.GetClickButtonDown() : false);
        }
        
        public static bool GetTriggerButton() {
            return (_instance != null ? _instance.GetTriggerButton() : false);
        }
        
        public static bool GetTriggerButtonDown() {
            return (_instance != null ? _instance.GetTriggerButtonDown() : false);
        }
        
        public static bool GetBackButtonDown() {
            return (_instance != null ? _instance.GetBackButtonDown() : false);
        }
        
        public static Quaternion GetOrientation() {
            return (_instance != null ? _instance.GetOrientation() : Quaternion.identity);
        }
        
        public static void SetVisible(bool state) {
            if (_instance != null) {
                _instance.SetVisible(state);
            }
        }
        
        public static Transform GetLaser() {
            return (_instance != null ? _instance.GetLaser() : null);
        }
        
        /* dependencies */
        public LayerMask uiLayerMask;
        
        /* scratchpad */
        private static IPointer _instance = null;
        
        void Start() {
            _instance = GetComponent<IPointer>();
        }
        
        void Update() {
            RaycastHit hit;
            var isPressed = Input.GetButtonDown("Fire1") || Pointer.GetClickButtonDown() || Pointer.GetTriggerButtonDown();
            
            if (isPressed) {
                var laser = Pointer.GetLaser();
                
                if (Physics.Raycast(laser.position, laser.forward, out hit, Mathf.Infinity, uiLayerMask)) {
                    GameSystem.instance.OnItemClicked(hit.transform.name);
                }
            }
        }
    }

}