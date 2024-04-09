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
        private PowerUpManager powerUpManager;
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

        public void SetPauseMenu(GameObject menu)
        {
                pauseMenuUI = menu;
                pauseMenuUI.SetActive(false); 
        }

        void Update (){

                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused){
                                Resume();
                        }
                        else{
                                Pause();
                        }
                }
                if (Input.GetKeyDown(KeyCode.K)){
                        Debug.Log("swap");
                        SceneManager.LoadScene("ShopScene");
                }
        }

        public void Pause(){
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameisPaused = true;
                // timeText.SetActive(false);
        }

        public void Resume(){
                
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
                Time.timeScale = 1f;
        }

        public void StartGame(){
                if(GameController.Instance != null) {
                        GameController.Instance.health = GameController.Instance.baseHealth;
                        powerUpManager = FindObjectOfType<PowerUpManager>();
                        foreach (PowerUp powerUp in GameController.Instance.activePowerUps) {
                                if (!powerUpManager.powerUpRegister.Contains(powerUp)) {
                                        powerUpManager.powerUpRegister.Add(powerUp);
                                }
                        }
                        GameController.Instance.activePowerUps.Clear();
                }
            SceneManager.LoadScene("MainScene");
        }
        public void Credits(){
            SceneManager.LoadScene("CreditScene");
        }
        public void Controls(){
            SceneManager.LoadScene("ControlsScene");
        }
        public void Tutorial(){
            SceneManager.LoadScene("0Scene");
        }
        public void Return(){
                Time.timeScale = 1f;
                StartCoroutine(LoadMainMenuAsync());
        }

        public void ShopReturn(){
                ShopInstance shop = GameObject.FindGameObjectWithTag("ShopHandler").GetComponent<ShopInstance>();
                shop.ExitShop();
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainScene");
        }

        IEnumerator LoadMainMenuAsync()
        {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

                while (!asyncLoad.isDone)
                {
                        yield return null; 
                }

        }
        public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        

}