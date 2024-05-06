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
        public static float volumeLevel = 0.5f;
        //public float startLevel = 0.5f;
        private Slider sliderVolumeCtrl;
        public AudioMixer mixer;
        //public GameObject sliderTemp;
        private static int levelNum = 0;
        // public GameObject timeText;
        // public GameObject slider1;
        // public GameObject slider2;
        // public GameObject slider3;
        public AudioSource buySFX;
        public AudioSource rejectSFX;

        void Awake (){
                Scene currentScene = SceneManager.GetActiveScene();
                string sceneName = currentScene.name;
                if(sceneName == "MainMenu") {
                        levelNum = 0;
                }
                //volume:
                
                
                // SetLevel(volumeLevel);
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                // if (sliderTemp != null){
                //         Debug.Log("volume level is" + volumeLevel);
                //         sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                //         sliderVolumeCtrl.value = volumeLevel;
                // }
        }

        void Start (){
                pauseMenuUI.SetActive(true);
                SetLevel(volumeLevel);
                pauseMenuUI.SetActive(false);
                
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                // if (sliderTemp != null){
                //         Debug.Log("start level is" + volumeLevel);
                //         sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                //         sliderVolumeCtrl.value = volumeLevel;
                // }
                
                GameisPaused = false;
        }

        void Update (){
                // Debug.Log("volume level is" + volumeLevel);
                // SetLevel(volumeLevel);
                // GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                // if (sliderTemp != null){
                //         sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                //         volumeLevel = sliderVolumeCtrl.value;
                // }
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
                if(GameObject.FindGameObjectWithTag("MusicManager") != null) {
                        
                Destroy(GameObject.FindGameObjectWithTag("MusicManager").gameObject);
                }
                if(GameController.Instance != null) {
                        GameController.Instance.health = GameController.Instance.baseHealth;
                        GameController.Instance.maxHealth = GameController.Instance.baseHealth;
                        GameController.Instance.balance = 0;
                        powerUpManager = FindObjectOfType<PowerUpManager>();
                        foreach (PowerUp powerUp in GameController.Instance.activePowerUps) {
                                if (!powerUpManager.powerUpRegister.Contains(powerUp)) {
                                        powerUp.Deactivate();
                                        powerUpManager.powerUpRegister.Add(powerUp);
                                }
                        }
                        GameController.Instance.activePowerUps.Clear();
                        Debug.Log("loading trans");
                        SceneManager.LoadScene("LevelOneTransition");
                } else {
                         SceneManager.LoadScene("level0Transition");
                }
                
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
                if(GameObject.FindGameObjectWithTag("MusicManager") != null) {

                Destroy(GameObject.FindGameObjectWithTag("MusicManager").gameObject);
                }
                SceneManager.LoadSceneAsync("MainMenu");
        }
        public void ShopReturn(){
                ShopInstance shop = GameObject.FindGameObjectWithTag("ShopHandler").GetComponent<ShopInstance>();
                shop.ExitShop();
                Time.timeScale = 1f;
                if(GameObject.FindGameObjectWithTag("MusicManager") != null) {
                        
                Destroy(GameObject.FindGameObjectWithTag("MusicManager").gameObject);
                }
                levelNum++;
                switch(levelNum) {
                        case 1:
                                SceneManager.LoadScene("Lvl2Transition");
                        break;
                case 2:
                        SceneManager.LoadScene("Lvl3Transition");
                        break;
                case 3:
                        SceneManager.LoadScene("Lvl4Transition");
                        break;
                default:
                        SceneManager.LoadScene("Lvl5Transition"); 
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

        public void BuyHP() {
                int healthGain = 20;
        if (PlayerHasEnoughCurrency(2)) {
                if(GameController.Instance.health < GameController.Instance.maxHealth) {
                        if(buySFX != null) {
                        buySFX.Play();
                }
                
                GameController.Instance.balance -= 2;
                if(GameController.Instance.health + healthGain > GameController.Instance.maxHealth) {
                        GameController.Instance.health = GameController.Instance.maxHealth;
                } else {
                        GameController.Instance.health += healthGain;
                }
                } else {
                        if(rejectSFX != null) {
                        rejectSFX.Play();
                        }
                }
                

        } else {
                if(rejectSFX != null) {
                        rejectSFX.Play();
                }
        }
        }
        public bool PlayerHasEnoughCurrency(float cost)
        {
                if(GameController.Instance.balance >= cost) {
                return true; 
                }
                return false;
        }


        public void SetLevel (float sliderValue){
                //set volume:
                // if(mixer != null && sliderValue != null) {
                mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                volumeLevel = sliderValue;
                // }

                //set slider display:
                GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        sliderVolumeCtrl.value = volumeLevel;
                        Debug.Log("volume level is: " + volumeLevel + ", slider value is" + sliderVolumeCtrl.value);
                }
        }

        public void ToggleAutorun(bool isOn)
        {
                
                if (GameController.Instance != null)
                {
                        GameController.Instance.autoRun = isOn;
                }
        }
        

}
