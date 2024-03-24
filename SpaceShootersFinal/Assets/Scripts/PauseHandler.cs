using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour {

        public static bool GameisPaused = false;
        public GameObject pauseMenuUI;
        // public AudioMixer mixer;
        // public static float volumeLevel = 1.0f;
        // private Slider sliderVolumeCtrl;
        // public GameObject timeText;
        // public GameObject slider1;
        // public GameObject slider2;
        // public GameObject slider3;

        void Awake (){
                // SetLevel (volumeLevel);
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                // if (sliderTemp != null){
                //         sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                //         sliderVolumeCtrl.value = volumeLevel;
                // }
        }

        void Start (){
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }

        void Update (){
                // if(GameisPaused) {
                //         Cursor.visible = true;
                //         Cursor.lockState = CursorLockMode.None;
                // }
                // if (Input.GetKeyDown(KeyCode.Escape)){
                //         if (GameisPaused){
                //                 Resume();
                //         }
                //         else{
                //                 Pause();
                //         }
                // }
        }

        public void Pause(){
                pauseMenuUI.SetActive(true);
                // Time.timeScale = 0f;
                GameisPaused = true;
                // timeText.SetActive(false);
        }

        public void Resume(){
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }

        public void StartGame(){
            SceneManager.LoadScene("SampleScene");
        }
        public void Credits(){
            SceneManager.LoadScene("CreditScene");
        }
        public void Return(){
            SceneManager.LoadScene("MainMenu");
        }
        public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        

}