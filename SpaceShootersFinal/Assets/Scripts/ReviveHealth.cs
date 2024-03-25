using UnityEngine;

[CreateAssetMenu(fileName = "ReviveHealth", menuName = "PowerUp/Health/ReviveHealth")]
public class ReviveHealth : PowerUp
{
    public ReviveHealth()
    {
        powerUpName = "ReviveHealth";
        duration = 0f; // Instant effect
        cost = 10f;
        description = "Revives the health of the game controller to 100";
    }

    public override void Activate()
    {
        GameController.Instance.health = 100; // Assuming GameController has a variable to hold health
    }

    public override void Deactivate()
    {
        // No need for deactivation as it's an instant effect
    }
}
