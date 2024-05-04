using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class level3_fail : MonoBehaviour
{
    public string sceneToLoad = "LoseScene";
    // Start is called before the first frame update
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("finalWall"))
        {
                SceneManager.LoadScene(sceneToLoad);
                  // Destroy the GameObject this script is attached to
        }
    }
}
