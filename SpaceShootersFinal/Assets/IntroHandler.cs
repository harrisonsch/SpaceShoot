using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHandler : MonoBehaviour
{
        public GameObject skipButton;
   
    void Start()
    {
        if(GameController.Instance != null) {
                skipButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
