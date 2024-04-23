using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButtonScript : MonoBehaviour
{
    public void ButtonClicked() {
        SceneManager.LoadScene("MainMenu");
    }
}
