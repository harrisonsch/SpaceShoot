using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class thirdroomwall : MonoBehaviour
{
    public TextMeshProUGUI finalText;
    public GameObject enemyBlocks; // Reference to the enemy blocks GameObject
    private float moveDuration = 2f; // Duration for each block to reach its final position
    public Vector3 vPositionOffset = new Vector3(0, -800, 0); // Start position offset to make blocks appear from above
    public Vector3 hPositionOffset = new Vector3(0, 0, -700);
    private float groupDelay = 2.5f;
    public string roomTag = "thirdroom";
    private AudioSource spawn;

    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponent<AudioSource>();
        // Set the enemyBlocks GameObject and its children to be inactive initially
        if (enemyBlocks != null)
        {
            SetGameObjectAndChildrenActive(enemyBlocks, false);
        }
    }

    // OnCollisionStay is called once per frame for every collider/rigidbody that is touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(roomTag))
        {
            Debug.Log("Enemy collision");
            if (enemyBlocks != null) // Check if the reference is not null
            {
                
                StartCoroutine(ActivateEnemyBlocks()); // Start coroutine to activate enemy blocks gradually
            }
        }
    }

    private IEnumerator ActivateEnemyBlocks()
    {
        Debug.Log("Activating enemy blocks...");
        enemyBlocks.SetActive(true);

        List<Transform> children = new List<Transform>();
        foreach (Transform child in enemyBlocks.transform)
        {
        children.Add(child);
        // Set the initial position for each block
        if (child.gameObject.tag == "vblock") { // Fixed missing quotation mark and extra parenthesis
                child.position += vPositionOffset;
        } else if (child.gameObject.tag == "hblock") { // Fixed missing quotation mark and extra parenthesis
                child.position += hPositionOffset;
        }
        child.gameObject.SetActive(true);
        }
        for (int i = 0; i < children.Count; i += 3)
        {
                spawn.Play();
            for (int j = i; j < Mathf.Min(i + 3, children.Count); j++)
            {
                Transform child = children[j];
                if (child.gameObject.tag == "vblock") {
                    StartCoroutine(MoveBlock(child, child.position - vPositionOffset, moveDuration));
                } else if (child.gameObject.tag == "hblock") {
                    StartCoroutine(MoveBlock(child, child.position - hPositionOffset, moveDuration));
                }
            }
            // Wait for the group to move and then delay before the next group
            yield return new WaitForSeconds(groupDelay);
        }
        yield return new WaitForSeconds(moveDuration); 
        Debug.Log("All enemy blocks are in position.");
    }

    private IEnumerator MoveBlock(Transform block, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = block.position;
        while (time < duration)
        {
            block.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        block.position = targetPosition; // Ensure it ends exactly at the target position
    }

    private void SetGameObjectAndChildrenActive(GameObject obj, bool isActive)
    {
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(isActive);
        }
        obj.SetActive(isActive);
    }
}