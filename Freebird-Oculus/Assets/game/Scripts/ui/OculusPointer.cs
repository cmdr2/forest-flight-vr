using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cmdr2.ui {

    public class OculusPointer : MonoBehaviour, IPointer {
        /* dependencies */
        public GameObject leftController = null;
        public GameObject rightController = null;
        public Transform leftLaser;
        public Transform rightLaser;
    
        void Start () {
    		SetDisplayFrequency(72.0f);
    	}

        public bool GetClickButton() {
            return OVRInput.Get(OVRInput.Button.PrimaryTouchpad);
        }

        public bool GetClickButtonDown() {
            return OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad);
        }

        public bool GetTriggerButton() {
            return OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        }

        public bool GetTriggerButtonDown() {
            return OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
        }
        
        public bool GetBackButtonDown() {
            return Input.GetKeyDown(KeyCode.Escape) || OVRInput.GetDown(OVRInput.Button.Back);
        }
        
        public Quaternion GetOrientation() {
            var lConnected = OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);
            var rConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);

            if (lConnected) {
                return OVRInput.GetLocalControllerRotation (OVRInput.Controller.LTrackedRemote);
            } else if (rConnected) {
                return OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTrackedRemote);
            }

            return Quaternion.identity; // maybe just use Controller.Active instead?
        }
        
        public void SetVisible(bool state) {
            var lConnected = OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);
            var rConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);

            if (lConnected) {
                leftController.SetActive(state);
                rightController.SetActive(false);
            } else if (rConnected) {
                leftController.SetActive(false);
                rightController.SetActive(state);
            } else {
                leftController.SetActive (false);
                rightController.SetActive (false);
            }
        }
        
        public Transform GetLaser() {
            var lConnected = OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);
            var rConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);

            if (lConnected) {
                return leftLaser;
            }
            
            return rightLaser;
        }
        
        private bool SetDisplayFrequency(float targetFreq) {
            if (OVRManager.display.displayFrequency == targetFreq) {
                return true;
            }

            bool done = false;
            var freqs = OVRManager.display.displayFrequenciesAvailable;

            foreach (var freq in freqs) {
                if (targetFreq == freq) {
                    OVRManager.display.displayFrequency = targetFreq;
                    done = true;
                    break;
                }
            }

            return done;
        }
    }

}