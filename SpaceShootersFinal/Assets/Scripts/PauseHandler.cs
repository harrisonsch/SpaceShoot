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
        public static float volumeLevel;
        public static float startLevel = 1.0f;
        private Slider sliderVolumeCtrl;
        public AudioMixer mixer;
        public GameObject sliderTemp;
        private static int levelNum = 0;
        // public GameObject timeText;
        // public GameObject slider1;
        // public GameObject slider2;
        // public GameObject slider3;

        void Awake (){
                Scene currentScene = SceneManager.GetActiveScene();
                string sceneName = currentScene.name;
                if(sceneName == "MainMenu") {
                        levelNum = 0;
                }
                SetLevel(volumeLevel);
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        Debug.Log("volume level is" + volumeLevel);
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        sliderVolumeCtrl.value = volumeLevel;
                }
        }

        void Start (){
                SetLevel(startLevel);
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        Debug.Log("start level is" + startLevel);
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        sliderVolumeCtrl.value = startLevel;
                }
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }

        void Update (){
                Debug.Log("volume level is" + volumeLevel);
                SetLevel(volumeLevel);
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        volumeLevel = sliderVolumeCtrl.value;
                }
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
            SceneManager.LoadScene("0Scene");
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
                Destroy(GameObject.FindGameObjectWithTag("MusicManager").gameObject);
                SceneManager.LoadSceneAsync("MainMenu");
        }
        public void ShopReturn(){
                ShopInstance shop = GameObject.FindGameObjectWithTag("ShopHandler").GetComponent<ShopInstance>();
                shop.ExitShop();
                Time.timeScale = 1f;
                Destroy(GameObject.FindGameObjectWithTag("MusicManager").gameObject);
                levelNum++;
                switch(levelNum) {
                        case 1:
                                SceneManager.LoadScene("Lvl2");
                        break;
                case 2:
                        SceneManager.LoadScene("level 3");
                        break;
                case 3:
                        SceneManager.LoadScene("MainScene");
                        break;
                default:
                        SceneManager.LoadScene("FrankScene"); 
                        break;
                }
        }


        public void QuitGame(){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        public void SetLevel (float sliderValue){
                if(mixer != null && sliderValue != null) {
                        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                        volumeLevel = sliderValue;
                }
        }
        

}
