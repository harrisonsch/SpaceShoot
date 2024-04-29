using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButtonScript : MonoBehaviour
{
         public bool intro = true;
        public bool levelone = false;
    public void ButtonClicked() {
        if(intro) {

        SceneManager.LoadScene("MainMenu");
        } else if (levelone) {
                SceneManager.LoadScene("NewScene1");
        }
    }
}
