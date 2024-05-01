using UnityEngine;

public class RoomBeamManager : MonoBehaviour
{
    public GameObject beamPrefab; // Prefab for the beams
    public Transform enemyPlane; // Reference to the enemy plane
    public float activationDistance = 10f; // Distance at which beams should appear

    private bool beamsActivated = false;

    void Update()
    {
        // Calculate the distance between the enemy plane and the rooms
        float distance = Vector3.Distance(transform.position, enemyPlane.position);

        // Check if the distance is within the activation range and beams are not yet activated
        if (distance <= activationDistance && !beamsActivated)
        {
            ActivateBeams();
        }
        else if (distance > activationDistance && beamsActivated)
        {
            DeactivateBeams();
        }
    }

    void ActivateBeams()
    {
        // Instantiate beams at the positions of the rooms
        foreach (Transform room in transform)
        {
            Instantiate(beamPrefab, room.position, Quaternion.identity, room);
        }
        beamsActivated = true;
    }

    void DeactivateBeams()
    {
        // Destroy all beam GameObjects in the rooms
        foreach (Transform room in transform)
        {
            foreach (Transform child in room)
            {
                Destroy(child.gameObject);
            }
        }
        beamsActivated = false;
    }
}
