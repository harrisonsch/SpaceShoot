using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButtonScript : MonoBehaviour
{
    void ButtonClicked() {
        SceneManager.LoadScene("MainMenu");
    }
}
