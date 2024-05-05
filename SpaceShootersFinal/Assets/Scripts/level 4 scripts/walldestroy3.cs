using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class walldestroy3 : MonoBehaviour
{
    public TextMeshProUGUI finalText;
    public GameObject enemyBlocks; // Reference to the enemy blocks GameObject
    public float moveDuration = 3f; // Duration for each block to reach its final position
    public Vector3 vPositionOffset = new Vector3(0, -800, 0); // Start position offset to make blocks appear from above
    public Vector3 hPositionOffset = new Vector3(0, 0, -700);

    void Start()
    {
        // Set the enemyBlocks GameObject and its children to be inactive initially
        SetGameObjectAndChildrenActive(enemyBlocks, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Enemy collision");
            if (enemyBlocks != null)
            {
                StartCoroutine(ActivateEnemyBlocks());
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
        foreach (Transform child in children)
        {
                if(child.gameObject.tag == "vblock)"){
                         StartCoroutine(MoveBlock(child, child.position - vPositionOffset, moveDuration));
                } else if (child.gameObject.tag == "hblock") {
                         StartCoroutine(MoveBlock(child, child.position - hPositionOffset, moveDuration));
                }
            // Start moving all blocks simultaneously without delay
           
        }

        yield return new WaitForSeconds(moveDuration); // Wait until all blocks have finished moving
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