using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sixteen : MonoBehaviour
{
    public TextMeshProUGUI finalText;
    public GameObject enemyBlocks; // Reference to the enemy blocks GameObject
    public float delayBetweenEnemyBlocks = 5f; // Delay between each enemy block activation

    // Start is called before the first frame update
    void Start()
    {
        // Set the enemyBlocks GameObject and its children to be inactive initially
        if (enemyBlocks != null)
        {
            SetGameObjectAndChildrenActive(enemyBlocks, false);
        }
    }

    // OnCollisionStay is called once per frame for every collider/rigidbody that is touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("16room"))
        {
            Debug.Log("Enemy collision");
            if (enemyBlocks != null) // Check if the reference is not null
            {
                StartCoroutine(ActivateEnemyBlocks()); // Start coroutine to activate enemy blocks gradually
            }
        }
    }

    // Coroutine to activate enemy blocks gradually
    private IEnumerator ActivateEnemyBlocks()
    {
        Debug.Log("Activating enemy blocks...");
        if (enemyBlocks != null)
        {
            enemyBlocks.SetActive(true); // Activate the enemyBlocks GameObject

            // Get all children of enemyBlocks
            List<Transform> children = new List<Transform>();
            foreach (Transform child in enemyBlocks.transform)
            {
                children.Add(child);
            }

            // Activate children in pairs
            for (int i = 0; i < children.Count; i += 2)
            {
                if (i < children.Count)
                {
                    Debug.Log("Activating children: " + children[i].name + " and " + children[i + 1].name);
                    children[i].gameObject.SetActive(true); // Activate the current enemy block
                    children[i + 1].gameObject.SetActive(true); // Activate the next enemy block
                }

                yield return new WaitForSeconds(delayBetweenEnemyBlocks); // Wait for specified delay
            }
        }
        Debug.Log("Enemy blocks activation complete.");
    }

    // Function to set a GameObject and its children active/inactive
    private void SetGameObjectAndChildrenActive(GameObject obj, bool isActive)
    {
        // Set the GameObject itself active/inactive
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(isActive); // Set each child GameObject active/inactive
        }
        obj.SetActive(isActive);
    }
}