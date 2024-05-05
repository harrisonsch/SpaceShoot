using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneScript : MonoBehaviour
{
        public bool intro = true;
        public bool levelone = false;
        public bool levelzero = false;
        public string level = "Lvl2";
    void OnEnable()
    {
        if(intro) {

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        } else if (levelone) {
                SceneManager.LoadScene("NewScene1", LoadSceneMode.Single);
        } else if (levelzero) {
                SceneManager.LoadScene("0Scene", LoadSceneMode.Single);
        } else {
                SceneManager.LoadScene(level, LoadSceneMode.Single);
        }
    }
}
