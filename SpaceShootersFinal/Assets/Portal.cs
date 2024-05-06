using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
        public string levelToLoad = "Level1Transition";
     private void OnCollisionEnter(Collision collision)
        {
                Debug.Log("Hit the portal!");
        if (collision.gameObject.CompareTag("Player"))
        {
                SceneManager.LoadScene(levelToLoad);
        }
    }
    private void OnCollisionStay(Collision collision)
        {
                Debug.Log("Hit the portal!");
        if (collision.gameObject.CompareTag("Player"))
        {
                SceneManager.LoadScene(levelToLoad);
        }
    }
}
