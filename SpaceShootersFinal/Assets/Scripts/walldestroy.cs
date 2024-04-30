using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class walldestroy : MonoBehaviour
{
        public TextMeshProUGUI finalText;
    // Start is called before the first frame update
     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                Debug.Log("walltag is " + gameObject.tag);
                if(gameObject.tag =="finalWall") {
                        Debug.Log("enabling");
                finalText.enabled = true;
                finalText.gameObject.SetActive(true);
            }
            Destroy(gameObject); // Destroy the GameObject this script is attached to
        }
    }
    
}
