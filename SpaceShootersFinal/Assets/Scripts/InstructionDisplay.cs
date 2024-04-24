using UnityEngine;
using TMPro;

public class InstructionDisplay : MonoBehaviour
{
    public TextMeshPro instructionText;
    public float displayDuration = 3f; // Default display duration is 3 seconds

    void Start()
    {
        // Show the instruction text when the game starts
        instructionText.gameObject.SetActive(true);

        // Hide the instruction text after specified duration
        Invoke("HideInstruction", displayDuration);
    }

    void HideInstruction()
    {
        // Hide the instruction text
        instructionText.gameObject.SetActive(false);
    }
}
