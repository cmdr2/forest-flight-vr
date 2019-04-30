using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cmdr2.ui;

namespace cmdr2 {

    public class GameSystem : MonoBehaviour {
        /* extern */
        public static GameSystem instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<GameSystem>();
                }
                return _instance;
            }
        }
    
        /* dependencies */
        public GameObject menu;
        public Transform spawnPoint;
        public GameObject player;
        public GameObject pointerGo;
        public AudioSource music;
        public UnityEngine.UI.Toggle safetyModeToggle;
        public UnityEngine.UI.Toggle musicToggle;
        
        /* scratchpad */
        private static GameSystem _instance = null;
        private FlightController flightController;

        void Start() {
            flightController = player.GetComponent<FlightController>();
            
            ShowMenu();
        }
        
        void Update() {
            if (Pointer.GetBackButtonDown()) {
                ShowMenu();
            }
        }

        private void ShowMenu() {
            Respawn();
            menu.SetActive(true);
            Pointer.SetVisible(true);
            flightController.enabled = false;
            flightController.model.SetActive(false);
        }
        
        private void ToggleSafetyMode() {
            FlightController.allowFullRotation = !FlightController.allowFullRotation;
            safetyModeToggle.isOn = FlightController.allowFullRotation;
        }
        
        private void ToggleMusic() {
            if (music.isPlaying) {
                music.Stop();
            } else {
                music.Play();
            }
            
            musicToggle.isOn = !music.isPlaying;
        }
        
        private void StartFlying() {
            menu.SetActive(false);
            Pointer.SetVisible(false);
            flightController.enabled = true;
            flightController.model.SetActive(true);
        }
        
        public void OnItemClicked(string itemName) {
            if (itemName == "SafetyModeToggle") {
                ToggleSafetyMode();
            }if (itemName == "AudioToggle") {
                ToggleMusic();
            } else if (itemName == "StartFlying") {
                print("AA: " + QualitySettings.antiAliasing);
                StartFlying();
            }
        }
        
        private void Respawn() {
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
    }

}