using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BGSoundScript : MonoBehaviour {

        private static BGSoundScript instance = null;

        public static BGSoundScript Instance{
                get {return instance;}
        }

        void Awake(){
                if (instance != null && instance != this){
                        Destroy(this.gameObject);
                        return;
                } else {
                        instance = this;
                }
                DontDestroyOnLoad(this.gameObject);
        }

        void Update() {
                Scene currentScene = SceneManager.GetActiveScene();
                string sceneName = currentScene.name;
                if(sceneName == "MainMenu") {
                        Debug.Log("in main");
                        Destroy(gameObject);
                }
        }
}
