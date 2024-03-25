using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Overpower", menuName = "PowerUp/Overpower")]
public class Overpower : PowerUp
{
    private Coroutine overpowerCoroutine;

    public Overpower()
    {
        powerUpName = "Overpower";
        duration = 10f; // Lasts for 10 seconds
        cost = 15f;
        description = "Player speed and damage x4 for 10 seconds, then health is halved";
    }

    public override void Activate()
    {
        if (overpowerCoroutine == null)
        {
            overpowerCoroutine = GameController.Instance.StartCoroutine(OverpowerCoroutine());
        }
    }

    public override void Deactivate()
    {
        if (overpowerCoroutine != null)
        {
            GameController.Instance.StopCoroutine(overpowerCoroutine);
            overpowerCoroutine = null;
        }
    }

    private IEnumerator OverpowerCoroutine()
    {
        // Increase speed and damage
        GameController.Instance.currSpeedMult *= 4;
        GameController.Instance.currDamageMult*= 4;

        yield return new WaitForSeconds(duration);

        // Decrease health
        GameController.Instance.health /= 2;

        // Reset speed and damage
        GameController.Instance.currSpeedMult /= 4;
        GameController.Instance.currDamageMult /= 4;
    }
}
