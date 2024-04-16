using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moveScript : MonoBehaviour
{

            public TextMeshProUGUI movedText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.Instance.hasMoved){
                        Debug.Log("enabled");
                        movedText.enabled = true;
                } else{
                        movedText.enabled = false;
                }
    }
}
